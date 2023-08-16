using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerguntasERespostas
{
    public enum RespostaCorreta
    {
        A,
        B,
        C,
        D
    }

    public class Pergunta
    {

        public int? codigo { get; set; }
        public string pergunta { get; set; }
        public int categoria { get; set; }

        public string RespA { get; set; }
        public string RespB { get; set; }
        public string RespC { get; set; }
        public string RespD { get; set; }
        public string RespCorreta { get; set; }

        public RespostaCorreta GetResposta()
        {
            if (this.RespCorreta == RespA)
                return RespostaCorreta.A;
            else if (this.RespCorreta == RespB)
                return RespostaCorreta.B;
            else if (this.RespCorreta == RespC)
                return RespostaCorreta.C;
            else
                return RespostaCorreta.D;
        }

        public void Carregar(int cod)
        {
            try
            {
                using (SQLHelper helper = new SQLHelper())
                {
                    SQLiteCommand cmd = helper.CriarComando();
                    SQLiteDataReader dr;

                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = string.Format("SELECT * FROM perguntas WHERE CODIGO = {0}", cod);

                    dr = helper.ExecutaReader(cmd);

                    while (dr.Read())
                    {
                        this.codigo = int.Parse(dr["CODIGO"].ToString());
                        this.pergunta = dr["PERGUNTA"].ToString();
                        this.categoria = int.Parse(dr["CATEGORIA"].ToString());
                        this.RespA = dr["RespA"].ToString();
                        this.RespB = dr["RespB"].ToString();
                        this.RespC = dr["RespC"].ToString();
                        this.RespD = dr["RespD"].ToString();
                        this.RespCorreta = dr["RespCorreta"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Salvar()
        {
            try
            {
                // Cadastrando Pergunta
                using (SQLHelper helper = new SQLHelper())
                {
                    SQLiteCommand cmd = helper.CriarComando();
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.Parameters.Add(helper.CriarParametro("@pergunta", this.pergunta));
                    cmd.Parameters.Add(helper.CriarParametro("@categoria", this.categoria));

                    cmd.Parameters.Add(helper.CriarParametro("@RespA", this.RespA));
                    cmd.Parameters.Add(helper.CriarParametro("@RespB", this.RespB));
                    cmd.Parameters.Add(helper.CriarParametro("@RespC", this.RespC));
                    cmd.Parameters.Add(helper.CriarParametro("@RespD", this.RespD));

                    cmd.Parameters.Add(helper.CriarParametro("@RespCorreta", this.RespCorreta));

                    if (this.codigo.HasValue)
                    {
                        cmd.CommandText = "Update Perguntas Set pergunta = @pergunta, categoria = @categoria, RespA = @RespA, RespB = @RespB, RespC = @RespC, RespD = @RespD, RespCorreta = @RespCorreta where codigo = @codigo;";
                        cmd.Parameters.Add(helper.CriarParametro("@codigo", this.codigo));
                    }
                    else
                        cmd.CommandText = "Insert into Perguntas (pergunta, categoria, RespA, RespB, RespC, RespD, RespCorreta) Values (@pergunta, @categoria, @RespA, @RespB, @RespC, @RespD, @RespCorreta);";
                    
                    helper.PersistirDados(cmd);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public void Apagar()
        {
            using (SQLHelper helper = new SQLHelper())
            {
                SQLiteCommand cmd = helper.CriarComando();
                cmd.CommandType = System.Data.CommandType.Text;

                if (this.codigo.HasValue)
                {
                    cmd.CommandText = "Delete from Perguntas where codigo = @codigo";
                    cmd.Parameters.Add(helper.CriarParametro("@codigo", this.codigo));

                    helper.PersistirDados(cmd);
                }
            }
        }

        public void Apagar(int codigo)
        {
            using (SQLHelper helper = new SQLHelper())
            {
                SQLiteCommand cmd = helper.CriarComando();
                cmd.CommandType = System.Data.CommandType.Text;

                if (this.codigo.HasValue)
                {
                    cmd.CommandText = "Delete from Perguntas where codigo = @codigo";
                    cmd.Parameters.Add(helper.CriarParametro("@codigo", codigo));

                    helper.PersistirDados(cmd);
                }
            }
        }

        public static List<Pergunta> TodasQuestoes(int numMaxPerguntas, List<int> categorias)
        {
            List<Pergunta> retorno = new List<PerguntasERespostas.Pergunta>();

            try
            {
                using (SQLHelper helper = new SQLHelper())
                {
                    SQLiteCommand cmd = helper.CriarComando();
                    SQLiteDataReader dr;

                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = string.Format("SELECT * FROM perguntas where categoria in (" + string.Join(",", categorias) + ") limit + " + numMaxPerguntas.ToString());

                    dr = helper.ExecutaReader(cmd);

                    while (dr.Read())
                    {
                        Pergunta p = new Pergunta();
                        p.codigo = int.Parse(dr["CODIGO"].ToString());
                        p.pergunta = dr["PERGUNTA"].ToString();
                        p.categoria = int.Parse(dr["CATEGORIA"].ToString());
                        p.RespA = dr["RespA"].ToString();
                        p.RespB = dr["RespB"].ToString();
                        p.RespC = dr["RespC"].ToString();
                        p.RespD = dr["RespD"].ToString();
                        p.RespCorreta = dr["RespCorreta"].ToString();

                        retorno.Add(p);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return retorno;
        }
    }
}
