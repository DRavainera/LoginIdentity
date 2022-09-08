using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using LoginIdentity.Models;
using LoginIdentity.Data;

namespace LoginIdentity.Controllers
{
    [Authorize]
    public class ProductoController : Controller
    {
        protected readonly ApplicationDbContext _context;

        public ProductoController(ApplicationDbContext context)
        {
            _context = context;
        }
        //Read
        public List<Producto> TraerProductos()
        {
            var listaProductos = _context.Producto.ToList();

            return listaProductos;
        }
        //ReadBy
        public Producto? TraerUnProducto(int id)
        {
            var producto = _context.Producto.Find(id);

            return producto;
        }
        //Create
        public bool CrearProducto(Producto producto)
        {
            try
            {
                _context.Producto.Add(producto);

                _context.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return false;
            }
        }
        //Update
        public bool ActualizarProducto(Producto producto)
        {
            try
            {
                if (producto != null)
                {
                    _context.Producto.Update(producto);

                    _context.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return false;
            }
        }
        //Delete
        public bool BorrarProducto(int id)
        {
            try
            {
                var producto = _context.Producto.Find(id);

                if (producto != null)
                {
                    _context.Producto.Remove(producto);

                    _context.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return false;
            }
        }

        public IActionResult Index()
        {
            return View(TraerProductos());
        }

        [Authorize(Policy = "rolecreation")]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Policy = "rolecreation")]
        public IActionResult Crear(Producto producto)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            var respuesta = CrearProducto(producto);

            if (respuesta)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        [Authorize(Policy = "rolecreation")]
        public IActionResult Actualizar(int id)
        {
            var producto = TraerUnProducto(id);

            return View(producto);
        }

        [HttpPost]
        [Authorize(Policy = "rolecreation")]
        public IActionResult Actualizar(Producto producto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var respuesta = ActualizarProducto(producto);

            if (respuesta)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        [Authorize(Policy = "rolecreation")]
        public IActionResult Borrar(int id)
        {
            var producto = TraerUnProducto(id);

            return View(producto);
        }

        [HttpPost]
        [Authorize(Policy = "rolecreation")]
        public IActionResult Borrar(Producto producto)
        {
            var respuesta = BorrarProducto(producto.Id);

            if (respuesta)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
    }
}
