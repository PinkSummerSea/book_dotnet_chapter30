using System;

namespace AutoLot.Api.Controllers;

public class OrdersController : BaseCrudController<Order, OrdersController>
{
    public OrdersController(IOrderRepo repo) : base(repo)
    {
    }
}
