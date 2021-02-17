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
        private bool[] postiLiberi;  //posto libero== true //posto occupato==false
        private delegate void thread1(int i);
        private static object x = new object();
        private static Semaphore semaphore;

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
        }

        void OccupaPostoThread1()
        {
            try
            {
                lock (x)
                {
                    semaphore.WaitOne();

                    int posto1 = int.Parse(txtThread1.Text) - 1;
                    if (posto1 <= 0 || posto1 >= 119)
                        throw new Exception("il posto inserito nella prima casella non esiste");

                    if (postiLiberi[posto1] != true)
                        throw new Exception("posto già occupato . current thread: "+Thread.CurrentThread);

                    postiLiberi[posto1] = false;
                    OccupaPostoGrafica(posto1);

                    semaphore.Release();
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        void OccupaPostoThread2()
        {
            try
            {
                lock (x)
                {
                    semaphore.WaitOne();

                    int posto2 = int.Parse(txtThread2.Text) - 1;
                    if (posto2 <= 0 || posto2 >= 119)
                        throw new Exception("il posto inserito nella seconda casella non esiste");


                    if (postiLiberi[posto2] != true)
                        throw new Exception("posto già occupato . current thread: " + Thread.CurrentThread);

                    postiLiberi[posto2] = false;
                    OccupaPostoGrafica(posto2);

                    semaphore.Release();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        void OccupaPostoGrafica(int i)
        {
            
        }

        private void btnSvuotaCinema_Click(object sender, RoutedEventArgs e)
        {
            SvuotaCinema();
        }

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAssegnaPosto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Thread t1 = new Thread(new ThreadStart(OccupaPostoThread1));
                Thread t2 = new Thread(new ThreadStart(OccupaPostoThread2));
                semaphore = new Semaphore(0, 1);

                t1.Start();
                t2.Start();

                semaphore.Release(1);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
