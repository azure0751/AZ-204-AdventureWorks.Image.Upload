using Azure.Storage.Blobs;

namespace AdventureWorks.Image.Upload
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string connectionString = string.Empty;
            Console.Write($"This Program will upload all the file from a local folder to Azure Storage Account.{Environment.NewLine} ");
            Console.Write($"{Environment.NewLine}Enter storage account connection string: ");
            connectionString = Console.ReadLine();

            Console.Write("Enter container name: ");
            string containerName = Console.ReadLine();

            string currentDirectory = Directory.GetCurrentDirectory() + "\\images";
           
            if (Directory.Exists(currentDirectory))
            {
                string[] files = Directory.GetFiles(currentDirectory);

                Console.WriteLine("Files in folder:");
                foreach (string file in files)
                {
                    //Console.WriteLine(Path.GetFileName(file)); // Prints only the file name, not the full path
                    UploadBlob(connectionString, containerName, file);
                }
            }
            else
            {
                Console.WriteLine("Folder does not exist.");
            }
        }

        private static void UploadBlob(string azureStorageConnectionstring, string containerName, string filePath)
        {
            BlobContainerClient containerClient = new BlobContainerClient(azureStorageConnectionstring, containerName);
            containerClient.CreateIfNotExists();

            BlobClient blobClient = containerClient.GetBlobClient(System.IO.Path.GetFileName(filePath));
            blobClient.Upload(filePath, overwrite: true);

            Console.WriteLine($"{filePath} uploaded successfully in container {containerName}.");
        }
    }
}