using System;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services
{
	public interface IAccountService
	{

		Task<bool> RegisterUser(UserRegisterModel model);

		Task<bool> ValidateUser(string meail, string password);
	}
}

