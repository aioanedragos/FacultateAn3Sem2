using Plugin.TextToSpeech;
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
    public partial class AlphabetLearning : ContentPage
    {

        string[] AlphabetLetter = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "R", "S", "T", "U", "V", "Z" };
        string[] Words = new string[] {"Ac",
                                       "Bec",
                                       "Cerb",
                                       "Dus",
                                       "Elefant",
                                       "Fetitat",
                                       "Gard",
                                       "Ham",
                                       "Ie",
                                       "Jocuri",
                                       "Kilogram",
                                       "Lup",
                                       "Mana",
                                       "Nuca",
                                       "Oi",
                                       "Pruna",
                                       "Rata",
                                       "Sac",
                                       "Tun",
                                       "Urs",
                                       "Varza",
                                       "Zebra"};

        int[] Accuracy = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        int index = 0;
        public AlphabetLearning()
        {
            InitializeComponent();
            //LetterRemain.Text = Char.ToUpper('a').ToString(); //Interogare in baza de date pentru litera
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            char Letter = Char.ToUpper('a');//Interogare in baza de date pentru litera
            index = Array.IndexOf(AlphabetLetter, Letter); 
            
            index++;
            

        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            var text = MainEntry.Text;

            CrossTextToSpeech.Current.Speak(text);
        }
    }
}