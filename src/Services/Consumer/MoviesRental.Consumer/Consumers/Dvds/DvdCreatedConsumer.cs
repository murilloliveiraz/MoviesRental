using MassTransit;
using MediatR;
using MoviesRental.Core.EventBus.Events;
using MoviesRental.Query.Application.Features.Dvds.Commands.CreateDvd;

namespace MoviesRental.Consumer.Consumers.Dvds
{
    public class DvdCreatedConsumer : IConsumer<DvdCreatedEvent>
    {
        private readonly IMediator mediator;
        private readonly ILogger<DvdCreatedEvent> _logger;

        public DvdCreatedConsumer(IMediator mediator, ILogger<DvdCreatedEvent> logger)
        {
            this.mediator = mediator;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<DvdCreatedEvent> context)
        {
            try
            {
                var @event = context?.Message ?? throw new ArgumentNullException(nameof(context), "Invalid message");
                var command = new CreateDvdCommand(
                    @event.Id,
                    @event.Title,
                    @event.Genre,
                    @event.Published,
                    @event.Available,
                    @event.Copies,
                    @event.DirectorId,
                    @event.CreatedAt,
                    @event.UpdatedAt
                    );
                _logger.LogInformation($"Creating Dvd {@event.Title}");
                var response = await mediator.Send(command, default);
                if (!response)
                {
                    throw new InvalidOperationException($"Something wrong happened during the creation of Dvd {command.Id}");
                }
                _logger.LogInformation($"Dvd {@event.Id} Created");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
