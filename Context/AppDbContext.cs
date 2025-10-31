using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestaoCondominio.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GestaoCondominio.Context
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Imovel> Imoveis { get; set; }
        public DbSet<Condomino> Condominos { get; set; }
        public DbSet<Locatario> Locatarios { get; set; }
        public DbSet<Proprietario> Proprietarios { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
    }
}