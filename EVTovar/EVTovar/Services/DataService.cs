using EVTovar.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
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

            //test
            //for (int i = 1; i <= 25; i++) db.InsertAsync(new Item { Name = i+"TEST", Description = "BlablablaBla" }).Wait();
        }

        public class FullItem
        {
            public string NameItem { get; set; }
            public string CategoryName { get; set; }
            public int idcko { get; set; }
        }

        public Task<List<T>> GetQueryAsync<T>(string query) where T : new()
        {
            //var items = db.QueryAsync<T>(@"SELECT I.Name as NameItem, I.Id as idcko, C.Name as CategoryName from Item I inner join Category C on I.CategoryID = C.Id order by I."+name+ " DESC", id);
            var items = db.QueryAsync<T>(query);
            return items;
        }

        public Task<List<T>> GetAllDataAsync<T>() where T : new()
        {
            return db.Table<T>().ToListAsync();
        }      

        public Task<T> GetDataByIdAsync<T>(int id) where T : BaseModel, new()
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
