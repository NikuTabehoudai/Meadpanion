using Meadpanion.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Meadpanion.Views
{
    public partial class MeadsDetailPage : ContentPage
    {
        public MeadsDetailPage()
        {
            InitializeComponent();
            BindingContext = new MeadsDetailViewModel();
        }
    }
}