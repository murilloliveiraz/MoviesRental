﻿using MediatR;
using MoviesRental.Query.Application.Contracts;
using MoviesRental.Query.Domain.Models;

namespace MoviesRental.Query.Application.Features.Dvds.Commands.CreateDvd
{
    public class CreateDvdCommandHandler: IRequestHandler<CreateDvdCommand, bool>
    {
        private readonly IDvdQueryRepository _repository;
        private readonly CreateDvdCommandValidator _validator;

        public CreateDvdCommandHandler(IDvdQueryRepository repository, CreateDvdCommandValidator validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<bool> Handle(CreateDvdCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
                return false;
            var dvd = await _repository.Get(request.Id);
            if(dvd is not null)
                return false;
            dvd = new Dvd
            {
                Id = request.Id,
                Title = request.Title,
                Genre = request.Genre,
                Published = request.Published,
                Available = request.Available,
                Copies = request.Copies,
                CreatedAt = request.CreatedAt,
                UpdatedAt = request.UpdatedAt,
                DirectorId = request.DirectorId
            };

            var result = await _repository.Create(dvd);
            if (result is null)
                return false;
            return true;
        }
    }
}
