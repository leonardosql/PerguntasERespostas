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
using System.Windows.Shapes;

namespace PerguntasERespostas
{
    /// <summary>
    /// Interaction logic for Mensagem2.xaml
    /// </summary>
    public partial class Mensagem2 : Window
    {
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        
        public static DependencyProperty JogadoresProperty =
            DependencyProperty.RegisterAttached(
            "Jogadores",
            typeof(List<Jogador>),
            typeof(Mensagem2), new UIPropertyMetadata());


        public List<Jogador> Jogadores
        {
            get
            {
                return (List<Jogador>)this.GetValue(JogadoresProperty);              
            }
            set
            {
                this.SetValue(JogadoresProperty, value);
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

        public Mensagem2(string msg, string placar,int segundos)
        {
            InitializeComponent();

            dispatcherTimer.Tick += dispatcherTimer_Tick;

            dispatcherTimer.Interval = new TimeSpan(0, 0, segundos);
            dispatcherTimer.Start();

            lblMsg.Content = msg;
            lblPlacar.Content = placar;
        }

        public Mensagem2(string msg, string placar, int segundos, List<Jogador> lstJogadores)
        {
            InitializeComponent();

            dispatcherTimer.Tick += dispatcherTimer_Tick;

            dispatcherTimer.Interval = new TimeSpan(0, 0, segundos);
            dispatcherTimer.Start();

            lblMsg.Content = msg;
            lblPlacar.Content = placar;
            Jogadores = new List<Jogador>();
            Jogadores.AddRange(lstJogadores);
        }
    }
}
