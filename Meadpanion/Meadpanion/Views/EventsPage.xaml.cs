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
    public partial class EventsPage : ContentPage
    {
        EventsViewModel _viewModel;

        public EventsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new EventsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}