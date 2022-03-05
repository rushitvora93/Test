using Core.Entities;
using System;
using Client.TestHelper.Factories;

namespace TestHelper.Factories
{
    public class CreateManufacturer : AbstractEntityFactory
    {
        public static Manufacturer Parametrized(long id, string name, string city, string county, string fax, string houseNumber, string person, string phoneNumber, string plz, string street, string comment = "")
        {
            return new Manufacturer
            {
                Id = new ManufacturerId(id),
                Name = new ManufacturerName(name),
                City = city,
                Country = county,
                Fax = fax,
                HouseNumber = houseNumber,
                Person = person,
                PhoneNumber = phoneNumber,
                Plz = plz,
                Street = street,
                Comment = comment
            };
        }

        public static Manufacturer IdAndCommentOnly(long manufacturerId, string comment)
        {
            var oldManufacturer = IdOnly(manufacturerId);
            oldManufacturer.Comment = comment;
            return oldManufacturer;
        }

        public static Manufacturer IdOnly(long id)
        {
            return Parametrized(id, "", null, null, null, "", null, null, "", null);
        }

        public static Manufacturer Anonymous()
        {
            return Parametrized(3, "", "", "", "", "", "", "", "", "");
        }

        public static Manufacturer WithName(string name)
        {
            var manu = Anonymous();
            manu.Name = new ManufacturerName(name);
            return manu;
        }
        public static Manufacturer Randomized(int seed)
        {
            var randomizer = new Random(seed);
            return Parametrized(
                randomizer.Next(),
                CreateString.Randomized((int)(randomizer.NextDouble() * 30), randomizer.Next()),
                CreateString.Randomized((int)(randomizer.NextDouble() * 50), randomizer.Next()),
                CreateString.Randomized((int)(randomizer.NextDouble() * 50), randomizer.Next()),
                CreateString.Randomized((int)(randomizer.NextDouble() * 50), randomizer.Next()),
                CreateString.Randomized((int)(randomizer.NextDouble() * 50), randomizer.Next()),
                CreateString.Randomized((int)(randomizer.NextDouble() * 50), randomizer.Next()),
                CreateString.Randomized((int)(randomizer.NextDouble() * 50), randomizer.Next()),
                CreateString.Randomized((int)(randomizer.NextDouble() * 50), randomizer.Next()),
                CreateString.Randomized((int)(randomizer.NextDouble() * 50), randomizer.Next()),
                CreateString.Randomized((int)(randomizer.NextDouble() * 50), randomizer.Next()));
        }
    }
}
