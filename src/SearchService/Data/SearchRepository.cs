using AutoMapper;
using MongoDB.Entities;
using SearchService.Models;
using SearchService.RequestHelpers;

namespace SearchService.Data;

public class SearchRepository : ISearchRepository
{
    private readonly IMapper _mapper;
    public SearchRepository(IMapper mapper)
    {
        _mapper = mapper;
    }
    public async Task<QueryResponse> SearchItems(SearchParams searchParams)
    {
        QueryResponse result = new();
        try
        {
            var query = DB.PagedSearch<Rating, Rating>();

            //CustomizeQuery(searchParams, ref query);

            if (!string.IsNullOrEmpty(searchParams.SearchTerm))
            {
                query.Match(Search.Full, searchParams.SearchTerm).SortByTextScore();
            }

            query = searchParams.OrderBy switch
            {
                "EstablishmentName" => query.Sort(x => x.Ascending(x => x.EstablishmentName)),
                "Username" => query.Sort(x => x.Ascending(x => x.Username)),
                "EstablishmentTypeName" => query.Sort(x => x.Ascending(x => x.EstablishmentTypeName)),
                _ => query.Sort(x => x.Ascending(x => x.FlaggedOn))
            };

            query = searchParams.FilterBy switch
            {
                "Color" => query.Match(x => x.Color == "Yellow"),
                "LimitResult" => query.Match(x => x.FlaggedOn > DateTime.UtcNow.AddDays(-7)),
                _ => query.Match(x => x.FlaggedOn < DateTime.UtcNow)
            };

            if (!string.IsNullOrEmpty(searchParams.Username))
            {
                query.Match(x => x.Username == searchParams.Username);
            }

            if (!string.IsNullOrEmpty(searchParams.EstablishmentName))
            {
                query.Match(x => x.EstablishmentName == searchParams.EstablishmentName);
            }

            if (!string.IsNullOrEmpty(searchParams.EstablishmentTypeName))
            {
                query.Match(x => x.EstablishmentTypeName == searchParams.EstablishmentTypeName);
            }

            if (!string.IsNullOrEmpty(searchParams.EstablishmentStatus))
            {
                query.Match(x => x.EstablishmentStatus == searchParams.EstablishmentStatus);
            }

            query.PageNumber(searchParams.PageNumber);
            query.PageSize(searchParams.PageSize);

            var queryResult = await query.ExecuteAsync();

            result.Results = queryResult.Results.ToList().AsReadOnly();
            result.TotalCount = queryResult.TotalCount;
            result.PageCount = queryResult.PageCount;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return result;
    }

    private void CustomizeQuery(SearchParams searchParams, ref PagedSearch<Rating, Rating> query)
    {
        if (!string.IsNullOrEmpty(searchParams.SearchTerm))
        {
            query.Match(Search.Full, searchParams.SearchTerm).SortByTextScore();
        }

        query = searchParams.OrderBy switch
        {
            "EstablishmentName" => query.Sort(x => x.Ascending(x => x.EstablishmentName)),
            "Username" => query.Sort(x => x.Ascending(x => x.Username)),
            "EstablishmentTypeName" => query.Sort(x => x.Ascending(x => x.EstablishmentTypeName)),
            _ => query.Sort(x => x.Ascending(x => x.FlaggedOn))
        };

        query = searchParams.FilterBy switch
        {
            "Color" => query.Match(x => x.Color == "Yellow"),
            "LimitResult" => query.Match(x => x.FlaggedOn > DateTime.UtcNow.AddDays(-7)),
            _ => query.Match(x => x.FlaggedOn < DateTime.UtcNow)
        };

        if (!string.IsNullOrEmpty(searchParams.Username))
        {
            query.Match(x => x.Username == searchParams.Username);
        }

        if (!string.IsNullOrEmpty(searchParams.EstablishmentName))
        {
            query.Match(x => x.EstablishmentName == searchParams.EstablishmentName);
        }

        if (!string.IsNullOrEmpty(searchParams.EstablishmentTypeName))
        {
            query.Match(x => x.EstablishmentTypeName == searchParams.EstablishmentTypeName);
        }


        if (!string.IsNullOrEmpty(searchParams.EstablishmentStatus))
        {
            query.Match(x => x.EstablishmentStatus == searchParams.EstablishmentStatus);
        }

        query.PageNumber(searchParams.PageNumber);
        query.PageSize(searchParams.PageSize);
    }
}
