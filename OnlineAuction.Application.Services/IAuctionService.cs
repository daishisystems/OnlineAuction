using OnlineAuction.Core.Entities;

namespace OnlineAuction.Application.Services
{
    public interface IAuctionService
    {
        void AddBid(Bid bid);
        Bid GetWinningBid();
        IEnumerable<Bid> GetAllBids();
    }
}
