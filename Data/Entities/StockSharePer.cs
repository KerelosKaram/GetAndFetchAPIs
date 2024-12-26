using GetAndFetchAPIs.Data;

public class StockSharePer : IApiData
{
    public int CompetitorsSharePerID { get; set; }
    public string? CategoryName { get; set; }
    public string? CompanyName { get; set; }
    public string? BrandName { get; set; }
    public decimal? Percentage { get; set; }
    public decimal Balance { get; set; }
    public DateTime Date { get; set; }
    public DateTime SQLInsertDate { get; set; }
    public string? Data { get; set; }
}