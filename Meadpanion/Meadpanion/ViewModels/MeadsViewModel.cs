using Meadpanion.Models;
using Meadpanion.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Meadpanion.Services;

namespace Meadpanion.ViewModels
{
    public class MeadsViewModel : BaseViewModel
    {
        public IDataStore<Mead> MeadDataStore => DependencyService.Get<IDataStore<Mead>>();

        private Mead _selectedItem;

        public ObservableCollection<Mead> Meads { get; }
        public Command LoadMeadsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Mead> ItemTapped { get; }

        public MeadsViewModel()
        {
            Title = "Browse Meads";
            Meads = new ObservableCollection<Mead>();
            LoadMeadsCommand = new Command(async () => await ExecuteLoadMeadsCommand());

            ItemTapped = new Command<Mead>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
        }

        async Task ExecuteLoadMeadsCommand()
        {

            IsBusy = true;

            try
            {
                Meads.Clear();
                var items = await MeadDataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Meads.Add(item);
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

        public Mead SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewMeadPage));
        }

        async void OnItemSelected(Mead item)
        {
            if (item == null)
                return;

            // This will push the MeadDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(MeadsDetailPage)}?{nameof(MeadsDetailViewModel.MeadID)}={item.ID}");
        }
    }
}