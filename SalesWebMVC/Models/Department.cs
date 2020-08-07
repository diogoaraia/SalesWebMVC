using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesWebMVC.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();
                
        public Department() 
        {
        }

        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }
    
        // Adicionar vendedor
        public void AddSeller(Seller seller)
        {
            Sellers.Add(seller);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            // return Sellers = pegar a lista de vendedores
            // .Sum(apenas as vendas nesse periodo de data)
            // seller => para cada vendedor
            // seller.TotalSales(initial, final) pegar o total de vendas no periodo e somar ao de todos vendedores
            return Sellers.Sum(seller => seller.TotalSales(initial, final));
        }
    }
}
