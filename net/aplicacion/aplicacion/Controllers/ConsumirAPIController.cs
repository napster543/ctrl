using aplicacion.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace aplicacion.Controllers
{
    public class ConsumirAPIController : Controller
    {
        public IActionResult Listar()
        {

            var url = $"https://api.iotsol.net/api/GetIMEIDataServicesByIMEIAndCompany";
            var request = (HttpWebRequest)WebRequest.Create(url);
            //string json = $"{{\"data\":\"{data}\"}}";
            string json = $"{{\"IMEI\": \"354330030646882\",\"CompanyID\": 10}}";
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
            ConsumoModel bsObj2 = new ConsumoModel();
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();

                            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(responseBody)))
                            {
                                // Deserialization from JSON 
                                DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(ConsumoModel));
                                bsObj2 = (ConsumoModel)deserializer.ReadObject(ms);
                                Console.Write("Name: " + bsObj2.IMEI); 
                                
                            }

                            // Do something with responseBody
                            Console.WriteLine(responseBody);
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                // Handle error
            }

            return View(bsObj2);
        }
    }
}
