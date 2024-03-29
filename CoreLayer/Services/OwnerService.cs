﻿using AutoMapper;
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
		#region DI
		private IFileManager _fileManager;
		private readonly IGenericRepository<Owner> _genericRepository;
		private readonly IMapper _mapper;
		private readonly CVContext _context;
		public OwnerService(CVContext context, IFileManager fileManager, IGenericRepository<Owner> genericRepository, IMapper mapper)
		{
			_fileManager = fileManager;
			_mapper = mapper;
			_genericRepository = genericRepository;
			_context = context;
		}
		#endregion

		public async Task<OperationResault> Insertasync(InsertOwnerDto insertOwnerDto)
		{
			try
			{
				var photoname = _fileManager.SaveFile(insertOwnerDto.ProfilePhoto, RootFile.InsertOwnerFile);
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

		public async Task<OwnerDto> GetProfileasync(int id)
        {
			var owner =await _genericRepository.Getasync(id);
			return owner.ToOwnerDto();
		}

		public async Task<OperationResault> ChangeStatusProfile(int id)
		{
			var owner = await _genericRepository.Getasync(id);
            if (owner.IsActive)
				owner.IsActive = false;
			else
				owner.IsActive = true;
			var res = _genericRepository.Update(owner);
			return res;
		}

		public async Task<OperationResault> Editasync(EditOwnerDto editOwnerDto)
		{
			try
			{
				var filename = _fileManager.SaveFile(editOwnerDto.ProfilePhotoFile, RootFile.InsertOwnerFile);
				var owner = editOwnerDto.ToOwner();
				owner.ProfilePhoto = filename;
				var updateRes = _genericRepository.Update(owner);
				return await Task.FromResult(updateRes);
			}
			catch (Exception ex)
			{
				return OperationResault.Error(ex.Message);
			}
		}
	}
}
