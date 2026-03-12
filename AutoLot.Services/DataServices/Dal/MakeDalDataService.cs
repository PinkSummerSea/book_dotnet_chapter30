using System;

namespace AutoLot.Services.DataServices.Dal;

public class MakeDalDataService : DalServiceBase<Make>, IMakeDataService
{
    public MakeDalDataService(MakeRepo repo) : base(repo)
    {
    }
}

