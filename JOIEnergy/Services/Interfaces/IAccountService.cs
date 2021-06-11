using JOIEnergy.Enums;

namespace JOIEnergy.Services.Interfaces
{
    public interface IAccountService
    {
        Supplier GetPricePlanIdForSmartMeterId(string smartMeterId);
    }
}