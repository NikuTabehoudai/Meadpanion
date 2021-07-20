using Meadpanion.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Meadpanion.Services;

namespace Meadpanion.ViewModels
{
    public class NewMeadViewModel : BaseViewModel
    {
        public IDataStore<Mead> DataStore => DependencyService.Get<IDataStore<Mead>>();

        private string name;
        private string recipe;
        private DateTime date = DateTime.Today;
        private float startingGravity = 1.010f;
        private float amount;
        private string note;
        private List<Recipe> recipesList;

        public NewMeadViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();

            //todo: create link to recipe list
            //temp recipe list for testing
            recipesList = new List<Recipe>();
            recipesList.Add(new Recipe() { Name = "Recipe 1", ID = 1 });
            recipesList.Add(new Recipe() { Name = "Recipe 2", ID = 2 });
            recipesList.Add(new Recipe() { Name = "Recipe 3", ID = 3 });
            recipesList.Add(new Recipe() { Name = "Recipe 4", ID = 4 });
            recipesList.Add(new Recipe() { Name = "Recipe 5", ID = 5 });

        }

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
            get => recipesList;
            set => SetProperty(ref recipesList, value);
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
            Mead newMead = new Mead()
            {
                Name = name
            };

            await DataStore.AddItemAsync(newMead);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
