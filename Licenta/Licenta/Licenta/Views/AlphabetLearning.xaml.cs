using Licenta.Tables;
using Plugin.TextToSpeech;
using SQLite;
using System;
using System.ComponentModel;
using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Licenta.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlphabetLearning : ContentPage
    {
        string _dbpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "Children.db");
        Children child;
        Guid _parentId;
        int maxLimit = 0;
        int minLimit = 0;
        int up = 1;
        int down = 0;
        int[] _accuracy = new int[22];
        string[] AlphabetLetter = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "R", "S", "T", "U", "V", "Z" };
        string[] Words = new string[] {"Ac",
                                       "Bec",
                                       "Cerb",
                                       "Dus",
                                       "Elefant",
                                       "Fetitat",
                                       "Gard",
                                       "Haina",
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

        string[] poze = new string[] {  "ac.jpg",
                                        "bec.jpg",
                                        "cerb.jpg",
                                        "dus.jpg",
                                        "elefant.jpg",
                                        "fetita.jpg",
                                        "gard.jpg",
                                        "haina.jpg",
                                        "Ie.jpg",
                                        "jocuri.jpg",
                                        "kilogram.jpg",
                                        "Lup.jpg",
                                        "mana.jpg",
                                        "nuca.jpg",
                                        "oi.jpg",
                                        "pruna.jpg",
                                        "rata.jpg",
                                        "sac.jpg",
                                        "tun.jpg",
                                        "urs.jpg",
                                        "varza.jpg",
                                        "zebra.jpg"};

        int index = 0;

        public AlphabetLearning()
        {
            InitializeComponent();
        }

        public AlphabetLearning(Guid userId, string name)
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);
            _parentId = userId;
            var db = new SQLiteConnection(_dbpath);
            child = db.Table<Children>().Where(x => x.ParentId == userId && x.ChildrenName == name).FirstOrDefault();
            index = Array.IndexOf(AlphabetLetter, child.LetterRemane.ToUpper());
            Ceva.Text = "Litera " + AlphabetLetter[index].ToString() + " de la " + Words[index].ToString();
            SomeImage.Source = poze[index];
            int indexIntermediar = index + 1;

            if(indexIntermediar % 3 == 0)
            {
                maxLimit = indexIntermediar - 1;
                minLimit = indexIntermediar - 2;
            }
            else
            {
                while(indexIntermediar % 3 != 0)
                {
                    indexIntermediar++;
                }
                maxLimit = indexIntermediar - 1;
                minLimit = maxLimit - 2;

            }
            Console.WriteLine("String intreg " + child.Accuracy);

            StringToVector(child.Accuracy);

            for (int i = 0; i < _accuracy.Length; i++)
                Console.WriteLine(_accuracy[i]);
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            var text = MainEntry.Text;
            if (String.IsNullOrEmpty(text))
                DisplayAlert("Eroare", "Introduceti litera afisata", "Ok");
            else
            {
                if (MainEntry.Text.ToUpper() == AlphabetLetter[index])
                {
                    _accuracy[index] += 20;
                    if(index < maxLimit && up == 1)
                    {
                        index++;
                    }
                    else if (up == 1)
                    {
                        index--;
                        up = 0;
                        down = 1;
                    }

                    if(index > minLimit && down == 1)
                    {
                        index--;
                    }
                    else if(down == 1)
                    {
                        index++;
                        up = 1;
                        down = 0;
                    }
                    if(_accuracy[minLimit] >= 80 && _accuracy[minLimit + 1] >= 80 && _accuracy[maxLimit] >= 80)
                    {
                        minLimit = maxLimit + 1;
                        maxLimit = maxLimit + 3;
                        index = minLimit;
                    }
                    Ceva.Text = "Litera " + AlphabetLetter[index].ToString() + " de la " + Words[index].ToString();
                    SomeImage.Source = poze[index];
                    child.LetterRemane = AlphabetLetter[index].ToString();
                    CrossTextToSpeech.Current.Speak(text);
                    var db = new SQLiteConnection(_dbpath);
                    //Children children = new Children()
                    //{
                     //   UserId = child.UserId,
                      //  LetterRemane = AlphabetLetter[index].ToString(),
                       // Accuracy = VectorToString(_accuracy)
                    //};
                    //db.Table<Children>().Delete(x => x.UserId == child.UserId && x.ChildrenName == child.ChildrenName);

                    Children children = new Children()
                    {
                        UserId = child.UserId,
                        ParentId = _parentId,
                        ChildrenName = child.ChildrenName,
                        Age = child.Age,
                        LetterRemane = AlphabetLetter[index],
                        Accuracy = VectorToString(_accuracy)
                    };

                  
                    
                    db.Update(children);

                }
                else
                {
                    DisplayAlert("Eroare", "Litera introdusa este gresita", "Ok");
                    _accuracy[index] -= 10;
                    if (_accuracy[index] < 0)
                        _accuracy[index] = 0;
                }
                    


            }
                
        }


        private string VectorToString(int [] Vector)
        {

            string intermediarString = Vector[0].ToString();
            for (int i = 1; i < Vector.Length; i++)
                intermediarString += "+" + Vector[i];
            return intermediarString;

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