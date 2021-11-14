using AspNetCore.IQueryable.Extensions.Attributes;
using AspNetCore.IQueryable.Extensions.Filter;
using AspNetCore.IQueryable.Extensions.Pagination;
using AspNetCore.IQueryable.Extensions.Sort;
#pragma warning disable CS8618

namespace Portal.TM.Api.ViewModels;
public class ProductSearch : IQueryPaging, IQuerySort
{
    public string? Name { get; set; }
    [QueryOperator(Operator = WhereOperator.Contains)]
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    
    [FromQuery(Name = "Data Creation Start")]
    public DateTime? DateCreationStart { get; set; }

    [FromQuery(Name = "Data Creation End")]
    public DateTime? DateCreationEnd { get; set; }
    public string? Sort { get; set; }
    public int? Limit { get; set; }
    public int? Offset { get; set; }
}
