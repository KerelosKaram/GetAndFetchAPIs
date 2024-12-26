using GetAndFetchAPIs.Data;

public class Sales : IApiData
{
    public int SalesID { get; set; }
    public DateTime Date { get; set; }
    public string? Cus_Id { get; set; }
    public string? Class_name { get; set; }
    public string? Channel { get; set; }
    public string? MG_Name { get; set; }
    public string? SG_Name { get; set; }
    public string? Product_Code { get; set; }
    public string? Product_Discription { get; set; }
    public string? U_name { get; set; }
    public long Invoice_ID { get; set; }
    public long Seller_ID { get; set; }
    public decimal Value { get; set; }
    public decimal Volume { get; set; }
    public DateTime InsertDate { get; set; }

/// <summary>
///
/// </summary>
    public DateTime SQLInsertDate { get; set; }
    public string? Data { get; set; }
}