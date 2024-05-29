using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Restaurante.API.Repositories.Interface
{
    public interface IRepository<T>
    {
        void Salvar(T entity);
        Task<IEnumerable<T>> GetAll();
        Task<T>? Get(Expression<Func<T, bool>> predicate);
        void Update(T entity);
        void Delete(T entity);
        Task<bool> SalvarAleracoes();
    }
}

