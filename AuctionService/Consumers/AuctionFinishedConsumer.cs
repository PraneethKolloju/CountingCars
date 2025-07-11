using AuctionService.Data;
using MassTransit;
using Contracts;
using AuctionService.Entities;

namespace AuctionService.Consumers
{
    public class AuctionFinishedConsumer : IConsumer<AuctionFinished>
    {
        private readonly AuctionDbContext _auctionDbContext;
        public AuctionFinishedConsumer(AuctionDbContext auctionDbContext)
        {
            _auctionDbContext = auctionDbContext;
        }
        public async Task Consume(ConsumeContext<AuctionFinished> context)
        {
            var auctionResult = await _auctionDbContext.Auctions.FindAsync(context.Message.AuctionId);
            if (context.Message.ItemSold)
            {
                auctionResult.Winner = context.Message.Winner;
                // auctionResult.Seller = context.Message.Seller;
                auctionResult.SoldAmount = context.Message.Amount;
            }
            auctionResult.Status = auctionResult.SoldAmount > auctionResult.ReservePrice ? Status.Finished : Status.ReserveNotMet;
            await _auctionDbContext.SaveChangesAsync();   
        }
    }
}