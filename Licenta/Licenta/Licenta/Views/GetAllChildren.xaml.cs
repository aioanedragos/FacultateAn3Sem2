using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Licenta.Tables;

namespace Licenta.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GetAllChildren : ContentPage
    {

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listView.ItemsSource = await App.Database.GetPeopleAsync();
        }

        public GetAllChildren()
        {
            InitializeComponent();
        }

        public async void Button_Clicked(object sender, EventArgs e)
        {
            
            listView.ItemsSource = await App.Database.GetPeopleAsync();
        }
    }
}