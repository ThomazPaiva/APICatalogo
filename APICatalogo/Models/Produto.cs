using APICatalogo.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APICatalogo.Models
{
    [Table("Produtos")]
    public class Produto : IValidatableObject
    {
        [Key]
        public int ProdutoId { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(20, ErrorMessage = "O nome deve ter entre 5 e 20 caracteres", MinimumLength = 5)]
        //[PrimeiraLetraMaiuscula]
        public string Nome { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "A descrição deve ter no máximo {1} caracteres")]
        public string Descricao { get; set; }

        [Range(1, 1000, ErrorMessage = "O preço deve estar entre {1} e {2}")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Preco { get; set; }

        [Required]
        [StringLength(300)]
        public string ImagemURL { get; set; }

        public float Estoque { get; set; }

        public DateTime DataCadastro { get; set; }

        public int CategoriaId { get; set; }

        [JsonIgnore]
        public Categoria Categoria { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(this.Nome))
            {
                var primeiraLetra = this.Nome[0].ToString();
                if(primeiraLetra != primeiraLetra.ToUpper())
                {
                    yield return new ValidationResult("A primeira letra do produto deve ser maiúscula!", 
                    new[]
                    {
                        nameof(this.Nome)
                    });
                }
            }
        }
    }
}
