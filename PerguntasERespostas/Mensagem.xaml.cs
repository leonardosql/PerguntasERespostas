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
    /// Interaction logic for Mensagem.xaml
    /// </summary>
    public partial class Mensagem : Window
    {
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

        public Mensagem(string msg, int segundos)
        {
            InitializeComponent();

            dispatcherTimer.Tick += dispatcherTimer_Tick;

            dispatcherTimer.Interval = new TimeSpan(0, 0, segundos);
            dispatcherTimer.Start();

            lblMsg.Content = msg;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
