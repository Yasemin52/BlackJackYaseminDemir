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

      
        string[] kaartNamen;
        int totaalSpeler;
        int totaalBank;
        bool isSpeler = true;
        Random rnd = new Random();
        int[] kaartWaarde;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BtnHit.IsEnabled = false; 
            BtnStand.IsEnabled = false;
            BtnDeel.IsEnabled = true;
            LblBankTotaal.Content = "0";
            LblSpelerTotaal.Content = "0";
            BuildArray();
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
            totaalSpeler = 0;
            totaalBank = 0;
            
            for (int i = 0; i < 2; i++)
            {
                GeefKaart(isSpeler);
             
            }
            GeefKaart(!isSpeler);
            LblSpelerTotaal.Content = totaalSpeler;
            
        }

       
        private void GeefKaart(bool isSpeler)
        {
            
            
            if(isSpeler)
            {
                    int waarde = rnd.Next(kaartNamen.Length);
                    string naam = kaartNamen[waarde];
                    LstbxSpeler.Items.Add(naam);
                    totaalSpeler += kaartWaarde[waarde];
               
            }

            else
            {
                    int waarde = rnd.Next(kaartNamen.Length);
                    string naam = kaartNamen[waarde];
                    LstbxBank.Items.Add(naam);
                    totaalBank += kaartWaarde[waarde];
               
            }
           
        }

        private void BuildArray()
        {

            kaartNamen = new string[52] { "Schoppen 2", "Schoppen 3", "Schoppen 4", "Schoppen 5", "Schoppen 6", "Schoppen 7", "Schoppen 8", "Schoppen 9", "Schoppen 10", "Schoppen Boer", "Schoppen Dame", "Schoppen Koning", "Schoppen Aas", "Ruiten 2", "Ruiten 3", "Ruiten 4", "Ruiten 5", "Ruiten 6", "Ruiten 7", "Ruiten 8", "Ruiten 9", "Ruiten 10", "Ruiten Boer", "Ruiten Dame", "Ruiten Koning", "Ruiten Aas", "Klaveren 2", "Klaveren 3", "Klaveren 4", "Klaveren 5", "Klaveren 6", "Klaveren 7", "Klaveren 8", "Klaveren 9", "Klaveren 10", "Klaveren Boer", "Klaveren Dame", "Klaveren Koning", "Klaveren Aas", "Harten 2", "Harten 3", "Harten 4", "Harten 5", "Harten 6", "Harten 7", "Harten 8", "Harten 9", "Harten 10", "Harten Boer", "Harten Dame", "Harten Koning", "Harten Aas" };
            kaartWaarde = new int[52] { 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10, 1 };

        }

    

        private void BtnHit_Click(object sender, RoutedEventArgs e)
        {

            GeefKaart(isSpeler);
            LblSpelerTotaal.Content = totaalSpeler;
            
            if (totaalSpeler > 21)
            {
                LblResultaat.Content = "VERLOREN";
                LblResultaat.Foreground = new SolidColorBrush(Colors.Red);
              
                BtnDeel.IsEnabled = true;
                BtnHit.IsEnabled = false;
                BtnStand.IsEnabled = false;
            }

          
        }


        private void BtnStand_Click(object sender, RoutedEventArgs e)
        {
           
            do
            {
                GeefKaart(!isSpeler);
            }
            while (totaalBank <= 17);

            LblBankTotaal.Content= totaalBank;
            
            Gamestatus();

            BtnHit.IsEnabled = false;
            BtnStand.IsEnabled = false;
            BtnDeel.IsEnabled = true;
        }

        private void Gamestatus()
        {
         
            if (totaalSpeler > 21)
            {
                LblResultaat.Content = "VERLOREN";
                LblResultaat.Foreground = new SolidColorBrush(Colors.Red);
            
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
              
                return;
            }

            if (totaalSpeler < totaalBank)
            {
                LblResultaat.Content = "VERLOREN";
                LblResultaat.Foreground = new SolidColorBrush(Colors.Red);
              
                return;
            }

            if (totaalSpeler > totaalBank)
            {
                LblResultaat.Content = "GEWONNEN";
                LblResultaat.Foreground = new SolidColorBrush(Colors.Green);
               
            }

          
        }
    }
}
