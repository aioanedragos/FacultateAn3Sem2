using Licenta.Tables;
using SQLite;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Licenta.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        public void Button_Clicked(object sender, EventArgs e)
        {
            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "RegisterUserTable.db");
            var db = new SQLiteConnection(dbpath);
            db.CreateTable<RegisterUserTable>();

            var item = new RegisterUserTable()
            {
                UserName = EntryUserName.Text,
                Password = EntryUserPassword.Text,
                Email = EntryUserEmail.Text,
                PhoneNumber = EntryUserPhoneNumber.Text
            };
            db.Insert(item);
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await this.DisplayAlert("Felicitari", "Cont Creat", "Da", "Nu");

                if (result)
                    await Navigation.PushAsync(new LoginPAge());
            });
        }
    }
}