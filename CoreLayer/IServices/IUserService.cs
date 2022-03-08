using CoreLayer.Dtos;
using CoreLayer.Utilities;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.IServices
{
	public interface IUserService
	{
		OperationResault SignUpUser(User user);
		bool ExistUser(string phonenumber);
		UserDto ValideUser(string phonenumber,string PassWord);
		OperationResault ActivateUser(string phonenumber, string ActivateCode);
		User GetUserByphoneNumber(string phonenumber);
		OperationResault RecoverUser(string phonenumber);
	}
}
