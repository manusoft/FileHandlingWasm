using FileHandlingWasm.Shared.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace FileHandlingWasm.Client.Helpers;

public class FilesManager
{
    HttpClient client;

    public FilesManager(HttpClient _client)
    {
        client = _client;    
    }

    public async Task<List<string>> GetFileNamesAsync()
    {
        try
        {
            var result = await client.GetAsync("files");
            result.EnsureSuccessStatusCode();
            string responseBody = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<string>>(responseBody);
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<bool> UploadFileChunkAsync(FileChunk fileChunk)
    {
        try
        {
            var result = await client.PostAsJsonAsync("files", fileChunk);
            result.EnsureSuccessStatusCode();
            string responseBody = await result.Content.ReadAsStringAsync();
            return Convert.ToBoolean(responseBody);
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
