using CoreLayer.Hubs;
using CoreLayer.IServices;
using CoreLayer.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CoreLayer.Services
{
	public class FileManager : IFileManager
	{
		private readonly IHubContext<UploadHub, IUploadHub> _uploadHub;
		public FileManager(IHubContext<UploadHub, IUploadHub> uploadHub)
		{
			_uploadHub = uploadHub;
		}

		public string SaveFile(IFormFile File, string SavePath)
		{
			var FileName = Guid.NewGuid() + Path.GetExtension(File.FileName);
			if (SavePath.StartsWith('/'))
			{
				SavePath = SavePath.Substring(1, SavePath.Length - 1);
			}
			var folderpath = Path.Combine(Directory.GetCurrentDirectory(), SavePath.Replace("/", "\\"));
			if (!Directory.Exists(folderpath))
			{
				Directory.CreateDirectory(folderpath);
			}
			var fullpath = Path.Combine(folderpath, FileName);
			using var stream = new FileStream(fullpath, FileMode.Create);
			File.CopyTo(stream);
			return FileName;
		}
		public List<string> SaveListFile(List<IFormFile> Files, string SavePath)

		{
			List<string> filenames = new List<string>();
			foreach (var File in Files)
			{
				var FileName = Guid.NewGuid() + Path.GetExtension(File.FileName);
				var folderpath = Path.Combine(Directory.GetCurrentDirectory(), SavePath.Replace("/", "\\"));
				if (!Directory.Exists(folderpath))
				{
					Directory.CreateDirectory(folderpath);
				}
				var fullpath = Path.Combine(folderpath, FileName);
				using var stream = new FileStream(fullpath, FileMode.Create);
				File.CopyTo(stream);
				filenames.Add(FileName);
			}
			return filenames;
		}
		public OperationResault DeleteFile(string filePath, string fileName)
		{
			try
			{
				if (filePath.StartsWith('/'))
				{
					filePath = filePath.Substring(1, filePath.Length - 1);
				}
				var folderpath = Path.Combine(Directory.GetCurrentDirectory(), filePath.Replace("/", "\\"));
				var fullpath = Path.Combine(folderpath, fileName);
				File.Delete(folderpath + fileName);
				return OperationResault.Success();
			}
			catch (Exception ex)
			{
				return OperationResault.Error(ex.Message);
			}
		}

		public async Task<string> SaveProgress(IFormFile file, string SavePath, CancellationToken cancellationToken)
		{
			byte[] buffer = new byte[16 * 1024];
			long totalBytes = file.Length;
			var FileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
			if (SavePath.StartsWith('/'))
			{
				SavePath = SavePath.Substring(1, SavePath.Length - 1);
			}
			var folderpath = Path.Combine(Directory.GetCurrentDirectory(), SavePath.Replace("/", "\\"));
			if (!Directory.Exists(folderpath))
			{
				Directory.CreateDirectory(folderpath);
			}
			int progress = 0;
			var fullpath = Path.Combine(folderpath, FileName);
			using (var output = new FileStream(fullpath, FileMode.Create))
			{
				using (Stream input = file.OpenReadStream())
				{
					long totalReadBytes = 0;
					int readBytes;
					var x = cancellationToken.IsCancellationRequested;
					while ((readBytes = input.Read(buffer, 0, buffer.Length)) > 0 && !cancellationToken.IsCancellationRequested)
					{
						await output.WriteAsync(buffer, 0, readBytes);
						totalReadBytes += readBytes;
						progress = (int)(totalReadBytes / (float)totalBytes * 100.0);
						await _uploadHub.Clients.All.SendProgress(file.FileName, progress);
						//await Task.Delay(100);
					}
				}
			}
			if (cancellationToken.IsCancellationRequested)
				DeleteFile(SavePath, FileName);
			return FileName;
		}

	}
}
