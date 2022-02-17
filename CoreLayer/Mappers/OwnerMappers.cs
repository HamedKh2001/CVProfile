using CoreLayer.Dtos;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Mappers
{
    public static class OwnerMappers
    {
        public static OwnerDto ToOwnerDto(this Owner owner)
        {
			return new OwnerDto()
			{
				About = owner.About,
				BirthDate = owner.BirthDate,
				City = owner.City,
				Email = owner.Email,
				FullName = owner.FullName,
				IsActive = owner.IsActive,
				Languages = owner.Languages,
				Phonenumber = owner.Phonenumber,
				ProfilePhoto = owner.ProfilePhoto,
			};
		}
    }
}
