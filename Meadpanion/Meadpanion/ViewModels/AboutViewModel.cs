using System;
using System.IO;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Meadpanion.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "";


            MeadsCommand = new Command(OnMead);
            RecipeCommand = new Command(OnRecipe);
            ReadingsCommand = new Command(OnReadings);
            EventsCommand = new Command(OnEvents);
            CopyDBCommand = new Command(OnCopyDB);
            DeleteDBCommand = new Command(OnDeleteDB);
        }

        //for testing purposes only.
        public Command MeadsCommand { get; }
        public Command RecipeCommand { get; }
        public Command ReadingsCommand { get; }
        public Command EventsCommand { get; }
        public Command CopyDBCommand { get; }
        public Command DeleteDBCommand { get; }

        public void OnCopyDB()
        {
            var file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Mead.db3");
            var despath = "storage/emulated/0/Android/data/com.companyname.meadpanion/cache";

            try { 
            File.Copy(file, despath);
            }
            catch
            {
                File.Delete(despath + "/Mead.db3");
                OnCopyDB();
            }
        }

        public void OnDeleteDB()
        {
            var file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Mead.db3");

            try
            {
                File.Delete(file);

            }
            catch
            {
                //just to prevent crashing.
            }
        }


        public async void OnMead()
        {
            await App.Database.ClearMeadData();
        }

        public async void OnRecipe()
        {
            await App.Database.ClearRecipeData();
        }

        public async void OnReadings()
        {
            await App.Database.ClearReadingData();
        }

        public async void OnEvents()
        {
            await App.Database.ClearEventData();
        }

    }
}