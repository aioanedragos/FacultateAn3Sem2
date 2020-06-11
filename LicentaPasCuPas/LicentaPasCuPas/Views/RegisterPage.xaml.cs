using LicentaPasCuPas.DataBase;
using System;
using System.Text.RegularExpressions;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LicentaPasCuPas.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        protected override async void OnAppearing()
        {
           base.OnAppearing();
           listView.ItemsSource = await App.Database.GetPeopleAsync();
        }

        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(email.Text) && !string.IsNullOrWhiteSpace(password.Text))
            {
                //var ConfirmPassword = confirmpassword.Text;
                var emailPattern = "^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$";
                if (!String.IsNullOrWhiteSpace(email.Text) && !(Regex.IsMatch(email.Text, emailPattern)))
                {
                    await DisplayAlert("Alert", "Enter an valid Email", "OK");
                }
                else if (password.Text != confirmpassword.Text)
                {
                    await DisplayAlert("Alert", "Confirm Password and password doesnt match", "OK");
                }
                else
                {
                   
                    await App.Database.SavePersonAsync(new User
                    {
                        Email = email.Text,
                        Password = password.Text
                    });
                    email.Text = password.Text = string.Empty;
                    listView.ItemsSource = await App.Database.GetPeopleAsync();
                    await Navigation.PushAsync(new MainPage());
                }
            }
        }
    }
}
