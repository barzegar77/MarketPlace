using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.PublicClasses
{
	public interface IFileUploader
	{
		List<string> UploadFile(List<IFormFile> files, string path);
	}

	public class FileUploader : IFileUploader
	{
		public List<string> UploadFile(List<IFormFile> files, string path)
		{
			List<string> fileNames = new List<string>();
			string fileName = "";
			string rootPath = Directory.GetCurrentDirectory() + "\\wwwroot";

			foreach(var itm in files)
			{
				fileName  = Guid.NewGuid().ToString().Replace("-", "").ToLower() + Path.GetExtension(itm.FileName);
				string currentPath = rootPath + path + fileName;

				using(var fs = new FileStream(currentPath, FileMode.Create))
				{
					itm.CopyTo(fs);
				}

				fileNames.Add(fileName);
				fileName = "";
			}

			return fileNames;
		}
	}
}
