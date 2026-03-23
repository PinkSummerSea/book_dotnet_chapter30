

namespace AutoLot.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class MakesController : BaseCrudController<Make, MakesController>
    {


        public MakesController(IMakeDataService mainDataService):base(mainDataService)
        {    
        }

        [HttpGet]
        [Route("/Admin")]
        [Route("/Admin/[controller]")]
        [Route("/Admin/[controller]/[action]")]
        public override async Task<IActionResult> IndexAsync()
        {
            return await base.IndexAsync();
        }
        protected override async Task<SelectList> GetLookUpValuesAsync()
        {
            return await Task.FromResult<SelectList>(null);
        }
    }
}