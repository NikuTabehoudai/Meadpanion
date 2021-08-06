using Meadpanion.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Meadpanion.Services;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Linq;

namespace Meadpanion.ViewModels
{
    [QueryProperty(nameof(MeadID), nameof(MeadID))]
    public class NewEventViewModel : BaseViewModel
    {
        public IDataStore<MeadEvents> MeadEventsDataStore => DependencyService.Get<IDataStore<MeadEvents>>();
        public IDataStore<Mead> MeadDataStore => DependencyService.Get<IDataStore<Mead>>();
        public IDataStore<Reading> ReadingDataStore => DependencyService.Get<IDataStore<Reading>>();

        private int meadID;
        private int bottles;
        private DateTime date = DateTime.Today;
        private float oldGravity;
        private float newGravity;
        private string note;
        private string selectedEventType = "";
        private List<string> eventTypeList;

        public NewEventViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();

            eventTypeList = new List<string>() 
                { 
                    "Primary Fermentation", 
                    "Secendary Fermentation", 
                    "Racked", 
                    "Bottled", 
                    "BackSweetened", 
                    "Discarded", 
                    "Tasting", 
                    "Step Feeding"
                };

        }


        private bool ValidateSave()
        {
            if (selectedEventType == "Step Feeding")
            {
                return oldGravity >= 0.980f && oldGravity <= 1.160f && newGravity >= 0.980f && newGravity <= 1.160f; 
            }
            return true;
        }

        #region Bindings

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
                case "bottled":
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
                Date = date,
                EventType = selectedEventType,
                Note = note
            };

            switch (selectedEventType.ToLower())
            {
                case "bottled":
                    newEvent.Botteling = true;
                    newEvent.Bottles = bottles;
                    break;
                case "step feeding":
                    var mead = await MeadDataStore.GetItemAsync(MeadID);
                    mead.StepFed = true;
                    newEvent.StepFeeding = true;
                    newEvent.OldGravity = oldGravity;
                    newEvent.NewGravity = newGravity;
                    await MeadDataStore.UpdateItemAsync(mead);
                    await ReadingDataStore.AddItemAsync(new Reading() { MeadId = meadID, Date = date, GravityReading = oldGravity, Note = "Reading before Step Feeding" });
                    await ReadingDataStore.AddItemAsync(new Reading() { MeadId = meadID, Date = date.AddSeconds(20), GravityReading = newGravity, Note = "Reading after Step Feeding", StepFeeding = true });
                    break;
            }

            await MeadEventsDataStore.AddItemAsync(newEvent);

            var eventList = await MeadEventsDataStore.GetItemsAsync(MeadID);

            if (eventList.Last().Date <= date)
            {
                ChangeMeadStatus(selectedEventType);
            }

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void ChangeMeadStatus(string status)
        {
            var mead = await MeadDataStore.GetItemAsync(MeadID);

            switch (status.ToLower())
            {
                case "primary fermantation":
                    mead.Status = status;
                    break;
                case "secondary fermantation":
                    mead.Status = status;
                    break;
                case "bottled":
                    mead.Status = status;
                    break;
                case "finished":
                    mead.Status = status;
                    break;
                case "discarded":
                    mead.Status = status;
                    break;
                case "racked":
                    mead.Status = status;
                    break;
            }
            mead.LastStatusChange = date;
            await MeadDataStore.UpdateItemAsync(mead);
        }

    }
}
