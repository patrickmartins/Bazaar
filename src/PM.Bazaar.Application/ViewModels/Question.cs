using PM.Bazaar.Application.ViewModels.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace PM.Bazaar.Application.ViewModels
{
    public class QuestionViewModel : ViewModel
    {
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public UserViewModel User { get; set; }
        public ResponseViewModel Response { get; set; }
    }

    public class RegisterQuestionViewModel : ViewModel
    {
        public int IdAdvertiser { get; set; }
        public int IdAd { get; set; }

        [Required(ErrorMessage = "A pergunta não pode ser vazia")]
        [StringLength(2000, ErrorMessage = "A pergunta deve conter no minímo 5 e no máximo 2000 caracteres", MinimumLength = 5)]
        public string Description { get; set; }
    }
}