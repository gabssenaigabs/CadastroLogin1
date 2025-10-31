using System;
using System.ComponentModel.DataAnnotations;

namespace GestaoCondominio.Models
{
    public class Condomino
    {
        public int Id { get; set; }

        [Required]
        public string? Nome { get; set; }

        public string? Email { get; set; }

        public string? CPF { get; set; }

        public string? Telefone { get; set; }

        public int? ImovelId { get; set; }
        public Imovel? Imovel { get; set; }
    }
}
