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

        public MainWindow()
        {
            InitializeComponent();
            SvuotaCinema();
        }

        void SvuotaCinema()
        {
            //ripristino dei posti nell'array
            postiLiberi = new bool[119];
            for(int i = 0; i < postiLiberi.Length; i++)
            {
                postiLiberi[i] = true;
            }
            //riprinistino dei posti nell'interfaccia grafica
            s1.Opacity = 1; s2.Opacity = 1; s3.Opacity = 1; s4.Opacity = 1;s5.Opacity = 1; s6.Opacity = 1; s7.Opacity = 1; s8.Opacity = 1; s9.Opacity = 1; s10.Opacity = 1;
            s11.Opacity = 1; s12.Opacity = 1; s13.Opacity = 1; s14.Opacity = 1; s15.Opacity = 1; s16.Opacity = 1; s17.Opacity = 1; s18.Opacity = 1; s19.Opacity = 1; s20.Opacity = 1;
            s21.Opacity = 1; s22.Opacity = 1; s23.Opacity = 1; s24.Opacity = 1; s25.Opacity = 1; s26.Opacity = 1; s27.Opacity = 1; s28.Opacity = 1; s29.Opacity = 1; s30.Opacity = 1;
            s31.Opacity = 1; s32.Opacity = 1; s33.Opacity = 1; s34.Opacity = 1; s35.Opacity = 1; s36.Opacity = 1; s37.Opacity = 1; s38.Opacity = 1; s39.Opacity = 1; s40.Opacity = 1;
            s41.Opacity = 1; s42.Opacity = 1; s43.Opacity = 1; s44.Opacity = 1; s45.Opacity = 1; s46.Opacity = 1; s47.Opacity = 1; s48.Opacity = 1; s49.Opacity = 1; s50.Opacity = 1;
            s51.Opacity = 1; s52.Opacity = 1; s53.Opacity = 1; s54.Opacity = 1; s55.Opacity = 1; s56.Opacity = 1; s57.Opacity = 1; s58.Opacity = 1; s59.Opacity = 1; s60.Opacity = 1;
            s61.Opacity = 1; s62.Opacity = 1; s63.Opacity = 1; s64.Opacity = 1; s65.Opacity = 1; s66.Opacity = 1; s67.Opacity = 1; s68.Opacity = 1; s69.Opacity = 1; s70.Opacity = 1;
            s71.Opacity = 1; s72.Opacity = 1; s73.Opacity = 1; s74.Opacity = 1; s75.Opacity = 1; s76.Opacity = 1; s77.Opacity = 1; s78.Opacity = 1; s79.Opacity = 1; s80.Opacity = 1;
            s81.Opacity = 1; s82.Opacity = 1; s83.Opacity = 1; s84.Opacity = 1; s85.Opacity = 1; s86.Opacity = 1; s87.Opacity = 1; s88.Opacity = 1; s89.Opacity = 1; s90.Opacity = 1;
            s91.Opacity = 1; s92.Opacity = 1; s93.Opacity = 1; s94.Opacity = 1; s95.Opacity = 1; s96.Opacity = 1; s97.Opacity = 1; s98.Opacity = 1; s99.Opacity = 1; s100.Opacity = 1;
            s101.Opacity = 1; s102.Opacity = 1; s103.Opacity = 1; s104.Opacity = 1; s105.Opacity = 1; s106.Opacity = 1; s107.Opacity = 1; s108.Opacity = 1; s109.Opacity = 1; s110.Opacity = 1;
            s111.Opacity = 1; s112.Opacity = 1; s113.Opacity = 1; s114.Opacity = 1; s115.Opacity = 1; s116.Opacity = 1; s117.Opacity = 1; s118.Opacity = 1; s119.Opacity = 1;
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
                    //controllo che il numero del posto non sia già stato occupato per una maggiore efficienza
                    int f = rnd.Next(119); int g = rnd.Next(119);
                    bool fine;
                    do
                    {
                        fine = true;
                        if (postiLiberi[f] == false)
                        {
                            f = rnd.Next(119);
                            fine = false;
                        }

                    } while (fine == false);
                    
                    do
                    {
                        fine = true;
                        if (postiLiberi[g] == false)
                        {
                            g = rnd.Next(119);
                            fine = false;
                        }

                    } while (fine == false);


                    t1 = new Thread(new ParameterizedThreadStart(OccupaPostoTest));
                    t2 = new Thread(new ParameterizedThreadStart(OccupaPostoTest));

                    t1.Start(f);
                    t2.Start(g);

                    
                    OccupaPostoGrafica(f);
                    OccupaPostoGrafica(g);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
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
                
                OccupaPostoGrafica(posto1); //dato che il thread sembra non posso modificare lo stato nella interfaccia grafica
                OccupaPostoGrafica(posto2); //lo faccio fare dopo l'esecuzione del thread
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            txtThread1.Text = ""; //ripristino il testo
            txtThread2.Text = "";
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
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        void OccupaPostoTest(object i) //funzione utilizzata dal bottone btnTest_Click per non aver messaggi di errore
        {
            lock (x)
            {
                try
                {

                    int num = (int)i;


                    if (postiLiberi[num] != true)
                        throw new Exception("posto già occupato");

                    postiLiberi[num] = false;  //occupo il posto
                }
                catch (Exception ex)
                {
                    
                }
            }

        }
        
        void OccupaPostoGrafica(int i) //visualizzazione posto occupato
        {
            int posto = i;
            posto++;
            if (posto == 1)
            {
                s1.Opacity = 0.5;
            }
            else if(posto == 2)
            {
                s2.Opacity = 0.5;
            }
            else if (posto == 3)
            {
                s3.Opacity = 0.5;
            }
            else if (posto == 4)
            {
                s4.Opacity = 0.5;
            }
            else if (posto == 5)
            {
                s5.Opacity = 0.5;
            }
            else if (posto == 6)
            {
                s6.Opacity = 0.5;
            }
            else if (posto == 7)
            {
                s7.Opacity = 0.5;
            }
            else if (posto == 8)
            {
                s8.Opacity = 0.5;
            }
            else if (posto == 9)
            {
                s9.Opacity = 0.5;
            }
            else if (posto == 10)
            {
                s10.Opacity = 0.5;
            }
            else if (posto == 11)
            {
                s11.Opacity = 0.5;
            }
            else if (posto == 12)
            {
                s12.Opacity = 0.5;
            }
            else if (posto == 13)
            {
                s13.Opacity = 0.5;
            }
            else if (posto == 14)
            {
                s14.Opacity = 0.5;
            }
            else if (posto == 15)
            {
                s15.Opacity = 0.5;
            }
            else if (posto == 16)
            {
                s16.Opacity = 0.5;
            }
            else if (posto == 17)
            {
                s17.Opacity = 0.5;
            }
            else if (posto == 18)
            {
                s18.Opacity = 0.5;
            }
            else if (posto == 19)
            {
                s19.Opacity = 0.5;
            }
            else if (posto == 20)
            {
                s20.Opacity = 0.5;
            }
            else if (posto == 21)
            {
                s21.Opacity = 0.5;
            }
            else if (posto == 22)
            {
                s22.Opacity = 0.5;
            }
            else if (posto == 23)
            {
                s23.Opacity = 0.5;
            }
            else if (posto == 24)
            {
                s24.Opacity = 0.5;
            }
            else if (posto == 25)
            {
                s25.Opacity = 0.5;
            }
            else if (posto == 26)
            {
                s26.Opacity = 0.5;
            }
            else if (posto == 27)
            {
                s27.Opacity = 0.5;
            }
            else if (posto == 28)
            {
                s28.Opacity = 0.5;
            }
            else if (posto == 29)
            {
                s29.Opacity = 0.5;
            }
            else if (posto == 30)
            {
                s30.Opacity = 0.5;
            }
            else if (posto == 31)
            {
                s31.Opacity = 0.5;
            }
            else if (posto == 32)
            {
                s32.Opacity = 0.5;
            }
            else if (posto == 33)
            {
                s33.Opacity = 0.5;
            }
            else if (posto == 34)
            {
                s34.Opacity = 0.5;
            }
            else if (posto == 35)
            {
                s35.Opacity = 0.5;
            }
            else if (posto == 36)
            {
                s36.Opacity = 0.5;
            }
            else if (posto == 37)
            {
                s37.Opacity = 0.5;
            }
            else if (posto == 38)
            {
                s38.Opacity = 0.5;
            }
            else if (posto == 39)
            {
                s39.Opacity = 0.5;
            }
            else if (posto == 40)
            {
                s40.Opacity = 0.5;
            }
            else if (posto == 41)
            {
                s41.Opacity = 0.5;
            }
            else if (posto == 42)
            {
                s42.Opacity = 0.5;
            }
            else if (posto == 43)
            {
                s43.Opacity = 0.5;
            }
            else if (posto == 44)
            {
                s44.Opacity = 0.5;
            }
            else if (posto == 45)
            {
                s45.Opacity = 0.5;
            }
            else if (posto == 46)
            {
                s46.Opacity = 0.5;
            }
            else if (posto == 47)
            {
                s47.Opacity = 0.5;
            }
            else if (posto == 48)
            {
                s48.Opacity = 0.5;
            }
            else if (posto == 49)
            {
                s49.Opacity = 0.5;
            }
            else if (posto == 50)
            {
                s50.Opacity = 0.5;
            }
            else if (posto == 51)
            {
                s51.Opacity = 0.5;
            }
            else if (posto == 52)
            {
                s52.Opacity = 0.5;
            }
            else if (posto == 53)
            {
                s53.Opacity = 0.5;
            }
            else if (posto == 54)
            {
                s54.Opacity = 0.5;
            }
            else if (posto == 55)
            {
                s55.Opacity = 0.5;
            }
            else if (posto == 56)
            {
                s56.Opacity = 0.5;
            }
            else if (posto == 57)
            {
                s57.Opacity = 0.5;
            }
            else if (posto == 58)
            {
                s58.Opacity = 0.5;
            }
            else if (posto == 59)
            {
                s59.Opacity = 0.5;
            }
            else if (posto == 60)
            {
                s60.Opacity = 0.5;
            }
            else if (posto == 61)
            {
                s61.Opacity = 0.5;
            }
            else if (posto == 62)
            {
                s62.Opacity = 0.5;
            }
            else if (posto == 63)
            {
                s63.Opacity = 0.5;
            }
            else if (posto == 64)
            {
                s64.Opacity = 0.5;
            }
            else if (posto == 65)
            {
                s65.Opacity = 0.5;
            }
            else if (posto == 66)
            {
                s66.Opacity = 0.5;
            }
            else if (posto == 67)
            {
                s67.Opacity = 0.5;
            }
            else if (posto == 68)
            {
                s68.Opacity = 0.5;
            }
            else if (posto == 69)
            {
                s69.Opacity = 0.5;
            }
            else if (posto == 70)
            { 
                s70.Opacity = 0.5;
            }
            else if (posto == 71)
            {
                s71.Opacity = 0.5;
            }
            else if (posto == 72)
            {
                s72.Opacity = 0.5;
            }
            else if (posto == 73)
            {
                s73.Opacity = 0.5;
            }
            else if (posto == 74)
            {
                s74.Opacity = 0.5;
            }
            else if (posto == 75)
            {
                s75.Opacity = 0.5;
            }
            else if (posto == 76)
            {
                s76.Opacity = 0.5;
            }
            else if (posto == 77)
            {
                s77.Opacity = 0.5;
            }
            else if (posto == 78)
            {
                s78.Opacity = 0.5;
            }
            else if (posto == 79)
            {
                s79.Opacity = 0.5;
            }
            else if (posto == 80)
            {
                s80.Opacity = 0.5;
            }
            else if (posto == 81)
            {
                s81.Opacity = 0.5;
            }
            else if (posto == 82)
            {
                s82.Opacity = 0.5;
            }
            else if (posto == 83)
            {
                s83.Opacity = 0.5;
            }
            else if (posto == 84)
            {
                s84.Opacity = 0.5;
            }
            else if (posto == 85)
            {
                s85.Opacity = 0.5;
            }
            else if (posto == 86)
            {
                s86.Opacity = 0.5;
            }
            else if (posto == 87)
            {
                s87.Opacity = 0.5;
            }
            else if (posto == 88)
            {
                s88.Opacity = 0.5;
            }
            else if (posto == 89)
            {
                s89.Opacity = 0.5;
            }
            else if (posto == 90)
            {
                s90.Opacity = 0.5;
            }
            else if (posto == 91)
            {
                s91.Opacity = 0.5;
            }
            else if (posto == 92)
            {
                s92.Opacity = 0.5;
            }
            else if (posto == 93)
            {
                s93.Opacity = 0.5;
            }
            else if (posto == 94)
            {
                s94.Opacity = 0.5;
            }
            else if (posto == 95)
            {
                s95.Opacity = 0.5;
            }
            else if (posto == 96)
            {
                s96.Opacity = 0.5;
            }
            else if (posto == 97)
            {
                s97.Opacity = 0.5;
            }
            else if (posto == 98)
            {
                s98.Opacity = 0.5;
            }
            else if (posto == 99)
            {
                s99.Opacity = 0.5;
            }
            else if (posto == 100)
            {
                s100.Opacity = 0.5;
            }
            else if (posto == 101)
            {
                s101.Opacity = 0.5;
            }
            else if (posto == 102)
            {
                s102.Opacity = 0.5;
            }
            else if (posto == 103)
            {
                s103.Opacity = 0.5;
            }
            else if (posto == 104)
            {
                s104.Opacity = 0.5;
            }
            else if (posto == 105)
            {
                s105.Opacity = 0.5;
            }
            else if (posto == 106)
            {
                s106.Opacity = 0.5;
            }
            else if (posto == 107)
            {
                s107.Opacity = 0.5;
            }
            else if (posto == 108)
            {
                s108.Opacity = 0.5;
            }
            else if (posto == 109)
            {
                s109.Opacity = 0.5;
            }
            else if (posto == 110)
            {
                s110.Opacity = 0.5;
            }
            else if (posto == 111)
            {
                s111.Opacity = 0.5;
            }
            else if (posto == 112)
            {
                s112.Opacity = 0.5;
            }
            else if (posto == 113)
            {
                s113.Opacity = 0.5;
            }
            else if (posto == 114)
            {
                s114.Opacity = 0.5;
            }
            else if (posto == 115)
            {
                s115.Opacity = 0.5;
            }
            else if (posto == 116)
            {
                s116.Opacity = 0.5;
            }
            else if (posto == 117)
            {
                s117.Opacity = 0.5;
            }
            else if (posto == 118)
            {
                s118.Opacity = 0.5;
            }
            else if (posto == 119)
            {
                s119.Opacity = 0.5;
            }
            
        }
    }
}
