using System.Security.Cryptography;
using System.Text;

namespace Tips
{
    internal class Program
    {
        private static string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        static void Main(string[] args)
        {
            var fileName = string.Empty;
            try
            {
                fileName = Path.GetTempFileName();
                var sb = new StringBuilder();
                foreach (var _ in Enumerable.Range(0, 5000)) {
                    var line = RandomNumberGenerator.GetString(choices: chars, length: 1000);
                    sb.AppendLine(line);
                }
                File.WriteAllText(fileName, sb.ToString());
                var b = new List<ResourceHolder>();
                foreach (var i in Enumerable.Range(1, 10))
                {
                    b.Add( new ResourceHolder(fileName));
                    Console.WriteLine($"[{i}] ResourceHolder created.");
                }
            }
            finally
            {
                Console.WriteLine($"Please delete me '{fileName}'");
                //if (!string.IsNullOrEmpty(fileName))
                //{
                //    File.Delete(fileName);
                //}
            }
        }
    }

    /// <summary>
    /// Utilizing the IDisposable interface is a crucial C# performance tip.
    /// It helps you properly manage unmanaged resources and ensures that your application’s memory usage is efficient.
    /// </summary>
    public class ResourceHolder
    {
        private Stream _stream;

        public ResourceHolder(string filePath)
        {
            _stream = File.OpenRead(filePath);
        }

        // Missing: IDisposable implementation
    }
}
