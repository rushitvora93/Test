namespace UI_TestProjekt.TestModel
{
    public class Manufacturer
    {
        private string name = "";
        private string contactPerson = "";
        private string phoneNumber = "";
        private string fax = "";
        private string street = "";
        private string houseNumber = "";
        private string zipCode = "";
        private string city = "";
        private string country = "";
        private string comment = "";

        public string Name { get => name; set => name = value; }
        public string ContactPerson { get => contactPerson; set => contactPerson = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public string Fax { get => fax; set => fax = value; }
        public string Street { get => street; set => street = value; }
        public string HouseNumber { get => houseNumber; set => houseNumber = value; }
        public string ZipCode { get => zipCode; set => zipCode = value; }
        public string City { get => city; set => city = value; }
        public string Country { get => country; set => country = value; }
        public string Comment { get => comment; set => comment = value; }

        public Manufacturer(string name, string contactPerson, string phoneNumber, string fax, string street, string houseNumber, string zipCode, string city, string country, string comment)
        {
            Name = name;
            ContactPerson = contactPerson;
            PhoneNumber = phoneNumber;
            Fax = fax;
            Street = street;
            HouseNumber = houseNumber;
            ZipCode = zipCode;
            City = city;
            Country = country;
            Comment = comment;
        }

        public Manufacturer() { }

        public static class ManufacturerListHeaderStrings
        {
            public const string Name = "Name";
            public const string ContactPerson = "Contact Person";
            public const string PhoneNumber = "Phone number";
            public const string Fax = "Fax";
            public const string Street = "Street";
            public const string HouseNumber = "House number";
            public const string ZipCode = "Zip/Postal Code";
            public const string City = "City";
            public const string Country = "Country";
        }

        public static class DefaultManufacturer
        {
            public const string AtlasCopco = "Atlas Copco";
            public const string BLM = "BLM";
            public const string BMW = "BMW";
            public const string Crane = "Crane";
            public const string CSP = "CSP";
            public const string Gedore = "Gedore";
            public const string GWK = "GWK";
            //public const string Oetiker = "Oetiker";
            public const string REC = "REC";
            public const string Saltus = "Saltus";
            public const string Schatz = "Schatz";
            public const string SCS = "SCS";
            //public const string Stahlwille = "Stahlwille";
        }
    }
}
