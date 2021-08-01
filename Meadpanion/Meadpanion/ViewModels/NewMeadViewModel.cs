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
    public class NewMeadViewModel : BaseViewModel
    {
        public IDataStore<Mead> MeadDataStore => DependencyService.Get<IDataStore<Mead>>();
        public IDataStore<Recipe> RecipeDataStore => DependencyService.Get<IDataStore<Recipe>>();
        public IDataStore<Reading> ReadingDataStore => DependencyService.Get<IDataStore<Reading>>();

        private string name;
        private string recipe;
        private DateTime date = DateTime.Today;
        private float startingGravity;
        private float amount;
        private string note;
        private List<Recipe> recipeList = new List<Recipe>();
        private Recipe selectedRecipe;


        public NewMeadViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
            LoadRecipe();
        }

        async void LoadRecipe()
        {
            try
            {
                //Item ID is not used for Recipe's
                var items = await RecipeDataStore.GetItemsAsync(0);
                foreach (var item in items)
                {
                    recipeList.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        #region Bindings

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(name) && startingGravity >= 0.980f && startingGravity <= 1.160f;
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string Recipe
        {
            get => recipe;
            set => SetProperty(ref recipe, value);
        }

        public DateTime Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }

        public float StartingGravity
        {
            get => startingGravity;
            set => SetProperty(ref startingGravity, value);
        }

        public float Amount
        {
            get => amount;
            set => SetProperty(ref amount, value);
        }

        public string Note
        {
            get => note;
            set => SetProperty(ref note, value);
        }

        public List<Recipe> RecipeList
        {
            get => recipeList;
            set => SetProperty(ref recipeList, value);
        }

        public Recipe SelectedRecipe
        { 
            get => selectedRecipe; 
            set => SetProperty(ref selectedRecipe, value); 
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
            Mead newMead = new Mead()
            {
                Name = name,
                RecipeID = selectedRecipe.ID,
                Date = date,
                Amount = amount,
                Note = note,
                Active = true,
             };
            await MeadDataStore.AddItemAsync(newMead);

            await ReadingDataStore.AddItemAsync(new Reading() { MeadId = newMead.ID, Date = date, GravityReading = startingGravity, Note = "Original Gravity", ABV = "0", OriginalGravity = true }) ;

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
