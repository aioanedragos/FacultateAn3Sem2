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
        Guid ceva;
        public GetAllChildren(Guid parentId)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            ceva = parentId;
            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Children.db");
            var db = new SQLiteConnection(dbpath);
            var myList = db.Table<Children>().Where(x => x.ParentId == parentId); 
            //var myList = db.Query<Children>("SELECT ChildrenName, UserId, Age FROM Children WHERE ParentId = ?", parentId);
            MyButtons.Children.Clear(); 
            
            foreach (var item in myList)
            {
                Console.WriteLine(item.LetterRemane.ToString());
                var btn = new Button()
                {
                    Text = item.ChildrenName + " " + item.Age.ToString(),
                    StyleId = item.ChildrenName,
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
            //await Navigation.PushAsync(new NavigationPage(new AlphabetLearning(new Guid(ceva))));
            await Navigation.PushAsync(new NavigationPage(new AlphabetLearning(ceva, myId)));
        }

    }
}