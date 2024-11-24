using Microsoft.AspNetCore.Mvc;
using OnlineAuction.Application.Services;
using OnlineAuction.Core.Entities;

namespace OnlineAuction.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuctionController : ControllerBase
    {
        private readonly IAuctionService _auctionService;

        public AuctionController(IAuctionService auctionService)
        {
            _auctionService = auctionService;
        }

        [HttpPost("add-bid")]
        public IActionResult AddBid([FromBody] Bid bid)
        {
            _auctionService.AddBid(bid);
            return Ok("Bid added successfully.");
        }

        [HttpGet("winning-bid")]
        public IActionResult GetWinningBid()
        {
            var winningBid = _auctionService.GetWinningBid();
            return Ok(winningBid);
        }

        [HttpGet("bids")]
        public IActionResult GetAllBids()
        {
            var bids = _auctionService.GetAllBids();
            return Ok(bids);
        }
    }
}
