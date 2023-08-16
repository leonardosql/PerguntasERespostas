using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
using System.Configuration;
using System.Data.SQLite;

namespace PerguntasERespostas
{
    public class Enums
    {
        public enum TipoConexão
        {
            MDF = 1,
            SERVIDOR = 2
        }
    }
    public class Constantes
    {
        public const string strConexaoMDF = @"Data Source=""{0}""";
        public const string nomeArquivoBase = "PerguntasRespostas.s3db";
    }

    public class SQLHelper : IDisposable
    {
        private static SQLiteConnection _conn;
        private static SQLiteTransaction _transacao;

        public SQLHelper()
        {
            _conn = new SQLiteConnection(string.Format(Constantes.strConexaoMDF, RecuperaDiretorioExecucao(Constantes.nomeArquivoBase)));
            _conn.Open();
            _transacao = _conn.BeginTransaction();
        }

        public SQLiteCommand CriarComando()
        {
            return _conn.CreateCommand();
        }

        public SQLiteParameter CriarParametro(string strParametro, object valor)
        {
            return new SQLiteParameter(strParametro, valor);
        }

        public void PersistirDados(SQLiteCommand cmd)
        {
            try
            {
                cmd.Transaction = _transacao;
                cmd.ExecuteNonQuery();
                _transacao.Commit();
            }
            catch (Exception ex)
            {
                _transacao.Rollback();
                throw ex;
            }

        }

        public void PersistirDadosVariosCommands(List<SQLiteCommand> cmds)
        {
            try
            {
                foreach (SQLiteCommand cmd in cmds)
                {
                    cmd.Transaction = _transacao;
                    cmd.ExecuteNonQuery();
                }
                _transacao.Commit();
            }
            catch (Exception ex)
            {
                _transacao.Rollback();
                throw ex;
            }

        }

        public DataTable RetornaDataTable(SQLiteCommand cmd)
        {
            DataTable retornoDt = new DataTable();
            try
            {
                cmd.Transaction = _transacao;
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
                adapter.Fill(retornoDt);
                return retornoDt;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// RetornaDataTable
        /// </summary>
        /// <param name="cmd"> Comando</param>
        /// <param name="QtdRegRet">Qtd. registros retornados</param>
        /// <returns></returns>
        public DataTable RetornaDataTable(SQLiteCommand cmd, ref int QtdRegRet)
        {
            DataTable retornoDt = new DataTable();
            try
            {
                cmd.Transaction = _transacao;
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
                adapter.Fill(retornoDt);
                QtdRegRet = retornoDt.Rows.Count;
                return retornoDt;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public SQLiteDataReader ExecutaReader(SQLiteCommand cmd)
        {
            DataTable retornoDt = new DataTable();
            try
            {
                cmd.Transaction = _transacao;
                SQLiteDataReader reader = cmd.ExecuteReader();

                return reader;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public object ExecutaEscalar(SQLiteCommand cmd)
        {
            object aux;
            try
            {
                cmd.Transaction = _transacao;
                aux = cmd.ExecuteScalar();
                _transacao.Commit();
                return aux;
            }
            catch (Exception ex)
            {
                _transacao.Rollback();
                throw ex;
            }
        }

        public void Dispose()
        {
            _transacao.Dispose();
            if (_conn.State == System.Data.ConnectionState.Open)
            {
                _conn.Close();
            }
            _conn.Dispose();
        }

        public static string RecuperaDiretorioExecucao(string nomeArquivo)
        {
            string sCaminho = ConfigurationManager.AppSettings["CAMINHO_BD"];
            if (!string.IsNullOrWhiteSpace(sCaminho))
            {
                return System.IO.Path.Combine(sCaminho, nomeArquivo);
            }
            else
            {
                String strAppDir = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).ToString();
                String strFullPathToMyFile = System.IO.Path.Combine(strAppDir, nomeArquivo);
                return strFullPathToMyFile;
            }
        }

        public static bool VerificaSeExisteArquivoDeBancoDeDados()
        {
            string sCaminho = ConfigurationManager.AppSettings["CAMINHO_BD"];
            if (!string.IsNullOrWhiteSpace(sCaminho))            
                return System.IO.File.Exists(System.IO.Path.Combine(sCaminho, Constantes.nomeArquivoBase));            
            else
            {
                String strAppDir = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).ToString();
                String strFullPathToMyFile = System.IO.Path.Combine(strAppDir, Constantes.nomeArquivoBase);
                return System.IO.File.Exists(strFullPathToMyFile);
            }
        }


    }
}
