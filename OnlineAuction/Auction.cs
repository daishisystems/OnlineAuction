using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineAuction
{
    public class Auction
    {
        private List<Bid> Bids { get; set; } = new List<Bid>();

        /// <summary>
        /// Adds a new bid to the auction and determines the winning bid.
        /// </summary>
        public void AddBid(Bid newBid)
        {
            // Add the new bid to the collection
            Bids.Add(newBid);

            // If there are multiple bids, determine the winning bid
            if (Bids.Count > 1)
            {
                DetermineWinningBid();
            }
        }

        private void DetermineWinningBid()
        {
            bool bidIncremented;

            do
            {
                bidIncremented = false;

                // Iterate through all bids
                foreach (var currentBid in Bids)
                {
                    // Find the current highest bid among all other bidders
                    var maxOtherBid = Bids
                        .Where(b => b != currentBid)
                        .Max(b => b.StartingBid);

                    // Increment the current bid if:
                    // 1. It is less than or equal to the maxOtherBid.
                    // 2. The incremented value does not exceed its MaxBid.
                    if (currentBid.StartingBid <= maxOtherBid && currentBid.StartingBid + currentBid.AutoIncrement <= currentBid.MaxBid)
                    {
                        currentBid.StartingBid += currentBid.AutoIncrement;
                        bidIncremented = true;
                    }
                }

            } while (bidIncremented); // Exit the loop when no bids can increment further
        }


        /// <summary>
        /// Returns the current winning bid after calculation.
        /// </summary>
        public Bid GetWinningBid()
        {
            // If there's only one bid, it's automatically the winning bid
            if (Bids.Count == 1)
            {
                return Bids.First();
            }

            // Otherwise, return the highest bid after simulation
            return Bids.OrderByDescending(b => b.StartingBid).First();
        }
    }

    public class Bid
    {
        public string Bidder { get; set; }
        public decimal StartingBid { get; set; }
        public decimal MaxBid { get; set; }
        public decimal AutoIncrement { get; set; }

        public Bid(string bidder, decimal startingBid, decimal maxBid, decimal autoIncrement)
        {
            Bidder = bidder;
            StartingBid = startingBid;
            MaxBid = maxBid;
            AutoIncrement = autoIncrement;
        }
    }
}
