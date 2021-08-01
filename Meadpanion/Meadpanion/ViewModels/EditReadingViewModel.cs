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
    [QueryProperty(nameof(ReadingID), nameof(ReadingID))]
    public class EditReadingViewModel : BaseViewModel
    {
        public IDataStore<Reading> ReadingDataStore => DependencyService.Get<IDataStore<Reading>>();

        private int readingID;
        private int meadID;
        private DateTime date = DateTime.Today;
        private float gravity;
        private string note;
        private string abv;
        private bool originalGravity;
        private Reading reading;


        public EditReadingViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return gravity >= 0.980f && gravity <= 1.160f;
        }

        #region Bindings

        public int ReadingID
        {
            get
            {
                return readingID;
            }
            set
            {
                readingID = value;
                LoadReadingID(value);
            }
        }

        public float Gravity
        {
            get => gravity;
            set => SetProperty(ref gravity, value);
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

        #endregion


        private async void LoadReadingID(int id)
        {
            reading = await ReadingDataStore.GetItemAsync(id);

            Date = reading.Date;
            Note = reading.Note;
            Gravity = reading.GravityReading;
            meadID = reading.MeadId;
            abv = reading.ABV;
            originalGravity = reading.OriginalGravity;
            
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
            Reading newReading = new Reading()
            {
                ID = readingID,
                Date = date,
                Note = note,
                GravityReading = gravity,
                MeadId = meadID,
                ABV = abv,
                OriginalGravity = originalGravity
            };

            await ReadingDataStore.UpdateItemAsync(newReading);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("../..");
        }
    }
}
