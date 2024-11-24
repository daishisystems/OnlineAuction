namespace OnlineAuction
{
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
