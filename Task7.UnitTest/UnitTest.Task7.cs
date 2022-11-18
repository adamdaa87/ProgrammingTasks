using Task7.Models;

namespace Task7.UnitTest
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(new[] { "Cages", "Carnivores", "Tigers" }, 2)]
        [InlineData(new[] { "Aquarium", "Frogs" }, 23)]
        [InlineData(new[] { "Cages", "Herbivores" }, 7)]
        [InlineData(new[] { "Cages", "Herbivores", "Gnus" }, 5)]
        [InlineData(new[] { "Aquarium" }, 56)]
        public void GetTotalQuantity_ShouldReturn_TheExpectedValue(string[] paths, int expected)
        {
            //Arrange
            var zoo = Zoo.Build();

            //Act
            var result = zoo.GetTotalQuantity(paths);

            //Assert
            Assert.Equal(expected, result);
        }
    }
}