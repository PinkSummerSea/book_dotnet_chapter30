using System;

namespace AutoLot.Api.Controllers;

public class CarsController : BaseCrudController<Car, CarsController>
{
    public CarsController(ICarRepo repo) : base(repo)
    {
    }
    [HttpGet("bymake/{id?}")]
    public ActionResult<IEnumerable<Car>> GetCarsByMake(int? id)
    {
        if(id.HasValue && id.Value > 0)
        {
            return Ok(((ICarRepo)MainRepo).GetAllBy(id.Value));
        }
        return Ok(MainRepo.GetAllIgnoreQueryFilters());
    }
}
