namespace OnlineAuction
{
    public class Auction
    {
        /// <summary>
        /// Determines if the newBid can be exceeded by increments of autoIncrement, and returns the minimum valid bid.
        /// </summary>
        /// <param name="newBid">The new bid amount to be evaluated.</param>
        /// <param name="currentBid">The current highest bid amount.</param>
        /// <param name="maxBid">The maximum allowable bid amount for the bidder.</param>
        /// <param name="autoIncrement">The auto-increment value used to increment bids.</param>
        /// <returns>(bool, decimal): A tuple where the boolean indicates if bidding can continue, and the decimal is the next valid bid.</returns>
        public (bool CanExceedNewBid, decimal NextValidBid) CalculateNextBid(decimal newBid, decimal currentBid, decimal maxBid, decimal autoIncrement)
        {
            if (newBid >= maxBid)
            {
                // If the new bid is already greater than or equal to the max bid, bidding cannot continue
                return (false, maxBid);
            }

            // Calculate the next valid bid by finding the smallest multiple of autoIncrement strictly greater than newBid
            decimal nextBid = currentBid + autoIncrement * Math.Ceiling((newBid - currentBid) / autoIncrement);

            // Ensure the next bid exceeds newBid
            if (nextBid <= newBid)
            {
                nextBid += autoIncrement;
            }

            // Ensure the next bid does not exceed maxBid
            if (nextBid <= maxBid)
            {
                return (true, nextBid);
            }

            // If the next valid bid exceeds maxBid, bidding cannot continue
            return (false, maxBid);
        }
    }
}
