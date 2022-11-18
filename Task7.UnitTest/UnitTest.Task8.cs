using Task8;

namespace Task7.UnitTest8
{
    public class UnitTest8
    {


        [Fact]
        public void GetTotalQuantity_ShouldReturn75_WhenAllAmimalsAreIncreasedByOne()
        {
            //Arrange
            var zoo = Zoo.Build();
            zoo.AddAnimals(new[] { "Aquarium", "Fishes" }, 1);
            zoo.AddAnimals(new[] { "Aquarium", "Frogs" }, 1);
            zoo.AddAnimals(new[] { "Aquarium", "Spiders" }, 1);
            zoo.AddAnimals(new[] { "Cages", "Carnivores", "Tigers" }, 1);
            zoo.AddAnimals(new[] { "Cages", "Carnivores", "Lions" }, 1);
            zoo.AddAnimals(new[] { "Cages", "Herbivores", "Zebras" }, 1);
            zoo.AddAnimals(new[] { "Cages", "Herbivores", "Gnus" }, 1);

            //Act
            var result = zoo.GetTotalQuantity(Array.Empty<string>());

            //Assert
            Assert.Equal(75, result);
        }
        
        [Fact]
        public void GetTotalQuantity_ShouldReturn12_When5ElephantsGetAdded()
        {
            //Arrange
            var zoo = Zoo.Build();
            zoo.AddAnimals(new[] { "Cages", "Herbivores", "Elephants" }, 5);

            //Act
            var result = zoo.GetTotalQuantity(new[] { "Cages", "Herbivores"});

            //Assert
            Assert.Equal(12, result);
        }
    }
}