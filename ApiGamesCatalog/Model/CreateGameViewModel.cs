using System.ComponentModel.DataAnnotations;

namespace ApiGamesCatalog.Model
{
    public class CreateGameViewModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do jogo deve conter de 3 a 100 caracteres")]
        public string Name { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "O nome da produtora deve conter até 100 caracteres")]
        public string Producer { get; set; }

        [Required]
        [Range(1, 1000, ErrorMessage = "O preço do jogo deve ser entre R$ 1,00 até R$ 1.000,00")]
        public double Price { get; set; }
    }
}
