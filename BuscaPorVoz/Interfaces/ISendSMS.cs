using System;

namespace BuscaPorVoz
{
    public interface ISendSMS
    {
        void SendSMS(string text, string telefone);
    }
}

