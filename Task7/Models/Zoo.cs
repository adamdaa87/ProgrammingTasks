
namespace Task7.Models
{
    public class Zoo
    {
        public class Aquarium
        {
            public int Fishes { get; set; }
            public int Frogs { get; set; }
            public int Spiders { get; set; }

            public int GetTotal()
            {
                return Fishes + Frogs + Spiders;
            }
        }
        public class Cages
        {
            public Carnivores Carnivores { get; set; }
            public Herbivores Herbivores { get; set; }

            public int GetTotal()
            {
                return Carnivores.GetTotal() + Herbivores.GetTotal();
            }
        }
        public class Carnivores
        {
            public int Tigers { get; set; }
            public int Lions { get; set; }

            public int GetTotal()
            {
                return Tigers + Lions;
            }
        }
        public class Herbivores
        {
            public int Zebras { get; set; }
            public int Gnus { get; set; }

            public int GetTotal()
            {
                return Zebras + Gnus;
            }
        }

        public Aquarium _aquarium;
        public Cages _cages;
        public Dictionary<string, int> _zooDictionary;

        public Zoo(Aquarium aquarium, Cages cages, Dictionary<string, int> zooDictionary)
        {
            _aquarium = aquarium;
            _cages = cages;
            _zooDictionary = zooDictionary;
        }

        // this function should return an initial Zoo object based on the zoo data structure image with all animals
        public static Zoo Build()
        {
            var aquarium = new Aquarium { Fishes = 32, Frogs = 23, Spiders = 1 };

            var carnivores = new Carnivores { Tigers = 2, Lions = 3 };
            var herbivores = new Herbivores { Zebras = 2, Gnus = 5 };
            var cages = new Cages { Carnivores = carnivores, Herbivores = herbivores };

            var zooDictionary = new Dictionary<string, int>
            {
                { "", aquarium.GetTotal() + cages.GetTotal() },
                { "Aquarium", aquarium.GetTotal() },
                { "Fishes", aquarium.Fishes },
                { "Frogs", aquarium.Frogs },
                { "Spiders", aquarium.Spiders },
                { "Cages", cages.GetTotal() },
                { "Carnivores", cages.Carnivores.GetTotal() },
                { "Tigers", cages.Carnivores.Tigers },
                { "Lions", cages.Carnivores.Lions },
                { "Herbivores", cages.Herbivores.GetTotal() },
                { "Zebras", cages.Herbivores.Zebras },
                { "Gnus", cages.Herbivores.Gnus }
            };

            return new Zoo(aquarium, cages, zooDictionary);
        }

        // this function returns total number of animals under a certain category.
        public int GetTotalQuantity(string[] path)
        {
            return path.Any() ? _zooDictionary[path[path.Length - 1]] : _zooDictionary[""];
        }
    }
}
