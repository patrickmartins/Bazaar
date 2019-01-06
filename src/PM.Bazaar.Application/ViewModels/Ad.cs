using System;
using PM.Bazaar.Application.ViewModels.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PM.Bazaar.Application.ViewModels
{
    public class AdViewModel : ViewModel
    {
        public string Title { get; set; }
        public int IdCategory { get; set; }
        public double Price { get; set; }
        public int Picture { get; set; }
    }

    public class SearchAdViewModel : ViewModel
    {
        public enum OrderBy
        {
            MinPrice,
            MaxPrice,
            Publish
        }

        public int[] Categories { get; set; }
        public double MaxPrice { get; set; }
        public double MinPrice { get; set; }
        public OrderBy Order { get; set; }
        public int Page { get; set; }

        [StringLength(20, MinimumLength = 3, ErrorMessage = "A chave de busca deve conter no mínimo 3 e no máximo 20 caracteres")]
        public string KeywordSearch { get; set; }

        [Range(5, 50, ErrorMessage = "O tamnho da página deve ser no mínimo 5 e no máximo 50")]
        public int PageSize { get; set; }

        public SearchAdViewModel()
        {
            KeywordSearch = string.Empty;
            Categories = new int[0];
        }
    }

    public class DetailedAdViewModel : ViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int IdCategory { get; set; }
        public double Price { get; set; }
        public AccountViewModel Advertiser { get; set; }
        public int[] Pictures { get; set; }
        public ICollection<QuestionViewModel> Questions { get; set; }
    }

    public class RegisterAdViewModel : ViewModel
    {
        [Required]
        [StringLength(60, MinimumLength = 5, ErrorMessage = "O título deve conter no mínimo 5 e no máximo 60 caracteres")]
        public string Title { get; set; }

        [Required]
        [StringLength(5000, MinimumLength = 100, ErrorMessage = "A descrição deve conter no mínimo 100 e no máximo 5000 caracteres")]
        public string Description { get; set; }

        [Required]
        public int IdCategory { get; set; }

        public int IdAdvertiser { get; set; }

        [Required]
        [Range(5, 1000, ErrorMessage = "O preço do item deve ser no mínimo R$5 e no máximo R$1000")]
        public double Price { get; set; }
        public Guid[] Pictures { get; set; }
    }
}
