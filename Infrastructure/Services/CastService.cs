using System;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;

namespace Infrastructure.Services
{
	public class CastService : ICastService
	{
		public readonly ICastRepository _castRepository;

		public CastService(ICastRepository castRepository)
		{
			_castRepository = castRepository;
		}

		public async Task<CastDetailsModel> GetCastDetails(int id)
        {
			var castDetails = await _castRepository.GetById(id);

			string gender = "";
			if (castDetails.Gender == "1")
            {
				gender = "Female";
            }
			else if (castDetails.Gender == "2")
			{
				gender = "Male";
            }
			else
            {
				gender = "Non-binary";
            }

			var cast = new CastDetailsModel
			{
				Id = castDetails.Id,
				Name = castDetails.Name,
				Gender = gender,
				TmdbUrl = castDetails.TmdbUrl,
				ProfilePath = castDetails.ProfilePath
			};

            foreach (var movie in castDetails.MoviesOfCast)
            {
				cast.Movies.Add(new MovieCardModel { Id = movie.MovieId, PosterUrl = movie.Movie.PosterUrl, Title = movie.Movie.Title });
            }

			return cast;
        }
    }
}

