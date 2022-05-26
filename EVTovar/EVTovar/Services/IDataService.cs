using EVTovar.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EVTovar.Services
{
    public interface IDataService
    {
        Task<List<T>> GetAllDataAsync<T>() where T : new();
        Task<T> GetDataAsync<T>(int id) where T : BaseModel, new();
        Task<int> RemoveItemAsync<T>(T item);
        Task<int> SaveItemAsync<T>(T item);
        Task<int> UpdateItemAsync<T>(T item);
    }
}