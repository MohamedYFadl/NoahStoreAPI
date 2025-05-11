namespace NoahStore.Core.Entities.Order
{
    public class ShippingAddress
    {
        public ShippingAddress()
        {
            
        }
        public ShippingAddress(int id, string firstName, string lastName, string city, string street, string zipCode, string state)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            City = city;
            Street = street;
            ZipCode = zipCode;
            State = state;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string State { get; set; }
    }
}