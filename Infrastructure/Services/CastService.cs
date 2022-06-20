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

		public CastDetailsModel GetCastDetails(int id)
        {
			var castDetails = _castRepository.GetById(id);

			var cast = new CastDetailsModel
			{
				Id = castDetails.Id,
				Name = castDetails.Name,
				ProfilePath = castDetails.ProfilePath
			};

			return cast;
        }
	}
}

