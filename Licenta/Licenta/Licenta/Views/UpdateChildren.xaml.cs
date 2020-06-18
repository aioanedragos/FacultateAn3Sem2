using Licenta.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using SQLite;
using SQLitePCL;

namespace Licenta.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateChildren : ContentPage
    {

        Children _children = new Children();

        string _dbpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "Children.db");

        Guid _parentId;

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listView.ItemsSource = await App.Database.GetPeopleAsync();
        }

        public UpdateChildren( )
        {
            InitializeComponent();

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(_dbpath);
            listView.ItemsSource = db.Table<Children>().Where(x => x.ParentId == _parentId).ToList();
            listView.ItemSelected += ListView_ItemSelected;
            if (EntryName.Text != null) {
                Children children = new Children()
                {
                    UserId = Guid.Parse(EntryId.Text),
                    ChildrenName = EntryName.Text,
                    Age = Convert.ToInt32(EntryAge.Text)
                };

                db.Update(children);

                await Navigation.PopAsync();
            }
            
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            _children = (Children)e.SelectedItem;
            EntryId.Text = _children.UserId.ToString();
            EntryName.Text = _children.ChildrenName;
            EntryAge.Text = _children.Age.ToString();
        }
    }
}