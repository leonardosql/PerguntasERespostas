using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerguntasERespostas
{
    public class Relatorio
    {
        public string Pergunta { get; set; }
        public bool IsAcertou { get; set; }
        public string QuemRespondeu { get; set; }
        public bool UsouAjuda { get; set; }
        public string Categoria { get; set; }
        public Equipe Equipe { get; set; }

        // Equipe;Categoria;Acertou;Pergunta;Quem Respondeu; Usou Ajuda; Data;
    }
}
