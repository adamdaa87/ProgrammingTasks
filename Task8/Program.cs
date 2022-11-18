// See https://aka.ms/new-console-template for more information

using Task8;


var paths1 = new[] { "Cages", "Carnivores", "Tigers" };
var paths2 = new[] { "Cages", "Herbivores", "Elephants" };

var zoo = Zoo.Build();
zoo.MoveAnimals(new[] { "Cages", "Carnivores", "Tigers" }, new[] { "Cages", "Tigers" });

var res = zoo.GetTotalQuantity(Array.Empty<string>());
Console.WriteLine(res);

