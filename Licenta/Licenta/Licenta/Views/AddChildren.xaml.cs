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

        string _dbpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments),"Children.db");
        public AddChildren()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(_dbpath);
            db.CreateTable<Children>();

            Children children = new Children()
            {
                ChildrenName = EntryName.Text,
                Age = Convert.ToInt32(EntryAge.Text),
                LetterRemane = 'a',
                Accuracy = 0
            };
            db.Insert(children);
            await DisplayAlert(null, children.ChildrenName + " Saved", "ok");
            await Navigation.PopAsync();

        }
    }
}