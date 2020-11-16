using AnalisadorCPFeCNPJ.BLL;
using AnalisadorCPFeCNPJ.Interfaces;
using AnalisadorCPFeCNPJ.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AnalisadorCPFeCNPJ
{
    class Program
    {
        private static string _CaminhoEntrada, _CaminhoSaida, _NomeArquivoSaidaCPFs, _NomeArquivoSaidaCNPJs;
        private static List<string> _ListaCPFs, _ListaCNPJs;
        private static IValidadorDigito _ValidadorCPF, _ValidadorCNPJ;
        private static LeitorArquivo _LeitorArquivo;
        private static EscritorArquivo _EscritorArquivo;
        private static Stopwatch _TempoCPF, _TempoCNPJ, _TempoLeituraArquivo, _TempoTotal;

        static void Main(string[] args)
        {
            _TempoCPF = new Stopwatch();
            _TempoCNPJ = new Stopwatch();
            _TempoLeituraArquivo = new Stopwatch();
            _TempoTotal = new Stopwatch();
            _TempoTotal.Start();

            _ListaCPFs = new List<string>();
            _ListaCNPJs = new List<string>();
            _ValidadorCPF = new ValidadorCPF(_ListaCPFs);
            _ValidadorCNPJ = new ValidadorCNPJ(_ListaCNPJs);
            _CaminhoEntrada = ConfigurationManager.AppSettings["CaminhoArquivoEntrada"].ToString();
            _CaminhoSaida = ConfigurationManager.AppSettings["CaminhoArquivoSaida"].ToString();
            _NomeArquivoSaidaCPFs = ConfigurationManager.AppSettings["NomeArquivoSaidaCPFs"].ToString();
            _NomeArquivoSaidaCNPJs = ConfigurationManager.AppSettings["NomeArquivoSaidaCNPJs"].ToString();
            _LeitorArquivo = new LeitorArquivo(@_CaminhoEntrada, _ListaCPFs, _ListaCNPJs);
            _EscritorArquivo = new EscritorArquivo();

            Console.WriteLine("------------------------- Inicio Leitura --------------------------");
            _TempoLeituraArquivo.Start();
            _LeitorArquivo.LerArquivo();
            _TempoLeituraArquivo.Stop();
            Console.WriteLine("------------------------- Termino Leitura -------------------------");

            Thread threadCPF = new Thread(new ThreadStart(RunCPF));
            Thread threadCNPJ = new Thread(new ThreadStart(RunCNPJ));
            threadCPF.Name = "Thread 1 - Calculo CPF";
            threadCNPJ.Name = "Thread 2 - Calculo CNPJ";
            threadCPF.Start();
            threadCNPJ.Start();

            Console.WriteLine("------------------------- Esperando Thread 1 ----------------------");
            threadCPF.Join();
            Console.WriteLine("------------------------- Esperando Thread 2 ----------------------");
            threadCNPJ.Join();
            _TempoTotal.Stop();
            Console.WriteLine(string.Format("Tempo Leitura do arquivo: {0}", _TempoLeituraArquivo.Elapsed));
            Console.WriteLine(string.Format("Tempo Calculo dos CPFs  : {0}", _TempoCPF.Elapsed));
            Console.WriteLine(string.Format("Tempo Calculo dos CNPJs : {0}", _TempoCNPJ.Elapsed));
            Console.WriteLine(string.Format("Tempo Total Execucao    : {0}", _TempoTotal.Elapsed));

            Console.WriteLine("------------------------- Inicio Escrita --------------------------");
            _EscritorArquivo.EscreverLista(@_CaminhoSaida, _NomeArquivoSaidaCPFs, _ValidadorCPF.GetListaComDigitos());
            _EscritorArquivo.EscreverLista(@_CaminhoSaida, _NomeArquivoSaidaCNPJs, _ValidadorCNPJ.GetListaComDigitos());
            Console.WriteLine("------------------------- Termino Escrita -------------------------");
            Console.WriteLine("------------------------- Finalizado ------------------------------");

            Console.ReadKey();
        }

        static void RunCPF()
        {
            _TempoCPF.Start();
            Console.WriteLine("------------------------- Inicio Calculo CPF ----------------------");
            _ValidadorCPF.Calcular();
            Console.WriteLine("------------------------- Termino Calculo CPF ---------------------");
            _TempoCPF.Stop();
        }

        static void RunCNPJ()
        {
            _TempoCNPJ.Start();
            Console.WriteLine("------------------------- Inicio Calculo CNPJ ---------------------");
            _ValidadorCNPJ.Calcular();
            Console.WriteLine("------------------------- Termino Calculo CNPJ --------------------");
            _TempoCNPJ.Stop();
        }
    }
}
