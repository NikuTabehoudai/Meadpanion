using System.Collections.Generic;
using SQLite;
using System.Threading.Tasks;
using System;
using Meadpanion.Models;

namespace Meadpanion
{
    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Mead>().Wait();
            _database.CreateTableAsync<Reading>().Wait();
            _database.CreateTableAsync<MeadEvents>().Wait();
            _database.CreateTableAsync<Recipe>().Wait();

        }


        #region Mead
        public Task<List<Mead>> GetMeadsAsync()
        {
            return _database.Table<Mead>().ToListAsync();
        }

        public Task<int> SaveMeadAsync(Mead mead)
        {
            return _database.InsertAsync(mead);
        }

        public Task<int> DeleteMeadAsync(Mead mead)
        {
            return _database.DeleteAsync(mead);
        }

        public Task<int> UpdateMeadAsync(Mead mead)
        {
            return _database.UpdateAsync(mead);
        }

        public Task<Mead> GetSingleMeadAsync(int ID)
        {
            return _database.Table<Mead>().Where(s => s.ID == ID).FirstAsync();
        }


        #endregion

        #region Reading
        public Task<List<Reading>> GetReadingsAsync(int ID)
        {
            return _database.Table<Reading>().Where(s => s.MeadId == ID).OrderBy<DateTime>(s => s.Date).ToListAsync();
        }

        public Task<int> SaveReadingsAsync(Reading readings)
        {
            return _database.InsertAsync(readings);
        }

        public Task<int> DeleteReadingAsync(Reading readings)
        {
            return _database.DeleteAsync(readings);
        }

        public Task<int> UpdateRaedingsAsync(Reading readings)
        {
            return _database.UpdateAsync(readings);
        }

        public Task<Reading> GetSingleReadingAsync(int ID)
        {
            return _database.Table<Reading>().Where(s => s.ID == ID).FirstAsync();
        }

        #endregion

        #region Recipe
        public Task<List<Recipe>> GetRecipeAsync()
        {
            return _database.Table<Recipe>().ToListAsync();
        }

        public Task<int> SaveRecipeAsync(Recipe recipe)
        {
            return _database.InsertAsync(recipe);
        }

        public Task<int> UpdateRecipeAsync(Recipe recipe)
        {
            return _database.UpdateAsync(recipe);
        }

        public Task<int> DeleteRecipeAsync(Recipe recipe)
        {
            return _database.DeleteAsync(recipe);
        }

        public Task<Recipe> GetSingleRecipe(int ID)
        {
            return _database.Table<Recipe>().Where(s => s.ID == ID).FirstAsync();
        }


        #endregion

        #region Events

        public Task<List<MeadEvents>> GetEventsAsync(int ID)
        {
            return _database.Table<MeadEvents>().Where(s => s.MeadId == ID).OrderBy<DateTime>(s => s.Date).ToListAsync();
        }

        public Task<int> SaveEventAsync(MeadEvents meadEvents)
        {
            return _database.InsertAsync(meadEvents);
        }

        public Task<int> DeleteEventAsync(MeadEvents meadEvents)
        {
            return _database.DeleteAsync(meadEvents);
        }

        public Task<int> UpdateEventAsync(MeadEvents meadEvents)
        {
            return _database.UpdateAsync(meadEvents);
        }

        public Task<MeadEvents> GetSingleEventAsync(int ID)
        {
            return _database.Table<MeadEvents>().Where(s => s.ID == ID).FirstAsync();
        }

        #endregion


        #region Testing

        public async Task<int> ClearMeadData()
        {
            return await _database.DeleteAllAsync<Mead>();
        }

        public async Task<int> ClearRecipeData()
        {
            return await _database.DeleteAllAsync<Recipe>();
        }

        public async Task<int> ClearReadingData()
        {
            return await _database.DeleteAllAsync<Reading>();
        }

        public async Task<int> ClearEventData()
        {
            return await _database.DeleteAllAsync<MeadEvents>();
        }

        #endregion






    }


}
