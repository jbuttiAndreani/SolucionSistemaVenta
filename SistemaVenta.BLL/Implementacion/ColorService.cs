using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaVenta.BLL.Interfaces;
using SistemaVenta.DAL.Interfaces;
using SistemaVenta.Entity;

namespace SistemaVenta.BLL.Implementacion
{
    public class ColorService : IColorService
    {
        private readonly IGenericRepository<Color> _repositorio;
      
        public ColorService(IGenericRepository<Color> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<Color>> Lista()
        {
            IQueryable<Color> query = await _repositorio.Consultar();
            return query.ToList();
        }
        public async Task<Color> Crear(Color entidad)
        {
            try
            {
                Color color_creada = await _repositorio.Crear(entidad);
                if(color_creada.IdColor == 0)
                    throw new TaskCanceledException("No se pudo crear el color");

                return color_creada;
            }
            catch {
                throw;
            }
        }

        public async Task<Color> Editar(Color entidad)
        {
            try
            {
                Color color_encontrada = await _repositorio.Obtener(c => c.IdColor == entidad.IdColor);
                color_encontrada.Descripcion = entidad.Descripcion;
                color_encontrada.EsActivo = entidad.EsActivo;

                bool respuesta = await _repositorio.Editar(color_encontrada);

                if (!respuesta)
                    throw new TaskCanceledException("No se pudo modificar el Color");

                return color_encontrada;
            }
            catch {
                throw;
            }
        }

        public async Task<bool> Eliminar(int idColor)
        {
            try
            {
                Color color_encontrada = await _repositorio.Obtener(c => c.IdColor == idColor);

                if(color_encontrada == null)
                    throw new TaskCanceledException("El color no existe");

                bool respuesta = await _repositorio.Eliminar(color_encontrada);

                return respuesta;
            }
            catch {
                throw;
            }
        }

      
    }
}
