using SQLite;

public class LegoMinifigsRepository
{
    private readonly SQLiteAsyncConnection _db;

    public LegoMinifigsRepository(AppDatabase database)
    {
        _db = database.Connection;
    }

    public Task<List<LegoMinifig>> GetAllAsync()
        => _db.Table<LegoMinifig>().ToListAsync();

    public Task InsertAsync(LegoMinifig minifig)
        => _db.InsertAsync(minifig);

    public Task InsertOrReplaceAsync(LegoMinifig minifig)
        => _db.InsertOrReplaceAsync(minifig);
}