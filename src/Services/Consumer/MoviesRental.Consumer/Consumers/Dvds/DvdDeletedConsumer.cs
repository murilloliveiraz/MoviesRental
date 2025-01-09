using MassTransit;
using MediatR;
using MoviesRental.Core.EventBus.Events;
using MoviesRental.Query.Application.Features.Dvds.Commands.DeleteDvd;

namespace MoviesRental.Consumer.Consumers.Dvds
{
    public class DvdDeletedConsumer : IConsumer<DvdDeletedEvent>
    {
        private readonly IMediator mediator;
        private readonly ILogger<DvdDeletedEvent> _logger;

        public DvdDeletedConsumer(IMediator mediator, ILogger<DvdDeletedEvent> logger)
        {
            this.mediator = mediator;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<DvdDeletedEvent> context)
        {
            try
            {
                var @event = context?.Message ?? throw new ArgumentNullException(nameof(context), "Invalid message");
                if(string.IsNullOrEmpty(@event.Id) || @event.DeletedAt > DateTime.Now)
                {
                    _logger.LogError("Invalid message");
                    throw new InvalidOperationException($"Failed to delete dvd {@event.Id}");
                }
                var command = new DeleteDvdCommand(
                    @event.Id,
                    @event.DeletedAt
                );

                _logger.LogInformation($"Deleting Dvd {@event.Id}");
                var response = await mediator.Send(command, default);
                if (!response)
                {
                    _logger.LogError($"Something wrong happened during the deletion of Dvd {command.Id}");
                    throw new InvalidOperationException($"Failed to delete dvd {@event.Id}");
                }
                _logger.LogInformation($"Dvd {@event.Id} deleted");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error ocurred while trying to process DvdDeletedEvent");
                throw;
            }
        }
    }
}
