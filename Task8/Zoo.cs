namespace Task8
{
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using System.IO;

    public class Zoo
    {
        private interface INode
        {
            public string Name { get; set; }
            public int Amount { get; }
            public void UpdateName(string name);
            public void UpdateAmount(int amount);
            public void UpdateNode(INode node, IList<INode>? children);
            public INode? GetNode(string[] path);
        }

        private class Location : INode
        {
            public List<INode> Children { get; set; } = new();
            public string Name { get; set; }
            public int Amount => Children.Sum(c => c.Amount);

            public Location() { }

            public Location(string name, List<INode> children) : this()
            {
                Name = name;
                this.Children.AddRange(children);
            }
            public void UpdateName(string name) { this.Name = name; }
            public void UpdateAmount(int amount) { }
            public void UpdateNode(INode node, IList<INode>? children)
            {
                Name = node.Name;
                if (children is not null)
                    this.Children.AddRange(children);
            }
            public INode? GetNode(string[] path)
            {
                if (path.Length == 0)
                    return this;
                string childName = path[0];
                return Children.FirstOrDefault(c => c.Name == childName)?.GetNode(path.Skip(1).ToArray());
            }
        }

        private class Animal : INode
        {
            public string Name { get; set; }
            public int Amount { get; set; }

            public Animal(string name, int amount)
            {
                Name = name;
                Amount = amount;
            }

            public void UpdateName(string name) { Name = name; }
            public void UpdateAmount(int amount) { Amount = amount; }
            public void UpdateNode(INode node, IList<INode>? children)
            {
                Name = node.Name;
                Amount = node.Amount;
            }
            public INode? GetNode(string[] path)
            {
                return path.Length != 0 ? null : this;
            }
        }

        private Location root = new Location();

        // this function should return an initial Zoo object based on the zoo data structure image with all animals
        public static Zoo Build()
        {
            Zoo zoo = new Zoo();
            zoo.root = new Location("", new List<INode>()
            {
                new Location("Aquarium", new List<INode>()
                {
                    new Animal("Fishes", 32),
                    new Animal("Frogs", 23),
                    new Animal("Spiders", 1)
                }),
                new Location("Cages", new List<INode>()
                {
                    new Location("Carnivores", new List<INode>()
                    {
                        new Animal("Tigers", 2),
                        new Animal("Lions", 3),
                    }),
                    new Location("Herbivores", new List<INode>()
                    {
                        new Animal("Zebras", 2),
                        new Animal("Gnus", 5),
                    })
                })
            });
            return zoo;
        }

        // this function returns total number of animals under a certain category.
        public int GetTotalQuantity(string[] path)
        {
            return root.GetNode(path)?.Amount ?? 0;
        }

        // this function can be used to create animals in a new or existing category
        public void AddAnimals(string[] path, int amount)
        {


            var nodeRes = root.GetNode(path);
            if (nodeRes != null && nodeRes is Animal)
                root.GetNode(path).UpdateAmount(root.GetNode(path).Amount + amount);

            else if (nodeRes == null)
            {
                var pathWithoutLastElement = path.Take(path.Count() - 1).ToArray();
                var node = root.GetNode(pathWithoutLastElement);
                List<INode> newChild = new List<INode> { new Animal(path[path.Length - 1], amount) };
                node.UpdateNode(new Location(node.Name, new List<INode>()), newChild);
            }
            else
            {
                var pathWithoutLastElement = path.Take(path.Count() - 1).ToArray();
                var node = root.GetNode(pathWithoutLastElement);
                List<INode> newChild = new List<INode> { new Animal(path[path.Length - 1], amount) };
                node.UpdateNode(new Location(node.Name, new List<INode>()), newChild);
            }
            // AddAnimals(new [] {"Cages", "Carnivores", "Tigers"}, 2)
            // should increase the number of tigers by two
            // AddAnimals(new [] {"Cages", "Herbivores", "Elephants"}, 2)
            // should create a new animal Elephants with number of two
            // this function should only update the data structure
        }

        // fromPath: should point to an animal or category (if one move all within that category)
        // toPath: need to point to point to an animal or empty category
        public void MoveAnimals(string[] fromPath, string[] toPath)
        {
            if (fromPath.Length > toPath.Length)
            {
                var nodeToMove = root.GetNode(fromPath);
                var nodeToMoveParent = (Location)root.GetNode(fromPath.Take(fromPath.Count() - 1).ToArray());
                var nodeToMoveParentParent = (Location)root.GetNode(fromPath.Take(fromPath.Count() - 2).ToArray());
                nodeToMoveParentParent.Children.Add(new Animal(nodeToMove.Name, nodeToMove.Amount));
                nodeToMoveParentParent.Children.Remove(nodeToMoveParent);
            }

            else if (fromPath.Length < toPath.Length)
            {
                var nodeToMove = root.GetNode(fromPath);
                var nodeToMoveParent = (Location)root.GetNode(fromPath.Take(fromPath.Count() - 1).ToArray());
                var nameOfNewCategory = toPath[toPath.Length - 2];
                var newList = new List<INode> { new Animal(nodeToMove.Name, nodeToMove.Amount) };
                nodeToMoveParent.Children.Add(new Location(nameOfNewCategory, newList));
                nodeToMoveParent.Children.Remove(nodeToMove);
            }

            else
            {
                var nodeToMove = root.GetNode(fromPath);
                var nodeToMoveParent = (Location)root.GetNode(fromPath.Take(fromPath.Count() - 1).ToArray());
                var nodeDestination = root.GetNode(toPath);
                nodeDestination.UpdateAmount(nodeDestination.Amount + nodeToMove.Amount);
                nodeToMoveParent.Children.Remove(nodeToMove);
            }

            Console.WriteLine();


            // MoveAnimals(new [] {"Aquarium","Frogs"}, new [] {"Aquarium", "Fishes"}) should
            // should move Frogs into Fishes and give total count of Fishes to 55

            //MoveAnimals(new[] { "Cages", "Carnivores", "Tigers" }, new[] { "Cages", "Tigers" })
            // MoveAnimals(new [] {"Aquarium", "Fishes"}, new [] {"Aquarium", "FreshWater", "Fishes"}) should
            // add a new category FreshWater within the existing Aquarium category
            // and move Fishes from the Aquarium category to the new one.

            // this function should only update the data structure
        }
    }
}
