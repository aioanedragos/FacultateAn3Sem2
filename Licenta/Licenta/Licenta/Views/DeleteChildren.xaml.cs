using Licenta.Tables;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Licenta.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeleteChildren : ContentPage
    {
        Children _children = new Children();

        Guid _parentId;

        string _dbpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "Children.db");

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listView.ItemsSource = await App.Database.GetPeopleAsync();
        }

        public DeleteChildren(Guid parentId)
        {
            InitializeComponent();
            _parentId = parentId;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            listView.ItemsSource = await App.Database.GetPeopleAsync();
            listView.ItemSelected += ListView_ItemSelected;

            var db = new SQLiteConnection(_dbpath);
            db.Table<Children>().Delete(x => x.UserId == _children.UserId && x.ParentId == _parentId);
            await Navigation.PopAsync();
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            _children = (Children)e.SelectedItem;

        }
    }
}