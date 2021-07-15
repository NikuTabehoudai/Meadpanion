using Meadpanion.Models;
using Meadpanion.ViewModels;
using Meadpanion.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Meadpanion.Views
{
    public partial class MeadsPage : ContentPage
    {
        MeadsViewModel _viewModel;

        public MeadsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new MeadsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}