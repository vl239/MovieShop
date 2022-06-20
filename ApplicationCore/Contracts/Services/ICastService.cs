using System;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services
{
	public interface ICastService
	{
		CastModel GetCastDetails(int id);
	}
}

