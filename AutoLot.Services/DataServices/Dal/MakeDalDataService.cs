using System;

namespace AutoLot.Services.DataServices.Dal;

public class MakeDalDataService : DalServiceBase<Make>, IMakeDataService
{
    public MakeDalDataService(IMakeRepo repo) : base(repo)
    {
    }
}

