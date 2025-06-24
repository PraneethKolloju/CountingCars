using MassTransit;
using Contracts;
using AutoMapper;
using MongoDB.Entities;

namespace searchservice
{
    public class AuctionCreatedConsumer : IConsumer<AuctionCreated>
    {
        private readonly IMapper _mapper;

        public AuctionCreatedConsumer(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task Consume(ConsumeContext<AuctionCreated> context)
        {
            try
            {
                Console.WriteLine("--> Consuming Auction Created: " + context.Message.Id);
                var item = _mapper.Map<Item>(context.Message);
                await item.SaveAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}