using MassTransit;
using MediatR;
using MoviesRental.Core.EventBus.Events;
using MoviesRental.Query.Application.Features.Directors.Commands.CreateDirector;

namespace MoviesRental.Consumer.Consumers.Directors
{
    public class DirectorCreatedConsumer : IConsumer<DirectorCreatedEvent>
    {
        private readonly IMediator mediator;
        private readonly ILogger<DirectorCreatedEvent> _logger;

        public DirectorCreatedConsumer(IMediator mediator, ILogger<DirectorCreatedEvent> logger)
        {
            this.mediator = mediator;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<DirectorCreatedEvent> context)
        {
            try
            {
                var @event = context?.Message ?? throw new ArgumentNullException(nameof(context));
                var command = new CreateDirectorCommand(@event.Id, @event.FullName, @event.CreatedAt, @event.UpdatedAt);
                _logger.LogInformation($"Creating director {@event.FullName}");
                var response = await mediator.Send(command, default);
                if (!response)
                {
                    _logger.LogError($"Something wrong happened during the creation of director {command.Id}");
                    throw new InvalidOperationException($"Failed to create director {command.Id}");
                }
                _logger.LogInformation($"Director {@event.Id} Created");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error ocorrued while consuming the DirectorCreatedEvent");
                throw;
            }
        }
    }
}
