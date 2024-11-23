public partial class Auction
{
    public class Bidder
    {
        public string Name { get; set; }
        public decimal StartingBid { get; set; }
        public decimal MaxBid { get; set; }
        public decimal AutoIncrement { get; set; }
        public decimal CurrentBid { get; set; }

        public Bidder(string name, decimal startingBid, decimal maxBid, decimal autoIncrement)
        {
            Name = name;
            StartingBid = startingBid;
            MaxBid = maxBid;
            AutoIncrement = autoIncrement;
            CurrentBid = startingBid;
        }
    }
}
