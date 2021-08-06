using Meadpanion.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Meadpanion.Services;
using System.Linq;
using System.Collections.Generic;

namespace Meadpanion.ViewModels
{
    [QueryProperty(nameof(MeadID), nameof(MeadID))]
    public class EditMeadViewModel : BaseViewModel
    {
        public IDataStore<Mead> MeadDataStore => DependencyService.Get<IDataStore<Mead>>();
        public IDataStore<Recipe> RecipeDataStore => DependencyService.Get<IDataStore<Recipe>>();
        public IDataStore<Reading> ReadingDataStore => DependencyService.Get<IDataStore<Reading>>();

        public Mead Mead { get; set; }
        public List<Reading> Readings { get; set; }

        private int meadID;
        private string name;
        private string recipe;
        private DateTime date;
        private float startingGravity;
        private float amount;
        private string note;
        private List<Recipe> recipeList = new List<Recipe>();
        private Recipe selectedRecipe;
        private List<string> statusList;
        private string selectedStatus;
        private string customStatus;
        private DateTime lastStatusChanged;
        
        public EditMeadViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
            LoadRecipe();

            statusList = new List<string>()
            {
                "Primary",
                "Secundary",
                "Aging",
                "Bottled",
                "Finished",
                "Discarded",
                "Custom"
            };

        }


        async void LoadRecipe()
        {
            try
            {
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

        async void LoadMeadByID()
        {
            try
            {
                Mead = await MeadDataStore.GetItemAsync(meadID);
                Readings = (List<Reading>)await ReadingDataStore.GetItemsAsync(MeadID);

                SetProperies();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private void SetProperies()
        {
            Name = Mead.Name;
            Date = Mead.Date;
            StartingGravity = Readings[0].GravityReading;
            Amount = Mead.Amount;
            Note = Mead.Note;
            SelectedRecipe = recipeList.FirstOrDefault(s => s.ID == Mead.RecipeID);
            SelectedStatus = Mead.Status;
            lastStatusChanged = Mead.LastStatusChange;
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

        public List<String> StatusList
        {
            get => statusList;
            set => SetProperty(ref statusList, value);
        }

        public string SelectedStatus
        {
            get => selectedStatus;
            set {
                SetProperty(ref selectedStatus, value);
                ShowCustomEntryLine();
            }
        }

        public string CustomStatus
        {
            get => customStatus;
            set => SetProperty(ref customStatus, value);
        }

        private bool showCustomEntry;
        public bool ShowCustomEntry
        {
            get => showCustomEntry;
            set => SetProperty(ref showCustomEntry, value);
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
                LoadMeadByID();
            }
        }

        #endregion

        private void ShowCustomEntryLine()
        {
            if (SelectedStatus == "Custom")
            {
                ShowCustomEntry = true;
            }
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
            var status = selectedStatus;

            if (status == "Custom")
            {
                status = customStatus;
            }

            if (selectedStatus != Mead.Status)
            {
                lastStatusChanged = DateTime.Today;
            }

            Mead newMead = new Mead()
            {
                ID = meadID,
                Name = name,
                RecipeID = selectedRecipe.ID,
                Date = date,
                Amount = amount,
                Note = note,
                Status = status,
                LastStatusChange = lastStatusChanged
            };
            await MeadDataStore.UpdateItemAsync(newMead);

            Readings[0].GravityReading = startingGravity;
            Readings[0].Date = date;
            //Readings[0].ABV = "0%";

            await ReadingDataStore.UpdateItemAsync(Readings[0]);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("../..");
        }
    }
}
