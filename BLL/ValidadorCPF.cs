using AnalisadorCPFeCNPJ.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalisadorCPFeCNPJ.BLL
{
    class ValidadorCPF : IValidadorDigito
    {
        public int CPFAvaliados = 0;
        private List<string> _ListCPFs;
        private List<string> _ListCPFsComDigito;

        public ValidadorCPF(List<string> listCPFs)
        {
            this._ListCPFs = listCPFs;
            _ListCPFsComDigito = new List<string>();
        }

        public void Calcular()
        {
            int primeiroDigitoVerificador = 0;
            int segundoDigitoVerificador = 0;

            if (_ListCPFs.Count > 0)
            {
                foreach (string cpf in _ListCPFs)
                {
                    for (int i = 0; i < 9; i++)
                    {
                        if (cpf[i] >= 48 && cpf[i] <= 57)
                        {
                            int digito = cpf[i] - 48;
                            primeiroDigitoVerificador += digito * (10 - i);
                        }
                    }

                    primeiroDigitoVerificador = primeiroDigitoVerificador % 11;
                    primeiroDigitoVerificador = 11 - primeiroDigitoVerificador;
                    if (primeiroDigitoVerificador >= 10) primeiroDigitoVerificador = 0;

                    for (int i = 0; i < 9; i++)
                    {
                        if (cpf[i] >= 48 && cpf[i] <= 57)
                        {
                            int digito = cpf[i] - 48;
                            segundoDigitoVerificador += digito * (11 - i);
                        }
                    }

                    segundoDigitoVerificador += primeiroDigitoVerificador * 2;
                    segundoDigitoVerificador = segundoDigitoVerificador % 11;
                    segundoDigitoVerificador = 11 - segundoDigitoVerificador;
                    if (segundoDigitoVerificador >= 10) segundoDigitoVerificador = 0;

                    _ListCPFsComDigito.Add(string.Concat(cpf, primeiroDigitoVerificador, segundoDigitoVerificador));
                }
                Console.WriteLine(string.Format("Total de CPFs: {0}", _ListCPFsComDigito.Count));
            }
            else Console.WriteLine("Nao ha CPFs para calculo do digito.");

        }

        public List<string> GetListaComDigitos()
        {
            return _ListCPFsComDigito;
        }
    }
}
