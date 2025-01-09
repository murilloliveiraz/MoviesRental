using MassTransit;
using MediatR;
using MoviesRental.Core.EventBus.Events;
using MoviesRental.Query.Application.Features.Directors.Commands.CreateDirector;
using MoviesRental.Query.Application.Features.Directors.Commands.UpdateDirector;

namespace MoviesRental.Consumer.Consumers.Directors
{
    public class DirectorUpdatedConsumer : IConsumer<DirectorUpdatedEvent>
    {
        private readonly IMediator mediator;
        private readonly ILogger<DirectorUpdatedEvent> _logger;

        public DirectorUpdatedConsumer(IMediator mediator, ILogger<DirectorUpdatedEvent> logger)
        {
            this.mediator = mediator;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<DirectorUpdatedEvent> context)
        {
            try
            {
                var @event = context?.Message ?? throw new ArgumentNullException(nameof(context), "Invalid Message");
                var command = new UpdateDirectorCommand(@event.Id, @event.FullName, @event.UpdatedAt);
                _logger.LogInformation($"Updating director {@event.FullName}");
                var response = await mediator.Send(command, default);
                if (!response)
                {
                    throw new InvalidOperationException($"Something wrong happened during the update of director {command.Id}");
                }
                _logger.LogInformation($"Director {@event.FullName} updated succesfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
