using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileHandlingWasm.Shared.Models;

public class FileChunk
{
    public string FileName { get; set; } = ""; // no path
    public long Offset { get; set; }
    public byte[] Data { get; set; }

    public bool FirstChunck = false;
}
