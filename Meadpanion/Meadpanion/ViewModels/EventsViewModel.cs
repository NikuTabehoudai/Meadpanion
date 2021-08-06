using Meadpanion.Models;
using Meadpanion.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Meadpanion.Services;
using System.Linq;

namespace Meadpanion.ViewModels
{
    [QueryProperty(nameof(MeadID), nameof(MeadID))]
    class EventsViewModel : BaseViewModel
    {

            public IDataStore<MeadEvents> MeadEventsDataStore => DependencyService.Get<IDataStore<MeadEvents>>();

            private MeadEvents _selectedItem;

            private int meadID;
            public ObservableCollection<MeadEvents> MeadEvents { get; set; }
            public Command LoadEventsCommand { get; }
            public Command AddEventCommand { get; }
            public Command<MeadEvents> ItemTapped { get; }

            public EventsViewModel()
            {
                Title = "Events";
                MeadEvents = new ObservableCollection<MeadEvents>();
            LoadEventsCommand = new Command(async () => await ExecuteLoadEventsCommand(meadID));

                ItemTapped = new Command<MeadEvents>(OnItemSelected);

            AddEventCommand = new Command(OnAddEvent);
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

            async Task ExecuteLoadEventsCommand(int ID)
            {

                IsBusy = true;

                try
                {
                    MeadEvents.Clear();
                    var list = await MeadEventsDataStore.GetItemsAsync(ID);
                    foreach (var item in list)
                    {
                        MeadEvents.Add(item);
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

            public MeadEvents SelectedItem
            {
                get => _selectedItem;
                set
                {
                    SetProperty(ref _selectedItem, value);
                    OnItemSelected(value);
                }
            }

            private async void OnAddEvent(object obj)
            {
                await Shell.Current.GoToAsync($"{nameof(NewEventPage)}?{nameof(NewEventViewModel.MeadID)}={meadID}");
            }

            async void OnItemSelected(MeadEvents item)
            {
                if (item == null)
                    return;

                await Shell.Current.GoToAsync($"{nameof(EventDetailPage)}?{nameof(EventDetailViewModel.EventID)}={item.ID}");
            }

        }
    }
