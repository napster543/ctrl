using aplicacion.Datos;
using aplicacion.Models;
using Microsoft.AspNetCore.Mvc;

namespace aplicacion.Controllers
{
    public class EventoController : Controller
    {
        DatosEventos _datosEventos = new DatosEventos();
        public IActionResult Listar()
        {
            var oLista = _datosEventos.Listar();
            return View(oLista);
        }
        //public IActionResult Listar(string cadena)
        //{
        //    var oLista = _datosEventos.Listar(cadena);
        //    return View(oLista);
        //}
        public IActionResult Guardar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Guardar(EventosModels model)
        {

            if (!ModelState.IsValid)
                return View();

            var respuesta = _datosEventos.Guardar(model);
            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }

        public IActionResult Editar(int IdEvento)
        {
            var oContacto = _datosEventos.Obtener(IdEvento);
            return View(oContacto);
        }
        [HttpPost]
        public IActionResult Editar(EventosModels oEvento)
        {
            if (!ModelState.IsValid)
                return View();


            var respuesta = _datosEventos.Editar(oEvento);
            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }

        public IActionResult Eliminar(int IdEvento)
        {
            var oContacto = _datosEventos.Obtener(IdEvento);
            return View(oContacto);
        }
        [HttpPost]
        public IActionResult Eliminar(EventosModels oEvento)
        {

            var respuesta = _datosEventos.Eliminar(oEvento.Id);
            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }

    }
}
