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
                // If there's only one bid, it's the winner
                return Bids.First();
            }

            bool bidIncremented;

            do
            {
                bidIncremented = false;

                foreach (var currentBid in Bids)
                {
                    // Calculate maxOtherBid with default value to handle empty sequences
                    var maxOtherBid = Bids
                        .Where(b => b != currentBid)
                        .Select(b => b.StartingBid)
                        .DefaultIfEmpty(0) // Default to 0 if no other bids
                        .Max();

                    // Increment the current bid if it can compete
                    if (currentBid.StartingBid <= maxOtherBid && currentBid.StartingBid + currentBid.AutoIncrement <= currentBid.MaxBid)
                    {
                        currentBid.StartingBid += currentBid.AutoIncrement;
                        bidIncremented = true;
                    }
                }

            } while (bidIncremented);

            // Return the bid with the highest StartingBid
            return Bids.OrderByDescending(b => b.StartingBid).First();
        }
    }
}
