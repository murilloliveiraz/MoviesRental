using MassTransit;
using MediatR;
using MoviesRental.Core.EventBus.Events;
using MoviesRental.Query.Application.Features.Dvds.Commands.UpdateDvd;

namespace MoviesRental.Consumer.Consumers.Dvds
{
    public class DvdUpdatedConsumer : IConsumer<DvdUpdatedEvent>
    {
        private readonly IMediator mediator;
        private readonly ILogger<DvdUpdatedEvent> _logger;

        public DvdUpdatedConsumer(IMediator mediator, ILogger<DvdUpdatedEvent> logger)
        {
            this.mediator = mediator;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<DvdUpdatedEvent> context)
        {
            try
            {
                var @event = context?.Message ?? throw new ArgumentNullException(nameof(context), "Invalid message");
                var command = new UpdateDvdCommand(
                    @event.Id,
                    @event.Title,
                    @event.Genre,
                    @event.Published,
                    @event.Copies,
                    @event.DirectorId,
                    @event.UpdatedAt
                    );
                _logger.LogInformation($"Updating Dvd {@event.Title}");
                var response = await mediator.Send(command, default);
                if (!response)
                {
                    _logger.LogError($"Something wrong happened during the update of Dvd {command.Id}");
                    throw new InvalidOperationException($"Something wrong happened during the update of Dvd {command.Id}");
                }
                _logger.LogInformation($"Dvd {@event.Id} Updated");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
