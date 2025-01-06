﻿using MediatR;
using MoviesRental.Query.Application.Contracts;

namespace MoviesRental.Query.Application.Features.Dvds.Queries.GetDvd
{
    public class GetDvdQueryHandler: IRequestHandler<GetDvdQuery, GetDvdResponse>
    {
        private readonly IDvdQueryRepository _repository;

        public GetDvdQueryHandler(IDvdQueryRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetDvdResponse> Handle(GetDvdQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Title))
                return default;

            var dvd = await _repository.GetByTitle(request.Title);
            if (dvd is null)
                return default;

            return new GetDvdResponse(dvd.Id, dvd.Title, dvd.Genre, dvd.Published, dvd.Copies, dvd.DirectorId, dvd.CreatedAt, dvd.UpdatedAt);
        }
    }
}
