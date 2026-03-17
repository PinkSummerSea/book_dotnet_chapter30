using System;

namespace AutoLot.Api.Controllers;

public class CreditRisksController : BaseCrudController<CreditRisk, CreditRisksController>
{
    public CreditRisksController(ICreditRiskRepo repo) : base(repo)
    {
    }
}
