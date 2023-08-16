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
    /// Interaction logic for NovoJogo.xaml
    /// </summary>
    public partial class NovoJogo : Window
    {
        Menu menu;

        public NovoJogo(Menu m)
        {
            menu = m;
            InitializeComponent();
            
            listBoxCategorias.ItemsSource = Categoria.CarregaCombo();
            listBoxCategorias.SelectedValuePath = "Codigo";
            listBoxCategorias.DisplayMemberPath = "Nome";            
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            menu.Show();
        }

        private void NovoJogo_Click(object sender, RoutedEventArgs e)
        {
            int numeroMaxPerguntas;

            if (!int.TryParse(txtNumPerguntas.Text, out numeroMaxPerguntas))
                numeroMaxPerguntas = 10000;

            List<int> lstCategorias = new List<int>();
            foreach (var item in listBoxCategorias.SelectedItems)
                lstCategorias.Add((int)((PerguntasERespostas.Categoria)item).Codigo);

            if (lstCategorias.Count == 0)
            {
                MessageBox.Show("Selecione ao menos uma categoria de perguntas.");
                return;
            }
            
            var lstPerguntas = MainWindow.CarregarRelacaoDePerguntas(numeroMaxPerguntas, lstCategorias);
            var lstJogadores = MainWindow.CarregarRelacaoDeJogadores();

            if (lstJogadores.Count == 0)
            {
                MessageBox.Show("Não é possivel iniciar partida, não existe jogadores cadastrados.");
                return;
            }

            if (lstPerguntas.Count == 0)
            {
                MessageBox.Show("Não é possivel iniciar partida, não existe perguntas cadastradas.");
                return;
            }

            if (lstJogadores.Count > lstPerguntas.Count)
            {
                MessageBox.Show("Não é possivel iniciar partida, existe mais jogadores que perguntas.");
                return;
            }

            if (lstJogadores.Select(x => x.Time == Equipe.A).Count() != lstJogadores.Select(x => x.Time == Equipe.B).Count())
            {
                MessageBox.Show("Não é possivel iniciar partida, equipe A e B não possui a mesma quantidade de jogadores.");
                return;
            }

            var r = new MainWindow(menu, numeroMaxPerguntas, lstCategorias, (radEliminativo.IsChecked == true ? MainWindow.ModoJogo.Eliminativo : MainWindow.ModoJogo.MaisAcertos));
            menu.Hide();
            this.Hide();
            r.Show();
        }
    }
}
