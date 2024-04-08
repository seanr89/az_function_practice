

public class MovieDbService
{
    private readonly HttpClient _httpClient;
    private readonly string _remoteServiceBaseUrl;

    public MovieDbService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // public async Task<Catalog> GetCatalogItems(int page, int take,
    //                                            int? brand, int? type)
    // {
    //     var uri = API.Catalog.GetAllCatalogItems(_remoteServiceBaseUrl,
    //                                              page, take, brand, type);

    //     var responseString = await _httpClient.GetStringAsync(uri);

    //     var catalog = JsonConvert.DeserializeObject<Catalog>(responseString);
    //     return catalog;
    // }
}