using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using LicentaPasCuPas.Views;
using LicentaPasCuPas.DataBase;
using System.Text.RegularExpressions;

namespace LicentaPasCuPas
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {

        //protected override async void OnAppearing()
        //{
        //    base.OnAppearing();
        //   listView.ItemsSource = await App.Database.GetPeopleAsync();
        //}
        public MainPage()
        {
            InitializeComponent();
        }

        async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(email.Text) && !string.IsNullOrWhiteSpace(password.Text))
            {
                await App.Database.SavePersonAsync(new User
                {
                    Email = email.Text,
                    Password = password.Text
                }) ;

                var select = App.Database.GetPersonAsync(email.Text);
                if (select.Result.Email != null)
                {
                    await DisplayAlert("Alert", "Email existent", "OK");
                }
                else {
                    await DisplayAlert("Alert", "Cont creat", "OK");
                }




                
            }
        }
    }
}
