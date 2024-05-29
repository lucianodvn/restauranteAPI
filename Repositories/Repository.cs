

using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurante.API.DbConext;
using Restaurante.API.Repositories.Interface;

namespace Restaurante.API.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly Context _context;

        public Repository(Context context)
        {
            _context = context;
        }

        public void Salvar(T entity)
        {
            _context.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<T?> Get(Expression<Func<T, bool>> predicate)
        {
            var response = await _context.Set<T>().FirstOrDefaultAsync(predicate);
            return response;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task<bool> SalvarAleracoes()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}

