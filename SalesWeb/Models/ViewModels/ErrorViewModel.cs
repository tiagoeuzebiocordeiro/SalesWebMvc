using System;

namespace SalesWeb.Models.ViewModels
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; } //Id interno da requisição que podemos mostrar na pagina de erro
        public string Message { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
