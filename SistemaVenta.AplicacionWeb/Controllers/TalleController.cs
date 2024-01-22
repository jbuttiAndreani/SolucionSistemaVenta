using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using SistemaVenta.AplicacionWeb.Models.ViewModels;
using SistemaVenta.AplicacionWeb.Utilidades.Response;
using SistemaVenta.BLL.Interfaces;
using SistemaVenta.Entity;
using Microsoft.AspNetCore.Authorization;
using SistemaVenta.BLL.Implementacion;

namespace SistemaVenta.AplicacionWeb.Controllers
{
    [Authorize]
    public class TalleController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ITalleService _TalleServicio;
        public TalleController(IMapper mapper, ITalleService TalleServicio)
        {
            _mapper = mapper;
            _TalleServicio = TalleServicio;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Lista() { 
            
            List<VMTalle> vmTalleLista = _mapper.Map<List<VMTalle>>( await _TalleServicio.Lista());
            return StatusCode(StatusCodes.Status200OK, new { data = vmTalleLista });
        
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody]VMTalle modelo)
        {
            GenericResponse<VMTalle> gResponse = new GenericResponse<VMTalle>();

            try
            {
                Talle Talle_creada = await _TalleServicio.Crear(_mapper.Map<Talle>(modelo));
                modelo = _mapper.Map<VMTalle>(Talle_creada);

                gResponse.Estado = true;
                gResponse.Objeto = modelo;
            }
            catch(Exception ex) {
                gResponse.Estado = false;
                gResponse.Mensaje = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, gResponse);
        }

        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] VMTalle modelo)
        {
            GenericResponse<VMTalle> gResponse = new GenericResponse<VMTalle>();

            try
            {
                Talle Talle_editada = await _TalleServicio.Editar(_mapper.Map<Talle>(modelo));
                modelo = _mapper.Map<VMTalle>(Talle_editada);

                gResponse.Estado = true;
                gResponse.Objeto = modelo;
            }
            catch (Exception ex)
            {
                gResponse.Estado = false;
                gResponse.Mensaje = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, gResponse);
        }

        [HttpDelete]
        public async Task<IActionResult> Eliminar(int IdTalle) {

            GenericResponse<string> gResponse = new GenericResponse<string>();
            try
            {
                gResponse.Estado = await _TalleServicio.Eliminar(IdTalle);

            }
            catch (Exception ex) {
                gResponse.Estado = false;
                gResponse.Mensaje = ex.Message;
            }
            return StatusCode(StatusCodes.Status200OK, gResponse);
        }

    }
}
