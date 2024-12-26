using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GetAndFetchAPIs.Data;
using Microsoft.EntityFrameworkCore;

public class ApiDataService
{
    private readonly AppDbContext _ctx;
    private const string ApiKeyHeaderName = "X-API-Key";
    private const string ApiKeyValue = @"$m£9&\Ima~/H>}9SB7Os@n1)xoK2|NDlzf793M#B)cfiS0$a"; // Replace with your actual API key

    public ApiDataService(AppDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task UpdateAllTablesAsync()
    {
        var apiConfigurations = new List<ApiConfigurationBase>
        {
            new ApiConfiguration<Sales>("http://41.33.204.202:8000/pg_company/api/v1/get_pg_company_data?StartDate={start}&EndDate={end}", _ctx.Sales!, _ctx),
            new ApiConfiguration<CompetitorsSales>("http://41.33.204.202:8000/pg_company/api/v1/get_pg_company_competitors_data?StartDate={start}&EndDate={end}", _ctx.CompetitorsSales!, _ctx),
            new ApiConfiguration<Stock>("http://41.33.204.202:8000/pg_company/api/v1/company_items_stock?StartDate={start}&EndDate={end}", _ctx.Stock!, _ctx),
            new ApiConfiguration<CompetitorsStock>("http://41.33.204.202:8000/pg_company/api/v1/company_items_stock_with_competitors?StartDate={start}&EndDate={end}", _ctx.CompetitorsStock!, _ctx),
            new ApiConfiguration<ValueSharePer>("http://41.33.204.202:8000/pg_company/api/v1/value_percentage_per_category_per_brand?StartDate={start}&EndDate={end}", _ctx.ValueSharePer!, _ctx),
            new ApiConfiguration<StockSharePer>("http://41.33.204.202:8000/pg_company/api/v1/stock_percentage_per_category_per_brand?StartDate={start}&EndDate={end}", _ctx.StockSharePer!, _ctx),
            // Add other configurations with corresponding DbSets...
        };

        string start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd");
        string end = DateTime.Now.ToString("yyyy-MM-dd");

        foreach (var apiConfig in apiConfigurations)
        {
            string apiUrl = apiConfig.ApiUrlTemplate!
                .Replace("{start}", start)
                .Replace("{end}", end);

            string data = await GetDataFromApiAsync(apiUrl);
            await apiConfig.UpdateDatabaseAsync(data);
        }
    }

    private async Task<string> GetDataFromApiAsync(string apiUrl)
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                // Add the API key to the request headers
                client.DefaultRequestHeaders.Add("X-API-Key", @"$m£9&\Ima~/H>}9SB7Os@n1)xoK2|NDlzf793M#B)cfiS0$a");

                // Send the GET request
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                // Check if the response is successful
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    // Handle unsuccessful response
                    Console.WriteLine($"Error: {response.StatusCode}, {response.ReasonPhrase}");
                    return $"Error: {response.StatusCode}, {response.ReasonPhrase}";
                }
            }
            catch (HttpRequestException ex)
            {
                // Handle errors
                Console.WriteLine($"HTTP Request Error: {ex.Message}");
                return $"Error: {ex.Message}";
            }
            catch (Exception ex)
            {
                // Handle general errors
                Console.WriteLine($"General Error: {ex.Message}");
                return $"Error: {ex.Message}";
            }
        }
    }
}

public class ApiConfiguration<T> : ApiConfigurationBase where T : class, IApiData, new()
{
    private readonly DbSet<T> _dbSet;
    private readonly AppDbContext _ctx;

    public ApiConfiguration(string apiUrlTemplate, DbSet<T> dbSet, AppDbContext ctx)
    {
        ApiUrlTemplate = apiUrlTemplate;
        _dbSet = dbSet;
        _ctx = ctx;
    }

    public override async Task UpdateDatabaseAsync(string newData)
    {
        // Delete current month's data based on SQLInsertDate
        var currentMonthData = await _dbSet
            .Where(d => d.SQLInsertDate.Month == DateTime.Now.Month && d.SQLInsertDate.Year == DateTime.Now.Year)
            .ToListAsync();

        _dbSet.RemoveRange(currentMonthData);

        // Insert new data
        var newEntry = new T
        {
            Data = newData,
            SQLInsertDate = DateTime.Now
        };

        await _dbSet.AddAsync(newEntry);
        await _ctx.SaveChangesAsync();
    }
}

public abstract class ApiConfigurationBase
{
    public string? ApiUrlTemplate { get; set; }
    public abstract Task UpdateDatabaseAsync(string newData);
}