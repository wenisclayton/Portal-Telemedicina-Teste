using AspNetCore.IQueryable.Extensions.Attributes;
using AspNetCore.IQueryable.Extensions.Filter;
using AspNetCore.IQueryable.Extensions.Pagination;
using AspNetCore.IQueryable.Extensions.Sort;

namespace Portal.TM.Api.ViewModels;

public class UserSearch : IQueryPaging, IQuerySort
{
    [FromQuery(Name = "Name User")]
    public string? UserName { get; set; }
    public string? Email { get; set; }
    
    [QueryOperator(Operator = WhereOperator.Contains)]
    public string? FullName { get; set; }
    
    [FromQuery(Name = "Data Creation Start")]
    public DateTime? DateCreationStart { get; set; }

    [FromQuery(Name = "Data Creation End")]
    public DateTime? DateCreationEnd { get; set; }
    public string? Sort { get; set; }
    public int? Limit { get; set; }
    public int? Offset { get; set; }
}