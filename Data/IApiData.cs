namespace GetAndFetchAPIs.Data
{
    public interface IApiData
    {
        DateTime SQLInsertDate { get; set; }
        string? Data { get; set; }
    }
}