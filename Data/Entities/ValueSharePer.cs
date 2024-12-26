using GetAndFetchAPIs.Data;

public class ValueSharePer : IApiData
{
    public int SharePerID { get; set; }
    public string? CategoryName { get; set; }
    public string? CompanyName { get; set; }
    public string? BrandName { get; set; }
    public decimal Percentage { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime SQLInsertDate { get; set; }
    public string? Data { get; set; }

}