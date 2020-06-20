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


        public UpdateChildren(Guid parentId)
        {
            InitializeComponent();
            _parentId = parentId;
            var db = new SQLiteConnection(_dbpath);
            listView.ItemsSource = db.Table<Children>().Where(x => x.ParentId == _parentId).ToList();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(_dbpath);
            //listView.ItemsSource = db.Table<Children>().Where(x => x.ParentId == _parentId && x.ChildrenName == EntryName.Text).ToList();
            listView.ItemSelected += ListView_ItemSelected;
            if (EntryName.Text != null) {

                Guid id = Guid.Parse(EntryId.Text);
                string letterRemain = db.Table<Children>().Where(x => x.UserId == id).FirstOrDefault().LetterRemane;
                string accuracy = db.Table<Children>().Where(x => x.UserId == id).FirstOrDefault().Accuracy;
                //db.Update(children);
                db.Table<Children>().Delete(x => x.UserId == id && x.ParentId == _parentId);
                Children children = new Children()
                {
                    ChildrenName = EntryName.Text,
                    ParentId = _parentId,
                    Age = Convert.ToInt32(EntryAge.Text),
                    LetterRemane = letterRemain,
                    Accuracy = accuracy
                };
                db.Insert(children);
                listView.ItemsSource = db.Table<Children>().Where(x => x.ParentId == _parentId).ToList();
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