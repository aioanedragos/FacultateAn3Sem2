using System;
using System.IO;
using Licenta.Tables;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Licenta.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPAge : ContentPage
    {
        public LoginPAge()
        {
            InitializeComponent();
        }

        async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistrationPage());
        }

        async void Button_Clicked_1(object sender, EventArgs e)
        {
            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "RegisterUserTable.db");
            var db = new SQLiteConnection(dbpath);
            var myquery = db.Table<RegisterUserTable>().Where(u => u.UserName.Equals(EntryUser.Text) && u.Password.Equals(EntryPassword.Text)).FirstOrDefault();
            if (myquery != null) {
                App.Current.MainPage = new NavigationPage(new HomePage());
            }
            else
            {
                Device.BeginInvokeOnMainThread(async () => {
                    var result = await this.DisplayAlert("Eroare", "Date introduse gresit", "Da", "Nu");
                    if (result)
                        await Navigation.PushAsync(new LoginPAge());
                    else
                        await Navigation.PushAsync(new LoginPAge());

                });
            }
        }
    }
}