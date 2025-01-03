﻿using MediatR;

namespace MoviesRental.Application.Features.Dvds.Commands.UpdateDvd
{
    public record UpdateDvdCommand(
        Guid Id,
        string Title,
        int Genre,
        DateTime Published,
        Guid DirectorId,
        int Copies
        ): IRequest<UpdateDvdResponse>;
}
