using Licenta.Tables;
using SQLite;
using System;
using System.IO;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Security.Cryptography;
using System.Text;

namespace Licenta.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        public async void Button_Clicked(object sender, EventArgs e)
        {
            string passwordHash;
            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "RegisterUserTable.db");
            var db = new SQLiteConnection(dbpath);
            db.CreateTable<RegisterUserTable>();

            var myquery = db.Table<RegisterUserTable>().Where(u => u.UserName.Equals(EntryUserName.Text)).FirstOrDefault();
            if(myquery == null)
            {
                using (MD5 md5Hash = MD5.Create())
                {
                    passwordHash = GetMd5Hash(md5Hash, EntryUserPassword.Text);
                    Console.WriteLine("Parola din register este " + passwordHash.ToString());
                }

                var item = new RegisterUserTable()
                {
                    UserName = EntryUserName.Text,
                    Password = passwordHash,
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
            else
            {
               await DisplayAlert("Eroare", "UserName deja existent", "Ok");
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