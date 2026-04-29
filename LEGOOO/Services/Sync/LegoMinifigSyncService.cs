using System.Diagnostics;
using LEGOOO;

public class LegoMinifigSyncService
{
    private readonly RestService _api;
    private readonly LegoMinifigsRepository _repo;

    public LegoMinifigSyncService(RestService api, LegoMinifigsRepository repo)
    {
        _api = api;
        _repo = repo;
    }

    public async Task SyncLegoMinifigsAsync()
    {
        Debug.WriteLine("Starting to grab minifigs...");
        var minifigs = await _api.GetLegoMinifigsAsync();

        foreach (var minifig in minifigs)
        {
            await _repo.InsertOrReplaceAsync(minifig);
            Debug.WriteLine("INSERTED FIGURE: " + minifig.MinifigName);
        }
    }
}