using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Diagnostics;

namespace PerguntasERespostas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int placarEquipeA, placarEquipeB;
        bool jogoEmAndamento;
        Equipe jogadorAtual;

        public bool encerrar;

        List<Questoes> lstPerguntas;
        Dictionary<int, string> DicCategoria;
        Questoes pergunta;

        List<Jogador> lstJogadores;
        Jogador JogadorA;
        Jogador JogadorB;

        Menu _menu;
        Image ajudaTemp;

        int _maxPerguntas;
        List<int> lstCategorias;

        ModoJogo _modoJogo;

        bool exibirAjudar1, exibirAjudar2, exibirAjudar3, exibirAjudar4;

        public List<Relatorio> estatisticas;

        public enum ModoJogo
        {
            Eliminativo,
            MaisAcertos
        }

        public MainWindow(Menu m, int maxPerguntas, List<int> cat, ModoJogo modoJogo)
        {
            _menu = m;
            lstCategorias = cat;
            _maxPerguntas = maxPerguntas;
            _modoJogo = modoJogo;
            encerrar = false;

            dispatcherTimer.Tick += new EventHandler(dt_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);

            InitializeComponent();
            IniciarPartida();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {

        }

        private void btnSair_Click(object sender, RoutedEventArgs e)
        {
            if (jogoEmAndamento &&
                MessageBox.Show("Tem certeza que deseja encerrar essa partida? Todo progresso será perdido.", "Atenção", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.No)
                return;

            stopWatch.Stop();
            dispatcherTimer.Stop();
            

            try
            {
                // Grava relatório de estatistica
                if (estatisticas != null && estatisticas.Count > 0)
                {
                    if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"Relatorio"))
                        Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"Relatorio");

                    string nomeArq = AppDomain.CurrentDomain.BaseDirectory + @"Relatorio\Relatorio_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".csv";
                    using (StreamWriter sw = new StreamWriter(nomeArq, true, System.Text.Encoding.UTF8))
                    {
                        string linha;
                        sw.WriteLine("Equipe;Categoria;Acertou;Pergunta;Quem Respondeu;Usou Ajuda;Data;");

                        foreach (var item in estatisticas.Where(x => x.Equipe == Equipe.A).ToList())
                        {
                            linha = (item.Equipe == Equipe.A ? "A" : "B") + ";" + item.Categoria + ";" + (item.IsAcertou == true ? "Sim" : "Não") + ";" + item.Pergunta + ";" + item.QuemRespondeu + ";" + (item.UsouAjuda == true ? "Sim" : "Não") + ";" + DateTime.Now.ToString();
                            sw.WriteLine(linha);
                        }

                        foreach (var item in estatisticas.Where(x => x.Equipe == Equipe.B).ToList())
                        {
                            linha = (item.Equipe == Equipe.A ? "A" : "B") + ";" + item.Categoria + ";" + (item.IsAcertou == true ? "Sim" : "Não") + ";" + item.Pergunta + ";" + item.QuemRespondeu + ";" + (item.UsouAjuda == true ? "Sim" : "Não") + ";" + DateTime.Now.ToString();
                            sw.WriteLine(linha);
                        }

                        sw.Close();
                    }
                }
            }
            catch (Exception ex) { throw; }

            _menu.Show();
            this.Close();
        }

        private string GetResposta()
        {
            if (RespostaA != null && RespostaA.IsChecked.Value)
                return pergunta.Respostas[0].ToString();
            else if (RespostaB != null && RespostaB.IsChecked.Value)
                return pergunta.Respostas[1].ToString();
            else if (RespostaC != null && RespostaC.IsChecked.Value)
                return pergunta.Respostas[2].ToString();
            else if (RespostaD != null && RespostaD.IsChecked.Value)
                return pergunta.Respostas[3].ToString();
            else
                return string.Empty;
        }

        private int ajuadaA = 0, ajuadaB = 0;

        private void btnResponder_Click(object sender, RoutedEventArgs e)
        {
            if (!jogoEmAndamento)
                return;

            Relatorio rel = new Relatorio();

            if (jogadorAtual == Equipe.A)
                JogadorA.JaJogou = true;
            else
                JogadorB.JaJogou = true;

            rel.Pergunta = pergunta.Pergunta;
            rel.QuemRespondeu = (jogadorAtual == Equipe.A ? JogadorA.Nome : JogadorB.Nome);
            rel.Categoria = DicCategoria[pergunta.CodCategoria].ToString();
            rel.Equipe = jogadorAtual;

            if (jogadorAtual == Equipe.A &&
                ajuadaA != lstJogadores.Count(x => x.Time == Equipe.A && x.Ajudou == true))
            {
                ajuadaA++;
                rel.UsouAjuda = true;
            }
            else if (jogadorAtual == Equipe.B &&
                ajuadaB != lstJogadores.Count(x => x.Time == Equipe.B && x.Ajudou == true))
            {
                rel.UsouAjuda = true;
                ajuadaB++;
            }

            if (GetResposta() == pergunta.RespostaCorreta)
            {
                rel.IsAcertou = true;
                var al = new Mensagem("Resposta Certa", 2);
                al.ShowDialog();

                if (_modoJogo == ModoJogo.MaisAcertos)
                {
                    if (jogadorAtual == Equipe.A)
                        placarEquipeA++;
                    else
                        placarEquipeB++;
                }
                else
                {
                    placarEquipeA = lstJogadores.Count(x => x.Time == Equipe.A && x.Eliminado == false);
                    placarEquipeB = lstJogadores.Count(x => x.Time == Equipe.B && x.Eliminado == false);
                }

            }
            else
            {
                rel.IsAcertou = false;
                if (_modoJogo == ModoJogo.Eliminativo)
                {
                    if (jogadorAtual == Equipe.A)
                        JogadorA.Eliminado = true;
                    else
                        JogadorB.Eliminado = true;

                    placarEquipeA = lstJogadores.Count(x => x.Time == Equipe.A && x.Eliminado == false);
                    placarEquipeB = lstJogadores.Count(x => x.Time == Equipe.B && x.Eliminado == false);
                }

                var al = new Mensagem("Resposta Errada", 2);
                al.ShowDialog();

            }

            if (ajudaTemp != null)
                ajudaTemp.Opacity = 50.0;

            pergunta.Respondida = true;

            estatisticas.Add(rel);
            ProximaPergunta();
        }

        private void ProximaPergunta()
        {
            jogoEmAndamento = true;
            CarregarPlacar();

            if (_modoJogo == ModoJogo.MaisAcertos)
                if (lstPerguntas != null && lstPerguntas.Any(x => x.Respondida == false))
                {
                    var question = lstPerguntas.Where(x => x.Respondida == false).ToList();
                    Random r = new Random();
                    pergunta = question[(int)r.Next(question.Count)];

                    CarregarPergunta();
                }
                else
                {
                    stopWatch.Stop();
                    dispatcherTimer.Stop();

                    Mensagem2 al;
                    if (placarEquipeA != placarEquipeB)
                        al = new Mensagem2(string.Format("Equipe {0} venceu!", (placarEquipeA > placarEquipeB ? "A" : "B")), string.Format("{0} x {1}", placarEquipeA, placarEquipeB), 30, lstJogadores.Where(x => x.Time == (placarEquipeA > placarEquipeB ? Equipe.A : Equipe.B)).ToList());
                    else
                        al = new Mensagem2(string.Format("Empate"), string.Format("{0} x {1}", placarEquipeA, placarEquipeB), 30, lstJogadores);
                    al.ShowDialog();

                    jogoEmAndamento = false;
                }

            if (_modoJogo == ModoJogo.Eliminativo)
                if (lstPerguntas != null && lstPerguntas.Any(x => x.Respondida == false)
                    && lstJogadores.Count(x => x.Time == Equipe.A && x.JaJogou == false) > 0
                    && lstJogadores.Count(x => x.Time == Equipe.B && x.JaJogou == false) > 0)
                {
                    var question = lstPerguntas.Where(x => x.Respondida == false).ToList();
                    Random r = new Random();
                    pergunta = question[(int)r.Next(question.Count)];

                    CarregarPergunta();
                }
                else
                {
                    Mensagem2 al;
                    if (placarEquipeA != placarEquipeB)
                        al = new Mensagem2(string.Format("Equipe {0} venceu!", (placarEquipeA > placarEquipeB ? "A" : "B")), string.Format("{0} x {1}", placarEquipeA, placarEquipeB), 30, lstJogadores.Where(x => x.Time == (placarEquipeA > placarEquipeB ? Equipe.A : Equipe.B)).ToList());
                    else
                        al = new Mensagem2(string.Format("Empate"), string.Format("{0} x {1}", placarEquipeA, placarEquipeB), 30, lstJogadores);
                    al.ShowDialog();

                    jogoEmAndamento = false;
                    encerrar = true;

                    _menu.Show();
                    this.Close();

                }
        }

        private void CarregarPlacar()
        {
            // Ajustando Placar
            lblPlacar.Content = String.Format("{0} x {1}", placarEquipeA, placarEquipeB);
        }

        private void RespostaA_Click(object sender, RoutedEventArgs e)
        {
            btnResponder.IsEnabled = true;
        }

        double Aw, Ah, Bw, Bh;

        private void CarregarPergunta()
        {
            if (JogadorA == null || JogadorB == null || (JogadorA.JaJogou && JogadorB.JaJogou))
                SorteiaProximoJogador();

            // Ajusta jogador
            if (jogadorAtual == Equipe.A)
                jogadorAtual = Equipe.B;
            else
                jogadorAtual = Equipe.A;

            // Definindo tamanhos padrao;
            borderA.Width = Aw;
            borderA.Height = Ah;
            borderB.Width = Bw;
            borderB.Height = Bh;

            //Height="164" Width="163"
            borderA1.Height = 164;
            borderA1.Width = 163;
            borderA2.Height = 164;
            borderA2.Width = 163;

            borderB1.Height = 164;
            borderB1.Width = 163;
            borderB2.Height = 164;
            borderB2.Width = 163;

            // Ajustando Cores
            if (jogadorAtual == Equipe.A)
            {
                borderA.BorderBrush = Brushes.Red;
                borderA.BorderThickness = new Thickness(3);
                borderA1.BorderBrush = Brushes.Red;
                borderA1.BorderThickness = new Thickness(3);
                borderA2.BorderBrush = Brushes.Red;
                borderA2.BorderThickness = new Thickness(3);


                borderB.BorderBrush = Brushes.Black;
                borderB.Width -= borderB.Width / 2 * 0.5;
                borderB.Height -= borderB.Height / 2 * 0.5;
                borderB1.BorderBrush = Brushes.Black;
                //borderB1.Width = borderB1.Width / 2;
                //borderB1.Height = borderB1.Height / 2;
                borderB2.BorderBrush = Brushes.Black;
                //borderB2.Width = borderB2.Width / 2;
                //borderB2.Height = borderB2.Height / 2;

                borderB.BorderThickness = new Thickness(1);
                borderB1.BorderThickness = new Thickness(1);
                borderB2.BorderThickness = new Thickness(1);

                borderA1.Visibility = Visibility.Visible;
                borderA2.Visibility = Visibility.Visible;
                borderB1.Visibility = Visibility.Hidden;
                borderB2.Visibility = Visibility.Hidden;

            }
            else
            {
                borderA.BorderBrush = Brushes.Black;
                borderA.Width -= borderA.Width / 2 * 0.5;
                borderA.Height -= borderA.Height / 2 * 0.5;
                borderA1.BorderBrush = Brushes.Black;
                //borderA1.Width = borderA1.Width / 2;
                //borderA1.Height = borderA1.Height / 2;
                borderA2.BorderBrush = Brushes.Black;
                //borderA2.Width = borderA2.Width / 2;
                //borderA2.Height = borderA2.Height / 2;

                borderA.BorderThickness = new Thickness(1);
                borderA1.BorderThickness = new Thickness(1);
                borderA2.BorderThickness = new Thickness(1);

                borderB.BorderBrush = Brushes.Red;
                borderB1.BorderBrush = Brushes.Red;
                borderB2.BorderBrush = Brushes.Red;

                borderB.BorderThickness = new Thickness(3);
                borderB1.BorderThickness = new Thickness(3);
                borderB2.BorderThickness = new Thickness(3);

                borderA1.Visibility = Visibility.Hidden;
                borderA2.Visibility = Visibility.Hidden;
                borderB1.Visibility = Visibility.Visible;
                borderB2.Visibility = Visibility.Visible;

            }

            // Carregar Jogador
            if (System.IO.File.Exists(JogadorA.Imagem))
                imgJogadorA.Source = new BitmapImage(new Uri(JogadorA.Imagem));

            if (System.IO.File.Exists(JogadorB.Imagem))
                imgJogadorB.Source = new BitmapImage(new Uri(JogadorB.Imagem));

            // Ajustando Placar
            CarregarPlacar();

            // Carrega Pergunta
            var z = lstPerguntas.Where((x => x.Respondida == false)).ToList();
            bool flag = true;
            while (flag)
            {
                Random r = new Random();
                pergunta = z[r.Next(z.Count)];

                if (pergunta.Respostas.Count != 4)
                    lstPerguntas.Remove(pergunta);
                else
                {
                    flag = false;
                    lblPergunta.Content = pergunta.Pergunta;
                }

                if (lstPerguntas.Count == 0)
                    return;
            }

            // Carrega Respostas
            RespostaA.Content = String.Format("a) {0}", pergunta.Respostas[0]); RespostaA.IsChecked = false;
            RespostaB.Content = String.Format("b) {0}", pergunta.Respostas[1]); RespostaB.IsChecked = false;
            RespostaC.Content = String.Format("c) {0}", pergunta.Respostas[2]); RespostaC.IsChecked = false;
            RespostaD.Content = String.Format("d) {0}", pergunta.Respostas[3]); RespostaD.IsChecked = false;

            btnResponder.IsEnabled = false;

            lblx1.Visibility = (exibirAjudar1 && borderA1.Visibility == Visibility.Visible ? Visibility.Visible : Visibility.Hidden);
            lblx2.Visibility = (exibirAjudar2 && borderA2.Visibility == Visibility.Visible ? Visibility.Visible : Visibility.Hidden);
            lblx3.Visibility = (exibirAjudar3 && borderB1.Visibility == Visibility.Visible ? Visibility.Visible : Visibility.Hidden);
            lblx4.Visibility = (exibirAjudar4 && borderB2.Visibility == Visibility.Visible ? Visibility.Visible : Visibility.Hidden);

            stopWatch.Reset();
            lblTempo.Content = "00";
            btnTempo.Content = "Start";
            //stopWatch.Start();
            //dispatcherTimer.Start();

        }

        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        Stopwatch stopWatch = new Stopwatch();
        int currentTime = 0;

        void dt_Tick(object sender, EventArgs e)
        {
            if (stopWatch.IsRunning)
            {
                TimeSpan ts = stopWatch.Elapsed;
                currentTime = ts.Seconds;
                lblTempo.Content = (30 - currentTime);

                if(currentTime >= 29)
                {
                    var al = new Mensagem("Tempo esgotado...", 2);
                    al.ShowDialog();
                    btnResponder_Click(null, null);
                }
            }
        }


        private static List<Questoes> _CarregarRelacaoDePerguntas;
        public static List<Questoes> CarregarRelacaoDePerguntas(int numMaxPerguntas, List<int> categorias)
        {
            _CarregarRelacaoDePerguntas = new List<Questoes>();

            foreach (var i in PerguntasERespostas.Pergunta.TodasQuestoes(numMaxPerguntas, categorias))
            {
                Questoes q = new PerguntasERespostas.Questoes();
                q.Pergunta = i.pergunta;
                q.RespostaCorreta = i.RespCorreta;
                q.CodCategoria = i.categoria;
                q.Respostas = new List<string>();

                q.Respostas.Add(i.RespA);
                q.Respostas.Add(i.RespB);
                q.Respostas.Add(i.RespC);
                q.Respostas.Add(i.RespD);

                _CarregarRelacaoDePerguntas.Add(q);
            }

            return _CarregarRelacaoDePerguntas;
        }

        private static List<Jogador> _CarregarRelacaoDeJogadores;

        private void btnTempo_Click(object sender, RoutedEventArgs e)
        {
            if (stopWatch.IsRunning)
            {
                btnTempo.Content = "Continuar";
                stopWatch.Stop();
                dispatcherTimer.Stop();
            }
            else
            {
                stopWatch.Start();
                dispatcherTimer.Start();
                btnTempo.Content = "Pausar";
            }              
        }

        public static List<Jogador> CarregarRelacaoDeJogadores()
        {
            _CarregarRelacaoDeJogadores = new List<Jogador>();
            _CarregarRelacaoDeJogadores = PerguntasERespostas.Jogador.Todos();

            return _CarregarRelacaoDeJogadores;
        }

        private void IniciarPartida()
        {
            exibirAjudar1 = false;
            exibirAjudar2 = false;
            exibirAjudar3 = false;
            exibirAjudar4 = false;

            lblx1.Visibility = Visibility.Hidden;
            lblx2.Visibility = Visibility.Hidden;
            lblx3.Visibility = Visibility.Hidden;
            lblx4.Visibility = Visibility.Hidden;

            estatisticas = new List<Relatorio>();

            DicCategoria = Categoria.CarregarCategorias();
            lstPerguntas = CarregarRelacaoDePerguntas(_maxPerguntas, lstCategorias);
            lstJogadores = CarregarRelacaoDeJogadores();

            try
            {
                imgAjudaA1.Source = new BitmapImage(new Uri(@"/Image/help.jpg", UriKind.Relative));
                imgAjudaA2.Source = new BitmapImage(new Uri(@"/Image/help.jpg", UriKind.Relative));
                imgAjudaB1.Source = new BitmapImage(new Uri(@"/Image/help.jpg", UriKind.Relative));
                imgAjudaB2.Source = new BitmapImage(new Uri(@"/Image/help.jpg", UriKind.Relative));
            }
            catch { }

            ajudaTemp = null;
            jogoEmAndamento = true;

            if (_modoJogo == ModoJogo.Eliminativo)
            {
                placarEquipeA = lstJogadores.Count(x => x.Time == Equipe.A && x.Eliminado == false);
                placarEquipeB = lstJogadores.Count(x => x.Time == Equipe.B && x.Eliminado == false);
            }
            else
            {
                placarEquipeA = 0;
                placarEquipeB = 0;
            }

            // Guarda Tamanhos
            Aw = borderA.Width;
            Ah = borderA.Height;
            Bw = borderB.Width;
            Bh = borderB.Height;

            SorteiaProximoJogador();
            CarregarPergunta();
        }

        private void imgAjudaA1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (jogadorAtual == Equipe.A)
            {
                var z = SorteiaAjuda(Equipe.A);
                if (!String.IsNullOrWhiteSpace(z) && System.IO.File.Exists(z))
                {
                    imgAjudaA1.Source = new BitmapImage(new Uri(z));
                    ajudaTemp = imgAjudaA1;
                    exibirAjudar1 = true;
                }
            }
        }

        private void imgAjudaA2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (jogadorAtual == Equipe.A)
            {
                var z = SorteiaAjuda(Equipe.A);
                if (!String.IsNullOrWhiteSpace(z) && System.IO.File.Exists(z))
                {
                    imgAjudaA2.Source = new BitmapImage(new Uri(z));
                    ajudaTemp = imgAjudaA2;
                    exibirAjudar2 = true;
                }
            }
        }

        private void imgAjudaB1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (jogadorAtual == Equipe.B)
            {
                var z = SorteiaAjuda(Equipe.B);
                if (!String.IsNullOrWhiteSpace(z) && System.IO.File.Exists(z))
                {
                    imgAjudaB1.Source = new BitmapImage(new Uri(z));
                    ajudaTemp = imgAjudaB1;
                    exibirAjudar3 = true;
                }
            }
        }

        private void imgAjudaB2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (jogadorAtual == Equipe.B)
            {
                var z = SorteiaAjuda(Equipe.B);
                if (!String.IsNullOrWhiteSpace(z) && System.IO.File.Exists(z))
                {
                    imgAjudaB2.Source = new BitmapImage(new Uri(z));
                    ajudaTemp = imgAjudaB2;
                    exibirAjudar4 = true;
                }
            }
        }

        private void SorteiaProximoJogador()
        {
            lblx1.Visibility = (exibirAjudar1 && borderA1.Visibility == Visibility.Visible ? Visibility.Visible : Visibility.Hidden);
            lblx2.Visibility = (exibirAjudar2 && borderA2.Visibility == Visibility.Visible ? Visibility.Visible : Visibility.Hidden);
            lblx3.Visibility = (exibirAjudar3 && borderB1.Visibility == Visibility.Visible ? Visibility.Visible : Visibility.Hidden);
            lblx4.Visibility = (exibirAjudar4 && borderB2.Visibility == Visibility.Visible ? Visibility.Visible : Visibility.Hidden);

            if (lstJogadores.Any(x => x.JaJogou == false && x.Eliminado == false))
            {
                var timeA = lstJogadores.Where((x => x.JaJogou == false && x.Time == Equipe.A)).ToList();
                var timeB = lstJogadores.Where((x => x.JaJogou == false && x.Time == Equipe.B)).ToList();

                Random r = new Random();
                JogadorA = timeA[(int)r.Next(timeA.Count)];
                JogadorB = timeB[(int)r.Next(timeB.Count)];
            }
        }

        private string SorteiaAjuda(Equipe equi)
        {
            if (lstJogadores.Count(x => x.Time == equi && x.Ajudou == true) < 2)
            {
                var player = (equi == Equipe.A ? JogadorA : JogadorB);

                var time = lstJogadores.Where((x => x.Time == equi
                                                    && x.Ajudou == false
                                                    && x.Codigo != player.Codigo)).ToList();
                if (time != null && time.Count > 0)
                {
                    Random r = new Random();
                    var jogador = time[(int)r.Next(time.Count)];
                    jogador.Ajudou = true;
                    return jogador.Imagem;
                }
            }
            return string.Empty;
        }
    }
}
