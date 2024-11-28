using CobaSQLite.Models;
using SQLite;
namespace CobaSQLite;

public class CategoryRepository
{
    private string _dbPath;
    private SQLiteAsyncConnection conn;

    public string StatusMessage { get; set; }

    public CategoryRepository(string dbPath)
    {
        _dbPath = dbPath;
    }

    private async Task Init()
    {
        if (conn != null)
            return;

        conn = new SQLiteAsyncConnection(_dbPath);
        await conn.CreateTableAsync<Category>();
    }

    public async Task AddNewCategory(string name, string description)
    {
        try
        {
            await Init();

            if (string.IsNullOrEmpty(name))
                throw new Exception("Valid name required");
            if (string.IsNullOrEmpty(description))
                throw new Exception("Valid description required");

            var result = await conn.InsertAsync(new Category
            {
                Name = name,
                Description = description
            });

            StatusMessage = $"{result} record(s) added (Name: {name}, Description: {description})";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Failed to add {name}. Error: {ex.Message}";
        }
    }

    public async Task<List<Category>> GetAllCategory()
    {
        try
        {
            await Init();
            return await conn.Table<Category>().ToListAsync();
        }
        catch (Exception ex)
        {
            StatusMessage = $"Failed to retrieve data. Error: {ex.Message}";
        }

        return new List<Category>();
    }

    public async Task UpdateCategory(int id, string name, string description)
    {
        try
        {
            await Init();

            var category = await conn.FindAsync<Category>(id);
            if (category == null)
                throw new Exception($"Category with ID {id} not found.");

            category.Name = name;
            category.Description = description;

            var result = await conn.UpdateAsync(category);
            StatusMessage = $"{result} record(s) updated (ID: {id}, Name: {name}, Description: {description})";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Failed to update category. Error: {ex.Message}";
        }
    }

    public async Task DeleteCategory(int id)
    {
        try
        {
            await Init();

            var category = await conn.FindAsync<Category>(id);
            if (category == null)
                throw new Exception($"Category with ID {id} not found.");

            var result = await conn.DeleteAsync(category);
            StatusMessage = $"{result} record(s) deleted (ID: {id})";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Failed to delete category. Error: {ex.Message}";
        }
    }
}
