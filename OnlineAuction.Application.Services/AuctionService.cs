using OnlineAuction.Core.Entities;
using System.Security.Cryptography;

namespace OnlineAuction.Application.Services
{
    public class AuctionService : IAuctionService
    {
        private readonly Auction _auction;

        public AuctionService()
        {
            _auction = new Auction();
        }

        public void AddBid(Bid bid)
        {
            _auction.AddBid(bid);
        }

        public Bid GetWinningBid()
        {
            return _auction.DetermineWinningBid();
        }

        public IEnumerable<Bid> GetAllBids()
        {
            return _auction.GetBids();
        }
    }
}
