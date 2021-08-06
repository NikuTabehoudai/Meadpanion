using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Meadpanion.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Meadpanion.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewEventPage : ContentPage
    {
        public NewEventPage()
        {
            InitializeComponent();
            BindingContext = new NewEventViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

        }
    }
}