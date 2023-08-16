using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.IO;

namespace PerguntasERespostas
{
    /// <summary>
    /// Interaction logic for CadastrarJogador.xaml
    /// </summary>
    public partial class CadastrarJogador : Window
    {
        HelperPesquisaDAO pesquisa;
        private string tabela;
        private string[] parametros;

        public CadastrarJogador()
        {
            InitializeComponent();

            cbxCategoria.ItemsSource = Categoria.CarregaCombo();
            cbxCategoria.SelectedValuePath = "Codigo";
            cbxCategoria.DisplayMemberPath = "Nome";
            CarregarGrid();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void CarregarGrid()
        {
            pesquisa = new HelperPesquisaDAO("select j.Codigo as Código, j.Nome, j.Equipe, c.Nome as Categoria from Jogador j inner join Categoria c on j.Categoria = c.Codigo");

            var dt = pesquisa.Carregar();
            dgGrid.DataContext = dt;

            if (dt != null && dt.Rows.Count > 0)
            {
                lblToTA.Content = (from DataRow r in dt.Rows
                                   where (string)r["Equipe"] == "A"
                                   select 1).ToList().Count.ToString();

                lblToTB.Content = (from DataRow r in dt.Rows
                                   where (string)r["Equipe"] == "B"
                                   select 1).ToList().Count.ToString();
            }
            else
            {
                lblToTA.Content = "0";
                lblToTB.Content = "0";
            }
        }

        private void dgGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (dgGrid.SelectedItems.Count > 0)
            //{
            //    DataRowView row = (DataRowView)dgGrid.SelectedItems[0];
            //    txtCod.Text = row[0].ToString();
            //    CarregarJogador();
            //}
        }
        private void CarregarJogador()
        {
            if (!String.IsNullOrWhiteSpace(txtCod.Text))
            {
                Jogador P1 = new PerguntasERespostas.Jogador();
                P1.Carregar(int.Parse(txtCod.Text));

                txtCod.Text = P1.Codigo.ToString();
                txtNom.Text = P1.Nome.ToString();
                txtImagem.Text = P1.Imagem;

                if (!string.IsNullOrWhiteSpace(P1.categoria.ToString()))
                    cbxCategoria.SelectedValue = P1.categoria;

                if (P1.Time == Equipe.A)
                    radEquipeA.IsChecked = true;
                else
                    radEquipeB.IsChecked = true;

                try
                {
                    if (!string.IsNullOrWhiteSpace(P1.Imagem) && System.IO.File.Exists(P1.Imagem))
                        imgAjudaA1.Source = new BitmapImage(new Uri(P1.Imagem));
                }
                catch (Exception)
                {
                    txtImagem.Text = string.Empty;
                    imgAjudaA1.Source = null;
                }

            }
            else
            {
                txtCod.Text = string.Empty;
                txtNom.Text = string.Empty;
                txtImagem.Text = string.Empty;
                radEquipeA.IsChecked = false;
                radEquipeB.IsChecked = false;
                imgAjudaA1.Source = null;
                cbxCategoria.SelectedIndex = -1;
                txtNom.Focus();
            }
        }

        private void btnAdicionar_Click(object sender, RoutedEventArgs e)
        {
            if (txtNom.Text.Length > 0
                && txtImagem.Text.Length > 0
                && cbxCategoria.SelectedIndex != -1
                && ((radEquipeA.IsChecked.HasValue && radEquipeA.IsChecked.Value)
                       || (radEquipeB.IsChecked.HasValue && radEquipeB.IsChecked.Value)))
            {
                Jogador P1 = new PerguntasERespostas.Jogador();

                if (string.IsNullOrWhiteSpace(txtCod.Text))
                    P1.Codigo = null;
                else
                    P1.Codigo = int.Parse(txtCod.Text);

                P1.Nome = txtNom.Text;

                if (!string.IsNullOrEmpty(txtImagem.Text))
                {
                    var img = txtImagem.Text;

                    FileInfo fi = new FileInfo(txtImagem.Text);

                    Directory.CreateDirectory(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "ImagensJogadores"));
                    img = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "ImagensJogadores", string.Concat(fi.Name, "_", DateTime.Now.Millisecond.ToString()));
                    File.Copy(txtImagem.Text, img);
                    P1.Imagem = img;
                }
                P1.categoria = (int)cbxCategoria.SelectedValue;

                if (radEquipeA.IsChecked.HasValue && radEquipeA.IsChecked.Value)
                    P1.Time = Equipe.A;
                else
                    P1.Time = Equipe.B;

                P1.Salvar();

                CarregarGrid();
                txtCod.Text = string.Empty;
                txtNom.Text = string.Empty;
                txtImagem.Text = string.Empty;
                radEquipeA.IsChecked = false;
                radEquipeB.IsChecked = false;
                imgAjudaA1.Source = null;
                cbxCategoria.SelectedIndex = -1;

                txtNom.Focus();
            }
            else
                MessageBox.Show("Campos obrigatórios não preenchidos.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Exclamation);

        }

        private void btnPesquisar_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg;  *.png";
            dialog.Title = "Selecione uma foto";
            bool? result = dialog.ShowDialog();
            if (result.HasValue && result.Value)
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(dialog.FileName))
                    {
                        imgAjudaA1.Source = new BitmapImage(new Uri(dialog.FileName));
                        txtImagem.Text = dialog.FileName;
                    }
                }
                catch (Exception)
                {
                    txtImagem.Text = string.Empty;
                    imgAjudaA1.Source = null;
                }
            }
        }

        private void btnApagarTodos_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Tem certeza que deseja apagar todos os jogadores cadastrados?", "Atenção", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.Yes)
            {
                Jogador A = new Jogador();
                A.Apagar();
                CarregarGrid();
            }
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtCod.Text))
            {
                Jogador A = new Jogador() { Codigo = int.Parse(txtCod.Text) };
                A.Apagar();

                txtCod.Text = string.Empty;
                txtNom.Text = string.Empty;
                txtImagem.Text = string.Empty;
                radEquipeA.IsChecked = false;
                radEquipeB.IsChecked = false;
                imgAjudaA1.Source = null;
                cbxCategoria.SelectedIndex = -1;

                CarregarGrid();
                txtNom.Focus();
            }
        }

        private void btnNovo_Click(object sender, RoutedEventArgs e)
        {
            txtCod.Text = string.Empty;
            txtNom.Text = string.Empty;
            txtImagem.Text = string.Empty;
            radEquipeA.IsChecked = false;
            radEquipeB.IsChecked = false;
            imgAjudaA1.Source = null;
            cbxCategoria.SelectedIndex = -1;

            CarregarGrid();
            txtNom.Focus();
        }

        private void dgGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgGrid.SelectedItems.Count > 0)
            {
                DataRowView row = (DataRowView)dgGrid.SelectedItems[0];
                txtCod.Text = row[0].ToString();
                CarregarJogador();
            }
        }
    }
}
