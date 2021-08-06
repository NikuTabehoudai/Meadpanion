using Meadpanion.ViewModels;
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


            Routing.RegisterRoute(nameof(NewMeadPage), typeof(NewMeadPage));
            Routing.RegisterRoute(nameof(NewRecipePage), typeof(NewRecipePage));
            Routing.RegisterRoute(nameof(EditMeadPage), typeof(EditMeadPage));
            Routing.RegisterRoute(nameof(MeadsDetailPage), typeof(MeadsDetailPage));

            Routing.RegisterRoute(nameof(EditReadingPage), typeof(EditReadingPage));
            Routing.RegisterRoute(nameof(ReadingsPage), typeof(ReadingsPage));
            Routing.RegisterRoute(nameof(NewReadingPage), typeof(NewReadingPage));
            Routing.RegisterRoute(nameof(ReadingDetailPage), typeof(ReadingDetailPage));

            Routing.RegisterRoute(nameof(EventsPage), typeof(EventsPage));
            Routing.RegisterRoute(nameof(NewEventPage), typeof(NewEventPage));
            Routing.RegisterRoute(nameof(EditEventPage), typeof(EditEventPage));
            Routing.RegisterRoute(nameof(EventDetailPage), typeof(EventDetailPage));

            Routing.RegisterRoute(nameof(RecipeDetailPage), typeof(RecipeDetailPage));

        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
