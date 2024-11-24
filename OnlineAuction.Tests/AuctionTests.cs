using Xunit;
using System.Collections.Generic;
using OnlineAuction;

namespace OnlineAuction.Tests
{
    public class AuctionTests
    {
        [Theory]
        [MemberData(nameof(GetAuctionTestCases))]
        public void Auction_ShouldDetermineWinningBidCorrectly(List<Bid> bids, string expectedWinner, decimal expectedWinningBid)
        {
            // Arrange
            var auction = new Auction();

            // Add all bids to the auction
            foreach (var bid in bids)
            {
                auction.AddBid(bid);
            }

            // Act
            var winner = auction.GetWinningBid();

            // Assert
            Assert.Equal(expectedWinner, winner.Bidder);
            Assert.Equal(expectedWinningBid, winner.StartingBid);
        }

        /// <summary>
        /// Provides test cases for the auction logic.
        /// </summary>
        public static IEnumerable<object[]> GetAuctionTestCases()
        {
            return new List<object[]>
        {
            // Single bid scenario
            new object[]
            {
                new List<Bid>
                {
                    new Bid("Alice", 50, 80, 3)
                },
                "Alice",
                50
            },

            // Three bidders with one maxing out
            new object[]
            {
                new List<Bid>
                {
                    new Bid("Alice", 50, 80, 3),
                    new Bid("Bob", 60, 82, 2),
                    new Bid("Charlie", 55, 85, 5)
                },
                "Charlie",
                85
            },

            // Multiple bidders with close competition
            new object[]
            {
                new List<Bid>
                {
                    new Bid("Alice", 50, 80, 3),
                    new Bid("Bob", 60, 82, 2),
                    new Bid("Charlie", 70, 85, 5)
                },
                "Charlie",
                85
            },

            // Bidder with a high auto-increment but low max
            new object[]
            {
                new List<Bid>
                {
                    new Bid("Alice", 50, 80, 3),
                    new Bid("Bob", 60, 65, 10), // Bob has high increment but low max
                    new Bid("Charlie", 70, 85, 5)
                },
                "Charlie",
                85
            },            

            // All bidders max out
            new object[]
            {
                new List<Bid>
                {
                    new Bid("Alice", 50, 80, 3),
                    new Bid("Bob", 60, 82, 2),
                    new Bid("Charlie", 70, 75, 5)
                },
                "Bob",
                82
            },

            // Single high bidder from the start
            new object[]
            {
                new List<Bid>
                {
                    new Bid("Alice", 50, 80, 3),
                    new Bid("Bob", 60, 82, 2),
                    new Bid("Charlie", 100, 150, 10)
                },
                "Charlie",
                100
            }
        };
        }
    }
}