using System;
using Portfolio.Models;
using Services.Common.Models;

namespace Portfolio.Services
{
    public interface IBaseRepository<T>
    {
        Task<PageData<T>> GetAllAsync(PageOptions options);
        Task<T?> GetByIdAsync(int id);
        Task<T?> CreateAsync(T tag);
        Task<T> UpdateAsync(T tag);
        Task<T?> DeleteAsync(int id);
    }
}

