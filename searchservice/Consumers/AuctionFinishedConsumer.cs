using Contracts;
using MassTransit;
using MongoDB.Entities;

namespace searchservice.Consumers
{
    public class AuctionFinishedConsumer : IConsumer<AuctionFinished>
    {
        public async Task Consume(ConsumeContext<AuctionFinished> context)
        {
            var auction = await DB.Find<Item>().OneAsync(context.Message.AuctionId);
            if (auction == null)
            {
                auction.Winner = context.Message.Winner;
                auction.SoldAmount = (int)context.Message.Amount;
            }
            auction.Status = "Finished";
            await auction.SaveAsync();
        }

    }
}