using ParkingManager.Model.Entities;

namespace ParkingManager.Interface
{
    public interface IParkingManagerRepository
    {
        decimal CalculateTotalCost(string entryTime, string exitTime);
        IEnumerable<ParkingTicket> GetTicketsByDate(DateTime date);
    }
}
