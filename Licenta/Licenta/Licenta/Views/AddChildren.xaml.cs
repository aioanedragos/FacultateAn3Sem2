using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using Licenta.Tables;

namespace Licenta.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddChildren : ContentPage
    {

        Guid _parentId;

        int[] _accuracy = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        string _dbpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments),"Children.db");
        public AddChildren(Guid parentId)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            _parentId = parentId;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(_dbpath);
            db.CreateTable<Children>();
            if(EntryName.Text != null && EntryAge.Text != null)
            {
                int succes = 1;
                string numbers = "0123456789";
                for (int i = 0; i < EntryAge.Text.Length; i++)
                    if (numbers.Contains(EntryAge.Text[i]) == false)
                        succes = 0;
                if (succes == 1)
                {
                    Children children = new Children()
                    {
                        ParentId = _parentId,
                        ChildrenName = EntryName.Text,
                        Age = Convert.ToInt32(EntryAge.Text),
                        LetterRemane = "a",
                        Accuracy = "0+0+0+0+0+0+0+0+0+0+0+0+0+0+0+0+0+0+0+0+0+0"
                    };
                    db.Insert(children);
                    await DisplayAlert(null, children.ChildrenName + " Saved", "ok");
                    await Navigation.PopAsync();

                }
                else
                {
                    await DisplayAlert("Eroare", "Varsta nu este introdusa corect", "Ok");
                }
            }
            else
            {
                await DisplayAlert("Eroare", "Datele sunt introduse gresit", "Ok");
            }
            

        }
    }
}