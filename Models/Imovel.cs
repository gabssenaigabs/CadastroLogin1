using System;
using System.ComponentModel.DataAnnotations;

namespace GestaoCondominio.Models
{
    public class Imovel
    {
        public int Id { get; set; }

        [Required]
        public string? Endereco { get; set; }

        public string? Bloco { get; set; }

        public string? Unidade { get; set; }

        public string? Observacoes { get; set; }
    }
}
