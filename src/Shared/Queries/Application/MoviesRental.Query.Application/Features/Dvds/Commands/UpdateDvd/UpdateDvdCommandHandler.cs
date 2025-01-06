﻿using MediatR;
using MoviesRental.Query.Application.Contracts;

namespace MoviesRental.Query.Application.Features.Dvds.Commands.UpdateDvd
{
    public class UpdateDvdCommandHandler : IRequestHandler<UpdateDvdCommand, bool>
    {
        private readonly IDvdQueryRepository _repository;
        private readonly UpdateDvdCommandValidator _validator;

        public UpdateDvdCommandHandler(IDvdQueryRepository repository, UpdateDvdCommandValidator validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<bool> Handle(UpdateDvdCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
                return false;
            var dvd = await _repository.Get(request.Id);
            if (dvd is null)
                return false;
            
            dvd.Title = request.Title;
            dvd.Genre = request.Genre;
            dvd.Published = request.Published;
            dvd.Copies = request.Copies;
            dvd.DirectorId = request.DirectorId;
            dvd.UpdatedAt = request.UpdatedAt;

            return await _repository.Update(dvd);
        }
    }
}
