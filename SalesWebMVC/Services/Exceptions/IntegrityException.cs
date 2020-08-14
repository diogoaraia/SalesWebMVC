using System;

// EXCEÇÃO PERSONALIZADA DE SERVIÇO PARA ERROS DE INTEGRIDADE REFERENCIAL
namespace SalesWebMVC.Services.Exceptions
{
     // herda do ApplicationException
    public class IntegrityException : ApplicationException
    {
        public IntegrityException(string message) : base(message)
        {

        }
    }
}
