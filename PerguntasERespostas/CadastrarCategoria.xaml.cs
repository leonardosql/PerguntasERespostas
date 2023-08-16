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

namespace PerguntasERespostas
{
    /// <summary>
    /// Interaction logic for CadastrarCategoria.xaml
    /// </summary>
    public partial class CadastrarCategoria : Window
    {
        HelperPesquisaDAO pesquisa;
        private string tabela;
        private string[] parametros;

        public CadastrarCategoria()
        {
            InitializeComponent();
            CarregarGrid();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void CarregarGrid()
        {
            tabela = "Categoria";
            this.parametros = new string[] { "Codigo as Código", "Nome as Descrição" };
            pesquisa = new HelperPesquisaDAO(this.parametros, this.tabela);

            var dt = pesquisa.Carregar();
            dgGrid.DataContext = dt;


        }

        private void CarregarCategoria()
        {
            if (!String.IsNullOrWhiteSpace(txtCod.Text))
            {
                Categoria cat = new PerguntasERespostas.Categoria();
                cat.Carregar(int.Parse(txtCod.Text));

                txtCod.Text = cat.Codigo.ToString();
                txtNom.Text = cat.Nome.ToString();
            }
            else
            {
                txtCod.Text = string.Empty;
                txtNom.Text = string.Empty;
                txtNom.Focus();
            }
        }

        private void btnAdicionar_Click(object sender, RoutedEventArgs e)
        {
            if (txtNom.Text.Length > 0)
            {
                Categoria cat = new PerguntasERespostas.Categoria();

                if (string.IsNullOrWhiteSpace(txtCod.Text))
                    cat.Codigo = null;
                else
                    cat.Codigo = int.Parse(txtCod.Text);

                cat.Nome = txtNom.Text;

                cat.Salvar();

                CarregarGrid();
                txtCod.Text = string.Empty;
                txtNom.Text = string.Empty;

                txtNom.Focus();
            }
            else
                MessageBox.Show("Campos obrigatórios não preenchidos.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Exclamation);

        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtCod.Text))
                {
                    Categoria A = new Categoria() { Codigo = int.Parse(txtCod.Text) };
                    A.Apagar();

                    txtCod.Text = string.Empty;
                    txtNom.Text = string.Empty;

                    CarregarGrid();
                    txtNom.Focus();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Não é possivel excluir essa categoria, porque existe perguntas cadastradas com essa.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void btnNovo_Click(object sender, RoutedEventArgs e)
        {
            txtCod.Text = string.Empty;
            txtNom.Text = string.Empty;

            CarregarGrid();
            txtNom.Focus();
        }

        private void dgGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgGrid.SelectedItems.Count > 0)
            {
                DataRowView row = (DataRowView)dgGrid.SelectedItems[0];
                txtCod.Text = row[0].ToString();
                CarregarCategoria();
            }
        }
    }

}

