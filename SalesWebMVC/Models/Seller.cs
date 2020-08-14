using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using SalesWebMVC.Models;

namespace SalesWebMVC.Models
{
    public class Seller
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} required")] // tem requisição do nome
        // requer 60 caracteres e no minimo 3
        // Parametros {0} = Name, {2} = segundo valor (MinimumLength) e {1} = primeiro valor (60)
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} size should be between {2} and {1}")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} required")] // tem requisição do nome
        [DataType(DataType.EmailAddress)] // aciona os emails para já ser enviado ao clicar sobre
        [EmailAddress(ErrorMessage = "Enter a valid email")] // mensagem de erro ao inserir email
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} required")] // tem requisição do nome
        [Display(Name = "Birth Date")] // rótulo
        [DataType(DataType.Date)] // apenas data, mas há diversas opções
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")] // a data no formato dia/mes/ano
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "{0} required")] // tem requisição do nome
        [Range(100.0, 50000.0, ErrorMessage = "{0} must be from {1} to {2}")] // requisição do valor minimo e maximo
        [Display(Name = "Base Salary")] // rótulo
        [DisplayFormat(DataFormatString = "{0:F2}")] // o valor ter duas casas decimais de valor zero
        public double BaseSalary { get; set; }

        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();


        public Seller() 
        {
        }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        // Adicionar vendas de sr em SalesRecord
        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }

        // Remover vendas de sr em SalesRecord
        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        // Trazer o total de vendas entre a data inicial e final
        // Necessário incluir no inicio using System.Linq;
        public double TotalSales(DateTime initial, DateTime final)
        {
            // Sales = lista de vendas associada ao vendedor
            // Where = filtro do período
            // sr = SalesRecord
            // Pegar todo objeto sr, tal que sr.Date >= initial e sr.Date <= final
            // Após calcular a soma baseado na soma do sr que leva em sr.Amount
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }
    }
}
