using System;

namespace AutoLot.Api.Controllers;

public class RadiosController : BaseCrudController<Radio, RadiosController>
{
    public RadiosController(IRadioRepo repo) : base(repo)
    {
    }
}
