using Meadpanion.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Meadpanion.Services;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Meadpanion.ViewModels
{
    [QueryProperty(nameof(EventID), nameof(EventID))]
    public class EditEventViewModel : BaseViewModel
    {
        public IDataStore<MeadEvents> MeadEventsDataStore => DependencyService.Get<IDataStore<MeadEvents>>();

        private int eventID;
        private int meadID;
        private int bottles;
        private DateTime date = DateTime.Today;
        private float oldGravity;
        private float newGravity;
        private string note;
        private string selectedEventType = "";
        private List<string> eventTypeList;

        public EditEventViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();

            eventTypeList = new List<string>() 
                { 
                    "Primary Fermentation", 
                    "Secendary Fermentation", 
                    "Racking", 
                    "Botteling", 
                    "Backsweeting", 
                    "Discarded", 
                    "Tasting", 
                    "Step Feeding"
                };

        }


        private bool ValidateSave()
        {
            //return gravity >= 0.980f && gravity <= 1.160f;
            return true;
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

        public float OldGravity
        {
            get => oldGravity;
            set => SetProperty(ref oldGravity, value);
        }

        public float NewGravity
        {
            get => newGravity;
            set => SetProperty(ref newGravity, value);
        }


        public DateTime Date
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

        public List<string> EventTypeList
        { 
            get => eventTypeList;
            set => SetProperty(ref eventTypeList, value);    
        }

        public string SelectedEventType
        {
            get => selectedEventType;
            set {
                SetProperty(ref selectedEventType, value);
                ShowSelectedEvent(value);
                }
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

        private void ShowSelectedEvent(string eventType)
        {
            HideSelectedEvents();
            switch (eventType.ToLower())
            {
                case "botteling":
                    ShowBottelingGrid = true;
                    break;
                case "step feeding":
                    ShowStepFeedingGrid = true;
                    break;
            }
        }

        private void HideSelectedEvents()
        {
            ShowBottelingGrid = false;
            ShowStepFeedingGrid = false;
        }

        private async void LoadEvent(int eventID)
        {
            var item = await MeadEventsDataStore.GetItemAsync(eventID);

            meadID = item.MeadId;
            date = item.Date;
            selectedEventType = item.EventType;
            note = item.Note;
            showBottelingGrid = item.Botteling;
            showBottelingGrid = item.StepFeeding;
            bottles = item.Bottles;
            oldGravity = item.OldGravity;
            newGravity = item.NewGravity;
        }


        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            MeadEvents newEvent = new MeadEvents()
            {
                MeadId = meadID,
                ID = eventID,
                Date = date,
                EventType = selectedEventType,
                Note = note
            };

            switch (selectedEventType.ToLower())
            {
                case "botteling":
                    newEvent.Botteling = true;
                    newEvent.Bottles = bottles;
                    break;
                case "step feeding":
                    newEvent.StepFeeding = true;
                    newEvent.OldGravity = oldGravity;
                    newEvent.NewGravity = newGravity;
                    break;
            }

            await MeadEventsDataStore.UpdateItemAsync(newEvent);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
