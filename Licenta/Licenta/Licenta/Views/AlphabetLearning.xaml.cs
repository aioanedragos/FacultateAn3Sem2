using Licenta.Tables;
using Plugin.TextToSpeech;
using SQLite;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
        string _dbpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "Children.db");
        int[] _accuracy = new int[22];
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

        int index = 0;

        public AlphabetLearning()
        {
            InitializeComponent();
        }

        public AlphabetLearning(Guid userId, string name)
        {
            InitializeComponent();
            var db = new SQLiteConnection(_dbpath);
            var child = db.Table<Children>().Where(x => x.ParentId == userId && x.ChildrenName == name).FirstOrDefault();
            var index = Array.IndexOf(AlphabetLetter, child.LetterRemane.ToUpper());
            Ceva.Text = "Litera " + AlphabetLetter[index].ToString() + " de la " + Words[index].ToString();
            Console.WriteLine("String intreg " + child.Accuracy);

            StringToVector(child.Accuracy);

            for (int i = 0; i < _accuracy.Length; i++)
                Console.WriteLine(_accuracy[i]);
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

        private void StringToVector(string stringVector)
        {
            string intermediarString = "";
            int index = 0;
            for(int i = 0; i < stringVector.Length; i++)
            {
                if(i + 1 > stringVector.Length)
                {
                    intermediarString += stringVector[i];
                    _accuracy[index] = Convert.ToInt32(intermediarString);
                }

                else if (stringVector[i] != '+')
                    intermediarString += stringVector[i];
                else
                {
                    _accuracy[index++] = Convert.ToInt32(intermediarString);
                    intermediarString = "";
                }
            }
        }
    }
}