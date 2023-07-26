using ParkingManager.Interface;
using ParkingManager.Model.Entities;
using System.Globalization;

namespace ParkingManager.Repository.Implemetation
{
    public class ParkingManagerRepository : IParkingManagerRepository
    {
        private readonly ParkingManagerDbContext _context;

        public ParkingManagerRepository(ParkingManagerDbContext context)
        {
            _context = context;
        }
        public decimal CalculateTotalCost(string entryTime, string exitTime)
        {
            var entryTimeDateTime = DateTime.ParseExact(entryTime, "HH:mm", CultureInfo.InvariantCulture);
            var exitTimeDateTime = DateTime.ParseExact(exitTime, "HH:mm", CultureInfo.InvariantCulture);

            var parkingRule = _context.PackingRules.FirstOrDefault();
            TimeSpan parkingDuration = exitTimeDateTime - entryTimeDateTime;

            var totalCost = _context.PackingTickets.FirstOrDefault(x => x.ExitTime == exitTime && x.EntryTime == entryTime);
            
            if (parkingDuration > TimeSpan.FromHours(1))
            {
                var additionalHours = (int)Math.Ceiling(parkingDuration.Subtract(TimeSpan.FromHours(1)).TotalHours);
            }

            var parkingTicket = new ParkingTicket
            {
                EntryTime = entryTimeDateTime.ToString(),
                ExitTime = exitTimeDateTime.ToString(),
                AmountToPay = totalCost.AmountToPay,
                Name = "Ticket",
                HoursSpent = (int)Math.Ceiling(parkingDuration.TotalHours),
                Date = entryTimeDateTime.Date // Store only the date portion
            };

            _context.PackingTickets.Add(parkingTicket);
            _context.SaveChanges();

            return totalCost.AmountToPay;
        }

       
        public IEnumerable<ParkingTicket> GetTicketsByDate(DateTime date)
        {
            var tickets = _context.PackingTickets.Where(ticket => ticket.Date.Date == date.Date).ToList();
            return tickets;
        }
    }
}
