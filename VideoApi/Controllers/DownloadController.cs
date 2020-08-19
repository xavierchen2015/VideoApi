using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using System.IO;

namespace VideoApi.Controllers
{
    public class DownloadController : ApiController
    {// GET api/values
        public async Task<HttpResponseMessage> Get(string url)
        {
            try
            {
                await GetFile(url);
                return Request.CreateResponse(HttpStatusCode.OK, "done");
            }
            catch
            {
                await GetFile(url);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "error");
            }

        }

        public static async Task GetFile(string uri)
        {
            byte[] bytes = new byte[0];
            string path = @"D:\xxx.mp4";
            using (var client = new HttpClient())
            {

                using (var result = await client.GetAsync(uri))
                {
                    if (result.IsSuccessStatusCode)
                    {
                        bytes = await result.Content.ReadAsByteArrayAsync();
                    }

                }
                using (MemoryStream ms = new MemoryStream())
                using (FileStream file = new FileStream(path, FileMode.Create, System.IO.FileAccess.Write))
                {
                    ms.Read(bytes, 0, (int)ms.Length);
                    file.Write(bytes, 0, bytes.Length);
                    ms.Close();
                }
            }
        }
    }
}
