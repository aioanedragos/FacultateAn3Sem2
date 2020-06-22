using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
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
            if(EntryUser.Text != null && EntryPassword.Text != null)
            {
                var myquery = db.Table<RegisterUserTable>().Where(u => u.UserName.Equals(EntryUser.Text)).FirstOrDefault();

                var passwordHash = myquery.Password;
                int success = 0;


                using (MD5 md5Hash = MD5.Create())
                {
                    Console.WriteLine("Parola din register este " + VerifyMd5Hash(md5Hash, EntryPassword.Text, passwordHash).ToString());
                    if (VerifyMd5Hash(md5Hash, EntryPassword.Text, passwordHash))
                    {
                        success = 1;
                    }
                    else
                    {
                        success = 0;
                    }
                }

                if (success != 0)
                {
                    App.Current.MainPage = new NavigationPage(new HomePage(myquery.UserId));
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
            else
            {
                await this.DisplayAlert("Eroare", "Introduceti datele contului", "OK");
            }
           
        }

        static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }
}