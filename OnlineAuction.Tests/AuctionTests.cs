using System;
using Xunit;

namespace OnlineAuction.Tests
{
    public class AuctionTests
    {
        [Theory]
        [InlineData(95, 80, 100, 5, true, 100)]  // Next valid bid is exactly maxBid
        [InlineData(70, 50, 90, 10, true, 80)]  // Next valid bid is within maxBid
        [InlineData(100, 95, 100, 5, false, 100)]  // Exceeding maxBid is not allowed
        [InlineData(80, 50, 75, 10, false, 75)]  // MaxBid is reached but cannot exceed
        [InlineData(60, 40, 100, 20, true, 80)]  // Exactly matches newBid on first increment
        [InlineData(60, 40, 100, 10, true, 70)]  // Multiple increments required within range
        [InlineData(200, 150, 150, 10, false, 150)]  // newBid is greater than or equal to maxBid
        public void CalculateNextBid_ShouldReturnExpectedResults(
            decimal newBid,
            decimal currentBid,
            decimal maxBid,
            decimal autoIncrement,
            bool expectedCanExceedNewBid,
            decimal expectedNextValidBid)
        {
            // Act
            Auction auction = new Auction();
            var result = auction.CalculateNextBid(newBid, currentBid, maxBid, autoIncrement);

            // Assert
            Assert.Equal(expectedCanExceedNewBid, result.CanExceedNewBid);
            Assert.Equal(expectedNextValidBid, result.NextValidBid);
        }

        [Fact]
        public void CalculateNextBid_ShouldThrowException_WhenAutoIncrementIsZero()
        {
            // Arrange
            decimal newBid = 100;
            decimal currentBid = 80;
            decimal maxBid = 150;
            decimal autoIncrement = 0;

            // Act & Assert
            Auction auction = new Auction();
            Assert.Throws<DivideByZeroException>(() =>
                auction.CalculateNextBid(newBid, currentBid, maxBid, autoIncrement)
            );
        }

        [Fact]
        public void CalculateNextBid_ShouldHandleEdgeCaseWithNoIncrement()
        {
            // Arrange
            decimal newBid = 100;
            decimal currentBid = 100;
            decimal maxBid = 100;
            decimal autoIncrement = 5;

            // Act
            Auction auction = new Auction();
            var result = auction.CalculateNextBid(newBid, currentBid, maxBid, autoIncrement);

            // Assert
            Assert.False(result.CanExceedNewBid);
            Assert.Equal(100, result.NextValidBid);
        }
    }
}
