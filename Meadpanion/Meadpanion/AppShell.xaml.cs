﻿using Meadpanion.ViewModels;
using Meadpanion.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Meadpanion
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(MeadsDetailPage), typeof(MeadsDetailPage));
            Routing.RegisterRoute(nameof(RecipeDetailPage), typeof(RecipeDetailPage));
            Routing.RegisterRoute(nameof(NewMeadPage), typeof(NewMeadPage));
            Routing.RegisterRoute(nameof(NewRecipePage), typeof(NewRecipePage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
