using Utility;

namespace Reservaton.Service
{
    public interface IReservationService
    {
        OperationResult ReserveTableForNow();
    }
}