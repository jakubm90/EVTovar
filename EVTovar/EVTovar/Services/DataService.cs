using EVTovar.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace EVTovar.Services
{

    public class DataService : IDataService
    {
        readonly SQLiteAsyncConnection db;

        public DataService()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MyData.db");
            db = new SQLiteAsyncConnection(dbPath);
            db.DropTableAsync<Item>();
            db.CreateTableAsync<Item>().Wait();
            db.CreateTableAsync<Category>().Wait();
            
            //test
            for (int i = 0; i < 10; i++) db.InsertAsync(new Item { Name = "TEST", Description = "Blablabla"}).Wait();
        }

        public Task<List<T>> GetAllDataAsync<T>() where T : new()
        {
            return db.Table<T>().ToListAsync();
        }

        public Task<T> GetDataAsync<T>(int id) where T : BaseModel, new()
        {
            return db.Table<T>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync<T>(T item)
        {
            return db.InsertAsync(item);
        }

        public Task<int> UpdateItemAsync<T>(T item)
        {
            return db.UpdateAsync(item);
        }

        public Task<int> RemoveItemAsync<T>(T item)
        {
            return db.DeleteAsync(item);
        }
    }
}
