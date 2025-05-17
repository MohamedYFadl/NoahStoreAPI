using NoahStore.Core.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoahStore.Core.Specifications
{
    public class OrderWithPaymentIntentSpecs:BaseSpecification<Orders>
    {
        public OrderWithPaymentIntentSpecs(string paymentIntentId)
            :base(p=>p.PaymentIntentId == paymentIntentId)
        {
            
        }
    }
}
