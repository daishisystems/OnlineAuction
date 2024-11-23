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
            // Validate inputs individually with specific error messages
            if (newBid <= 0)
            {
                throw new ArgumentException("The new bid must be greater than zero.", nameof(newBid));
            }
            if (currentBid <= 0)
            {
                throw new ArgumentException("The current bid must be greater than zero.", nameof(currentBid));
            }
            if (maxBid <= 0)
            {
                throw new ArgumentException("The max bid must be greater than zero.", nameof(maxBid));
            }
            if (autoIncrement <= 0)
            {
                throw new ArgumentException("The auto-increment value must be greater than zero.", nameof(autoIncrement));
            }

            // If the max bid is greater than the new bid, bidding cannot continue
            if (newBid > maxBid)
            {
                return (false, maxBid);
            }

            // If the new bid is greater than the current bid, return true and the new bid value
            if (newBid > currentBid)
            {
                return (true, newBid);
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
