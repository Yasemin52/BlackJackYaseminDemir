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
        BitmapImage[] kaartImagesDeck;

        /// <summary>
        /// gekozen kaarten worden voor de bank en speler in een list opgeslaan?
        /// </summary>
        List<int> gekozenKaartenSpeler = new List<int>();
        List<int> gekozenKaartenBank = new List<int>();
        
        /// <summary>
        /// per klik wordt het aantal opgeslaan?
        /// </summary>
        int hitCount;

        /// <summary>
        /// elk verloren of gewonnen ronde wordt bijgehouden (deel, hit/stand, einde ronde)
        /// </summary>
        int ronde;
        
        /// <summary>
        /// de totale scores worden bijgehouden
        /// </summary>
        int totaalSpeler;
        int totaalBank;

        /// <summary>
        /// verloren of gewonnen bedragen worden bijgehouden
        /// </summary>
        int gewonnenBedragSpeler;
        int verlorenBedragSpeler;

        /// <summary>
        /// ?
        /// </summary>
        bool isSpeler = true;

        Random rnd = new Random();

        int kapitaalSpeler = 100;
        int kapitaalBank = 100;

      
        int bankInzet;
        int spelerInzet;

        Random rndInzetBank = new Random();

        StringBuilder historiek = new StringBuilder();

        DispatcherTimer timer = new DispatcherTimer();


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GridScherm.Visibility = Visibility.Hidden;

            BtnDeel.IsEnabled = false;
            BtnHit.IsEnabled = false;
            BtnNieuweSpel.IsEnabled = true;
            BtnStand.IsEnabled = false;
            BtnDoubleDown.IsEnabled = false;
            kapitaalBank = 100;
            kapitaalSpeler = 100;
            LblBankTotaal.Content = "0";
            LblSpelerTotaal.Content = "0";
            ronde = 0;
            SldrKapitaal.IsEnabled = false;
            TxtInzet.Text = "";
            TxtInzetBank.Text = "";
            TxtKapitaal.Text = "100";
            TxtKapitaal.Text = kapitaalSpeler.ToString();
            TxtKapitaalBank.Text = "100";

            timer.Tick += new EventHandler(DispatcherTimer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
            BuildArray();

        }

        private void BtnNieuweSpel_Click(object sender, RoutedEventArgs e)
        {
            GridScherm.Visibility = Visibility.Visible;

            LblHistoriek.Content = null;
            historiek.Clear();
            BtnDeel.IsEnabled = false;
            BtnHit.IsEnabled = false;
            BtnDoubleDown.IsEnabled = false;
            BtnStand.IsEnabled = false;
            gekozenKaartenBank.Clear();
            gekozenKaartenSpeler.Clear();
            hitCount = 0;
            kapitaalBank = 100;
            kapitaalSpeler = 100;
            LblBankTotaal.Content = "0";
            LblResultaat.Content = null;
            LblSpelerTotaal.Content = "0";
            LstbxBank.Items.Clear();
            LstbxSpeler.Items.Clear();
            ronde = 0;
            SldrKapitaal.IsEnabled = true;
            SldrKapitaal.Value = 0;
            totaalBank = 0;
            totaalSpeler = 0;
            TxtInzet.Text = "";
            TxtInzetBank.Text = "";
            TxtInzetBank.Text = null;
            TxtKapitaal.Text = "100";
            TxtKapitaalBank.Text = "100";


            ImgKaartNull();

            if (BtnDeel.IsEnabled == false)
            {
                MessageBox.Show("Bepaal altijd eerst je inzet voor je een ronde start!");
            }
        }
        private void BtnDeel_Click(object sender, RoutedEventArgs e)
        {
            ImgKaartNull();

            gekozenKaartenSpeler.Clear();
            gekozenKaartenBank.Clear();
            BtnHit.IsEnabled = true;
            BtnStand.IsEnabled = true;
            BtnDoubleDown.IsEnabled = true;
            BtnDeel.IsEnabled = false;
            LstbxSpeler.Items.Clear();
            LstbxBank.Items.Clear();
            LblResultaat.Content = "";
            LblBankTotaal.Content = "0";
            LblSpelerTotaal.Content = "0";
            ronde += 1;
           
            TxtInzetBank.Text = "";
            hitCount = 0;
         
            SldrKapitaal.IsEnabled = false;
           
            totaalSpeler = 0;
            totaalBank = 0;

            bankInzet = rndInzetBank.Next(1, TxtKapitaalBank.Text.Length);
            spelerInzet = int.Parse(TxtInzet.Text);
            TxtInzetBank.Text = bankInzet.ToString();
            TxtKapitaalBank.Text = kapitaalBank.ToString();

            gewonnenBedragSpeler = (spelerInzet * 2);
            verlorenBedragSpeler = spelerInzet;

            /// Hier krijgt de speler 2 kaarten
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
            /// hier worden de gekozen kaarten getoond indien er geklikt (hitcount) wordt.
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
                BuildArray();
                GameDeck();
                LblResultaat.Content = "VERLOREN";
                LblResultaat.Foreground = new SolidColorBrush(Colors.Red);
                kapitaalSpeler = int.Parse(TxtKapitaal.Text) - spelerInzet;
                TxtKapitaal.Text = kapitaalSpeler.ToString();
                SldrKapitaal.IsEnabled = true;
                SldrKapitaal.Value = 0;
                BtnDeel.IsEnabled = false;
                BtnHit.IsEnabled = false;
                BtnStand.IsEnabled = false;
                kapitaalBank = kapitaalBank + (bankInzet * 2);
                TxtKapitaalBank.Text = kapitaalBank.ToString();
                historiek.Insert(0, $"Ronde{ronde}: je verloren bedrag: {verlorenBedragSpeler} – De som van je kapitaal is: {kapitaalSpeler} / de som van het kapitaal van de bank is: {kapitaalBank}\n");

            }

            /// wanneer je kapitaal lager is dan 0 kan je niet verder spelen. Wanneer de kapitaal van de bank lager is dan 0 moet je een nieuwe spel starten.

            if (kapitaalSpeler == 0)
            {
                BuildArray();
                GameDeck();
                BtnHit.IsEnabled = false;
                BtnStand.IsEnabled = false;
                BtnDeel.IsEnabled = false;
                SldrKapitaal.IsEnabled = false;
               
                MessageBox.Show("Helaas.. Je hebt al je geld verkwist. Volgende keer meer geluk!", "Je geld is op");
                GridScherm.Visibility = Visibility.Hidden;
            }

            if (kapitaalBank == 0)
            {
                BuildArray();
                GameDeck();
                BtnHit.IsEnabled = false;
                BtnStand.IsEnabled = false;
                BtnDeel.IsEnabled = false;
                SldrKapitaal.IsEnabled = false;

                MessageBox.Show("Jij hebt de game gewonnen!", "Start een nieuwe spel!");
                GridScherm.Visibility = Visibility.Hidden;
            }

            LblHistoriek.Content = $"De som van je kapitaal is: {kapitaalSpeler} / de som van het kapitaal van de bank is: {kapitaalBank}\n";
        }

        private void BtnStand_Click(object sender, RoutedEventArgs e)
        {
            gewonnenBedragSpeler = (spelerInzet * 2);
            verlorenBedragSpeler = spelerInzet;

            /// de bank krijgt hier kaarten random uitgedeeld, de score mag niet lager of gelijk aan 17 zijn
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
            BtnDoubleDown.IsEnabled = false;
            SldrKapitaal.IsEnabled = true;
           

            if (kapitaalSpeler == 0)
            {
                BtnHit.IsEnabled = false;
                BtnStand.IsEnabled = false;
                BtnDeel.IsEnabled = false;
                BtnDoubleDown.IsEnabled = false;
                SldrKapitaal.IsEnabled = false;
                
                MessageBox.Show("Helaas.. Je hebt al je geld verkwist. Volgende keer meer geluk", "Je geld is op");
                GridScherm.Visibility = Visibility.Hidden;
            }
            else {
                SldrKapitaal.Value = 0;
            }

            if (kapitaalBank == 0)
            {
                BtnHit.IsEnabled = false;
                BtnStand.IsEnabled = false;
                BtnDeel.IsEnabled = false;
                BtnDoubleDown.IsEnabled = false;
                SldrKapitaal.IsEnabled = false;

                MessageBox.Show("Jij hebt het spel gewonnen!", "Start een nieuwe spel!");
                GridScherm.Visibility = Visibility.Hidden;
            }

            LblHistoriek.Content = $" De som van je kapitaal is: {kapitaalSpeler} / De som van het kapitaal van de bank is: {kapitaalBank}\n";

        }

        private void BtnDoubleDown_Click(object sender, RoutedEventArgs e)
        {

            BtnDeel.IsEnabled = false;
            BtnHit.IsEnabled = false;
            BtnStand.IsEnabled = false;

            SldrKapitaal.IsEnabled = true;

            /// als de kapitaal lager is dan de inzet *2 dan krijgt de speler een messagebox te zien

            if ((spelerInzet * 2) > (int.Parse(TxtKapitaal.Text)))
            {
                MessageBox.Show("Je kapitaal is te laag!");
            }


                int inzetDouble = (spelerInzet * 2);
                TxtInzet.Text = inzetDouble.ToString();

                GeefKaart(isSpeler);
                do
                {
                    GeefKaart(!isSpeler);

                }
                while (totaalBank <= 17);

                ImgBankKaart2.Source = kaartImages[gekozenKaartenBank[1]];


                if (gekozenKaartenBank.Count > 2)
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

                if (totaalSpeler > 21)
                {
                    BuildArray();
                    GameDeck();
                    LblResultaat.Content = "VERLOREN";
                    LblResultaat.Foreground = new SolidColorBrush(Colors.Red);
                    kapitaalSpeler = int.Parse(TxtKapitaal.Text) - inzetDouble;
                    TxtKapitaal.Text = kapitaalSpeler.ToString();
                    kapitaalBank = kapitaalBank + ((bankInzet * 2)*2);
                    TxtKapitaalBank.Text = kapitaalBank.ToString();
                    historiek.Insert(0, $"Ronde{ronde}: je verloren bedrag: {verlorenBedragSpeler} – De som van je kapitaal is: {kapitaalSpeler} / de som van het kapitaal van de bank is: {kapitaalBank}\n");
                    return;
                }

                if (totaalSpeler == totaalBank)
                {
                    BuildArray();
                    GameDeck();
                    LblResultaat.Content = "PUSH";
                    LblResultaat.Foreground = new SolidColorBrush(Colors.Black);

                    return;
                }

                if (totaalBank > 21)
                {
                    BuildArray();
                    GameDeck();
                    LblResultaat.Content = "GEWONNEN";
                    LblResultaat.Foreground = new SolidColorBrush(Colors.Green);
                    kapitaalSpeler = int.Parse(TxtKapitaal.Text) + (inzetDouble * 2);
                    TxtKapitaal.Text = kapitaalSpeler.ToString();
                    kapitaalBank = kapitaalBank - (bankInzet*2);
                    TxtKapitaalBank.Text = kapitaalBank.ToString();
                    historiek.Insert(0, $"Ronde{ronde}: je gewonnen bedrag: {gewonnenBedragSpeler} – De som van je kapitaal is: {kapitaalSpeler} / de som van het kapitaal van de bank is: {kapitaalBank}\n");
                    return;
                }

                if (totaalSpeler < totaalBank)
                {
                    BuildArray();
                    GameDeck();
                    LblResultaat.Content = "VERLOREN";
                    LblResultaat.Foreground = new SolidColorBrush(Colors.Red);
                    kapitaalSpeler = int.Parse(TxtKapitaal.Text) - inzetDouble;
                    TxtKapitaal.Text = kapitaalSpeler.ToString();
                    kapitaalBank = kapitaalBank + ((bankInzet * 2)*2);
                    TxtKapitaalBank.Text = kapitaalBank.ToString();
                    historiek.Insert(0, $"Ronde{ronde}: je verloren bedrag: {verlorenBedragSpeler} – De som van je kapitaal is: {kapitaalSpeler} / de som van het kapitaal van de bank is: {kapitaalBank}\n");
                    return;
                }

                if (totaalSpeler > totaalBank)
                {
                    BuildArray();
                    GameDeck();
                    LblResultaat.Content = "GEWONNEN";
                    LblResultaat.Foreground = new SolidColorBrush(Colors.Green);
                    kapitaalSpeler = int.Parse(TxtKapitaal.Text) + (inzetDouble * 2);
                    TxtKapitaal.Text = kapitaalSpeler.ToString();
                    kapitaalBank = kapitaalBank - (bankInzet*2);
                    TxtKapitaalBank.Text = kapitaalBank.ToString();
                    historiek.Insert(0, $"Ronde{ronde}: je gewonnen bedrag: {gewonnenBedragSpeler} – De som van je kapitaal is: {kapitaalSpeler} / de som van het kapitaal van de bank is: {kapitaalBank}\n");

                }
                LblHistoriek.Content = $"De som van je kapitaal is: {kapitaalSpeler} / de som van het kapitaal van de bank is: {kapitaalBank}\n";


                if (kapitaalSpeler == 0)
                {
                    BtnHit.IsEnabled = false;
                    BtnStand.IsEnabled = false;
                    BtnDeel.IsEnabled = false;
                    SldrKapitaal.IsEnabled = false;

                    MessageBox.Show("Helaas.. Je hebt al je geld verkwist. Volgende keer meer geluk", "Je geld is op");
                    GridScherm.Visibility = Visibility.Hidden;
                }
                else
                {
                    SldrKapitaal.Value = 0;
                }

                if (kapitaalBank == 0)
                {
                    BtnHit.IsEnabled = false;
                    BtnStand.IsEnabled = false;
                    BtnDeel.IsEnabled = false;
                    SldrKapitaal.IsEnabled = false;

                    MessageBox.Show("Jij hebt het spel gewonnen!", "Start een nieuwe spel!");
                    GridScherm.Visibility = Visibility.Hidden;
                }

            
        }
        /// <summary>
        /// Bij deze methode wordt er random kaarten gekozen voor de speler en bank van de array kaartNamen.
        /// </summary>
        /// <param name="isSpeler">naam van de parameter, speler (isSpeler) of bank (!isSpeler) </param>
        private void GeefKaart(bool isSpeler)
        {
            int waarde = rnd.Next(kaartNamen.Length);
            string naam = kaartNamen[waarde];

            if (isSpeler)
            {
                LstbxSpeler.Items.Add(naam);
                gekozenKaartenSpeler.Add(waarde);

                if (naam.Contains("Schoppen Aas") || naam.Contains("Harten Aas") || naam.Contains("Ruiten Aas") || naam.Contains("Klaveren Aas"))
                {
                    if (totaalSpeler <= 10)
                    {
                        totaalSpeler += 11;
                    }
                    else
                    {
                        totaalSpeler += 1;
                    }
                } else
                {
                    totaalSpeler += kaartWaarde[waarde];
                }

     
         
                kaartImagesDeck[waarde] = null;
                GameDeck();
            }

            else
            {
                LstbxBank.Items.Add(naam);
                gekozenKaartenBank.Add(waarde);

                if (naam.Contains("Schoppen Aas") || naam.Contains("Harten Aas") || naam.Contains("Ruiten Aas") || naam.Contains("Klaveren Aas"))
                {
                    if (totaalBank <= 10)
                    {
                        totaalBank += 11;
                    }
                    else
                    {
                        totaalBank += 1;
                    }
                } else {
                    totaalBank += kaartWaarde[waarde];
                }
                
                kaartImagesDeck[waarde] = null;
                GameDeck();
            }

        }

        /// <summary>
        /// Hier worden de gegevens in Array opgeslaan. Namen van de kaarten, de waardes van de kaarten en ook de afbeeldingen.
        /// </summary>
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
            new BitmapImage(new Uri(@"\CardImages\Schoppen2.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Schoppen3.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Schoppen4.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Schoppen5.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Schoppen6.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Schoppen7.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Schoppen8.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Schoppen9.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Schoppen10.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\SchoppenBoer.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\SchoppenDame.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\SchoppenKoning.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\SchoppenAas.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Ruiten2.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Ruiten3.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Ruiten4.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Ruiten5.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Ruiten6.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Ruiten7.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Ruiten8.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Ruiten9.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Ruiten10.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\RuitenBoer.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\RuitenDame.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\RuitenKoning.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\RuitenAas.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Klaveren2.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Klaveren3.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Klaveren4.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Klaveren5.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Klaveren6.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Klaveren7.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Klaveren8.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Klaveren9.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Klaveren10.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\KlaverenBoer.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\KlaverenDame.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\KlaverenKoning.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\KlaverenAas.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Harten2.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Harten3.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Harten4.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Harten5.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Harten6.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Harten7.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Harten8.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Harten9.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Harten10.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\HartenBoer.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\HartenDame.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\HartenKoning.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\HartenAas.png", UriKind.Relative)),

        };
            kaartImagesDeck = new BitmapImage[]
                {
            new BitmapImage(new Uri(@"\CardImages\Schoppen2.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Schoppen3.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Schoppen4.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Schoppen5.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Schoppen6.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Schoppen7.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Schoppen8.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Schoppen9.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Schoppen10.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\SchoppenBoer.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\SchoppenDame.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\SchoppenKoning.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\SchoppenAas.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Ruiten2.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Ruiten3.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Ruiten4.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Ruiten5.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Ruiten6.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Ruiten7.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Ruiten8.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Ruiten9.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Ruiten10.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\RuitenBoer.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\RuitenDame.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\RuitenKoning.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\RuitenAas.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Klaveren2.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Klaveren3.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Klaveren4.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Klaveren5.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Klaveren6.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Klaveren7.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Klaveren8.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Klaveren9.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Klaveren10.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\KlaverenBoer.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\KlaverenDame.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\KlaverenKoning.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\KlaverenAas.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Harten2.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Harten3.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Harten4.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Harten5.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Harten6.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Harten7.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Harten8.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Harten9.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\Harten10.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\HartenBoer.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\HartenDame.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\HartenKoning.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\CardImages\HartenAas.png", UriKind.Relative)),

            };
            GameDeck();
        }


        /// <summary>
        /// Hier zijn alle afbeeldingen van de deck die worden weergegeven op het scherm.
        /// </summary>
        private void GameDeck()
        {
            ImgSchoppen2.Source = kaartImagesDeck[0];
            ImgSchoppen3.Source = kaartImagesDeck[1];
            ImgSchoppen4.Source = kaartImagesDeck[2];
            ImgSchoppen5.Source = kaartImagesDeck[3];
            ImgSchoppen6.Source = kaartImagesDeck[4];
            ImgSchoppen7.Source = kaartImagesDeck[5];
            ImgSchoppen8.Source = kaartImagesDeck[6];
            ImgSchoppen9.Source = kaartImagesDeck[7];
            ImgSchoppen10.Source = kaartImagesDeck[8];
            ImgSchoppenBoer.Source = kaartImagesDeck[9];
            ImgSchoppenDame.Source = kaartImagesDeck[10];
            ImgSchoppenKoning.Source = kaartImagesDeck[11];
            ImgSchoppenAas.Source = kaartImagesDeck[12];

            ImgRuiten2.Source = kaartImagesDeck[13];
            ImgRuiten3.Source = kaartImagesDeck[14];
            ImgRuiten4.Source = kaartImagesDeck[15];
            ImgRuiten5.Source = kaartImagesDeck[16];
            ImgRuiten6.Source = kaartImagesDeck[17];
            ImgRuiten7.Source = kaartImagesDeck[18];
            ImgRuiten8.Source = kaartImagesDeck[19];
            ImgRuiten9.Source = kaartImagesDeck[20];
            ImgRuiten10.Source = kaartImagesDeck[21];
            ImgRuitenBoer.Source = kaartImagesDeck[22];
            ImgRuitenDame.Source = kaartImagesDeck[23];
            ImgRuitenKoning.Source = kaartImagesDeck[24];
            ImgRuitenAas.Source = kaartImagesDeck[25];

            ImgKlaveren2.Source = kaartImagesDeck[26];
            ImgKlaveren3.Source = kaartImagesDeck[27];
            ImgKlaveren4.Source = kaartImagesDeck[28];
            ImgKlaveren5.Source = kaartImagesDeck[29];
            ImgKlaveren6.Source = kaartImagesDeck[30];
            ImgKlaveren7.Source = kaartImagesDeck[31];
            ImgKlaveren8.Source = kaartImagesDeck[32];
            ImgKlaveren9.Source = kaartImagesDeck[33];
            ImgKlaveren10.Source = kaartImagesDeck[34];
            ImgKlaverenBoer.Source = kaartImagesDeck[35];
            ImgKlaverenDame.Source = kaartImagesDeck[36];
            ImgKlaverenKoning.Source = kaartImagesDeck[37];
            ImgKlaverenAas.Source = kaartImagesDeck[38];

            ImgHarten2.Source = kaartImagesDeck[39];
            ImgHarten3.Source = kaartImagesDeck[40];
            ImgHarten4.Source = kaartImagesDeck[41];
            ImgHarten5.Source = kaartImagesDeck[42];
            ImgHarten6.Source = kaartImagesDeck[43];
            ImgHarten7.Source = kaartImagesDeck[44];
            ImgHarten8.Source = kaartImagesDeck[45];
            ImgHarten9.Source = kaartImagesDeck[46];
            ImgHarten10.Source = kaartImagesDeck[47];
            ImgHartenBoer.Source = kaartImagesDeck[48];
            ImgHartenDame.Source = kaartImagesDeck[49];
            ImgHartenKoning.Source = kaartImagesDeck[50];
            ImgHartenAas.Source = kaartImagesDeck[51];
        }


        /// <summary>
        /// Hier wordt er gekeken of de speler of bank heeft gewonnen of verloren of gelijkspel hebben. Bij bijvoorbeeld verloren, verliest de speler.
        /// Inzet wordt afgetrokken van de kapitaal. Bank krijgt zijn inzet dubbel terug. Ook wordt het verloren of gewonnenbedrag getoond in de label.
        /// </summary>
        private void Gamestatus()
        {
            if (totaalSpeler > 21)
            {
                BuildArray();
                GameDeck();
                LblResultaat.Content = "VERLOREN";
                LblResultaat.Foreground = new SolidColorBrush(Colors.Red);
                kapitaalSpeler = int.Parse(TxtKapitaal.Text) - spelerInzet;
                TxtKapitaal.Text = kapitaalSpeler.ToString();
                kapitaalBank = kapitaalBank + (bankInzet * 2);
                TxtKapitaalBank.Text = kapitaalBank.ToString();
                historiek.Insert(0, $"Ronde{ronde}: je verloren bedrag: {verlorenBedragSpeler} – De som van je kapitaal is: {kapitaalSpeler} / de som van het kapitaal van de bank is: {kapitaalBank}\n");
                return;
            }

            if (totaalSpeler == totaalBank)
            {
                BuildArray();
                GameDeck();
                LblResultaat.Content = "PUSH";
                LblResultaat.Foreground = new SolidColorBrush(Colors.Black);
               

                return;
            }

            if (totaalBank > 21)
            {
                BuildArray();
                GameDeck();
                LblResultaat.Content = "GEWONNEN";
                LblResultaat.Foreground = new SolidColorBrush(Colors.Green);
                kapitaalSpeler = int.Parse(TxtKapitaal.Text) + (spelerInzet * 2);
                TxtKapitaal.Text = kapitaalSpeler.ToString();
                kapitaalBank = kapitaalBank - bankInzet;
                TxtKapitaalBank.Text = kapitaalBank.ToString();
                historiek.Insert(0, $"Ronde{ronde}: je gewonnen bedrag: {gewonnenBedragSpeler} – De som van je kapitaal is: {kapitaalSpeler} / de som van het kapitaal van de bank is: {kapitaalBank}\n");

                return;
            }

            if (totaalSpeler < totaalBank)
            {
                BuildArray();
                GameDeck();
                LblResultaat.Content = "VERLOREN";
                LblResultaat.Foreground = new SolidColorBrush(Colors.Red);
                kapitaalSpeler = int.Parse(TxtKapitaal.Text) - spelerInzet;
                TxtKapitaal.Text = kapitaalSpeler.ToString();
                kapitaalBank = kapitaalBank + (bankInzet * 2);
                TxtKapitaalBank.Text = kapitaalBank.ToString();
                historiek.Insert(0, $"Ronde{ronde}: je verloren bedrag: {verlorenBedragSpeler} – De som van je kapitaal is: {kapitaalSpeler} / de som van het kapitaal van de bank is: {kapitaalBank}\n");
                return;
            }

            if (totaalSpeler > totaalBank)
            {
                BuildArray();
                GameDeck();
                LblResultaat.Content = "GEWONNEN";
                LblResultaat.Foreground = new SolidColorBrush(Colors.Green);
                kapitaalSpeler = int.Parse(TxtKapitaal.Text) + (spelerInzet * 2);
                TxtKapitaal.Text = kapitaalSpeler.ToString();
                kapitaalBank = kapitaalBank - bankInzet;
                TxtKapitaalBank.Text = kapitaalBank.ToString();
                historiek.Insert(0, $"Ronde{ronde}: je gewonnen bedrag: {gewonnenBedragSpeler} – De som van je kapitaal is: {kapitaalSpeler} / de som van het kapitaal van de bank is: {kapitaalBank}\n");

            }

        }

        /// <summary>
        /// Hier wordt er gekeken of de speler 10% van de kapitaal heeft ingezet.
        /// </summary>
  
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

            ImgKaartNull();
        }

        /// <summary>
        /// Code werd te vaak herhaald waardor ik deze in een methode heb geplaatst.
        /// </summary>
        private void ImgKaartNull()
        {
            ImgSpelerKaart1.Source = null;
            ImgSpelerKaart2.Source = null;
            ImgSpelerKaart3.Source = null;
            ImgSpelerKaart4.Source = null;
            ImgSpelerKaart5.Source = null;
            ImgSpelerKaart6.Source = null;
            ImgBankKaart1.Source = null;
            ImgBankKaart2.Source = null;
            ImgBankKaart3.Source = null;
            ImgBankKaart4.Source = null;
            ImgBankKaart5.Source = null;
            ImgBankKaart6.Source = null;
        }

        /// <summary>
        /// Hier wordt de tijd getoond in een label.
        /// </summary>
      
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            LblTijdstip.Content = $"{DateTime.Now.ToLongTimeString()}";
            
        }

        /// <summary>
        /// Hier wordt er een overzicht getoond van de gespeelde rondes wanneer je dubbel klikt op de label waar de historiek verschijnt
        /// </summary>
 
        private void LblHistoriek_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string historiekOverview = historiek.ToString();
            MessageBox.Show(historiekOverview);
        }

        

        

    }
}
