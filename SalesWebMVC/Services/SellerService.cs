using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMVC.Data;
using SalesWebMVC.Models;
using Microsoft.EntityFrameworkCore; // para o include
using SalesWebMVC.Services.Exceptions;

namespace SalesWebMVC.Services
{
    public class SellerService
    {
        // readonly = previne que a dependencia não seja alterada
        private readonly SalesWebMVCContext _context;

        public SellerService(SalesWebMVCContext context)
        {
            _context = context;
        }

        
        // RETORNAR LISTA COM TODOS OS VENDEDORES DO BANCO DE DADOS
        // public List<Seller> FindAll()
        // {
        //    return _context.Seller.ToList();
        // }

        // CONVERSÃO PARA MÉTODO ASSINCRONO
        public async Task<List<Seller>> FindAllAsync()
        {
            //await avisa ao compilador que é uma chamada assincrona
            return await _context.Seller.OrderBy(x => x.Name).ToListAsync();
        }





        // INSERIR VENDEDORES
        //public void Insert(Seller obj)
        //{
        // pegar o primeiro departamento do banco de dados e associar ao vendedor
        // obj.Department = _context.Department.First();
        //    _context.Add(obj);
        //    _context.SaveChanges();
        // }

        // CONVERSÃO PARA MÉTODO ASSINCRONO
        public async Task InsertAsync(Seller obj)
        {
            // pegar o primeiro departamento do banco de dados e associar ao vendedor
            // obj.Department = _context.Department.First();
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }



        //PROCURAR VENDEDOR
        // public Seller FindById(int id)
        // {
        //include traz o vendedor e o seu departamento, poderia trazer mais informações
        // necessário incluir o using Microsoft.EntityFrameworkCore;
        //return _context.Seller.FirstOrDefault(obj => obj.Id == id);
        //    return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);
        // }

        // CONVERSÃO PARA MÉTODO ASSINCRONO
        public async Task<Seller> FindByIdAsync(int id)
        {
            //include traz o vendedor e o seu departamento, poderia trazer mais informações
            // necessário incluir o using Microsoft.EntityFrameworkCore;
            //return _context.Seller.FirstOrDefault(obj => obj.Id == id);
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }



        //REMOVER O VENDEDOR
        //public void Remove(int id)
        //{
        //    var obj = _context.Seller.Find(id);
        //    _context.Seller.Remove(obj);
        //    _context.SaveChanges();
        //}

        // CONVERSÃO PARA MÉTODO ASSINCRONO
        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Seller.FindAsync(id);
                _context.Seller.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                // Caso queira por uma mensagem personalizada de erro
                // throw new IntegrityException("Can't delete seller because he/she has sales");
                throw new IntegrityException(e.Message);
            }
        }
        
        
        
        //ATUALIZAR VENDEDOR
        //public void Update(Seller obj)
        //{
            // Se não existir o vendedor
        //    if (!_context.Seller.Any(x => x.Id == obj.Id))
        //    {
                //lançar uma exceção
        //        throw new NotFoundException("Id not found");
        //    }
        //    try
        //    {
        //    _context.Update(obj);
        //    _context.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException e)
        //    {
        //        throw new DbConcurrencyException(e.Message);
        //    }

            // CONVERSÃO PARA MÉTODO ASSINCRONO
            public async Task UpdateAsync(Seller obj)
            {
            // Se não existir o vendedor
                bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);
                if (!hasAny)
                {
                    //lançar uma exceção
                    throw new NotFoundException("Id not found");
                }
                try
                {
                    _context.Update(obj);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException e)
                {
                    throw new DbConcurrencyException(e.Message);
                }
            }
    }
}
