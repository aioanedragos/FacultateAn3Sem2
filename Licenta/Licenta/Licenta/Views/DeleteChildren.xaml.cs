using Licenta.Tables;
using System;
using System.Collections.Generic;
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
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listView.ItemsSource = await App.Database.GetPeopleAsync();
        }

        public DeleteChildren()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            Children children = new Children();
        }
    }
}