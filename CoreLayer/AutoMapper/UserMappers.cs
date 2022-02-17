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
		public UserMappers() : base()
		{
			CreateMap<User, UserDto>();
			CreateMap<UserDto, User>();

			CreateMap<InsertOwnerDto, Owner>().ForMember(p => p.ProfilePhoto, de => de.Ignore());
			CreateMap<Owner, InsertOwnerDto>().ForMember(p=>p.ProfilePhoto,de=>de.Ignore());

			CreateMap<Owner, OwnerDto>();
			CreateMap<OwnerDto, Owner>();
		}
	}
}
