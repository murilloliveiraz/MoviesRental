using MassTransit;
using MediatR;
using MoviesRental.Core.EventBus.Events;
using MoviesRental.Query.Application.Features.Directors.Commands.CreateDirector;
using MoviesRental.Query.Application.Features.Directors.Commands.DeleteDirector;

namespace MoviesRental.Consumer.Consumers.Directors
{
    public class DirectorDeletedConsumer : IConsumer<DirectorDeletedEvent>
    {
        private readonly IMediator mediator;
        private readonly ILogger<DirectorDeletedEvent> _logger;

        public DirectorDeletedConsumer(IMediator mediator, ILogger<DirectorDeletedEvent> logger)
        {
            this.mediator = mediator;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<DirectorDeletedEvent> context)
        {
            try
            {
                var @event = context?.Message ?? throw new ArgumentNullException(nameof(context));
                var command = new DeleteDirectorCommand(@event.Id);
                _logger.LogInformation($"Deleting director {@event.Id}");
                var response = await mediator.Send(command, default);
                if (!response)
                {
                    throw new InvalidOperationException($"Something wrong happened during the remotion of director {command.Id}");
                }
                _logger.LogInformation($"Director {@event.Id} removed succesfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
