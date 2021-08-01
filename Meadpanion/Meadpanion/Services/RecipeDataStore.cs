using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Meadpanion.Models;
using System.Linq;

namespace Meadpanion.Services
{
    public class RecipeDataStore : IDataStore<Recipe>
    {
        private List<Recipe> recipes;

        public RecipeDataStore() 
        { 
            recipes = new List<Recipe>();
            //todo: create link to recipe list
            //temp recipe list for testing
            recipes.Add(new Recipe() { Name = "Recipe 1", ID = 1 });
            recipes.Add(new Recipe() { Name = "Recipe 2", ID = 2 });
            recipes.Add(new Recipe() { Name = "Recipe 3", ID = 3 });
            recipes.Add(new Recipe() { Name = "Recipe 4", ID = 4 });
            recipes.Add(new Recipe() { Name = "Recipe 5", ID = 5 });
        }

        public Task<int> AddItemAsync(Recipe item)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteItemAsync(int ID)
        {
            throw new NotImplementedException();
        }

        public async Task<Recipe> GetItemAsync(int id)
        {
            return await Task.FromResult(recipes.FirstOrDefault(s => s.ID == id));
        }

        public async Task<IEnumerable<Recipe>> GetItemsAsync(int notUsed)
        {
            return await Task.FromResult(recipes);
        }

        public Task<int> UpdateItemAsync(Recipe item)
        {
            throw new NotImplementedException();
        }
    }
}
