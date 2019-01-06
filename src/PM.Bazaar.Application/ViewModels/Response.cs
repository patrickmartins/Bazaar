using PM.Bazaar.Application.ViewModels.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace PM.Bazaar.Application.ViewModels
{
    public class ResponseViewModel : ViewModel
    {
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }

    public class RegisterResponseViewModel : ViewModel
    {
        public int IdAdvertiser { get; set; }
        public int IdAd { get; set; }
        public int IdQuestion { get; set; }

        [Required(ErrorMessage = "A resposta não pode ser vazia")]
        [StringLength(2000, ErrorMessage = "A resposta deve conter no minímo 5 e no máximo 2000 caracteres", MinimumLength = 5)]
        public string Description { get; set; }
    }
}