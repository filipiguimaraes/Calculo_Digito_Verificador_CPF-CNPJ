using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalisadorCPFeCNPJ.Util
{
    class EscritorArquivo
    {
        private StreamWriter _StreamWriter;

        public void EscreverLista(string caminhoArquivo, string nomeArquivo, List<string> listaString)
        {
            try
            {
                using (_StreamWriter = new StreamWriter(string.Concat(caminhoArquivo, nomeArquivo)))
                {
                    foreach (string item in listaString)
                    {
                        _StreamWriter.WriteLine(item);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
