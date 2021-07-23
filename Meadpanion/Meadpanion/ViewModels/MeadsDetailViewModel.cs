using Meadpanion.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Meadpanion.Services;
using Meadpanion.Util;
using Meadpanion.Views;

namespace Meadpanion.ViewModels
{
    [QueryProperty(nameof(MeadID), nameof(MeadID))]
    public class MeadsDetailViewModel : BaseViewModel
    {
        public IDataStore<Mead> MeadDataStore => DependencyService.Get<IDataStore<Mead>>();
        public IDataStore<Recipe> RecipeDataStore => DependencyService.Get<IDataStore<Recipe>>();

        private int meadID;
        private string date;
        private string recipeName;
        private float startingGravity;
        private string potentialABV;
        private float currentGravity;
        private string currentABV;
        private string amount;
        private string status;
        private string note;
        public int Id { get; set; }

        public Command EditMeadCommand { get; }

        public MeadsDetailViewModel()
        {
            EditMeadCommand = new Command(OnEditMead);

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
        
        public float StartingGravity
        {
            get => startingGravity;
            set => SetProperty(ref startingGravity, value);
        }

        public string PotentialABV
        {
            get => potentialABV;
            set => SetProperty(ref potentialABV, value);
        }

        public float CurrentGravity
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
                meadID = mead.ID;
                Title = mead.Name;
                Date = mead.Date.ToString("dd.MM.yyyy");
                RecipeName = recipe.Name;
                StartingGravity = mead.Readings[0].GravityReading;
                PotentialABV = Util.ABVCalculator.CalculateABV(startingGravity, 1.000f) + "%";
                CurrentGravity = mead.Readings[mead.Readings.Count -1].GravityReading;
                CurrentABV = Util.ABVCalculator.CalculateABV(startingGravity, currentGravity) + "%";
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
    }
}
