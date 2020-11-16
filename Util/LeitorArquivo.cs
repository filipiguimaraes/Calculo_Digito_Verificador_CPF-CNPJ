using System;
using System.Collections.Generic;
using System.IO;

namespace AnalisadorCPFeCNPJ.Util
{
    public class LeitorArquivo
    {
        StreamReader _Arquivo;
        readonly List<string> _ListaCPFs, _ListaCNPJs;


        public LeitorArquivo(string @caminhoArquivo)
        {
            if (File.Exists(@caminhoArquivo))
            {
                try
                {
                    _Arquivo = new StreamReader(@caminhoArquivo);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            else
            {
                Console.WriteLine(string.Format("Arquivo não encontrado. \n Caminho: {0} ", @caminhoArquivo));
            }
        }

        public LeitorArquivo(string @caminhoArquivo, List<string> listaCPFs, List<string> listaCNPJs)
        {
            if (File.Exists(@caminhoArquivo))
            {
                try
                {
                    _Arquivo = new StreamReader(@caminhoArquivo);
                    _ListaCPFs = listaCPFs;
                    _ListaCNPJs = listaCNPJs;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            else
            {
                Console.WriteLine(string.Format("Arquivo não encontrado. \n Caminho: {0} ", @caminhoArquivo));
            }
        }

        public int LerArquivo()
        {
            int qtdRegistros = 0;

            if (_Arquivo != null)
            {
                using (_Arquivo)
                {
                    string linha;
                    int count = 0;
                    while ((linha = _Arquivo.ReadLine()) != null)
                    {
                        linha = linha.TrimStart();
                        count++;

                        if (linha.Length == 9) _ListaCPFs.Add(linha);
                        else if (linha.Length == 12) _ListaCNPJs.Add(linha);
                    }
                    Console.WriteLine(string.Format("Total de registros: {0}", count));
                }
            }
            else
            {
                Console.WriteLine("Arquivo não encontrado!");
            }

            return qtdRegistros;
        }
    }
}
