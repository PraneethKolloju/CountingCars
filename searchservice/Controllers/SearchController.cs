using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;
using searchservice.RequestHelpers;

namespace searchservice.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Item>>> SearchItems([FromQuery] SearchParams searchParams)
        {
            var query = DB.PagedSearch<Item, Item>();

            query.Sort(x => x.Ascending(i => i.Make));

            if (!string.IsNullOrEmpty(searchParams.SearchTerm))
            {
                query.Match(Search.Full, searchParams.SearchTerm).SortByTextScore();
            }

            query = searchParams.OrderBy switch
            {
                "make" => query.Sort(i => i.Ascending(x => x.Make)),
                "new" => query.Sort(i => i.Descending(o => o.CreatedAt)),
                _ => query.Sort(i => i.Ascending(a => a.AuctionEnd))

            };

            query = searchParams.FilterBy switch
            {
                "Finished" => query.Match(i => i.AuctionEnd < DateTime.UtcNow),
                "EndingSoon" => query.Match(i => i.AuctionEnd < DateTime.UtcNow.AddHours(6) && i.AuctionEnd > DateTime.UtcNow),
                _ => query.Match(i => i.AuctionEnd > DateTime.UtcNow)
            };

            if (!string.IsNullOrEmpty(searchParams.Seller))
            {
                query.Match(i => i.Seller == searchParams.Seller);
            }

            if (!string.IsNullOrEmpty(searchParams.Winner))
            {
                query.Match(i => i.Winner == searchParams.Winner);
            }


            query.PageNumber(searchParams.PageNumber);
            query.PageSize(searchParams.PageSize);

            var result = await query.ExecuteAsync();

            return Ok(new
            {
                results = result.Results,
                pageCount = result.PageCount,
                totalCount = result.TotalCount
            });
        }
    }
}