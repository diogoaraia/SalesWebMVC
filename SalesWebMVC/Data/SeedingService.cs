using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMVC.Models;
using SalesWebMVC.Models.Enums;

namespace SalesWebMVC.Data
{
    public class SeedingService
    {
        private SalesWebMVCContext _context;
        
        // injeção de dependencia
        public SeedingService(SalesWebMVCContext context)
        {
            _context = context;
        }

        // operação de popular a base de dados
        public void Seed()
        {
            // se já existe um dado na base de dados será interrompido a operação
            if (_context.Department.Any() ||
                _context.Seller.Any() ||
                _context.SalesRecord.Any())
            {
                return; // o banco de dados já foi populado
            }

            Department d1 = new Department(1, "Computers");
            Department d2 = new Department(2, "Electronics");
            Department d3 = new Department(3, "Fashion");
            Department d4 = new Department(4, "Books");

            // vendedores
            Seller s1 = new Seller(1, "Bod Brow", "bob@gmail.com", new DateTime(1998, 4, 21), 1000.0, d1);
            Seller s2 = new Seller(2, "Maria Green", "mariagreen@gmail.com", new DateTime(1998, 4, 21), 2000.0, d2);
            Seller s3 = new Seller(3, "Alex Grey", "alexgrey@gmail.com", new DateTime(1998, 4, 21), 3000.0, d1);
            Seller s4 = new Seller(4, "Martha Red", "martha@gmail.com", new DateTime(1998, 4, 21), 5000.0, d4);
            Seller s5 = new Seller(5, "Donald Blue", "donald@gmail.com", new DateTime(1998, 4, 21), 8000.0, d3);
            Seller s6 = new Seller(6, "Alex Pink", "alexpink@gmail.com", new DateTime(1998, 4, 21), 3000.0, d2);

            // vendas
            SalesRecord r1 = new SalesRecord(1, new DateTime(2018, 09, 25), 11000.0, SaleStatus.Billed, s1);

            // insere os dados no banco de dados
            _context.Department.AddRange(d1, d2, d3, d4);

            _context.Seller.AddRange(s1,s2,s3,s4,s5,s6);

            _context.SalesRecord.AddRange(r1);

            // salva os inserts no banco
            _context.SaveChanges();
        }
    }
}
