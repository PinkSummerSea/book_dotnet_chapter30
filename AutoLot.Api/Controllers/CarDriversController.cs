using System;

namespace AutoLot.Api.Controllers;

public class CarDriversController : BaseCrudController<CarDriver, CarDriversController>
{
    public CarDriversController(ICarDriverRepo repo) : base(repo)
    {
    }
}
