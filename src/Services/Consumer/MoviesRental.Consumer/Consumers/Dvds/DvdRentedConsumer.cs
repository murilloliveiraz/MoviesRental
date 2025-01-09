using MassTransit;
using MediatR;
using MoviesRental.Core.EventBus.Events;
using MoviesRental.Query.Application.Features.Dvds.Commands.RentDvd;

namespace MoviesRental.Consumer.Consumers.Dvds
{
    public class DvdRentedConsumer : IConsumer<DvdRentedEvent>
    {
        private readonly IMediator mediator;
        private readonly ILogger<DvdRentedEvent> _logger;

        public DvdRentedConsumer(IMediator mediator, ILogger<DvdRentedEvent> logger)
        {
            this.mediator = mediator;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<DvdRentedEvent> context)
        {
            try
            {
                var @event = context?.Message ?? throw new ArgumentNullException(nameof(context), "Invalid message");
                if (string.IsNullOrEmpty(@event.Id))
                {
                    _logger.LogError("Invalid message");
                    throw new InvalidOperationException($"Failed to rent dvd {@event.Id}");
                }
                var command = new RentDvdCommand(
                    @event.Id,
                    @event.UpdatedAt
                    );
                _logger.LogInformation($"Renting Dvd {@event.Id}");
                var response = await mediator.Send(command, default);
                if (!response)
                {
                    _logger.LogError($"Something wrong happened during trying to rent Dvd: {@event.Id}");
                    throw new InvalidOperationException($"Something wrong happened during trying to rent Dvd: {@event.Id}");
                }
                _logger.LogInformation($"Dvd {@event.Id} Rented");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
