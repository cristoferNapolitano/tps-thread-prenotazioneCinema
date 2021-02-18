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
using System.Threading;

namespace tps_es_concorrenza_prentotazioneCinema
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static bool[] postiLiberi;  //posto libero== true //posto occupato==false
        private static object x = new object();
        Random rnd = new Random();

        Thread t1 = new Thread(new ParameterizedThreadStart(OccupaPosto));
        Thread t2 = new Thread(new ParameterizedThreadStart(OccupaPosto));


        public MainWindow()
        {
            InitializeComponent();
            SvuotaCinema();

        }

        void SvuotaCinema()
        {
            postiLiberi = new bool[119];
            for(int i = 0; i < postiLiberi.Length; i++)
            {
                postiLiberi[i] = true;
            }
            //ricorda riprinistino dei posti nell'interfaccia grafica
        }
        
        static void OccupaPostoGrafica(int i)
        {
            
        }

        private void btnSvuotaCinema_Click(object sender, RoutedEventArgs e)
        {
            SvuotaCinema();
        }

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            bool finito = false;
            while (finito==false)
            {
                try
                {
                    int f = rnd.Next(120); int g = rnd.Next(120);

                    t1.Start(rnd.Next(f));
                    t2.Start(rnd.Next(g));
                }
                catch(Exception)
                {

                }

                finito = true;
                for (int i = 0; i < postiLiberi.Length; i++)
                {
                    if (postiLiberi[i] == true)
                    {
                        finito = false;
                        break;
                    }
                    
                }
            }
        }

        private void btnAssegnaPosto_Click(object sender, RoutedEventArgs e)  //quando assegno i posti lo faccio 2 alla volta
        {
            int posto1, posto2;
            try
            {
                posto1 = int.Parse(txtThread1.Text);
                posto1--;
                if (posto1 <= 0 || posto1 >= 119)
                    throw new Exception("il posto inserito nella prima casella non esiste");

                posto2 = int.Parse(txtThread2.Text);
                posto2--;
                if (posto2 <= 0 || posto2 >= 119)
                    throw new Exception("il posto inserito nella seconda casella non esiste");

                

                t1.Start(posto1);
                t2.Start(posto2);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        static void OccupaPosto(object i)
        {
            lock (x)
            { 
                try
                {

                    int num = (int)i;


                    if (postiLiberi[num] != true)
                        throw new Exception("posto già occupato");

                    postiLiberi[num] = false;  //occupo il posto
                    OccupaPostoGrafica(num);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }
    }
}
