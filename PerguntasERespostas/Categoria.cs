using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerguntasERespostas
{
    public class Categoria
    {
        public int? Codigo { get; set; }
        public string Nome { get; set; }

        public static List<Categoria> CarregaCombo()
        {
            List<Categoria> lista = new List<Categoria>();

            using (SQLHelper helper = new SQLHelper())
            {
                SQLiteCommand cmd = helper.CriarComando();
                SQLiteDataReader dr;

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "select codigo, nome from categoria";

                dr = helper.ExecutaReader(cmd);

                while (dr.Read())
                {
                    Categoria categoria = new Categoria();
                    categoria.Codigo = int.Parse(dr[0].ToString());
                    categoria.Nome = dr[1].ToString();

                    lista.Add(categoria);
                }
            }

            return lista;
        }

        public void Carregar(int cod)
        {
            using (SQLHelper helper = new SQLHelper())
            {
                SQLiteCommand cmd = helper.CriarComando();
                SQLiteDataReader dr;

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = string.Format("SELECT * FROM CATEGORIA WHERE CODIGO = {0}", cod);

                dr = helper.ExecutaReader(cmd);

                while (dr.Read())
                {
                    this.Codigo = int.Parse(dr["CODIGO"].ToString());
                    this.Nome = dr["NOME"].ToString();
                }
            }
        }

        public static Dictionary<int, string> CarregarCategorias()
        {
            var DicCategoria = new Dictionary<int, string>();
            
            using (SQLHelper helper = new SQLHelper())
            {
                SQLiteCommand cmd = helper.CriarComando();
                SQLiteDataReader dr;

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = string.Format("SELECT * FROM CATEGORIA WHERE 1=1 LIMIT 100 ");

                dr = helper.ExecutaReader(cmd);

                while (dr.Read())
                    DicCategoria.Add(int.Parse(dr["CODIGO"].ToString()), dr["NOME"].ToString());

            }

            return DicCategoria;
        }

        public void Salvar()
        {
            using (SQLHelper helper = new SQLHelper())
            {
                SQLiteCommand cmd = helper.CriarComando();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.Add(helper.CriarParametro("@nome", this.Nome));

                if (this.Codigo.HasValue)
                {
                    cmd.CommandText = "Update Categoria Set Nome = @nome where codigo = @codigo;";
                    cmd.Parameters.Add(helper.CriarParametro("@codigo", this.Codigo));
                }
                else
                {
                    cmd.CommandText = "Insert into Categoria (nome) Values (@nome);";
                }

                helper.PersistirDados(cmd);
            }
        }

        public void Apagar()
        {
            using (SQLHelper helper = new SQLHelper())
            {
                SQLiteCommand cmd = helper.CriarComando();
                cmd.CommandType = System.Data.CommandType.Text;

                if (this.Codigo.HasValue)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(@"Delete from Perguntas where CATEGORIA = @codigo;");
                    sb.AppendLine(@"Delete from Categoria where codigo = @codigo;");

                    cmd.CommandText = sb.ToString();
                    cmd.Parameters.Add(helper.CriarParametro("@codigo", this.Codigo));
                }
                else
                {
                    //cmd.CommandText = "Delete from Categoria where codigo > -1;";
                }

                helper.PersistirDados(cmd);
            }
        }
    }
}
