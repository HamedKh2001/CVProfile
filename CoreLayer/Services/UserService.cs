using AutoMapper;
using CoreLayer.Dtos;
using CoreLayer.IServices;
using CORETest.Utilities;
using DataLayer.Context;
using DataLayer.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Services
{
	public class UserService : IUserService
	{
		private readonly IGenericRepository<User> _genericRepository;
		private readonly CVContext _context;
		private readonly IMapper _mapper;
		private readonly ILogger<UserService> _logger;
		public UserService(ILogger<UserService> logger, IGenericRepository<User> genericRepository, CVContext context, IMapper mapper)
		{
			_genericRepository = genericRepository;
			_context = context;
			_mapper = mapper;
			_logger = logger;
		}

		public OperationResault ActivateUser(string phonenumber, string ActivateCode)
		{
			var user = GetUserByphoneNumber(phonenumber);
			if (user == null)
				return OperationResault.NotFound();
			if (user.ActivateCode == ActivateCode)
			{
				user.IsActive = true;
				_genericRepository.Update(user);
				return OperationResault.Success();
			}
			return OperationResault.Error();
		}

		public bool ExistUser(string phonenumber)
		{
			return _context.Users.Any(u => u.Phonenumber == phonenumber);
		}

		public User GetUserByphoneNumber(string phonenumber)
		{
			var user = _context.Users.FirstOrDefault(u => u.Phonenumber == phonenumber);
			if (user != null)
				return user;
			return null;
		}

		public OperationResault SignUpUser(User user)
		{
			try
			{
				user.PassWord = user.PassWord.EncodeToMd5();
				var res = _genericRepository.Insertasync(user).Result;
				return OperationResault.Success();
			}
			catch (Exception ex)
			{
				return OperationResault.Error(ex.Message);
			}
		}

		public UserDto ValideUser(string phonenumber, string PassWord)
		{
			var user = _context.Users.FirstOrDefault(u => u.Phonenumber == phonenumber && u.PassWord == PassWord.EncodeToMd5());
			var xx = _mapper.Map<UserDto>(user);
			return xx;
			//return new UserDto()
			//{
			//	FullName = user.FullName,
			//	Role = user.Role,
			//	IsActive = user.IsActive,
			//	Id = user.Id
			//};
		}

		/// <summary>
		/// Success.Message = New PassWord
		/// </summary>
		/// <param name="phonenumber"></param>
		/// <returns></returns>
		public OperationResault RecoverUser(string phonenumber)
		{
			var guid = Guid.NewGuid(); ;
			var newPass = guid.ToString().Split('-').First();
			var user = GetUserByphoneNumber(phonenumber);
			user.PassWord = newPass.EncodeToMd5();
			var res = _genericRepository.Update(user);
			if (res.Status == OperationResultStatus.Success)
			{
				res.Message = $"رمز عبور جدید شما : {newPass} ";
				return res;
			}
			if (user == null)
				return OperationResault.NotFound("شماره مورد نظر موجود نیست");
			return OperationResault.Error();
		}
	}
}
