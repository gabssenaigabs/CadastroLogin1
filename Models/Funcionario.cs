using System;
using System.ComponentModel.DataAnnotations;

namespace GestaoCondominio.Models
{
    public class Funcionario
    {
        public int Id { get; set; }

        [Required]
        public string? Nome { get; set; }

        public string? Cargo { get; set; }

        public string? Telefone { get; set; }

        public string? Email { get; set; }
    }
}
