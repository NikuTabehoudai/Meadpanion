using Meadpanion.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Meadpanion.Views
{
    public partial class ReadingDetailPage : ContentPage
    {
        public ReadingDetailPage()
        {
            InitializeComponent();
            BindingContext = new ReadingDetailViewModel();
        }
    }
}