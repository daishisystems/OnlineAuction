using System;
using Xunit;

namespace OnlineAuction.Tests
{
    public class AuctionTests
    {
        [Theory]
        [InlineData(95, 80, 100, 5, true, 95)]  // Next valid bid is exactly maxBid
        [InlineData(70, 50, 90, 10, true, 70)]  // Next valid bid is within maxBid
        [InlineData(100, 95, 100, 5, true, 100)]  // Exceeding maxBid is not allowed
        [InlineData(80, 50, 75, 10, false, 75)]  // MaxBid is reached but cannot exceed
        [InlineData(60, 40, 100, 20, true, 60)]  // Exactly matches newBid on first increment
        [InlineData(60, 40, 100, 10, true, 60)]  // Multiple increments required within range
        [InlineData(200, 150, 150, 10, false, 150)]  // newBid is greater than or equal to maxBid
        [InlineData(55, 60, 85, 5, true, 60)]  // Spec test case #1
        [InlineData(625, 599, 725, 8, true, 625)]  // Spec test case #2
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
        public void CalculateNextBid_ShouldThrowArgumentException_ForInvalidNewBid()
        {
            // Arrange
            var auction = new Auction();

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                auction.CalculateNextBid(0, 60, 85, 5)
            );
        }

        [Fact]
        public void CalculateNextBid_ShouldThrowArgumentException_ForInvalidCurrentBid()
        {
            // Arrange
            var auction = new Auction();

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                auction.CalculateNextBid(55, 0, 85, 5)
            );
        }

        [Fact]
        public void CalculateNextBid_ShouldThrowArgumentException_ForInvalidMaxBid()
        {
            // Arrange
            var auction = new Auction();

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                auction.CalculateNextBid(55, 60, 0, 5)
            );
        }
    }
}
