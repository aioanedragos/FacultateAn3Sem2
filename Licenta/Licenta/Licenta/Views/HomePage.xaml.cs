using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Licenta.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NavigationPage(new AddChildren()));
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {

        }

        private async void Button_Clicked_3(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NavigationPage(new GetAllChildren()));
        }

        private async void Button_Clicked_4(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NavigationPage(new DeleteChildren()));
        }

        private async void Button_Clicked_5(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NavigationPage(new UpdateChildren()));
        }
    }
}