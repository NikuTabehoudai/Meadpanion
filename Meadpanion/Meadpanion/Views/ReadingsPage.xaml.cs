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
    public partial class ReadingsPage : ContentPage
    {
        ReadingsViewModel _viewModel;

        public ReadingsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ReadingsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}