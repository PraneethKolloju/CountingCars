using AuctionService.Data;
using AuctionService.DTOs;
using AuctionService.Entities;
using AutoMapper;
using Contracts;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Controller
{
    [ApiController]
    [Route("api/Auctions")]
    public class AuctionController : ControllerBase
    {
        private readonly AuctionDbContext _auctionDbcontext;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public AuctionController(AuctionDbContext auctionDbcontext, IMapper mapper, IPublishEndpoint publishEndpoint, ISendEndpointProvider sendEndpointProvider)
        {
            _auctionDbcontext = auctionDbcontext;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
            _sendEndpointProvider = sendEndpointProvider;
        }

        [HttpGet]
        public async Task<ActionResult<List<AuctionDTO>>> GetAuctions()
        {
            var auctions = await _auctionDbcontext.Auctions
                           .Include(x => x.Item)
                           .OrderBy(x => x.Item.Make)
                           .ToListAsync();

            return _mapper.Map<List<AuctionDTO>>(auctions);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<AuctionDTO>> GetAuctionById(Guid id)
        {
            var auction = await _auctionDbcontext.Auctions.Include(x => x.Item)
            .FirstOrDefaultAsync(i => i.Id == id);

            if (auction == null) return NotFound();

            return _mapper.Map<AuctionDTO>(auction);
        }

        [Authorize]
        [HttpPost]

        public async Task<ActionResult<AuctionDTO>> InsertAuctionData(CreateAuctionDTO auctiondto)
        {
            var auction = _mapper.Map<Auction>(auctiondto);

            auction.Seller = User.Identity.Name;
            _auctionDbcontext.Auctions.Add(auction);

            var newAuction = _mapper.Map<AuctionDTO>(auction);

            await _publishEndpoint.Publish(_mapper.Map<AuctionCreated>(newAuction));
            var result = await _auctionDbcontext.SaveChangesAsync() > 0;



            if (!result)
                return BadRequest("could process the request");
            return CreatedAtAction(nameof(GetAuctionById), new { auction.Id }, newAuction);
        }

        [Authorize]
        [HttpPut("{Id}")]
        public async Task<ActionResult> UpdateAuctionData(Guid Id, UpdateAuctionDTO updateAuctionDTO)
        {
            var ExistingData = await _auctionDbcontext.Auctions.Include(i => i.Item).FirstOrDefaultAsync(x => x.Id == Id);
            ExistingData.Seller = User.Identity.Name;
            if (ExistingData == null)
                return NotFound();

            ExistingData.Item.Make = updateAuctionDTO.Make ?? ExistingData.Item.Make;
            ExistingData.Item.Model = updateAuctionDTO.Model ?? ExistingData.Item.Model;
            ExistingData.Item.Mileage = updateAuctionDTO.Mileage ?? ExistingData.Item.Mileage;
            ExistingData.Item.Color = updateAuctionDTO.Color ?? ExistingData.Item.Color;
            ExistingData.Item.Year = updateAuctionDTO.Year ?? ExistingData.Item.Year;



            await _publishEndpoint.Publish(_mapper.Map<AuctionUpdated>(ExistingData));

            var result = await _auctionDbcontext.SaveChangesAsync() > 0;
            if (!result)
                return BadRequest("Problem while updating Data");
            return Ok();

        }

        [Authorize]
        [HttpDelete("{Id}")]

        public async Task<ActionResult> DeleteAuctionData(Guid Id)
        {
            var DatatoRemove = await _auctionDbcontext.Auctions.FirstOrDefaultAsync(i => i.Id == Id);
            if (DatatoRemove == null)
                return NotFound();

            _auctionDbcontext.Auctions.Remove(DatatoRemove);

            await _publishEndpoint.Publish(_mapper.Map<AuctionDeleted>(DatatoRemove));

            var result = await _auctionDbcontext.SaveChangesAsync() > 0;
            if (result)
                return Ok();
            else
                return NotFound();
        }


    }
}