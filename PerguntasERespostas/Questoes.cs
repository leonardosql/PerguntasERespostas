using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerguntasERespostas
{
    public class Questoes
    {
        public string Pergunta { get; set; }
        public List<string> Respostas { get; set; }
        public string RespostaCorreta { get; set; }
        public bool Respondida { get; set; }        
        public int CodCategoria { get; set; }
    }
}
