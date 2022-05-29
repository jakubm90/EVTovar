using EVTovar.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using static EVTovar.Services.DataService;

namespace EVTovar.Services
{
    public interface IDataService
    {
        Task<List<T>> GetAllDataAsync<T>() where T : new();
        Task<T> GetDataByIdAsync<T>(int id) where T : BaseModel, new();
        Task<List<T>> GetQueryAsync<T>(string query) where T : new();
        Task<int> RemoveItemAsync<T>(T item);
        Task<int> SaveItemAsync<T>(T item);
        Task<int> UpdateItemAsync<T>(T item);
    }
}