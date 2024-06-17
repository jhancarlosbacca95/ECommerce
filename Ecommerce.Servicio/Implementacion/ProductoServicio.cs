using AutoMapper;
using Ecommerce.DTO;
using Ecommerce.Modelo;
using Ecommerce.Repositorio.Contrato;
using Ecommerce.Servicio.Contrato;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Servicio.Implementacion
{
    public class ProductoServicio : IProductoServicio
    {
        private readonly IMapper _mapper;
        private readonly IGenericoRepositorio<Producto> _modeloRepositorio;

        public ProductoServicio(IMapper mapper, IGenericoRepositorio<Producto> modeloRepositorio)
        {
            _mapper = mapper;
            _modeloRepositorio = modeloRepositorio;
        }

        public async Task<List<ProductoDTO>> Catalogo(string categoria, string buscar)
        {
            try
            {
                var consulta = _modeloRepositorio.Consultar(p => p.Nombre.ToLower().Contains(buscar.ToLower()) &&
                p.IdCategoriaNavigation.Nombre.ToLower().Contains(categoria.ToLower()));

                List<ProductoDTO> lista = _mapper.Map<List<ProductoDTO>>(await consulta.ToListAsync());
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<ProductoDTO> Crear(ProductoDTO modelo)
        {
            try
            {
                var dbModelo = _mapper.Map<Producto>(modelo);
                var rspModelo = await _modeloRepositorio.Crear(dbModelo);

                if (rspModelo.IdProducto != 0)
                    return _mapper.Map<ProductoDTO>(rspModelo);
                else
                    throw new TaskCanceledException("No se pudo crear");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Editar(ProductoDTO modelo)
        {
            try
            {
                var consulta = _modeloRepositorio.Consultar(p=>p.IdProducto == modelo.IdProducto);
                var fromModelo = await consulta.FirstOrDefaultAsync();

                if (fromModelo!=null)
                {
                    fromModelo.Nombre = modelo.Nombre ?? fromModelo.Nombre;
                    fromModelo.Descripcion = modelo.Descripcion ?? fromModelo.Descripcion;
                    fromModelo.IdCategoria = modelo.IdCategoria ?? fromModelo.IdCategoria;
                    fromModelo.Precio = modelo.Precio ?? fromModelo.Precio;
                    fromModelo.PrecioOferta = modelo.PrecioOferta ?? fromModelo.PrecioOferta;
                    fromModelo.Cantidad = modelo.Cantidad ?? fromModelo.Cantidad;
                    fromModelo.Imagen = modelo.Imagen ?? fromModelo.Imagen;

                    var respuesta = await _modeloRepositorio.Editar(fromModelo);

                    if (!respuesta)
                        throw new TaskCanceledException("No se pudo editar");
                    return respuesta;

                }
                else
                {
                    throw new TaskCanceledException("No se encontraron resultados");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var consulta = _modeloRepositorio.Consultar(p => p.IdProducto == id);
                var fromModelo = await consulta.FirstOrDefaultAsync();
                if (fromModelo != null)
                {
                    var respuesta = await _modeloRepositorio.Delete(fromModelo);
                    if (!respuesta)
                        throw new TaskCanceledException("No se pudo eliminar");
                    return respuesta;
                }
                else
                {
                    throw new TaskCanceledException("No se encontraron resultados");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<List<ProductoDTO>> Lista(string buscar)
        {
            try
            {
                var consulta = _modeloRepositorio.Consultar(p => p.Nombre.ToLower().Contains(buscar.ToLower()));
                consulta = consulta.Include(c => c.IdCategoriaNavigation);

                List<ProductoDTO> lista = _mapper.Map<List<ProductoDTO>>( await consulta.ToListAsync());
                return lista;
            }
            catch (Exception ex)
            { 
                throw ex;
            }
        }

        public async Task<ProductoDTO> Obtener(int id)
        {
            try
            {
                var consulta = _modeloRepositorio.Consultar(p => p.IdProducto == id);
                consulta = consulta.Include(p => p.IdCategoriaNavigation);
                var fromDbModelo = await consulta.FirstOrDefaultAsync();

                if (fromDbModelo != null)
                    return _mapper.Map<ProductoDTO>(fromDbModelo);
                else
                    throw new TaskCanceledException("No se encontraton coincidencias");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
