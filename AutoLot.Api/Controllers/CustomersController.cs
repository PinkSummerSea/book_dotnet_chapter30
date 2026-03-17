using System;

namespace AutoLot.Api.Controllers;

public class CustomersController : BaseCrudController<Customer, CustomersController>
{
    public CustomersController(ICustomerRepo repo) : base(repo)
    {
    }
}
