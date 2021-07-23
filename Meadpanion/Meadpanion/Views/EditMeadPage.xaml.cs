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
    public partial class EditMeadPage : ContentPage
    {
        public EditMeadPage()
        {
            InitializeComponent();
            BindingContext = new EditMeadViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
        }

    }
}