using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Licenta.Tables;
using System.IO;
using SQLite;
using Newtonsoft.Json.Bson;

namespace Licenta.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GetAllChildren : ContentPage
    {

        public GetAllChildren(Guid parentId)
        {
            InitializeComponent();
            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Children.db");
            var db = new SQLiteConnection(dbpath);
            var myList = db.Table<Children>().Where(x => x.ParentId == parentId); //your list here
            MyButtons.Children.Clear(); //just in case so you can call this code several times np..
            foreach (var item in myList)
            {
                var btn = new Button()
                {
                    Text = item.ChildrenName,
                    StyleId = item.UserId.ToString(),
                    BackgroundColor = Xamarin.Forms.Color.HotPink,
                    TextColor = Xamarin.Forms.Color.White,
                    CornerRadius = 30
                    };
                btn.Clicked += OnDynamicBtnClicked;
                MyButtons.Children.Add(btn);
            }
        }

        private async void OnDynamicBtnClicked(object sender, EventArgs e)
        {
            var myBtn = sender as Button;
            var myId = myBtn.StyleId;
            await Navigation.PushAsync(new NavigationPage(new AlphabetLearning(Guid.Parse(myId))));
        }

    }
}