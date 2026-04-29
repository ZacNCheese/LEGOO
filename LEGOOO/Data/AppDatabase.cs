using SQLite;

public class AppDatabase
{
    private readonly SQLiteAsyncConnection _db;

    public AppDatabase(string dbPath)
    {
        _db = new SQLiteAsyncConnection(dbPath);

        // 👇 THIS creates your table
        _db.CreateTableAsync<LegoColor>().Wait();
    }

    public SQLiteAsyncConnection Connection => _db;
}