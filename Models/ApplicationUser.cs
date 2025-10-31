using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace GestaoCondominio.Models
{
    public enum UserRole
    {
        Morador,
        Sindico,
        Gestor
    }
    public class ApplicationUser : IdentityUser
    {
        public string? Nome { get; set; }
        public string? CPF { get; set; }
        public string? Bloco { get; set; }
        public string? Telefone { get; set; }
        public UserRole Role { get; set; }
        public DateTime? InicioMandato { get; set; }
        public string? BlocoResidencia { get; set; }
        public string? TelefoneComercial { get; set; }
        public string? Empresa { get; set; }
        public string? Cargo { get; set; }
    }
}