using System;

namespace AutoLot.Api.Controllers;

public class DriversController : BaseCrudController<Driver, DriversController>
{
    public DriversController(IDriverRepo repo) : base(repo)
    {
    }
}
