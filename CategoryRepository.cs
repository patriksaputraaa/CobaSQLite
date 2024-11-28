using CobaSQLite.Models;
using SQLite;
namespace CobaSQLite;

public class CategoryRepository
{
    string _dbPath;

    public string StatusMessage { get; set; }

    // TODO: Add variable for the SQLite connection
    //private SQLiteConnection conn;
    private SQLiteAsyncConnection conn;
    private async void Init()
    {
        // TODO: Add code to initialize the repository
        if (conn != null)
            return;

        conn = new SQLiteAsyncConnection(_dbPath);
        await conn.CreateTableAsync<Category>();
    }

    public CategoryRepository(string dbPath)
    {
        _dbPath = dbPath;
    }

    public void AddNewCategory(string name, string description)
    {
        int result = 0;
        try
        {
            // TODO: Call Init()
            Init();
            // basic validation to ensure a name was entered
            if (string.IsNullOrEmpty(name))
                throw new Exception("Valid name required");
            if (string.IsNullOrEmpty(description))
                throw new Exception("Valid desc required");
            // TODO: Insert the new person into the database
            result = conn.Insert(new Category { 
                Name = name,
                Description = description
            });

            StatusMessage = string.Format("{0} record(s) added (Name: {1}, Description: {2})", result, name, description);
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);
        }

    }

    public List<Category> GetAllCategory()
    {
        // TODO: Init then retrieve a list of Person objects from the database into a list
        try
        {
            Init();
            return conn.Table<Category>().ToList();
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }

        return new List<Category>();
    }
}
