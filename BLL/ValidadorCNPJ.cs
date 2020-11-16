using AnalisadorCPFeCNPJ.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalisadorCPFeCNPJ.BLL
{
    class ValidadorCNPJ : IValidadorDigito
    {
        public List<string> _ListCNPJs;
        private List<string> _ListCNPJsComDigito;
        public ValidadorCNPJ(List<string> listCNPJs)
        {
            this._ListCNPJs = listCNPJs;
            _ListCNPJsComDigito = new List<string>();
        }

        public void Calcular()
        {
            if (_ListCNPJs.Count > 0)
            {
                foreach (string cnpj in _ListCNPJs)
                {
                    int primeiroDigitoVerificador = 0;
                    int aux = 2;
                    for (int i = 11; i >= 0; i--)
                    {
                        if (cnpj[i] >= 48 && cnpj[i] <= 57)
                        {
                            int digito = cnpj[i] - 48;
                            primeiroDigitoVerificador += digito * (aux % 10);
                        }
                        aux++;
                        if (aux == 10) aux = 2;
                    }

                    primeiroDigitoVerificador = primeiroDigitoVerificador % 11;
                    primeiroDigitoVerificador = 11 - primeiroDigitoVerificador;
                    if (primeiroDigitoVerificador >= 10) primeiroDigitoVerificador = 0;

                    int segundoDigitoVerificador = 0;
                    aux = 3;
                    for (int i = 11; i >= 0; i--)
                    {
                        if (cnpj[i] >= 48 && cnpj[i] <= 57)
                        {
                            int digito = cnpj[i] - 48;
                            segundoDigitoVerificador += digito * (aux % 10);
                        }
                        aux++;
                        if (aux == 10) aux = 2;
                    }
                    segundoDigitoVerificador += primeiroDigitoVerificador * 2;
                    segundoDigitoVerificador = segundoDigitoVerificador % 11;
                    segundoDigitoVerificador = 11 - segundoDigitoVerificador;
                    if (segundoDigitoVerificador >= 10) segundoDigitoVerificador = 0;

                    _ListCNPJsComDigito.Add(string.Concat(cnpj, primeiroDigitoVerificador, segundoDigitoVerificador));
                }
                Console.WriteLine(string.Format("Total de CNPJs: {0}", _ListCNPJsComDigito.Count));
            }
            else Console.WriteLine("Nao ha CNPJs para calculo do digito.");
        }

        public List<string> GetListaComDigitos()
        {
            return _ListCNPJsComDigito;
        }
    }
}
