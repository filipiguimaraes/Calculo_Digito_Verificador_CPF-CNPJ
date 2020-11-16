using System.Collections.Generic;

namespace AnalisadorCPFeCNPJ.Interfaces
{
    interface IValidadorDigito
    {
        void Calcular();
        List<string> GetListaComDigitos();
    }
}
