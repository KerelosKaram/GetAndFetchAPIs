using GetAndFetchAPIs.Data;

public class CompetitorsStock : IApiData
{
    public int CompetitorsStockID { get; set; }
    public string? St_code { get; set; }
    public string? St_name { get; set; }
    public string? ItemCode { get; set; }
    public string? ItemName { get; set; }
    public decimal Balance { get; set; }
    public DateTime InsertedDate { get; set; }
    public string? BrandName { get; set; }
    public string? Company { get; set; }
    public string? MainGroup { get; set; }
    public string? SubGroup { get; set; }
    public string? SubGroup2 { get; set; }
    public DateTime SQLInsertDate { get; set; }
    public string? Data { get; set; }
}