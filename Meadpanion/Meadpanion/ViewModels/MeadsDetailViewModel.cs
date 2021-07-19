using Meadpanion.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Meadpanion.Services;

namespace Meadpanion.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class MeadsDetailViewModel : BaseViewModel
    {
        public IDataStore<Mead> DataStore => DependencyService.Get<IDataStore<Mead>>();

        private int MeadId;
        private string text;
        private string description;
        public int Id { get; set; }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public int ItemId
        {
            get
            {
                return MeadId;
            }
            set
            {
                MeadId = value;
                LoadItemId(value);
            }
        }

        public async void LoadItemId(int itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                Id = item.ID;
                Text = item.Name;
                Description = item.Name;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
