using AuctionService.Data;
using AuctionService.DTOs;
using AuctionService.Entities;
using AutoMapper;
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
        public AuctionController(AuctionDbContext auctionDbcontext, IMapper mapper)
        {
            _auctionDbcontext = auctionDbcontext;
            _mapper = mapper;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<AuctionDTO>> GetAuctionById(Guid id)
        {
            var auction = await _auctionDbcontext.Auctions.Include(x => x.Item)
            .FirstOrDefaultAsync(i => i.Id == id);

            if (auction == null) return NotFound();

            return _mapper.Map<AuctionDTO>(auction);
        }

        [HttpPost]

        public async Task<ActionResult<AuctionDTO>> InsertAuctionData(CreateAuctionDTO auctiondto)
        {
            var auction = _mapper.Map<Auction>(auctiondto);

            _auctionDbcontext.Auctions.Add(auction);

            var result = await _auctionDbcontext.SaveChangesAsync() > 0;

            if (!result)
                return BadRequest("could process the request");
            return CreatedAtAction(nameof(GetAuctionById), new { auction.Id }, _mapper.Map<AuctionDTO>(auction));
        }


        [HttpPut("{Id}")]
        public async Task<ActionResult> UpdateAuctionData(Guid Id, UpdateAuctionDTO updateAuctionDTO)
        {
            var ExistingData = await _auctionDbcontext.Auctions.Include(i => i.Item).FirstOrDefaultAsync(x => x.Id == Id);

            if (ExistingData == null)
                return NotFound();

            ExistingData.Item.Make = updateAuctionDTO.Make ?? ExistingData.Item.Make;
            ExistingData.Item.Model = updateAuctionDTO.Model ?? ExistingData.Item.Model;
            ExistingData.Item.Mileage = updateAuctionDTO.Mileage ?? ExistingData.Item.Mileage;
            ExistingData.Item.Color = updateAuctionDTO.Color ?? ExistingData.Item.Color;
            ExistingData.Item.Year = updateAuctionDTO.Year ?? ExistingData.Item.Year;

            var result = await _auctionDbcontext.SaveChangesAsync() > 0;
            if (!result)
                return BadRequest("Problem while updating Data");
            return Ok();

        }

        [HttpDelete("{Id}")]

        public async Task<ActionResult> DeleteAuctionData(Guid Id)
        {
            var DatatoRemove = await _auctionDbcontext.Auctions.FirstOrDefaultAsync(i => i.Id == Id);
            if (DatatoRemove == null)
                return NotFound();

            _auctionDbcontext.Auctions.Remove(DatatoRemove);

            var result = await _auctionDbcontext.SaveChangesAsync() > 0;
            if (result)
                return Ok();
            else
                return NotFound();
        }


    }
}