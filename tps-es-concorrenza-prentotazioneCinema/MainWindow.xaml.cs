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
        private object x = new object();
        Random rnd = new Random();
        Thread t1, t2;
        static int postoAiuto;

        private delegate void EseguiOccupaPostoGrafica();

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
            //riprinistino dei posti nell'interfaccia grafica
            s1.Opacity = 1;s2.Opacity = 1; s3.Opacity = 1; s4.Opacity = 1;s5.Opacity = 1; s6.Opacity = 100; s7.Opacity = 100; s8.Opacity = 100; s9.Opacity = 100; s10.Opacity = 100;
            s11.Opacity = 100; s12.Opacity = 100; s13.Opacity = 100; s14.Opacity = 100; s15.Opacity = 100; s16.Opacity = 100; s17.Opacity = 100; s18.Opacity = 100; s19.Opacity = 100; s20.Opacity = 100;
            s21.Opacity = 100; s22.Opacity = 100; s23.Opacity = 100; s24.Opacity = 100; s25.Opacity = 100; s26.Opacity = 100; s27.Opacity = 100; s28.Opacity = 100; s29.Opacity = 100; s30.Opacity = 100;
            s31.Opacity = 100; s32.Opacity = 100; s33.Opacity = 100; s34.Opacity = 100; s35.Opacity = 100; s36.Opacity = 100; s37.Opacity = 100; s38.Opacity = 100; s39.Opacity = 100; s40.Opacity = 100;
            s41.Opacity = 100; s42.Opacity = 100; s43.Opacity = 100; s44.Opacity = 100; s45.Opacity = 100; s46.Opacity = 100; s47.Opacity = 100; s48.Opacity = 100; s49.Opacity = 100; s50.Opacity = 100;
            s51.Opacity = 100; s52.Opacity = 100; s53.Opacity = 100; s54.Opacity = 100; s55.Opacity = 100; s56.Opacity = 100; s57.Opacity = 100; s58.Opacity = 100; s59.Opacity = 100; s60.Opacity = 100;
            s61.Opacity = 100; s62.Opacity = 100; s63.Opacity = 100; s64.Opacity = 100; s65.Opacity = 100; s66.Opacity = 100; s67.Opacity = 100; s68.Opacity = 100; s69.Opacity = 100; s70.Opacity = 100;
            s71.Opacity = 100; s72.Opacity = 100; s73.Opacity = 100; s74.Opacity = 100; s75.Opacity = 100; s76.Opacity = 100; s77.Opacity = 100; s78.Opacity = 100; s79.Opacity = 100; s80.Opacity = 100;
            s81.Opacity = 100; s82.Opacity = 100; s83.Opacity = 100; s84.Opacity = 100; s85.Opacity = 100; s86.Opacity = 100; s87.Opacity = 100; s88.Opacity = 100; s89.Opacity = 100; s90.Opacity = 100;
            s91.Opacity = 100; s92.Opacity = 100; s93.Opacity = 100; s94.Opacity = 100; s95.Opacity = 100; s96.Opacity = 100; s97.Opacity = 100; s98.Opacity = 100; s99.Opacity = 100; s100.Opacity = 100;
            s101.Opacity = 100; s102.Opacity = 100; s103.Opacity = 100; s104.Opacity = 100; s105.Opacity = 100; s106.Opacity = 100; s107.Opacity = 100; s108.Opacity = 100; s109.Opacity = 100; s110.Opacity = 100;
            s111.Opacity = 100; s112.Opacity = 100; s113.Opacity = 100; s114.Opacity = 100; s115.Opacity = 100; s116.Opacity = 100; s117.Opacity = 100; s118.Opacity = 100; s119.Opacity = 100;
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
                    t1 = new Thread(new ParameterizedThreadStart(OccupaPostoTest));
                    t2 = new Thread(new ParameterizedThreadStart(OccupaPostoTest));
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
            MessageBox.Show("finito ");
        }

        private void btnAssegnaPosto_Click(object sender, RoutedEventArgs e)  //quando assegno i posti lo faccio 2 alla volta
        {
            int posto1, posto2;
            try
            {
                posto1 = int.Parse(txtThread1.Text);
                posto1--;
                if (posto1 <= -1 || posto1 >= 119)
                    throw new Exception("il posto inserito nella prima casella non esiste");

                posto2 = int.Parse(txtThread2.Text);
                posto2--;
                if (posto2 <= -1 || posto2 >= 119)
                    throw new Exception("il posto inserito nella seconda casella non esiste");

                t1 = new Thread(new ParameterizedThreadStart(OccupaPosto));
                t2 = new Thread(new ParameterizedThreadStart(OccupaPosto));

                t1.Start(posto1);
                t2.Start(posto2);

                txtThread1.Text = ""; //ripristino il testo
                txtThread2.Text = "";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void OccupaPosto(object i)
        {
            lock(x)
            { 
                try
                {

                    int num = (int)i;


                    if (postiLiberi[num] != true)
                        throw new Exception("posto già occupato");

                    postiLiberi[num] = false;  //occupo il posto
                    //OccupaPostoGrafica(num);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        void OccupaPostoTest(object i)
        {
            lock (x)
            {
                try
                {

                    int num = (int)i;


                    if (postiLiberi[num] != true)
                        throw new Exception("posto già occupato");

                    postiLiberi[num] = false;  //occupo il posto
                    postoAiuto = num;
                    //EseguiOccupaPostoGrafica del1 = new EseguiOccupaPostoGrafica(OccupaPostoGrafica);
                    //del1();
                    OccupaPostoGrafica();
                }
                catch (Exception ex)
                {
                    
                }
            }

        }

        void OccupaPostoGrafica()
        {
            int posto = postoAiuto;
            postoAiuto++;//riporto il posto secondo la sequenza della grafica
            if (posto == 1)
            {
                s1.Opacity = 0.1;
            }
            else if(posto == 2)
            {
                s2.Opacity = 50;
            }
            else if (posto == 3)
            {
                s3.Opacity = 50;
            }
            else if (posto == 4)
            {
                s4.Opacity = 50;
            }
            else if (posto == 5)
            {
                s5.Opacity = 50;
            }
            else if (posto == 6)
            {
                s6.Opacity = 50;
            }
            else if (posto == 7)
            {
                s7.Opacity = 50;
            }
            else if (posto == 8)
            {
                s8.Opacity = 50;
            }
            else if (posto == 9)
            {
                s9.Opacity = 50;
            }
            else if (posto == 10)
            {
                s10.Opacity = 50;
            }
            else if (posto == 11)
            {
                s11.Opacity = 50;
            }
            else if (posto == 12)
            {
                s12.Opacity = 50;
            }
            else if (posto == 13)
            {
                s13.Opacity = 50;
            }
            else if (posto == 14)
            {
                s14.Opacity = 50;
            }
            else if (posto == 15)
            {
                s15.Opacity = 50;
            }
            else if (posto == 16)
            {
                s16.Opacity = 50;
            }
            else if (posto == 17)
            {
                s17.Opacity = 50;
            }
            else if (posto == 18)
            {
                s18.Opacity = 50;
            }
            else if (posto == 19)
            {
                s19.Opacity = 50;
            }
            else if (posto == 20)
            {
                s20.Opacity = 50;
            }
            else if (posto == 21)
            {
                s21.Opacity = 50;
            }
            else if (posto == 22)
            {
                s22.Opacity = 50;
            }
            else if (posto == 23)
            {
                s23.Opacity = 50;
            }
            else if (posto == 24)
            {
                s24.Opacity = 50;
            }
            else if (posto == 25)
            {
                s25.Opacity = 50;
            }
            else if (posto == 26)
            {
                s26.Opacity = 50;
            }
            else if (posto == 27)
            {
                s27.Opacity = 50;
            }
            else if (posto == 28)
            {
                s28.Opacity = 50;
            }
            else if (posto == 29)
            {
                s29.Opacity = 50;
            }
            else if (posto == 30)
            {
                s30.Opacity = 50;
            }
            else if (posto == 31)
            {
                s31.Opacity = 50;
            }
            else if (posto == 32)
            {
                s32.Opacity = 50;
            }
            else if (posto == 33)
            {
                s33.Opacity = 50;
            }
            else if (posto == 34)
            {
                s34.Opacity = 50;
            }
            else if (posto == 35)
            {
                s35.Opacity = 50;
            }
            else if (posto == 36)
            {
                s36.Opacity = 50;
            }
            else if (posto == 37)
            {
                s37.Opacity = 50;
            }
            else if (posto == 38)
            {
                s38.Opacity = 50;
            }
            else if (posto == 39)
            {
                s39.Opacity = 50;
            }
            else if (posto == 40)
            {
                s40.Opacity = 50;
            }
            else if (posto == 41)
            {
                s41.Opacity = 50;
            }
            else if (posto == 42)
            {
                s42.Opacity = 50;
            }
            else if (posto == 43)
            {
                s43.Opacity = 50;
            }
            else if (posto == 44)
            {
                s44.Opacity = 50;
            }
            else if (posto == 45)
            {
                s45.Opacity = 50;
            }
            else if (posto == 46)
            {
                s46.Opacity = 50;
            }
            else if (posto == 47)
            {
                s47.Opacity = 50;
            }
            else if (posto == 48)
            {
                s48.Opacity = 50;
            }
            else if (posto == 49)
            {
                s49.Opacity = 50;
            }
            else if (posto == 50)
            {
                s50.Opacity = 50;
            }
            else if (posto == 51)
            {
                s51.Opacity = 50;
            }
            else if (posto == 52)
            {
                s52.Opacity = 50;
            }
            else if (posto == 53)
            {
                s53.Opacity = 50;
            }
            else if (posto == 54)
            {
                s54.Opacity = 50;
            }
            else if (posto == 55)
            {
                s55.Opacity = 50;
            }
            else if (posto == 56)
            {
                s56.Opacity = 50;
            }
            else if (posto == 57)
            {
                s57.Opacity = 50;
            }
            else if (posto == 58)
            {
                s58.Opacity = 50;
            }
            else if (posto == 59)
            {
                s59.Opacity = 50;
            }
            else if (posto == 60)
            {
                s60.Opacity = 50;
            }
            else if (posto == 61)
            {
                s61.Opacity = 50;
            }
            else if (posto == 62)
            {
                s62.Opacity = 50;
            }
            else if (posto == 63)
            {
                s63.Opacity = 50;
            }
            else if (posto == 64)
            {
                s64.Opacity = 50;
            }
            else if (posto == 65)
            {
                s65.Opacity = 50;
            }
            else if (posto == 66)
            {
                s66.Opacity = 50;
            }
            else if (posto == 67)
            {
                s67.Opacity = 50;
            }
            else if (posto == 68)
            {
                s68.Opacity = 50;
            }
            else if (posto == 69)
            {
                s69.Opacity = 50;
            }
            else if (posto == 70)
            { 
                s70.Opacity = 50;
            }
            else if (posto == 71)
            {
                s71.Opacity = 50;
            }
            else if (posto == 72)
            {
                s72.Opacity = 50;
            }
            else if (posto == 73)
            {
                s73.Opacity = 50;
            }
            else if (posto == 74)
            {
                s74.Opacity = 50;
            }
            else if (posto == 75)
            {
                s75.Opacity = 50;
            }
            else if (posto == 76)
            {
                s76.Opacity = 50;
            }
            else if (posto == 77)
            {
                s77.Opacity = 50;
            }
            else if (posto == 78)
            {
                s78.Opacity = 50;
            }
            else if (posto == 79)
            {
                s79.Opacity = 50;
            }
            else if (posto == 80)
            {
                s80.Opacity = 50;
            }
            else if (posto == 81)
            {
                s81.Opacity = 50;
            }
            else if (posto == 82)
            {
                s82.Opacity = 50;
            }
            else if (posto == 83)
            {
                s83.Opacity = 50;
            }
            else if (posto == 84)
            {
                s84.Opacity = 50;
            }
            else if (posto == 85)
            {
                s85.Opacity = 50;
            }
            else if (posto == 86)
            {
                s86.Opacity = 50;
            }
            else if (posto == 87)
            {
                s87.Opacity = 50;
            }
            else if (posto == 88)
            {
                s88.Opacity = 50;
            }
            else if (posto == 89)
            {
                s89.Opacity = 50;
            }
            else if (posto == 90)
            {
                s90.Opacity = 50;
            }
            else if (posto == 91)
            {
                s91.Opacity = 50;
            }
            else if (posto == 92)
            {
                s92.Opacity = 50;
            }
            else if (posto == 93)
            {
                s93.Opacity = 50;
            }
            else if (posto == 94)
            {
                s94.Opacity = 50;
            }
            else if (posto == 95)
            {
                s95.Opacity = 50;
            }
            else if (posto == 96)
            {
                s96.Opacity = 50;
            }
            else if (posto == 97)
            {
                s97.Opacity = 50;
            }
            else if (posto == 98)
            {
                s98.Opacity = 50;
            }
            else if (posto == 99)
            {
                s99.Opacity = 50;
            }
            else if (posto == 100)
            {
                s100.Opacity = 50;
            }
            else if (posto == 101)
            {
                s101.Opacity = 50;
            }
            else if (posto == 102)
            {
                s102.Opacity = 50;
            }
            else if (posto == 103)
            {
                s103.Opacity = 50;
            }
            else if (posto == 104)
            {
                s104.Opacity = 50;
            }
            else if (posto == 105)
            {
                s105.Opacity = 50;
            }
            else if (posto == 106)
            {
                s106.Opacity = 50;
            }
            else if (posto == 107)
            {
                s107.Opacity = 50;
            }
            else if (posto == 108)
            {
                s108.Opacity = 50;
            }
            else if (posto == 109)
            {
                s109.Opacity = 50;
            }
            else if (posto == 110)
            {
                s110.Opacity = 50;
            }
            else if (posto == 111)
            {
                s111.Opacity = 50;
            }
            else if (posto == 112)
            {
                s112.Opacity = 50;
            }
            else if (posto == 113)
            {
                s113.Opacity = 50;
            }
            else if (posto == 114)
            {
                s114.Opacity = 50;
            }
            else if (posto == 115)
            {
                s115.Opacity = 50;
            }
            else if (posto == 116)
            {
                s116.Opacity = 50;
            }
            else if (posto == 117)
            {
                s117.Opacity = 50;
            }
            else if (posto == 118)
            {
                s118.Opacity = 50;
            }
            else if (posto == 119)
            {
                s119.Opacity = 50;
            }
            
        }
    }
}
