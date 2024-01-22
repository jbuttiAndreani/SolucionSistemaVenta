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
    public class TalleService : ITalleService
    {
        private readonly IGenericRepository<Talle> _repositorio;
        
        public TalleService(IGenericRepository<Talle> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<Talle>> Lista()
        {
            IQueryable<Talle> query = await _repositorio.Consultar();
            return query.ToList();
        }
        public async Task<Talle> Crear(Talle entidad)
        {
            try
            {
                Talle Talle_creada = await _repositorio.Crear(entidad);
                if (Talle_creada.IdTalle == 0)
                    throw new TaskCanceledException("No se pudo crear el Talle");

                return Talle_creada;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Talle> Editar(Talle entidad)
        {
            try
            {
                Talle Talle_encontrada = await _repositorio.Obtener(c => c.IdTalle == entidad.IdTalle);
                Talle_encontrada.Descripcion = entidad.Descripcion;
                Talle_encontrada.EsActivo = entidad.EsActivo;

                bool respuesta = await _repositorio.Editar(Talle_encontrada);

                if (!respuesta)
                    throw new TaskCanceledException("No se pudo modificar el Talle");

                return Talle_encontrada;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int idTalle)
        {
            try
            {
                Talle Talle_encontrada = await _repositorio.Obtener(c => c.IdTalle == idTalle);

                if (Talle_encontrada == null)
                    throw new TaskCanceledException("El Talle no existe");

                bool respuesta = await _repositorio.Eliminar(Talle_encontrada);

                return respuesta;
            }
            catch
            {
                throw;
            }
        }


    }
}