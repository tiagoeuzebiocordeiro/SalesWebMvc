using System;

namespace SalesWeb.Services.Exceptions {
    public class NotFoundException : ApplicationException {
    
        public NotFoundException(string message) : base(message) { }

        /*Pq estamos criando exceção personalizada?
         
            Quanto temos exceções personalizadas, temos um controler maior de 
            tratamento.

         
         */

    }
}
