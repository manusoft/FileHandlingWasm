using FileHandlingWasm.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace FileHandlingWasm.Server.Controllers;

[ApiController]
[Route("[Controller]")]
public class FilesController : Controller
{
	[HttpGet]
	public List<string> GetFiles()
	{
		var result = new List<string>();
		var files = Directory.GetFiles(Environment.CurrentDirectory + "\\Storage", "*.*");

        foreach (var file in files)
        {
			var name = Path.GetFileName(file);
			result.Add(name);
        }

		return result;
    }

	[HttpPost]
	public bool UploadFileChunk([FromBody] FileChunk fileChunk)
	{
		try
		{
			//get the local file name
			string filePath = Environment.CurrentDirectory + "\\Storage\\";
			string fileName = filePath + fileChunk.FileName;
			
			//delete the file is necessary
			if(fileChunk.FirstChunck && System.IO.File.Exists(fileName))
			{
				System.IO.File.Delete(fileName);
			}

			// open for writing
			using(var stream = System.IO.File.OpenWrite(fileName))
			{
				stream.Seek(fileChunk.Offset, SeekOrigin.Begin);
				stream.Write(fileChunk.Data,0, fileChunk.Data.Length);
			}

			return true;
		}
		catch (Exception ex)
		{
			var message = ex.Message;
			return false;
		}
	}
}
