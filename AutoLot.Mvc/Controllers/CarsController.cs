using Microsoft.AspNetCore.Mvc;

namespace AutoLot.Mvc.Controllers
{
    public class CarsController : BaseCrudController<Car, CarsController>
    {
        private readonly IMakeDataService _lookupDataService;
        public CarsController(ICarDataService mainDataService, IMakeDataService lookupDataService) : base(mainDataService)
        {
            _lookupDataService=lookupDataService;
        }


        // GET: CarsController
        public ActionResult Index()
        {
            return View();
        }

        protected override async Task<SelectList> GetLookUpValuesAsync()
        {
            return new SelectList(await _lookupDataService.GetAllAsync(), nameof(Make.Id), nameof(Make.Name));
        }

        [HttpGet("{makeId}/{makeName}")]
        public async Task<IActionResult> ByMakeAsync(int makeId, string makeName)
        {
            ViewBag.MakeName = makeName;
            return View(await ((ICarDataService)MainDataService).GetAllByMakeAsync(makeId));
        }
    }
}
