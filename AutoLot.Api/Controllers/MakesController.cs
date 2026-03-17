using System;

namespace AutoLot.Api.Controllers;

public class MakesController : BaseCrudController<Make, MakesController>
{
    public MakesController(IMakeRepo repo) : base(repo)
    {
    }
}
