using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Threading;

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
        List<int> gekozenKaartenDeck = new List<int>();
        
        int hitCount;
        
        int totaalSpeler;
        int totaalBank;

        int gewonnenBedragSpeler;
        int verlorenBedragSpeler;

        bool isSpeler = true;
        Random rnd = new Random();

        int kapitaalSpeler = 100;
        int kapitaalBank = 100;

        Random rndInzetBank = new Random();
       


        DispatcherTimer timer = new DispatcherTimer();

        private int aantalAzen;
       
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GridScherm.Visibility = Visibility.Hidden;

            BtnHit.IsEnabled = false;
            BtnStand.IsEnabled = false;
            BtnDeel.IsEnabled = false;
            SldrKapitaal.IsEnabled = false;
            BtnNieuweSpel.IsEnabled = true;
            LblBankTotaal.Content = "0";
            LblSpelerTotaal.Content = "0";
            TxtInzet.Text = "";
            TxtInzetBank.Text = "";
            TxtKapitaal.Text = "100";
            TxtKapitaalBank.Text = "100";
            kapitaalSpeler = 100;
            kapitaalBank = 100;
            TxtKapitaal.Text = kapitaalSpeler.ToString();
            timer.Tick += new EventHandler(DispatcherTimer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
            BuildArray();

        }

        private void BtnNieuweSpel_Click(object sender, RoutedEventArgs e)
        {
            GridScherm.Visibility = Visibility.Visible;
            if (BtnDeel.IsEnabled == false)
            {
                MessageBox.Show("Bepaal altijd eerst je inzet voor je een ronde start!");
            }
            BtnHit.IsEnabled = false;
            BtnStand.IsEnabled = false;
            BtnDeel.IsEnabled = false;
            SldrKapitaal.IsEnabled = true;
            LstbxSpeler.Items.Clear();
            LstbxBank.Items.Clear();
            LblResultaat.Content = null;
            LblBankTotaal.Content = null;
            LblSpelerTotaal.Content = null;
            totaalSpeler = 0;
            totaalBank = 0;
            LblBankTotaal.Content = "0";
            LblSpelerTotaal.Content = "0";
            TxtInzet.Text = "";
            TxtInzetBank.Text = "";
            TxtKapitaal.Text = "100";
            kapitaalSpeler = 100;
            kapitaalBank = 100;
            SldrKapitaal.Value = 0;
            TxtKapitaalBank.Text = "100";
            TxtInzetBank.Text = null;
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

            BtnHit.IsEnabled = true;
            BtnStand.IsEnabled = true;
            BtnDeel.IsEnabled = false;
            LstbxSpeler.Items.Clear();
            LstbxBank.Items.Clear();
            LblResultaat.Content = "";
            LblBankTotaal.Content = "0";
            LblSpelerTotaal.Content = "0";
           
            TxtInzetBank.Text = "";
            hitCount = 0;
           
            
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
            LblBankTotaal.Content = totaalBank;


        }

        private void BtnHit_Click(object sender, RoutedEventArgs e)
        {
            int bankInzet = rndInzetBank.Next(10, 100);
            TxtInzetBank.Text = bankInzet.ToString();
            TxtKapitaalBank.Text = kapitaalBank.ToString();

            GeefKaart(isSpeler);
            if(hitCount == 0) 
            {
                ImgSpelerKaart3.Source = kaartImages[gekozenKaartenSpeler[2]];
            }
            if(hitCount == 1)
            {
                ImgSpelerKaart4.Source = kaartImages[gekozenKaartenSpeler[3]];
            }
            if(hitCount == 2)
            {
                ImgSpelerKaart5.Source = kaartImages[gekozenKaartenSpeler[4]];
            }
            if(hitCount == 3)
            {
                ImgSpelerKaart6.Source = kaartImages[gekozenKaartenSpeler[5]];
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
                kapitaalBank = kapitaalBank + (bankInzet * 2);
                TxtKapitaalBank.Text = kapitaalBank.ToString();
               
            }

            if (kapitaalSpeler == 0)
            {
                BtnHit.IsEnabled = false;
                BtnStand.IsEnabled = false;
                BtnDeel.IsEnabled = false;
                SldrKapitaal.IsEnabled = false;
               
                MessageBox.Show("Helaas.. Je hebt al je geld verkwist. Volgende keer meer geluk!", "Je geld is op");
                GridScherm.Visibility = Visibility.Hidden;
            }

            if (kapitaalBank <= 0)
            {
                MessageBox.Show("Jij hebt de game gewonnen!", "Start een nieuwe spel!");
                GridScherm.Visibility = Visibility.Hidden;
            }


            



        }
        private void BtnStand_Click(object sender, RoutedEventArgs e)
        {
            gewonnenBedragSpeler = (int.Parse(TxtInzet.Text) * 2);
            verlorenBedragSpeler = int.Parse(TxtInzet.Text);

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

            if (gekozenKaartenBank.Count > 5)
            {
                ImgBankKaart6.Source = kaartImages[gekozenKaartenBank[5]];
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
                
                MessageBox.Show("Helaas.. Je hebt al je geld verkwist. Volgende keer meer geluk", "Je geld is op");
                GridScherm.Visibility = Visibility.Hidden;
            }
            else {
                SldrKapitaal.Value = 0;
            }

            if(kapitaalBank == 0)
            {
                MessageBox.Show("Jij hebt het spel gewonnen!", "Start een nieuwe spel!");
                GridScherm.Visibility = Visibility.Hidden;
            }
            

            LblHistoriek.Content = $"{gewonnenBedragSpeler}- {int.Parse(TxtInzet.Text)*2} / {kapitaalBank}";
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
                gekozenKaartenDeck.Remove(waarde);

            }

            else
            {
                int waarde = rnd.Next(kaartNamen.Length);
                string naam = kaartNamen[waarde];
                LstbxBank.Items.Add(naam);
                gekozenKaartenBank.Add(waarde);
                
                totaalBank += kaartWaarde[waarde];
                gekozenKaartenDeck.Remove(waarde);
            }

        }

        private void BuildArray()
        {
           
        
            kaartNamen = new string[52] { "Schoppen 2", "Schoppen 3", "Schoppen 4", "Schoppen 5", "Schoppen 6", "Schoppen 7", "Schoppen 8", "Schoppen 9", "Schoppen 10", "Schoppen Boer", "Schoppen Dame", "Schoppen Koning", "Schoppen Aas", 
                "Ruiten 2", "Ruiten 3", "Ruiten 4", "Ruiten 5", "Ruiten 6", "Ruiten 7", "Ruiten 8", "Ruiten 9", "Ruiten 10", "Ruiten Boer", "Ruiten Dame", "Ruiten Koning", "Ruiten Aas", 
                "Klaveren 2", "Klaveren 3", "Klaveren 4", "Klaveren 5", "Klaveren 6", "Klaveren 7", "Klaveren 8", "Klaveren 9", "Klaveren 10", "Klaveren Boer", "Klaveren Dame", "Klaveren Koning", "Klaveren Aas", 
                "Harten 2", "Harten 3", "Harten 4", "Harten 5", "Harten 6", "Harten 7", "Harten 8", "Harten 9", "Harten 10", "Harten Boer", "Harten Dame", "Harten Koning", "Harten Aas" };
            kaartWaarde = new int[52] { 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10, 1, 
                2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10, 1 };
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

            GameDeck(gekozenKaartenDeck);
           
        }


        private void GameDeck(List <int> gekozenKaartenDeck)
        {
          

            ImgSchoppen2.Source = kaartImages[0];
            ImgSchoppen3.Source = kaartImages[1];
            ImgSchoppen4.Source = kaartImages[2];
            ImgSchoppen5.Source = kaartImages[3];
            ImgSchoppen6.Source = kaartImages[4];
            ImgSchoppen7.Source = kaartImages[5];
            ImgSchoppen8.Source = kaartImages[6];
            ImgSchoppen9.Source = kaartImages[7];
            ImgSchoppen10.Source = kaartImages[8];
            ImgSchoppenBoer.Source = kaartImages[9];
            ImgSchoppenDame.Source = kaartImages[10];
            ImgSchoppenKoning.Source = kaartImages[11];
            ImgSchoppenAas.Source = kaartImages[12];

            ImgRuiten2.Source = kaartImages[13];
            ImgRuiten3.Source = kaartImages[14];
            ImgRuiten4.Source = kaartImages[15];
            ImgRuiten5.Source = kaartImages[16];
            ImgRuiten6.Source = kaartImages[17];
            ImgRuiten7.Source = kaartImages[18];
            ImgRuiten8.Source = kaartImages[19];
            ImgRuiten9.Source = kaartImages[20];
            ImgRuiten10.Source = kaartImages[21];
            ImgRuitenBoer.Source = kaartImages[22];
            ImgRuitenDame.Source = kaartImages[23];
            ImgRuitenKoning.Source = kaartImages[24];
            ImgRuitenAas.Source = kaartImages[25];

            ImgKlaveren2.Source = kaartImages[26];
            ImgKlaveren3.Source = kaartImages[27];
            ImgKlaveren4.Source = kaartImages[28];
            ImgKlaveren5.Source = kaartImages[29];
            ImgKlaveren6.Source = kaartImages[30];
            ImgKlaveren7.Source = kaartImages[31];
            ImgKlaveren8.Source = kaartImages[32];
            ImgKlaveren9.Source = kaartImages[33];
            ImgKlaveren10.Source = kaartImages[34];
            ImgKlaverenBoer.Source = kaartImages[35];
            ImgKlaverenDame.Source = kaartImages[36];
            ImgKlaverenKoning.Source = kaartImages[37];
            ImgKlaverenAas.Source = kaartImages[38];

            ImgHarten2.Source = kaartImages[39];
            ImgHarten3.Source = kaartImages[40];
            ImgHarten4.Source = kaartImages[41];
            ImgHarten5.Source = kaartImages[42];
            ImgHarten6.Source = kaartImages[43];
            ImgHarten7.Source = kaartImages[44];
            ImgHarten8.Source = kaartImages[45];
            ImgHarten9.Source = kaartImages[46];
            ImgHarten10.Source = kaartImages[47];
            ImgHartenBoer.Source = kaartImages[48];
            ImgHartenDame.Source = kaartImages[49];
            ImgHartenKoning.Source = kaartImages[50];
            ImgHartenAas.Source = kaartImages[51];

        

    }
      
        private void Gamestatus()
        {
            
            int bankInzet = rndInzetBank.Next(1, 100);
            TxtInzetBank.Text = bankInzet.ToString();
            TxtKapitaalBank.Text = kapitaalBank.ToString();

            

            if (kaartNamen.Contains("SchoppenAas") && kaartNamen.Contains("HartenAas") && kaartNamen.Contains("RuitenAas") && kaartNamen.Contains("KlaverenAas"))
            {
                if (totaalSpeler <= 10)
                {
                    totaalSpeler += 11;
                }
                else
                {
                    totaalSpeler += 1;
                }
                
                if (totaalBank <= 10)
                {
                    totaalBank += 11;
                }
                else
                {
                    totaalBank += 1;
                }
            }
          

            if (totaalSpeler > 21)
            {
                LblResultaat.Content = "VERLOREN";
                LblResultaat.Foreground = new SolidColorBrush(Colors.Red);
                kapitaalSpeler = int.Parse(TxtKapitaal.Text) - int.Parse(TxtInzet.Text);
                TxtKapitaal.Text = kapitaalSpeler.ToString();
                kapitaalBank = kapitaalBank + (bankInzet * 2);
                TxtKapitaalBank.Text = kapitaalBank.ToString();

                return;
            }

            if (totaalSpeler == totaalBank)
            {
                LblResultaat.Content = "PUSH";
                LblResultaat.Foreground = new SolidColorBrush(Colors.Black);
              
               
                return;
            }

            if (totaalBank > 21)
            {
                LblResultaat.Content = "GEWONNEN";
                LblResultaat.Foreground = new SolidColorBrush(Colors.Green);
                kapitaalSpeler = int.Parse(TxtKapitaal.Text) + (int.Parse(TxtInzet.Text)*2);
                
                TxtKapitaal.Text = kapitaalSpeler.ToString();
                kapitaalBank = kapitaalBank - bankInzet;
                TxtKapitaalBank.Text = kapitaalBank.ToString();

                return;
            }

            if (totaalSpeler < totaalBank)
            {
                LblResultaat.Content = "VERLOREN";
                LblResultaat.Foreground = new SolidColorBrush(Colors.Red);
                kapitaalSpeler = int.Parse(TxtKapitaal.Text) - int.Parse(TxtInzet.Text);
                TxtKapitaal.Text = kapitaalSpeler.ToString();
                kapitaalBank = kapitaalBank + (bankInzet*2);
                TxtKapitaalBank.Text = kapitaalBank.ToString();
                return;
            }

            if (totaalSpeler > totaalBank)
            {
                LblResultaat.Content = "GEWONNEN";
                LblResultaat.Foreground = new SolidColorBrush(Colors.Green);
                kapitaalSpeler = int.Parse(TxtKapitaal.Text) + (int.Parse(TxtInzet.Text) * 2);
                TxtKapitaal.Text = kapitaalSpeler.ToString();
                kapitaalBank = kapitaalBank - bankInzet;
                TxtKapitaalBank.Text = kapitaalBank.ToString();
             
            }

          



        }

        private void SldrKapitaal_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
           

            if (SldrKapitaal.Value >= kapitaalSpeler * 0.1)
            {
                BtnDeel.IsEnabled = true;
            }
            else
            {
                BtnDeel.IsEnabled = false;
            }

            SldrKapitaal.Maximum = kapitaalSpeler;

            TxtInzet.Text = SldrKapitaal.Value.ToString();

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

        private void Historiek()
        {

        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            LblTijdstip.Content = $"{DateTime.Now.ToLongTimeString()}";
            
        }

        private void LblHistoriek_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            StringBuilder historiek = new StringBuilder();
            int j = 10, i = 0;
            for (; i < 10; i++)
            {
                j += i;
                historiek.AppendLine($"punt: ");
            }
        }

      
    }
}
