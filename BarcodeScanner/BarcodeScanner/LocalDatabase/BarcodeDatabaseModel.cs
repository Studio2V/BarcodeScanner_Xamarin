using BarcodeScanner.Common;
using SQLite;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using BarcodeScanner.Models;

namespace BarcodeScanner.LocalDatabase
{
    public class BarcodeDatabaseModel
    {
        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<BarcodeDatabaseModel> Instance = new AsyncLazy<BarcodeDatabaseModel>(async () =>
        {
            var instance = new BarcodeDatabaseModel();
            CreateTableResult result = await Database.CreateTableAsync<ScanitemsList>();
            return instance;
        });

        public BarcodeDatabaseModel()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }

        public Task<List<ScanitemsList>> GetItemsAsync()
        {
            return Database.Table<ScanitemsList>().ToListAsync();
        }

        public Task<List<ScanitemsList>> GetItemsNotDoneAsync()
        {
            return Database.QueryAsync<ScanitemsList>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        }

        public Task<int> SaveItemAsync(ScanitemsList item)
        {
            return Database.InsertAsync(item);
        }

        public Task<int> DeleteItemAsync(ScanitemsList item)
        {
            return Database.DeleteAsync(item);
        }
    }

    public class AsyncLazy<T>
    {
        readonly Lazy<Task<T>> instance;

        public AsyncLazy(Func<T> factory)
        {
            instance = new Lazy<Task<T>>(() => Task.Run(factory));
        }

        public AsyncLazy(Func<Task<T>> factory)
        {
            instance = new Lazy<Task<T>>(() => Task.Run(factory));
        }

        public TaskAwaiter<T> GetAwaiter()
        {
            return instance.Value.GetAwaiter();
        }
    }
}
