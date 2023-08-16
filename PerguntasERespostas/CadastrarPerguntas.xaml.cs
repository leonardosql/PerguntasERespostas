using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
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
    public partial class CadastrarPerguntas : Window
    {

        public CadastrarPerguntas()
        {
            try
            {
                InitializeComponent();

                cbxCategoria.ItemsSource = Categoria.CarregaCombo();
                cbxCategoria.SelectedValuePath = "Codigo";
                cbxCategoria.DisplayMemberPath = "Nome";

                CarregarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("CadastrarPerguntas - " + ex.Message);
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void CarregarGrid()
        {
            try
            {


                StringBuilder sb = new StringBuilder();

                sb.AppendLine("SELECT p.Codigo as \"Código\", p.Pergunta, cat.NOME AS Categoria");
                sb.AppendLine("FROM Perguntas AS p INNER JOIN Categoria AS cat ON p.CATEGORIA = cat.CODIGO");
                
                DataTable dt;
                using (SQLHelper helper = new SQLHelper())
                {
                    SQLiteCommand cmd = helper.CriarComando();
                    cmd.CommandText = sb.ToString();

                    dt = helper.RetornaDataTable(cmd);
                }

                if (dt != null)
                    dgGrid.DataContext = dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show("CarregarGrid - " + ex.Message);
            }
        }

        private void CarregarPerguntasERespostas()
        {
            try
            {


                if (!String.IsNullOrWhiteSpace(txtCod.Text))
                {
                    Pergunta p = new PerguntasERespostas.Pergunta();
                    p.Carregar(int.Parse(txtCod.Text));

                    txtCod.Text = p.codigo.ToString();
                    txtPer.Text = p.pergunta.ToString();

                    txtRespA.Text = p.RespA;
                    txtRespB.Text = p.RespB;
                    txtRespC.Text = p.RespC;
                    txtRespD.Text = p.RespD;

                    cbxCategoria.SelectedValue = p.categoria;

                    radA.IsChecked = false;
                    radB.IsChecked = false;
                    radC.IsChecked = false;
                    radD.IsChecked = false;

                    var resp = p.GetResposta();
                    switch (resp)
                    {
                        case RespostaCorreta.A:
                            radA.IsChecked = true; break;
                        case RespostaCorreta.B:
                            radB.IsChecked = true; break;
                        case RespostaCorreta.C:
                            radC.IsChecked = true; break;
                        case RespostaCorreta.D:
                            radD.IsChecked = true; break;
                    }
                }
                else
                {
                    txtCod.Text = string.Empty;
                    txtPer.Text = string.Empty;
                    txtRespA.Text = string.Empty;
                    txtRespB.Text = string.Empty;
                    txtRespC.Text = string.Empty;
                    txtRespD.Text = string.Empty;
                    cbxCategoria.SelectedIndex = -1;

                    radA.IsChecked = false;
                    radB.IsChecked = false;
                    radC.IsChecked = false;
                    radD.IsChecked = false;

                    txtPer.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("CarregarPerguntasERespostas - " + ex.Message);
            }
        }

        private void btnAdicionar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtPer.Text.Length > 0
                    && txtRespA.Text.Length > 0
                    && txtRespB.Text.Length > 0
                    && txtRespC.Text.Length > 0
                    && txtRespD.Text.Length > 0
                    && cbxCategoria.SelectedIndex != -1
                    && (radA.IsChecked.Value || radB.IsChecked.Value || radC.IsChecked.Value || radD.IsChecked.Value))
                {
                    Pergunta p = new PerguntasERespostas.Pergunta();

                    if (string.IsNullOrWhiteSpace(txtCod.Text))
                        p.codigo = null;
                    else
                        p.codigo = int.Parse(txtCod.Text);

                    p.categoria = (int)cbxCategoria.SelectedValue;
                    p.pergunta = txtPer.Text;

                    p.RespA = txtRespA.Text;
                    p.RespB = txtRespB.Text;
                    p.RespC = txtRespC.Text;
                    p.RespD = txtRespD.Text; 

                    if (radA.IsChecked.Value)
                        p.RespCorreta = txtRespA.Text;
                    else if (radB.IsChecked.Value)
                        p.RespCorreta = txtRespB.Text;
                    else if (radC.IsChecked.Value)
                        p.RespCorreta = txtRespC.Text;
                    else
                        p.RespCorreta = txtRespD.Text;

                    p.Salvar();

                    CarregarGrid();

                    txtCod.Text = string.Empty;
                    txtPer.Text = string.Empty;
                    txtRespA.Text = string.Empty;
                    txtRespB.Text = string.Empty;
                    txtRespC.Text = string.Empty;
                    txtRespD.Text = string.Empty;
                    cbxCategoria.SelectedIndex = -1;

                    radA.IsChecked = false;
                    radB.IsChecked = false;
                    radC.IsChecked = false;
                    radD.IsChecked = false;

                    txtPer.Focus();
                }
                else
                    MessageBox.Show("Campos obrigatórios não preenchidos.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show("btnAdicionar_Click - " + ex.Message);
                throw;
            }
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtCod.Text))
                {
                    Pergunta A = new Pergunta() { codigo = int.Parse(txtCod.Text) };
                    A.Apagar();

                    txtCod.Text = string.Empty;
                    txtPer.Text = string.Empty;
                    txtRespA.Text = string.Empty;
                    txtRespB.Text = string.Empty;
                    txtRespC.Text = string.Empty;
                    txtRespD.Text = string.Empty;
                    cbxCategoria.SelectedIndex = -1;

                    radA.IsChecked = false;
                    radB.IsChecked = false;
                    radC.IsChecked = false;
                    radD.IsChecked = false;

                    txtPer.Focus();
                    CarregarGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("btnExcluir_Click - " + ex.Message);
            }
        }

        private void btnNovo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                txtCod.Text = string.Empty;
                txtPer.Text = string.Empty;
                txtRespA.Text = string.Empty;
                txtRespB.Text = string.Empty;
                txtRespC.Text = string.Empty;
                txtRespD.Text = string.Empty;
                cbxCategoria.SelectedIndex = -1;

                radA.IsChecked = false;
                radB.IsChecked = false;
                radC.IsChecked = false;
                radD.IsChecked = false;

                txtPer.Focus();
                CarregarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("btnNovo_Click - " + ex.Message);
            }
        }

        private void dgGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (dgGrid.SelectedItems.Count > 0)
                {
                    DataRowView row = (DataRowView)dgGrid.SelectedItems[0];
                    txtCod.Text = row[0].ToString();
                    CarregarPerguntasERespostas();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("btnNovo_Click - " + ex.Message);
            }
        }

    }
}

