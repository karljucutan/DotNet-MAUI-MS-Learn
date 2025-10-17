using People.Models;
using SQLite;

namespace People;

public class PersonRepository
{
    private string _dbPath;
    private SQLiteConnection _conn;

    public string StatusMessage { get; set; }

    // TODO: Add variable for the SQLite connection

    private void Init()
    {
        if (_conn != null)
            return;

        _conn = new SQLiteConnection(_dbPath);
        _conn.CreateTable<Person>();
    }

    public PersonRepository(string dbPath)
    {
        _dbPath = dbPath;                        
    }

    public void AddNewPerson(string name)
    {            
        int result = 0;
        try
        {
            Init();

            // basic validation to ensure a name was entered
            if (string.IsNullOrEmpty(name))
                throw new Exception("Valid name required");

            result = _conn.Insert(new Person { Name = name });

            StatusMessage = string.Format("{0} record(s) added (Name: {1})", result, name);
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);
        }

    }

    public List<Person> GetAllPeople()
    {
        try
        {
            Init();
            return _conn.Table<Person>().ToList();
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }

        return new List<Person>();
    }
}
