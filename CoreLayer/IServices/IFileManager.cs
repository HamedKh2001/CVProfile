using CORETest.Utilities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreLayer.Services.FileManager
{
	public interface IFileManager
	{
		string SaveFile(IFormFile formFile, string SavePath);
		List<string> SaveListFile(List<IFormFile> Files, string SavePath);
		OperationResault DeleteFile(string filePath, string fileName);
		Task<string> SaveProgress(IFormFile file, string SavePath, CancellationToken cancellationToken);
	}
}
