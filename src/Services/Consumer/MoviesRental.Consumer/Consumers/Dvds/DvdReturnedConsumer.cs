using MassTransit;
using MediatR;
using MoviesRental.Core.EventBus.Events;
using MoviesRental.Query.Application.Features.Dvds.Commands.ReturnDvd;

namespace MoviesRental.Consumer.Consumers.Dvds
{
    public class DvdReturnedConsumer : IConsumer<DvdReturnedEvent>
    {
        private readonly IMediator mediator;
        private readonly ILogger<DvdReturnedEvent> _logger;

        public DvdReturnedConsumer(IMediator mediator, ILogger<DvdReturnedEvent> logger)
        {
            this.mediator = mediator;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<DvdReturnedEvent> context)
        {
            try
            {
                var @event = context?.Message ?? throw new ArgumentNullException(nameof(context), "Invalid message");
                if (string.IsNullOrEmpty(@event.Id))
                {
                    _logger.LogError("Invalid message");
                    throw new InvalidOperationException($"Failed to return dvd {@event.Id}");
                }
                var command = new ReturnDvdCommand(
                    @event.Id,
                    @event.UpdatedAt
                    );
                _logger.LogInformation($"Return Dvd {@event.Id}");
                var response = await mediator.Send(command, default);
                if (!response)
                {
                    _logger.LogError($"Something wrong happened during trying to return Dvd: {@event.Id}");
                    throw new InvalidOperationException($"Something wrong happened during trying to return Dvd: {@event.Id}");
                }
                _logger.LogInformation($"Dvd {@event.Id} Returned");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
