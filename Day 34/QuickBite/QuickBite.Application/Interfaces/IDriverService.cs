using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using QuickBite.Application.Results;

namespace QuickBite.Application.Interfaces
{
    public interface IDriverService
    {
        Task<List<AvailableDriver>> FindAvailableDriversAsync(
            Location pickup,
            Location dropoff,
            CancellationToken ct);

        Task<DriverAssignment> AssignDriverAsync(
            string orderId,
            string driverId,
            CancellationToken ct);

        Task<DriverLocation> TrackDriverAsync(
            string driverId,
            CancellationToken ct);
    }
}
