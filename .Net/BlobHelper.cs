using Microsoft.Azure.WebJobs.Host;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BlobHelper
{
    public class BlobHelper
    {
        private string ConnectionString = "";
        private CloudBlobContainer Container = null;
        private readonly Object lockObj = new object();

        public BlobHelper(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public bool DocumentExist(string containerName, string path, TraceWriter log = null)
        {
            try
            {
                CloudBlobContainer container = GetContainer(containerName);
                if (container != null)
                {
                    var blob = container.GetBlockBlobReference(path);
                    if (blob != null && blob.Exists())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                log.Error(e.Message);
            }

            return false;
        }

        public async Task<bool> UpsertDocumentAsync(string containerName,string path, string contents, string contentType = "text/html", TraceWriter log = null)
        {
            try
            {
                CloudBlobContainer container = GetContainer(containerName);
                if (container != null)
                {
                    var blob = container.GetBlockBlobReference(path);

                    blob.Properties.ContentType = contentType;
                    using (Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(contents)))
                    {
                        await blob.UploadFromStreamAsync(stream);
                    }

                    return true;
                }
            }
            catch (Exception e)
            {
                log.Error(e.Message);
            }

            return false;
        }
        public async Task<string> DownloadTextDocumentAsync(string containerName, string path, TraceWriter log = null)
        {
            try
            {
                CloudBlobContainer container = GetContainer(containerName);
                if (container != null)
                {
                    var blob = container.GetBlockBlobReference(path);
                    string text = await blob.DownloadTextAsync();
                    return text;
                }
            }
            catch (Exception e)
            {
                log.Error(e.Message);
            }

            return "";
        }

        public async Task<Stream> DownloadStreamDocumentAsync(string containerName,string path, TraceWriter log = null)
        {
            try
            {
                CloudBlobContainer container = GetContainer(containerName);
                if (container != null)
                {
                    var blob = container.GetBlockBlobReference(path);
                    MemoryStream ms = new MemoryStream();
                    if (blob.Exists())
                    {
                        await blob.DownloadToStreamAsync(ms);
                        ms.Seek(0, SeekOrigin.Begin);
                    }
                    return ms;
                }
            }
            catch (Exception e)
            {
                log.Error(e.Message);
            }
            return new MemoryStream();
        }

        private CloudBlobContainer GetContainer(string containerName,TraceWriter log = null)
        {
            lock (lockObj)
            {
                if (Container == null || Container.Name!= containerName)
                {
                    if (CloudStorageAccount.TryParse(ConnectionString, out CloudStorageAccount storageAccount))
                    {
                        // If the connection string is valid, proceed with operations against Blob storage here.
                        CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
                        Container = cloudBlobClient.GetContainerReference(containerName);
                    }
                    else
                    {
                        LogErrorIfNotNull(log, "Invalid Connection string for BlobStorage");
                    }
                }
            }

            return Container;
        }

        private static void LogInfoIfNotNull(TraceWriter log, string str)
        {
            if (log != null)
            {
                log.Info(str);
            }
        }

        private static void LogErrorIfNotNull(TraceWriter log, string str)
        {
            if (log != null)
            {
                log.Error(str);
            }
        }
    }
}
