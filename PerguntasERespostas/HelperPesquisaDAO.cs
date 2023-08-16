using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerguntasERespostas
{
    public class HelperPesquisaDAO
    {
        private string tabela;
        private string colunaCodigo;
        private string colunaDescricao;
        private string colunaNome;
        private string filtro;
        private int qtdReg;
        private string[] param;
        private string consultaSQL;

        public int QtdReg
        {
            get { return this.qtdReg; }
        }

        public HelperPesquisaDAO(string _tabela, string _colunaCodigo, string _colunaNome, string _colunaDescricao, string where = "")
        {
            tabela = _tabela;
            filtro = where;
            colunaCodigo = _colunaCodigo;
            colunaDescricao = _colunaDescricao;
            colunaNome = _colunaNome;
            qtdReg = 0;
        }

        public HelperPesquisaDAO(string consulta)
        {
            this.consultaSQL = consulta;
        }


        public HelperPesquisaDAO(string[] parametros, string _tabela, string where = "")
        {
            tabela = _tabela;
            filtro = where;
            param = parametros;
            qtdReg = 0;
        }

        public DataTable Carregar()
        {
            qtdReg = 0;
            using (SQLHelper helper = new SQLHelper())
            {
                SQLiteCommand cmd = helper.CriarComando();

                if (!String.IsNullOrWhiteSpace(this.consultaSQL))
                    cmd.CommandText = consultaSQL;
                else if (param.Length <= 0)
                    cmd.CommandText = String.Format(My.Resources.Resources.PesquisaHelper, this.colunaCodigo, this.colunaNome, this.colunaDescricao, this.tabela);
                else
                    cmd.CommandText = String.Format(My.Resources.Resources.PesquisaHelper2, string.Join(",", this.param), this.tabela, this.filtro);

                return helper.RetornaDataTable(cmd, ref qtdReg);
            }
        }
    }
}
