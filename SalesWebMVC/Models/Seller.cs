using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesWebMVC.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public double BaseSalary { get; set; }
        public Department Department { get; set; }
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
