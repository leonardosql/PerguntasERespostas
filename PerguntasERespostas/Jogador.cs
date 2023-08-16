using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PerguntasERespostas
{
    public class Jogador
    {
        private string img;

        public int? Codigo { get; set; }
        public string Nome { get; set; }
        public string Imagem
        {
            get
            {
                if (System.IO.File.Exists(img))
                    return img;
                else  
                {
                    try
                    {
                        string[] fileName = img.Split('\\');
                        var path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "ImagensJogadores", fileName[fileName.Count() - 1]);
                        if (File.Exists(path))
                            return path;
                    }
                    catch { }
                }

                return System.IO.Path.Combine(Directory.GetCurrentDirectory(), "semImagem.jpeg") ;
            }
            set { img = value; }
        }
        public bool JaJogou { get; set; }
        public bool Eliminado { get; set; }
        public bool Ajudou { get; set; }
        public Equipe Time { get; set; }
        public int categoria { get; set; }

        public BitmapImage GetImage
        {
            get { return (System.IO.File.Exists(Imagem) ? new BitmapImage(new Uri(Imagem)) : null); }
        }

        public void Carregar(int cod)
        {
            using (SQLHelper helper = new SQLHelper())
            {
                SQLiteCommand cmd = helper.CriarComando();
                SQLiteDataReader dr;

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = string.Format("SELECT * FROM JOGADOR WHERE CODIGO = {0}", cod);

                dr = helper.ExecutaReader(cmd);

                while (dr.Read())
                {
                    this.Codigo = int.Parse(dr["CODIGO"].ToString());
                    this.Nome = dr["NOME"].ToString();
                    this.Imagem = dr["IMAGEM"].ToString();
                    this.Time = (dr["EQUIPE"].ToString() == "A" ? Equipe.A : Equipe.B);
                    this.categoria = (string.IsNullOrWhiteSpace(dr["CATEGORIA"].ToString()) ? -1 : int.Parse(dr["CATEGORIA"].ToString()));
                }
            }
        }

        public void Salvar()
        {
            using (SQLHelper helper = new SQLHelper())
            {
                SQLiteCommand cmd = helper.CriarComando();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.Add(helper.CriarParametro("@nome", this.Nome));
                cmd.Parameters.Add(helper.CriarParametro("@equipe", (this.Time == Equipe.A ? "A" : "B")));
                cmd.Parameters.Add(helper.CriarParametro("@imagem", this.Imagem));
                cmd.Parameters.Add(helper.CriarParametro("@categoria", this.categoria));

                if (this.Codigo.HasValue)
                {
                    cmd.CommandText = "Update Jogador Set Nome = @nome, Equipe = @equipe, Imagem = @imagem, Categoria = @categoria where codigo = @codigo;";
                    cmd.Parameters.Add(helper.CriarParametro("@codigo", this.Codigo));
                }
                else
                {
                    cmd.CommandText = "Insert into Jogador (nome, equipe, imagem, categoria) Values (@nome, @equipe, @imagem, @categoria);";
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
                    cmd.CommandText = "Delete from jogador where codigo = @codigo";
                    cmd.Parameters.Add(helper.CriarParametro("@codigo", this.Codigo));
                }
                else
                {
                    cmd.CommandText = "Delete from jogador where codigo > -1;";
                }

                helper.PersistirDados(cmd);
            }
        }

        public static List<Jogador> Todos()
        {
            List<Jogador> r = new List<Jogador>();

            using (SQLHelper helper = new SQLHelper())
            {
                SQLiteCommand cmd = helper.CriarComando();
                SQLiteDataReader dr;

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = string.Format("SELECT * FROM JOGADOR");

                dr = helper.ExecutaReader(cmd);

                while (dr.Read())
                {
                    Jogador player = new PerguntasERespostas.Jogador();
                    player.Codigo = int.Parse(dr["CODIGO"].ToString());
                    player.Nome = dr["NOME"].ToString();
                    player.Imagem = dr["IMAGEM"].ToString();
                    player.Time = (dr["EQUIPE"].ToString() == "A" ? Equipe.A : Equipe.B);
                    player.categoria = (string.IsNullOrWhiteSpace(dr["CATEGORIA"].ToString()) ? -1 : int.Parse(dr["CATEGORIA"].ToString()));

                    r.Add(player);
                }
            }

            return r;
        }
    }



    public enum Equipe
    {
        A,
        B
    }
}
