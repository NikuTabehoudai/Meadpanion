using Meadpanion.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Meadpanion.Services;

namespace Meadpanion.ViewModels
{
    public class MeadsDetailViewModel : BaseViewModel
    {
        public IDataStore<Mead> DataStore => DependencyService.Get<IDataStore<Mead>>();

        private int meadID;
        private string name;
        public int Id { get; set; }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public int MeadID
        {
            get
            {
                return MeadID;
            }
            set
            {
                MeadID = value;
                LoadItemId(value);
            }
        }

        public async void LoadItemId(int itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                meadID = item.ID;
                name = item.Name;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
