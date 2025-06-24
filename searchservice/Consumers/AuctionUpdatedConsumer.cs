using MassTransit;
using Contracts;
using AutoMapper;
using MongoDB.Entities;

namespace searchservice
{
    public class AuctionUpdatedConsumer : IConsumer<AuctionUpdated>
    {
        private readonly IMapper _mapper;

        public AuctionUpdatedConsumer(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task Consume(ConsumeContext<AuctionUpdated> context)
        {
            try
            {
                Console.WriteLine("--> updating ", context.Message);
                var item = _mapper.Map<Item>(context.Message);
                var result = await DB.Update<Item>()
                        .Match(a => a.ID == context.Message.Id)
                        .ModifyOnly(x => new
                        {
                            x.Color,
                            x.Make,
                            x.Model,
                            x.Year,
                            x.Mileage
                        }, item)
                        .ExecuteAsync();

                if (!result.IsAcknowledged)
                    throw new MessageException(typeof(AuctionUpdated), "Problem updating mongoDb");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}