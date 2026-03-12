using System;

namespace AutoLot.Services.DataServices.Interfaces;

public interface ICarDataService:IDataServiceBase<Car>
{
    Task<IEnumerable<Car>> GetAllByMakeAsync(int? makeId);
}
