using Meadpanion.Models;
using System;
using System.Diagnostics;
using Xamarin.Forms;
using Meadpanion.Services;
using Meadpanion.Views;

namespace Meadpanion.ViewModels
{
    [QueryProperty(nameof(ID), nameof(ID))]
    [QueryProperty(nameof(ABV), nameof(ABV))]
    public class ReadingDetailViewModel : BaseViewModel
    {
        public IDataStore<Reading> ReadingDataStore => DependencyService.Get<IDataStore<Reading>>();


        private string date;
        private string gravity;
        private string abv;
        private string note;
        private bool originalGravity;
        public int Id { get; set; }

        public Command EditReadingCommand { get; }
        public Command DeleteReadingCommand { get; }

        public ReadingDetailViewModel()
        {
            EditReadingCommand = new Command(OnEditReading);
            DeleteReadingCommand = new Command(OnDeleteReading);
        }

        #region Bindings

        public string Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }
        
        public string Gravity
        {
            get => gravity;
            set => SetProperty(ref gravity, value);
        }

        public string ABV
        {
            get => abv;
            set => SetProperty(ref abv, value);
        }

        public string Note
        {
            get => note;
            set => SetProperty(ref note, value);
        }

        public int ID
        {
            get
            {
                return Id;
            }
            set
            {
                Id = value;
                LoadReadingId(value);
            }
        }
        #endregion

        public async void LoadReadingId(int itemId)
        {
            try
            {
                var reading = await ReadingDataStore.GetItemAsync(itemId);

                Date = reading.Date.ToString("dd.MM.yyyy");
                Gravity = reading.GravityReading.ToString("0.000");
                Note = reading.Note;
                originalGravity = reading.OriginalGravity;

            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        async void OnEditReading(object obj)
        {
            // This will push the MeadDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(EditReadingPage)}?{nameof(EditReadingViewModel.ReadingID)}={Id}");
        }

        async void OnDeleteReading()
        {
            if (originalGravity)
                await App.Current.MainPage.DisplayAlert("Original Gravity", "This Reading is the Original Gravity, it cannot be deleted.", "OK");
            else
            {
                bool answer = await App.Current.MainPage.DisplayAlert("Delete Reading?", "Are you sure you wish to delete this reading?", "Yes", "No");

                if (answer)
                {
                    await ReadingDataStore.DeleteItemAsync(Id);
                    await Shell.Current.GoToAsync("..");
                }

            }
        }
    }
}
