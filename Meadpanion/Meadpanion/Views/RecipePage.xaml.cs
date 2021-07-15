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
    public partial class RecipePage : ContentPage
    {
        RecipeViewModel _viewModel;

        public RecipePage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new RecipeViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}