using Contracts;
using MassTransit;
using MongoDB.Entities;

namespace searchservice
{
    public class BidPlacedConsumer : IConsumer<BidPlaced>
    {

        public async Task Consume(ConsumeContext<BidPlaced> context)
        {
            var searchResult = await DB.Find<Item>().OneAsync(context.Message.Id);
            if (searchResult.CurrentHighBid > context.Message.Amount && context.Message.BidStatus.Contains("Accepted"))
            {
                searchResult.CurrentHighBid = context.Message.Amount;
                await searchResult.SaveAsync();
            }
        }
    }
}