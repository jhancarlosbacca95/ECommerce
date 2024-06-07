using Ecommerce.Repositorio.Contrato;
using Ecommerce.Repositorio.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repositorio.Implementacion
{
    public class GenericoRepositorio<TModelo> : IGenericoRepositorio<TModelo> where TModelo : class
    {
        private readonly DbecommerceContext _dbcontext;

        public GenericoRepositorio(DbecommerceContext contexto)
        {
            _dbcontext = contexto;
        }

        public IQueryable<TModelo> Consultar(Expression<Func<TModelo, bool>>? filtro = null)
        {
            IQueryable<TModelo> consulta = (filtro==null)? _dbcontext.Set<TModelo>():_dbcontext.Set<TModelo>().Where(filtro);
            return consulta;
        }

        public async Task<TModelo> Crear(TModelo modelo)
        {
            try
            {
                _dbcontext.Set<TModelo>().Add(modelo);
                await _dbcontext.SaveChangesAsync();
                return modelo;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Delete(TModelo modelo)
        {
            try
            {
                _dbcontext.Set<TModelo>().Remove(modelo);
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Editar(TModelo modelo)
        {
            try
            {
                _dbcontext.Set<TModelo>().Update(modelo);
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}
