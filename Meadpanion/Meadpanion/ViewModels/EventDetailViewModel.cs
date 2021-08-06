using Meadpanion.Models;
using System;
using Xamarin.Forms;
using Meadpanion.Services;
using Meadpanion.Views;
using System.Diagnostics;

namespace Meadpanion.ViewModels
{
    [QueryProperty(nameof(EventID), nameof(EventID))]
    public class EventDetailViewModel : BaseViewModel
    {
        public IDataStore<MeadEvents> MeadEventsDataStore => DependencyService.Get<IDataStore<MeadEvents>>();
        public IDataStore<Mead> MeadDataStore => DependencyService.Get<IDataStore<Mead>>();

        private int eventID;
        private int meadID;
        private int bottles;
        private string date;
        private string oldGravity;
        private string newGravity;
        private string note;
        private string eventType;
        private bool stepFed;

        public Command EditEventCommand { get;}
        public Command DeleteEventCommand { get;}


        public EventDetailViewModel()
        {
            EditEventCommand = new Command(OnEditEvent);
            DeleteEventCommand = new Command(OnDeleteEvent);
        }

        #region Bindings

        public int EventID
        {
            get
            {
                return eventID;
            }
            set
            {
                eventID = value;
                LoadEvent(value);
            }
        }

        public string OldGravity
        {
            get => oldGravity;
            set => SetProperty(ref oldGravity, value);
        }

        public string NewGravity
        {
            get => newGravity;
            set => SetProperty(ref newGravity, value);
        }


        public string Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }

        public string Note
        {
            get => note;
            set => SetProperty(ref note, value);
        }

        public int Bottles
        {
            get => bottles;
            set => SetProperty(ref bottles, value);
        }

        public string EventType
        {
            get => eventType;
            set => SetProperty(ref eventType, value);
        }

        private bool showStepFeedingGrid;
        public bool ShowStepFeedingGrid
        {
            get => showStepFeedingGrid;
            set => SetProperty(ref showStepFeedingGrid, value); 
        }

        private bool showBottelingGrid;
        public bool ShowBottelingGrid
        {
            get => showBottelingGrid;
            set => SetProperty(ref showBottelingGrid, value);
        }

        #endregion

        private async void LoadEvent(int eventID)
        {
            try
            {
                var item = await MeadEventsDataStore.GetItemAsync(eventID);

                meadID = item.MeadId;
                Date = item.Date.ToString("dd - MM - yyyy");
                Note = item.Note;
                EventType = item.EventType;
                showBottelingGrid = item.Botteling;
                ShowStepFeedingGrid = item.StepFeeding;
                Bottles = item.Bottles;
                OldGravity = item.OldGravity.ToString("0.000");
                NewGravity = item.NewGravity.ToString("0.000");
            }
            catch(Exception)
            {
                Debug.WriteLine("Failed to load Event");
            }
        }


        async void OnEditEvent(object obj)
        {
            // This will push the MeadDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(EditEventPage)}?{nameof(EditEventViewModel.EventID)}={eventID}");
        }

        async void OnDeleteEvent()
        {

            bool answer = await App.Current.MainPage.DisplayAlert("Deleting Event", "Are you sure you want to delete the " + eventType + " event", "Yes", "No");
            if (answer)
            { 
            await MeadEventsDataStore.DeleteItemAsync(EventID);

                await Shell.Current.GoToAsync("..");
            }

        }
    }
}
