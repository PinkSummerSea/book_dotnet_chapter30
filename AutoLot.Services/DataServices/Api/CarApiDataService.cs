using System;
using AutoLot.Services.ApiWrapper;

namespace AutoLot.Services.DataServices.Api;

public class CarApiDataService : ApiDataServiceBase<Car>, ICarDataService
{
    public CarApiDataService(ICarApiServiceWrapper serviceWrapper):base(serviceWrapper){}
    public async Task<IEnumerable<Car>> GetAllByMakeAsync(int? makeId)
    {
        if (makeId.HasValue)
        {
            return await ((ICarApiServiceWrapper)ServiceWrapper).GetCarsByMakeAsync(makeId.Value);
        } else
        {
            return await GetAllAsync();
        }
        
    }
    
}
