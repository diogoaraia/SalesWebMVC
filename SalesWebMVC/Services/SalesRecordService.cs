using SalesWebMVC.Data;
using SalesWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMVC.Services
{
    public class SalesRecordService
    {
        // readonly = previne que a dependencia não seja alterada
        private readonly SalesWebMVCContext _context;

        public SalesRecordService(SalesWebMVCContext context)
        {
            _context = context;
        }

        // BUSCA SIMPLES
        // OPERAÇÃO ASSINCRONA QUE BUSCA OS REGISTRO DE VENDA POR DATA
        // DATA MINIMA E MÁXIMA OPCIONAIS
        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj;
            // restrição de valor mínimo
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            // restrição de valor máximo
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }

            // returnar para tela de lista
            return await result
                .Include(x => x.Seller) // faz o JOIN das tabela de vendedores
                .Include(x => x.Seller.Department) // faz o JOIN das tabelas de vendedores e departamentos
                .OrderByDescending(x => x.Date) // Ordernar por data descrescente
                .ToListAsync(); // returnar para tela de lista
        }

        //BUSCA AGRUPADA
        public async Task<List<IGrouping<Department,SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj;
            // restrição de data mínima
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            // restrição de data máxima
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }

            // returnar para tela de lista
            return await result
                .Include(x => x.Seller) // faz o JOIN das tabela de vendedores
                .Include(x => x.Seller.Department) // faz o JOIN das tabelas de vendedores e departamentos
                .OrderByDescending(x => x.Date) // Ordernar por data descrescente
                .GroupBy(x => x.Seller.Department) // agrupar por departamento
                .ToListAsync(); // returnar para tela de lista
        }
    }
}
