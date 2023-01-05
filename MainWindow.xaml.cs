using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BlackJack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        int[] kaartWaarde;
        string[] kaartNamen;
        BitmapImage[] kaartImages;
        List<int> gekozenKaartenSpeler = new List<int>();
        List<int> gekozenKaartenBank = new List<int>();
        int hitCount;
        
        int totaalSpeler;
        int totaalBank;

        bool isSpeler = true;
        Random rnd = new Random();

        int kapitaalSpeler = 100;
        int inzetWaarde;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BtnHit.IsEnabled = false;
            BtnStand.IsEnabled = false;
            BtnDeel.IsEnabled = false;
            SldrKapitaal.IsEnabled = true;
            LblBankTotaal.Content = "0";
            LblSpelerTotaal.Content = "0";
            TxtInzet.Text = "0";
            TxtKapitaal.Text = kapitaalSpeler.ToString();
            BuildArray();
        }

        private void BtnNieuweSpel_Click(object sender, RoutedEventArgs e)
        {
            BtnHit.IsEnabled = false;
            BtnStand.IsEnabled = false;
            BtnDeel.IsEnabled = false;
            SldrKapitaal.IsEnabled = true;
            LstbxSpeler.Items.Clear();
            LstbxBank.Items.Clear();
            LblResultaat.Content = "";
            LblBankTotaal.Content = "0";
            LblSpelerTotaal.Content = "0";
            totaalSpeler = 0;
            totaalBank = 0;
            TxtInzet.Text = "0";
            kapitaalSpeler = 100;
            SldrKapitaal.Value = 0;
            TxtKapitaal.Text = kapitaalSpeler.ToString();
            hitCount = 0;
            ImgSpelerKaart1.Source = null;
            ImgSpelerKaart2.Source = null;
            ImgSpelerKaart3.Source = null;
            ImgSpelerKaart4.Source = null;
            ImgSpelerKaart5.Source = null;
            ImgBankKaart1.Source = null;
            ImgBankKaart2.Source = null;
            ImgBankKaart3.Source = null;
            ImgBankKaart4.Source = null;
            ImgBankKaart5.Source = null;
        }
        private void BtnDeel_Click(object sender, RoutedEventArgs e)
        {
            BtnHit.IsEnabled = true;
            BtnStand.IsEnabled = true;
            BtnDeel.IsEnabled = false;
            LstbxSpeler.Items.Clear();
            LstbxBank.Items.Clear();
            LblResultaat.Content = "";
            LblBankTotaal.Content = "0";
            LblSpelerTotaal.Content = "0";
            hitCount = 0;
            ImgSpelerKaart1.Source = null;
            ImgSpelerKaart2.Source = null;
            ImgSpelerKaart3.Source = null;
            ImgSpelerKaart4.Source = null;
            ImgSpelerKaart5.Source = null;
            ImgBankKaart1.Source = null;
            ImgBankKaart2.Source = null;
            ImgBankKaart3.Source = null;
            ImgBankKaart4.Source = null;
            ImgBankKaart5.Source = null;
            SldrKapitaal.IsEnabled = false;

            totaalSpeler = 0;
            totaalBank = 0;

            for (int i = 0; i < 2; i++)
            {
                GeefKaart(isSpeler);

            }
            GeefKaart(!isSpeler);
            ImgSpelerKaart1.Source = kaartImages[gekozenKaartenSpeler[0]];
            ImgSpelerKaart2.Source = kaartImages[gekozenKaartenSpeler[1]];
            ImgBankKaart1.Source = kaartImages[gekozenKaartenBank[0]];

            LblSpelerTotaal.Content = totaalSpeler;

        }

        private void BtnHit_Click(object sender, RoutedEventArgs e)
        {
           
            GeefKaart(isSpeler);
            if (hitCount == 0) {
                ImgSpelerKaart3.Source = kaartImages[gekozenKaartenSpeler.Last()];
            }
            if(hitCount == 1)
            {
                ImgSpelerKaart4.Source = kaartImages[gekozenKaartenSpeler.Last()];
            }
            if (hitCount == 2)
            {
                ImgSpelerKaart5.Source = kaartImages[gekozenKaartenSpeler.Last()];
            }

            hitCount += 1;
            LblSpelerTotaal.Content = totaalSpeler;

            if (totaalSpeler > 21)
            {
                LblResultaat.Content = "VERLOREN";
                LblResultaat.Foreground = new SolidColorBrush(Colors.Red);
                kapitaalSpeler = int.Parse(TxtKapitaal.Text) - int.Parse(TxtInzet.Text);
                TxtKapitaal.Text = kapitaalSpeler.ToString();
                SldrKapitaal.IsEnabled = true;
                SldrKapitaal.Value = 0;
                BtnDeel.IsEnabled = false;
                BtnHit.IsEnabled = false;
                BtnStand.IsEnabled = false;
            }

            if (kapitaalSpeler == 0)
            {
                BtnHit.IsEnabled = false;
                BtnStand.IsEnabled = false;
                BtnDeel.IsEnabled = false;
                SldrKapitaal.IsEnabled = false;
                BtnNieuweSpel.IsEnabled = true;
            }




        }
        private void BtnStand_Click(object sender, RoutedEventArgs e)
        {
            

            do
            {
                GeefKaart(!isSpeler);
             
            }
            while (totaalBank <= 17);
            
            ImgBankKaart2.Source = kaartImages[gekozenKaartenBank[1]];

            
            if(gekozenKaartenBank.Count > 2)
            {
                ImgBankKaart3.Source = kaartImages[gekozenKaartenBank[2]];
            }   

            if (gekozenKaartenBank.Count > 3)
            {
                ImgBankKaart4.Source = kaartImages[gekozenKaartenBank[3]];
            }

            if (gekozenKaartenBank.Count > 4)
            {
                ImgBankKaart5.Source = kaartImages[gekozenKaartenBank[4]];
            }
           

            LblBankTotaal.Content = totaalBank;

            Gamestatus();

            BtnHit.IsEnabled = false;
            BtnStand.IsEnabled = false;
            BtnDeel.IsEnabled = false;
            SldrKapitaal.IsEnabled = true;
           

            if (kapitaalSpeler == 0)
            {
                BtnHit.IsEnabled = false;
                BtnStand.IsEnabled = false;
                BtnDeel.IsEnabled = false;
                SldrKapitaal.IsEnabled = false;
                BtnNieuweSpel.IsEnabled = true;
            }
            else {
                SldrKapitaal.Value = 0;
            }
        }

        private void GeefKaart(bool isSpeler)
        {


            if (isSpeler)
            {
                int waarde = rnd.Next(kaartNamen.Length);
                string naam = kaartNamen[waarde];
                LstbxSpeler.Items.Add(naam);
                gekozenKaartenSpeler.Add(waarde);
                totaalSpeler += kaartWaarde[waarde];

            }

            else
            {
                int waarde = rnd.Next(kaartNamen.Length);
                string naam = kaartNamen[waarde];
                LstbxBank.Items.Add(naam);
                gekozenKaartenBank.Add(waarde);
                totaalBank += kaartWaarde[waarde];

            }

        }

        private void BuildArray()
        {
            kaartImages = new BitmapImage[]
            {
            new BitmapImage(new Uri(@"C:\Users\yasem\OneDrive\Documenten\Graduaat programmeren\Semester 1\Werkplekleren 1\Project draaiboek\BlackJack\CardImages\Schoppen2.png", UriKind.Absolute)),
            new BitmapImage(new Uri(@"C:\Users\yasem\OneDrive\Documenten\Graduaat programmeren\Semester 1\Werkplekleren 1\Project draaiboek\BlackJack\CardImages\Schoppen3.png", UriKind.Absolute)),
            new BitmapImage(new Uri(@"C:\Users\yasem\OneDrive\Documenten\Graduaat programmeren\Semester 1\Werkplekleren 1\Project draaiboek\BlackJack\CardImages\Schoppen4.png", UriKind.Absolute)),
            new BitmapImage(new Uri(@"C:\Users\yasem\OneDrive\Documenten\Graduaat programmeren\Semester 1\Werkplekleren 1\Project draaiboek\BlackJack\CardImages\Schoppen5.png", UriKind.Absolute)),
            new BitmapImage(new Uri(@"C:\Users\yasem\OneDrive\Documenten\Graduaat programmeren\Semester 1\Werkplekleren 1\Project draaiboek\BlackJack\CardImages\Schoppen6.png", UriKind.Absolute)),
            new BitmapImage(new Uri(@"C:\Users\yasem\OneDrive\Documenten\Graduaat programmeren\Semester 1\Werkplekleren 1\Project draaiboek\BlackJack\CardImages\Schoppen7.png", UriKind.Absolute)),
            new BitmapImage(new Uri(@"C:\Users\yasem\OneDrive\Documenten\Graduaat programmeren\Semester 1\Werkplekleren 1\Project draaiboek\BlackJack\CardImages\Schoppen8.png", UriKind.Absolute)),
            new BitmapImage(new Uri(@"C:\Users\yasem\OneDrive\Documenten\Graduaat programmeren\Semester 1\Werkplekleren 1\Project draaiboek\BlackJack\CardImages\Schoppen9.png", UriKind.Absolute)),
            new BitmapImage(new Uri(@"C:\Users\yasem\OneDrive\Documenten\Graduaat programmeren\Semester 1\Werkplekleren 1\Project draaiboek\BlackJack\CardImages\Schoppen10.png", UriKind.Absolute)),
            new BitmapImage(new Uri(@"C:\Users\yasem\OneDrive\Documenten\Graduaat programmeren\Semester 1\Werkplekleren 1\Project draaiboek\BlackJack\CardImages\SchoppenBoer.png", UriKind.Absolute)),
            new BitmapImage(new Uri(@"C:\Users\yasem\OneDrive\Documenten\Graduaat programmeren\Semester 1\Werkplekleren 1\Project draaiboek\BlackJack\CardImages\SchoppenDame.png", UriKind.Absolute)),
            new BitmapImage(new Uri(@"C:\Users\yasem\OneDrive\Documenten\Graduaat programmeren\Semester 1\Werkplekleren 1\Project draaiboek\BlackJack\CardImages\SchoppenKoning.png", UriKind.Absolute)),
            new BitmapImage(new Uri(@"C:\Users\yasem\OneDrive\Documenten\Graduaat programmeren\Semester 1\Werkplekleren 1\Project draaiboek\BlackJack\CardImages\SchoppenAas.png", UriKind.Absolute)),
            new BitmapImage(new Uri(@"C:\Users\yasem\OneDrive\Documenten\Graduaat programmeren\Semester 1\Werkplekleren 1\Project draaiboek\BlackJack\CardImages\Ruiten2.png", UriKind.Absolute)),
            new BitmapImage(new Uri(@"C:\Users\yasem\OneDrive\Documenten\Graduaat programmeren\Semester 1\Werkplekleren 1\Project draaiboek\BlackJack\CardImages\Ruiten3.png", UriKind.Absolute)),
            new BitmapImage(new Uri(@"C:\Users\yasem\OneDrive\Documenten\Graduaat programmeren\Semester 1\Werkplekleren 1\Project draaiboek\BlackJack\CardImages\Ruiten4.png", UriKind.Absolute)),
            new BitmapImage(new Uri(@"C:\Users\yasem\OneDrive\Documenten\Graduaat programmeren\Semester 1\Werkplekleren 1\Project draaiboek\BlackJack\CardImages\Ruiten5.png", UriKind.Absolute)),
            new BitmapImage(new Uri(@"C:\Users\yasem\OneDrive\Documenten\Graduaat programmeren\Semester 1\Werkplekleren 1\Project draaiboek\BlackJack\CardImages\Ruiten6.png", UriKind.Absolute)),
            new BitmapImage(new Uri(@"C:\Users\yasem\OneDrive\Documenten\Graduaat programmeren\Semester 1\Werkplekleren 1\Project draaiboek\BlackJack\CardImages\Ruiten7.png", UriKind.Absolute)),
            new BitmapImage(new Uri(@"C:\Users\yasem\OneDrive\Documenten\Graduaat programmeren\Semester 1\Werkplekleren 1\Project draaiboek\BlackJack\CardImages\Ruiten8.png", UriKind.Absolute)),
            new BitmapImage(new Uri(@"C:\Users\yasem\OneDrive\Documenten\Graduaat programmeren\Semester 1\Werkplekleren 1\Project draaiboek\BlackJack\CardImages\Ruiten9.png", UriKind.Absolute)),
            new BitmapImage(new Uri(@"C:\Users\yasem\OneDrive\Documenten\Graduaat programmeren\Semester 1\Werkplekleren 1\Project draaiboek\BlackJack\CardImages\Ruiten10.png", UriKind.Absolute)),
            new BitmapImage(new Uri(@"C:\Users\yasem\OneDrive\Documenten\Graduaat programmeren\Semester 1\Werkplekleren 1\Project draaiboek\BlackJack\CardImages\RuitenBoer.png", UriKind.Absolute)),
            new BitmapImage(new Uri(@"C:\Users\yasem\OneDrive\Documenten\Graduaat programmeren\Semester 1\Werkplekleren 1\Project draaiboek\BlackJack\CardImages\RuitenDame.png", UriKind.Absolute)),
            new BitmapImage(new Uri(@"C:\Users\yasem\OneDrive\Documenten\Graduaat programmeren\Semester 1\Werkplekleren 1\Project draaiboek\BlackJack\CardImages\RuitenKoning.png", UriKind.Absolute)),
            new BitmapImage(new Uri(@"C:\Users\yasem\OneDrive\Documenten\Graduaat programmeren\Semester 1\Werkplekleren 1\Project draaiboek\BlackJack\CardImages\RuitenAas.png", UriKind.Absolute)),
            new BitmapImage(new Uri(@"C:\Users\yasem\OneDrive\Documenten\Graduaat programmeren\Semester 1\Werkplekleren 1\Project draaiboek\BlackJack\CardImages\Klaveren2.png", UriKind.Absolute)),
            new BitmapImage(new Uri(@"C:\Users\yasem\OneDrive\Documenten\Graduaat programmeren\Semester 1\Werkplekleren 1\Project draaiboek\BlackJack\CardImages\Klaveren3.png", UriKind.Absolute)),
            new BitmapImage(new Uri(@"C:\Users\yasem\OneDrive\Documenten\Graduaat programmeren\Semester 1\Werkplekleren 1\Project draaiboek\BlackJack\CardImages\Klaveren4.png", UriKind.Absolute)),
            new BitmapImage(new Uri(@"C:\Users\yasem\OneDrive\Documenten\Graduaat programmeren\Semester 1\Werkplekleren 1\Project draaiboek\BlackJack\CardImages\Klaveren5.png", UriKind.Absolute)),
            new BitmapImage(new Uri(@"C:\Users\yasem\OneDrive\Documenten\Graduaat programmeren\Semester 1\Werkplekleren 1\Project draaiboek\BlackJack\CardImages\Klaveren6.png", UriKind.Absolute)),
            new BitmapImage(new Uri(@"C:\Users\yasem\OneDrive\Documenten\Graduaat programmeren\Semester 1\Werkplekleren 1\Project draaiboek\BlackJack\CardImages\Klaveren7.png", UriKind.Absolute)),
            new BitmapImage(new Uri(@"C:\Users\yasem\OneDrive\Documenten\Graduaat programmeren\Semester 1\Werkplekleren 1\Project draaiboek\BlackJack\CardImages\Klaveren8.png", UriKind.Absolute)),
            new BitmapImage(new Uri(@"C:\Users\yasem\OneDrive\Documenten\Graduaat programmeren\Semester 1\Werkplekleren 1\Project draaiboek\BlackJack\CardImages\Klaveren9.png", UriKind.Absolute)),
            new BitmapImage(new Uri(@"C:\Users\yasem\OneDrive\Documenten\Graduaat programmeren\Semester 1\Werkplekleren 1\Project draaiboek\BlackJack\CardImages\Klaveren10.png", UriKind.Absolute)),
            new BitmapImage(new Uri(@"C:\Users\yasem\OneDrive\Documenten\Graduaat programmeren\Semester 1\Werkplekleren 1\Project draaiboek\BlackJack\CardImages\KlaverenBoer.png", UriKind.Absolute)),
            new BitmapImage(new Uri(@"C:\Users\yasem\OneDrive\Documenten\Graduaat programmeren\Semester 1\Werkplekleren 1\Project draaiboek\BlackJack\CardImages\KlaverenDame.png", UriKind.Absolute)),
            new BitmapImage(new Uri(@"C:\Users\yasem\OneDrive\Documenten\Graduaat programmeren\Semester 1\Werkplekleren 1\Project draaiboek\BlackJack\CardImages\KlaverenKoning.png", UriKind.Absolute)),
            new BitmapImage(new Uri(@"C:\Users\yasem\OneDrive\Documenten\Graduaat programmeren\Semester 1\Werkplekleren 1\Project draaiboek\BlackJack\CardImages\KlaverenAas.png", UriKind.Absolute)),
            new BitmapImage(new Uri(@"C:\Users\yasem\OneDrive\Documenten\Graduaat programmeren\Semester 1\Werkplekleren 1\Project draaiboek\BlackJack\CardImages\Harten2.png", UriKind.Absolute)),
            new BitmapImage(new Uri(@"C:\Users\yasem\OneDrive\Documenten\Graduaat programmeren\Semester 1\Werkplekleren 1\Project draaiboek\BlackJack\CardImages\Harten3.png", UriKind.Absolute)),
            new BitmapImage(new Uri(@"C:\Users\yasem\OneDrive\Documenten\Graduaat programmeren\Semester 1\Werkplekleren 1\Project draaiboek\BlackJack\CardImages\Harten4.png", UriKind.Absolute)),
            new BitmapImage(new Uri(@"C:\Users\yasem\OneDrive\Documenten\Graduaat programmeren\Semester 1\Werkplekleren 1\Project draaiboek\BlackJack\CardImages\Harten5.png", UriKind.Absolute)),
            new BitmapImage(new Uri(@"C:\Users\yasem\OneDrive\Documenten\Graduaat programmeren\Semester 1\Werkplekleren 1\Project draaiboek\BlackJack\CardImages\Harten6.png", UriKind.Absolute)),
            new BitmapImage(new Uri(@"C:\Users\yasem\OneDrive\Documenten\Graduaat programmeren\Semester 1\Werkplekleren 1\Project draaiboek\BlackJack\CardImages\Harten7.png", UriKind.Absolute)),
            new BitmapImage(new Uri(@"C:\Users\yasem\OneDrive\Documenten\Graduaat programmeren\Semester 1\Werkplekleren 1\Project draaiboek\BlackJack\CardImages\Harten8.png", UriKind.Absolute)),
            new BitmapImage(new Uri(@"C:\Users\yasem\OneDrive\Documenten\Graduaat programmeren\Semester 1\Werkplekleren 1\Project draaiboek\BlackJack\CardImages\Harten9.png", UriKind.Absolute)),
            new BitmapImage(new Uri(@"C:\Users\yasem\OneDrive\Documenten\Graduaat programmeren\Semester 1\Werkplekleren 1\Project draaiboek\BlackJack\CardImages\Harten10.png", UriKind.Absolute)),
            new BitmapImage(new Uri(@"C:\Users\yasem\OneDrive\Documenten\Graduaat programmeren\Semester 1\Werkplekleren 1\Project draaiboek\BlackJack\CardImages\HartenBoer.png", UriKind.Absolute)),
            new BitmapImage(new Uri(@"C:\Users\yasem\OneDrive\Documenten\Graduaat programmeren\Semester 1\Werkplekleren 1\Project draaiboek\BlackJack\CardImages\HartenDame.png", UriKind.Absolute)),
            new BitmapImage(new Uri(@"C:\Users\yasem\OneDrive\Documenten\Graduaat programmeren\Semester 1\Werkplekleren 1\Project draaiboek\BlackJack\CardImages\HartenKoning.png", UriKind.Absolute)),
            new BitmapImage(new Uri(@"C:\Users\yasem\OneDrive\Documenten\Graduaat programmeren\Semester 1\Werkplekleren 1\Project draaiboek\BlackJack\CardImages\HartenAas.png", UriKind.Absolute)),

        };
        
            kaartNamen = new string[52] { "Schoppen 2", "Schoppen 3", "Schoppen 4", "Schoppen 5", "Schoppen 6", "Schoppen 7", "Schoppen 8", "Schoppen 9", "Schoppen 10", "Schoppen Boer", "Schoppen Dame", "Schoppen Koning", "Schoppen Aas", "Ruiten 2", "Ruiten 3", "Ruiten 4", "Ruiten 5", "Ruiten 6", "Ruiten 7", "Ruiten 8", "Ruiten 9", "Ruiten 10", "Ruiten Boer", "Ruiten Dame", "Ruiten Koning", "Ruiten Aas", "Klaveren 2", "Klaveren 3", "Klaveren 4", "Klaveren 5", "Klaveren 6", "Klaveren 7", "Klaveren 8", "Klaveren 9", "Klaveren 10", "Klaveren Boer", "Klaveren Dame", "Klaveren Koning", "Klaveren Aas", "Harten 2", "Harten 3", "Harten 4", "Harten 5", "Harten 6", "Harten 7", "Harten 8", "Harten 9", "Harten 10", "Harten Boer", "Harten Dame", "Harten Koning", "Harten Aas" };
            kaartWaarde = new int[52] { 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10, 1 };
           
          
        }

        private void GameBet()
        {
            
        }



        private void Gamestatus()
        {
           
            if (totaalSpeler > 21)
            {
                LblResultaat.Content = "VERLOREN";
                LblResultaat.Foreground = new SolidColorBrush(Colors.Red);
                kapitaalSpeler = int.Parse(TxtKapitaal.Text) - int.Parse(TxtInzet.Text);
                TxtKapitaal.Text = kapitaalSpeler.ToString();
                
                
                return;
            }

            else if (totaalSpeler == totaalBank)
            {
                LblResultaat.Content = "PUSH";
                LblResultaat.Foreground = new SolidColorBrush(Colors.Black);
              
               
                return;
            }

            else if (totaalBank > 21)
            {
                LblResultaat.Content = "GEWONNEN";
                LblResultaat.Foreground = new SolidColorBrush(Colors.Green);
                kapitaalSpeler = int.Parse(TxtKapitaal.Text) + (int.Parse(TxtInzet.Text)*2);
                TxtKapitaal.Text = kapitaalSpeler.ToString();
                
                
                return;
            }

            else if (totaalSpeler < totaalBank)
            {
                LblResultaat.Content = "VERLOREN";
                LblResultaat.Foreground = new SolidColorBrush(Colors.Red);
                kapitaalSpeler = int.Parse(TxtKapitaal.Text) - int.Parse(TxtInzet.Text);
                TxtKapitaal.Text = kapitaalSpeler.ToString();
                
                return;
            }

            else if (totaalSpeler > totaalBank)
            {
                LblResultaat.Content = "GEWONNEN";
                LblResultaat.Foreground = new SolidColorBrush(Colors.Green);
                kapitaalSpeler = int.Parse(TxtKapitaal.Text) + (int.Parse(TxtInzet.Text) * 2);
                TxtKapitaal.Text = kapitaalSpeler.ToString();
                
            }

            

            //if (totaalSpeler + 11 <= 21)
            //{
            //    totaalSpeler += 11;
            //}
            //else
            //{
            //    totaalSpeler += 1;
            //}



        }

        private void SldrKapitaal_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(SldrKapitaal.Value >= kapitaalSpeler * 0.1)
            {
                BtnDeel.IsEnabled = true;
            }
            else
            {
                BtnDeel.IsEnabled = false;
            }

            SldrKapitaal.Maximum = kapitaalSpeler;

            TxtInzet.Text = SldrKapitaal.Value.ToString();  
        }
    }
}
