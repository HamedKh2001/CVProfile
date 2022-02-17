using AutoMapper;
using CoreLayer.AutoMapper;
using CoreLayer.Dtos;
using CoreLayer.IServices;
using CoreLayer.Mappers;
using CoreLayer.Services.FileManager;
using CoreLayer.Utilities;
using CORETest.Utilities;
using DataLayer.Context;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Services
{
	public class OwnerService : IOwnerService
	{
		private IFileManager _fileManager;
		private readonly IGenericRepository<Owner> _genericRepository;
		private readonly IMapper _mapper;
		private readonly CVContext _context;
		public OwnerService(CVContext context,IFileManager fileManager, IGenericRepository<Owner> genericRepository, IMapper mapper)
		{
			_fileManager = fileManager;
			_mapper = mapper;
			_genericRepository = genericRepository;
			_context = context;
		}
		public async Task<OperationResault> Insert(InsertOwnerDto insertOwnerDto)
		{
			try
			{
				var photoname = _fileManager.SaveFile(insertOwnerDto.ProfilePhoto, RootFile.InsertUserPhoto);
				//var owner = _mapper.Map<Owner>(insertOwnerDto);
				var owner = new Owner()
				{
					About=insertOwnerDto.About,
					Phonenumber=insertOwnerDto.Phonenumber,
					Languages=insertOwnerDto.Languages,
					City=insertOwnerDto.City,
					BirthDate=insertOwnerDto.BirthDate,
					Email=insertOwnerDto.Email,
					FullName=insertOwnerDto.FullName,
				};
				owner.ProfilePhoto = photoname;
				var res = await _genericRepository.Insertasync(owner);
				//	var res =_genericRepository.Insertasync(new Owner()
				//	{
				//		ProfilePhoto= photoname,

				//	});
				return res;
			}
			catch (Exception ex)
			{
				return OperationResault.Error(ex.Message);
			}
		}

        public async Task<OwnerDto> GetActiveProfile()
        {
			var res = await _context.Myselves.FirstOrDefaultAsync(u => u.IsActive == true);
			return res.ToOwnerDto();
        }

        public async Task<List<OwnerDto>> GetallProfile()
        {
			return await _genericRepository.GetAll().Select(o=>o.ToOwnerDto()).ToListAsync();
		}
    }
}
