using Meadpanion.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Meadpanion.Views
{
    public partial class RecipeDetailPage : ContentPage
    {
        public RecipeDetailPage()
        {
            InitializeComponent();
            BindingContext = new RecipeDetailViewModel();
        }
    }
}