using Meadpanion.Models;
using Meadpanion.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Meadpanion.Services;
using Meadpanion.Util;
using System.Linq;
using Meadpanion.Events;

namespace Meadpanion.ViewModels
{
    [QueryProperty(nameof(MeadID), nameof(MeadID))]
    [QueryProperty(nameof(StepFed), nameof(StepFed))]
    public class ReadingsViewModel : BaseViewModel
    {
        public IDataStore<Reading> ReadingDataStore => DependencyService.Get<IDataStore<Reading>>();

        private Reading _selectedItem;
        private int meadID;

        public bool StepFed { get; set; }

        public ObservableCollection<Reading> Readings { get; set; }
        public Command LoadReadingsCommand { get; }
        public Command AddReadingCommand { get; }
        public Command<Reading> ItemTapped { get; }

        public ReadingsViewModel()
        {
            Title = "Readings";
            Readings = new ObservableCollection<Reading>();
            LoadReadingsCommand = new Command(async () => await ExecuteLoadReadingsCommand(meadID));

            ItemTapped = new Command<Reading>(OnItemSelected);

            AddReadingCommand = new Command(OnAddReading);
        }

        public int MeadID
        {
            get
            {
                return meadID;
            }
            set
            {
                meadID = value;
            }
        }

        async Task ExecuteLoadReadingsCommand(int ID)
        {

            IsBusy = true;

            try
            {
                Readings.Clear();

                if (StepFed)
                {
                    var temp = new StepFeeding();
                    await temp.Initialize(meadID);
                    foreach (var item in temp.ReadingList)
                        Readings.Add(item);
                }
                else { 
                var items = await ReadingDataStore.GetItemsAsync(MeadID);
                items = items.Where(s => s.MeadId == ID);
                var og = items.First(s => s.OriginalGravity == true).GravityReading;
                foreach (var item in items)
                {
                    if (!item.OriginalGravity)
                    {
                        item.ABV = (Util.ABVCalculator.StringCalculateABV(og, item.GravityReading));
                    }
                    else
                        item.ABV = "0";

                    Readings.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }


        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Reading SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddReading(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(NewReadingPage)}?{nameof(NewReadingViewModel.MeadID)}={meadID}");
        }

        async void OnItemSelected(Reading item)
        {
            if (item == null)
                return;

           await Shell.Current.GoToAsync($"{nameof(ReadingDetailPage)}?{nameof(ReadingDetailViewModel.ID)}={item.ID}&{ nameof(ReadingDetailViewModel.ABV)}={ item.ABV}");
        }

    }
}