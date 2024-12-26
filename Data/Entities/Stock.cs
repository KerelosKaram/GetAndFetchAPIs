using GetAndFetchAPIs.Data;

public class Stock : IApiData
{
    public int StockID { get; set; }
    public string? St_code { get; set; }
    public string? St_name { get; set; }
    public string? ItemCode { get; set; }
    public string? ItemName { get; set; }
    public decimal Balance { get; set; }
    public string? InsertedDate { get; set; } // Assuming this is a string representation of a date
    public DateTime SQLInsertDate { get; set; }
    public string? Data { get; set; }
}