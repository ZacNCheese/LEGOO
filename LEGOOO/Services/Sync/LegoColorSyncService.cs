using System.Diagnostics;
using LEGOOO;

public class LegoColorSyncService
{
    private readonly RestService _api;
    private readonly LegoColorRepository _repo;

    public LegoColorSyncService(RestService api, LegoColorRepository repo)
    {
        _api = api;
        _repo = repo;
    }

    public async Task SyncLegoColorsAsync()
    {
        var colors = await _api.GetLegoColorsAsync();

        foreach (var color in colors)
        {
            await _repo.InsertOrReplaceAsync(color);
        }
    }
}