using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;
using SalesWebMVC.Models.ViewModels;
using SalesWebMVC.Services;
using SalesWebMVC.Services.Exceptions;


namespace SalesWebMVC.Controllers
{
    public class SellersController : Controller
    {
        //declarar dependencias:
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;
        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        // EXIBIR LISTA DE VENDEDORES
        // public IActionResult Index()
        // {
        //retorna a lista de Seller
        //     var list = _sellerService.FindAll();
        //     return View(list);
        // }

        // CONVERSÃO PARA MÉTODO ASSINCRONO EXIBIR LISTA DE VENDEDORES
        public async Task<IActionResult> Index()
        {
            //retorna a lista de Seller
            var list = await _sellerService.FindAllAsync();
            return View(list);
        }




        // INSERIR VENDEDORES
        // public IActionResult Create()
        // {
        // buscar todos os departamento
        //     var departments = _departmentService.FindAll();
        //     var viewModel = new SellerFormViewModel { Departments = departments };
        //     return View(viewModel);
        // }

        // CONVERSÃO PARA MÉTODO ASSINCRONO INSERIR VENDEDORES
        public async Task<IActionResult> Create()
        {
            // buscar todos os departamento
            var departments = await _departmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }


        
        // VALIDAÇÃO DO INSERIR
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public IActionResult Create(Seller seller)
        // {
        //Não sendo validado as informações no form. é redirecionado para página seller.
        //    if (!ModelState.IsValid)
        //    {
        //        var departments = _departmentService.FindAll();
        //        var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
        //        return View(viewModel);
        //    }
        //    _sellerService.Insert(seller);
        // redirecionar para página de lista de vendedores
        //    return RedirectToAction(nameof(Index));
        // }

        // CONVERSÃO PARA MÉTODO ASSINCRONO VALIDAÇÃO DE INSERIR VENDEDORES
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            //Não sendo validado as informações no form. é redirecionado para página seller.
            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }
            await _sellerService.InsertAsync(seller);
            // redirecionar para página de lista de vendedores
            return RedirectToAction(nameof(Index));
        }


        
        // DELETAR VENDEDORES
        // public IActionResult Delete(int? id)
        // {
        //    if (id == null)
        //    {
                 //mensagem de erro: id não fornecido
        //        return RedirectToAction(nameof(Error), new { message = "Id not provided" });
        //    }
        //    var obj = _sellerService.FindById(id.Value);
        //    if (obj == null)
        //    {
                  //mensagem de erro: id não encontrado
        //       return RedirectToAction(nameof(Error), new { message = "Id not found" });
        //    }
        //    return View(obj);
        // }


        // CONVERSÃO PARA MÉTODO ASSINCRONO DELETE DE VENDEDORES
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                //mensagem de erro: id não fornecido
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                //mensagem de erro: id não encontrado
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }


        // VALIDAÇÃO DO DELETE DE VENDEDOR
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public IActionResult Delete(int id)
        // {
        //     _sellerService.Remove(id);
        //     return RedirectToAction(nameof(Index));
        // }

        // CONVERSÃO PARA MÉTODO ASSINCRONO VALIDAÇÃO DO DELETE DE VENDEDORES
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _sellerService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }




        //DETALHAR VENDEDORES
        // public IActionResult Details(int? id)
        // {
        //    if (id == null)
        //    {
        //        return RedirectToAction(nameof(Error), new { message = "Id not provided" });
        //    }
        //    var obj = _sellerService.FindById(id.Value);
        //    if (obj == null)
        //    {
        //        return RedirectToAction(nameof(Error), new { message = "Id not found" });
        //    }
        //    return View(obj);
        // }

        // CONVERSÃO PARA MÉTODO ASSINCRONO DETALHAR VENDEDORES                    
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }




        //EDITAR VENDEDORES
        // public IActionResult Edit(int? id)
        // {
        //    if (id == null)
        //    {
        //        return RedirectToAction(nameof(Error), new { message = "Id not provided" });
        //    }         
        //    var obj = _sellerService.FindById(id.Value);
        //    if (obj == null)
        //    {
        //        return RedirectToAction(nameof(Error), new { message = "Id not found" });
        //    }         
        //    List<Department> departments = _departmentService.FindAll();
        //    SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
        //    return View(viewModel);
        // }

        // CONVERSÃO PARA MÉTODO ASSINCRONO EDITAR VENDEDORES      
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            List<Department> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);
        }



        // VALIDAÇÃO DA EDIÇÃO DO VENDEDOR
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public IActionResult Edit(int id, Seller seller)
        // {
        //Não sendo validado as informações no form. é redirecionado para página seller.
        //    if (!ModelState.IsValid)
        //    {
        //        var departments = _departmentService.FindAll();
        //        var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
        //        return View(viewModel);
        //    }

        //    if (id != seller.Id)
        //    {
        //Mensagem de erro: id não corresponde
        //        return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
        //    }
        //    try
        //    {
        //        _sellerService.Update(seller);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch (ApplicationException e)
        //    {
        //Mensagem de erro será a mensagem da exceção
        //        return RedirectToAction(nameof(Error), new { message = e.Message });
        //    }
        // }


        // CONVERSÃO PARA MÉTODO ASSINCRONO VALIDAÇÃO DE EDIÇÃO DE VENDEDORES      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            //Não sendo validado as informações no form. é redirecionado para página seller.
            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }

            if (id != seller.Id)
            {
                //Mensagem de erro: id não corresponde
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            try
            {
                await _sellerService.UpdateAsync(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                //Mensagem de erro será a mensagem da exceção
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }



        //ENVIO DE ERRO
        // NÃO É PRECISO CONVERTER PARA ASSINCRONA POIS NÃO ACESSA A DADOS
        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }

    }
}