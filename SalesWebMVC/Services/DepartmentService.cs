using SalesWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using SalesWebMVC.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMVC.Services
{
    public class DepartmentService
    {
        // readonly = previne que a dependencia não seja alterada
        private readonly SalesWebMVCContext _context;

        public DepartmentService(SalesWebMVCContext context)
        {
            _context = context;
        }

        // traz a list de departamentos na ordem do nome
        // CONVERSÃO DA OPERAÇÃO PARA ASSINCRONA
        // public List<Department> FindAll()
        // {
        //    return _context.Department.OrderBy(x => x.Name).ToList();
        // }
        
        // CHAMADA ASSINCRONA
        public async Task<List<Department>> FindAllAsync()
        {
            //await avisa ao compilador que é uma chamada assincrona
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();
        }

    }
}
