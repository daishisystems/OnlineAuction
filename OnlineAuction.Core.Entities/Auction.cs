namespace OnlineAuction.Core.Entities
{
    public class Auction
    {
        private List<Bid> Bids { get; set; } = new List<Bid>();

        public void AddBid(Bid bid)
        {
            Bids.Add(bid);
        }

        public IEnumerable<Bid> GetBids()
        {
            return Bids;
        }

        public Bid DetermineWinningBid()
        {
            if (!Bids.Any())
            {
                throw new InvalidOperationException("No bids are present in the auction.");
            }

            if (Bids.Count == 1)
            {
                return Bids.First(); // Single bidder wins by default
            }

            bool bidIncremented;

            do
            {
                bidIncremented = false;

                foreach (var currentBid in Bids)
                {
                    // Get the current highest bid among competing bids
                    var maxOtherBid = Bids
                        .Where(b => b != currentBid)
                        .Select(b => b.StartingBid)
                        .DefaultIfEmpty(0)
                        .Max();

                    // Increment the current bid if:
                    // 1. It is less than or equal to the maxOtherBid.
                    // 2. The incremented value does not exceed its MaxBid.
                    if (currentBid.StartingBid <= maxOtherBid && currentBid.StartingBid + currentBid.AutoIncrement <= currentBid.MaxBid)
                    {
                        currentBid.StartingBid += currentBid.AutoIncrement;
                        bidIncremented = true;
                    }
                    // Special case: Allow exact increment to reach MaxBid if necessary to compete
                    else if (currentBid.StartingBid <= maxOtherBid && currentBid.StartingBid < currentBid.MaxBid)
                    {
                        currentBid.StartingBid = currentBid.MaxBid;
                        bidIncremented = true;
                    }
                }

            } while (bidIncremented);

            // Determine the final winning bid
            return Bids.OrderByDescending(b => b.StartingBid).ThenBy(b => b.Bidder).First();
        }

    }
}
