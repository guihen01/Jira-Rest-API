using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace ExecuteRestAPi
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Execute (Jira Server platform) REST API");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine(" REf : Goto : https://docs.atlassian.com/software/jira/docs/api/REST/8.13.2/");

            string rep;
            Console.WriteLine(" pathname complet du fichier json ? ");
            Console.WriteLine(" par exemple : C:/C#Rest-API/Curl/Test4-Post/test.json ");
            rep = Console.ReadLine();
            StreamReader sr = new StreamReader(rep);

            string json1;
            json1 = sr.ReadToEnd();

            var json = JsonConvert.SerializeObject(json1);
            var data = new StringContent(json1, Encoding.UTF8, "application/json");

            string url;
            Console.WriteLine(" pathname complet (URL)  ? ");
            Console.WriteLine("exemple : http://localhost:8080/rest/api/2/issue");
            Console.WriteLine(" URIs for Jira's REST API ");
            url = Console.ReadLine();
            //var url = "http://localhost:8080/rest/api/2/issue";

            using var client = new HttpClient();

            //Set Basic Authorisation Auth
            //var user = "username";

            string user;
            Console.WriteLine("user account in Jira for authentication");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine(" Jira username  ? ");
            user = Console.ReadLine();
            //var user = "guihen01";

            string password;
            Console.WriteLine(" Jira password  ? ");
            password = Console.ReadLine();
            //var password = "admin";

            var base64String = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{user}:{password}"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64String);

            var response = await client.PostAsync(url, data);

            // It would be better to make sure this request actually made it through

            string result = await response.Content.ReadAsStringAsync();

            //close out the client
            client.Dispose();

            Console.WriteLine(result);
        }
    }
}
