using System;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace DotNetSfs.Ws.Res
{
    internal static class ProcessZip
    {
        /// <summary>
        /// Extrae el archivo XML dentro del Array[](Zip)
        /// </summary>
        /// <param name="arrayZip">bytes of content zip</param>
        /// <returns>Stream que contiene el archivo XML-CDR </returns>
        /// <exception cref="FileNotFoundException">El archivo xml CDR de respuesta no fue encontrado</exception>
        public static Stream ExtractFile(byte[] arrayZip)
        {
            using (var zipContent= new MemoryStream(arrayZip))
            {
                using (var zip = new ZipArchive(zipContent))
                {
                    var cdr = zip.Entries.FirstOrDefault(f => f.Name.EndsWith(".xml", StringComparison.OrdinalIgnoreCase));
                    if(cdr == null)
                        throw new FileNotFoundException("El archivo xml CDR de respuesta no fue encontrado");
                    var stream = new MemoryStream();
                    cdr.Open()
                        .CopyTo(stream);
                    stream.Flush();
                    stream.Seek(0, SeekOrigin.Begin);
                    return stream;
                }
            }

        }


        /// <summary>
        /// Comprime un archivo, y guarda el zip en el mismo directorio con el mismo nombre.
        /// </summary>
        /// <param name="filename">filename</param>
        /// <param name="content">Content of file</param>
        /// <returns>bytes of zip</returns>
        public static byte[] CompressFile(string filename, byte[] content)
        {
            using (var mem = new MemoryStream())
            {
                using (var zip = new ZipArchive(mem, ZipArchiveMode.Create, false))
                {
                    var entry = zip.CreateEntry(filename);
                    using (var stream = entry.Open())
                    {
                        stream.Write(content, 0, content.Length);
                    }
                }
                return mem.ToArray();
            }
        }
    }
}
