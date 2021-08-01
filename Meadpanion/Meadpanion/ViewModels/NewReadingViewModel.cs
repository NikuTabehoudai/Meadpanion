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
    [QueryProperty(nameof(MeadID), nameof(MeadID))]
    public class NewReadingViewModel : BaseViewModel
    {
        public IDataStore<Reading> ReadingDataStore => DependencyService.Get<IDataStore<Reading>>();

        private int meadID;
        private DateTime date = DateTime.Today;
        private float gravity;
        private string note;


        public NewReadingViewModel()
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
                Date = date,
                Note = note,
                GravityReading = gravity,
                MeadId = meadID
            };

            await ReadingDataStore.AddItemAsync(newReading);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
