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
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void Sair_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
            this.Close();
        }

        private void NovoJogo_Click(object sender, RoutedEventArgs e)
        {
            var r = new NovoJogo(this);            
            r.Show();            
        }

        private void Jogadores_Click(object sender, RoutedEventArgs e)
        {
            var r = new CadastrarJogador();
            r.ShowDialog();
        }

        private void Categoria_Click(object sender, RoutedEventArgs e)
        {
            var r = new CadastrarCategoria();
            r.ShowDialog();
        }

        private void Perguntas_Click(object sender, RoutedEventArgs e)
        {
            var r = new CadastrarPerguntas();
            r.ShowDialog();
        }

        private void StackPanel_Loaded(object sender, RoutedEventArgs e)
        {
            if (!SQLHelper.VerificaSeExisteArquivoDeBancoDeDados())
            {
                MessageBox.Show("Não existe banco de dados na pasta do programa. Por favor adicione esse e tente novamente.");
                Environment.Exit(0);                
            }
        }
    }
}
