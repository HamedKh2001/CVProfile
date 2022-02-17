using AutoMapper;
using CoreLayer.Dtos;
using DataLayer.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.AutoMapper
{
	public class UserMappers : Profile
	{
		//public static void RegisterServices(IServiceCollection services, Startup T)
		//{
		//	services.AddAutoMapper(typeof(T));
		//}
		public UserMappers() : base()
		{
			CreateMap<User, UserDto>();
			CreateMap<UserDto, User>();

		}
	}
}
