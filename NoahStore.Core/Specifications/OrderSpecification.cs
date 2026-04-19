using NoahStore.Core.Entities.Order;

namespace NoahStore.Core.Specifications
{
    public class OrderSpecification : BaseSpecification<Orders>
    {
        public OrderSpecification(int id, string buyerEmail) : base(o => (o.Id == id && o.BuyerEmail == buyerEmail))
        {
            AddIncludes();

        }
        public OrderSpecification(string buyerEmail) : base(o => o.BuyerEmail == buyerEmail)
        {
            AddIncludes();
        }

        private void AddIncludes()
        {
            Includes.Add(o => o.shippingAddress);
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.OrderItems);
        }


    }
}
