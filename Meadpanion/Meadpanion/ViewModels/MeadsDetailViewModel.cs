using Meadpanion.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Meadpanion.Services;
using Meadpanion.Util;
using Meadpanion.Views;
using System.Collections.Generic;

namespace Meadpanion.ViewModels
{
    [QueryProperty(nameof(MeadID), nameof(MeadID))]
    public class MeadsDetailViewModel : BaseViewModel
    {
        public IDataStore<Mead> MeadDataStore => DependencyService.Get<IDataStore<Mead>>();
        public IDataStore<Recipe> RecipeDataStore => DependencyService.Get<IDataStore<Recipe>>();
        public IDataStore<Reading> ReadingDataStore => DependencyService.Get<IDataStore<Reading>>();


        private int meadID;
        private string date;
        private string recipeName;
        private string startingGravity;
        private string potentialABV;
        private string currentGravity;
        private string currentABV;
        private string amount;
        private string status;
        private string note;
        public int Id { get; set; }

        public Command EditMeadCommand { get; }
        public Command ReadingsCommand { get; }
        public Command DeleteMeadCommand { get; set; }

        public MeadsDetailViewModel()
        {
            EditMeadCommand = new Command(OnEditMead);
            ReadingsCommand = new Command(OnReadings);
            DeleteMeadCommand = new Command(OnDeleteMead);
        }

        #region Bindings

        public string Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }

        public string RecipeName
        {
            get => recipeName;
            set => SetProperty(ref recipeName, value);
        }
        
        public string StartingGravity
        {
            get => startingGravity;
            set => SetProperty(ref startingGravity, value);
        }

        public string PotentialABV
        {
            get => potentialABV;
            set => SetProperty(ref potentialABV, value);
        }

        public string CurrentGravity
        {
            get => currentGravity;
            set => SetProperty(ref currentGravity, value);
        }

        public string CurrentABV
        {
            get => currentABV;
            set => SetProperty(ref currentABV, value);
        }

        public string Amount
        {
            get => amount;
            set => SetProperty(ref amount, value);
        }

        public string Status
        {
            get => status;
            set => SetProperty(ref status, value);
        }

        public string Note
        {
            get => note;
            set => SetProperty(ref note, value);
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
                LoadMeadId(value);
            }
        }

        #endregion

        public async void LoadMeadId(int itemId)
        {
            try
            {
                var mead = await MeadDataStore.GetItemAsync(itemId);
                var recipe = await RecipeDataStore.GetItemAsync(mead.RecipeID);
                var readings = await ReadingDataStore.GetItemsAsync(itemId);
                List<Reading> readingsList = new List<Reading>();
                foreach (var item in readings)
                    readingsList.Add(item);

                meadID = mead.ID;
                Title = mead.Name;
                Date = mead.Date.ToString("dd.MM.yyyy");
                RecipeName = recipe.Name;
                StartingGravity = readingsList[0].GravityReading.ToString("0.000");
                PotentialABV = Util.ABVCalculator.CalculateABV(readingsList[0].GravityReading, 1.000f);
                CurrentGravity = readingsList[readingsList.Count -1 ].GravityReading.ToString("0.000");
                CurrentABV = Util.ABVCalculator.CalculateABV(readingsList[0].GravityReading, readingsList[readingsList.Count - 1].GravityReading);
                Amount = mead.Amount +"L";
                if (mead.Active) Status = "Active";
                else Status = "Inactive";
                Note = mead.Note;

            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        async void OnEditMead(object obj)
        {
            // This will push the MeadDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(EditMeadPage)}?{nameof(EditMeadViewModel.MeadID)}={meadID}");
        }

        async void OnReadings(object obj)
        {
            // This will push the MeadDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ReadingsPage)}?{nameof(ReadingsViewModel.MeadID)}={meadID}");
        }

        async void OnDeleteMead()
        {
            bool answer = await App.Current.MainPage.DisplayAlert("Deleting Mead", "Are you sure you want to delete the mead: " + Title, "Yes", "No");
            if (answer)
            {
                var readings = await ReadingDataStore.GetItemsAsync(meadID);
                foreach (var item in readings)
                {
                    await ReadingDataStore.DeleteItemAsync(item.ID);
                } 
                //TODO: Add Delete events.
                await MeadDataStore.DeleteItemAsync(meadID);
                await Shell.Current.GoToAsync("..");
            }
        }
    }
}
