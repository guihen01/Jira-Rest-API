using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace RestAPi
{
    public class Program
    {
        public static async System.Threading.Tasks.Task Main(string[] args)
        {
            Console.WriteLine("----------------------------------------------------------------------------");
            Console.WriteLine("Execute (Jira Server platform) POST REST API");
            Console.WriteLine("-----------------------------------------------------------------------------");
            Console.WriteLine(" REf : Goto : https://docs.atlassian.com/software/jira/docs/api/REST/8.13.2/");
            Console.WriteLine("-----------------------------------------------------------------------------");

            string url;
            Console.WriteLine(" pathname complet (URL) of the Post Rest API request ? ");
            Console.WriteLine("example : http://localhost:8080/rest/api/2/issue        ");
            Console.WriteLine(" URIs for Jira's REST API ");
            url = Console.ReadLine();
 
            string rep;
            Console.WriteLine("  ");
            Console.WriteLine(" name of the json file parameters to send to Jira server via post rest api ? ");
            Console.WriteLine(" example : test.json ");
            rep = Console.ReadLine();
            Console.WriteLine("  ");
            if (File.Exists(rep))
            {
                Console.WriteLine(" json file with request parameters to send to Jira server via post rest api {0} :", rep);
                Console.WriteLine("---------------------------------------------------------------------------------------");
                Console.WriteLine("  ");
            }
            else
            {
                Console.WriteLine(" json file {0} doesnt exist  ", rep);
            }

            string dir2 = Directory.GetCurrentDirectory();
            string pathjson = dir2 + "/" + rep;

            StreamReader sr = new StreamReader(pathjson);

            string json1;
            json1 = sr.ReadToEnd();

            var json = JsonConvert.SerializeObject(json1);
            var data = new StringContent(json1, Encoding.UTF8, "application/json");

            using var client = new HttpClient();

            string user;
            Console.WriteLine("user account in Jira for authentication");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine(" Jira username  ? ");
            user = Console.ReadLine();
            
            string password;
            Console.WriteLine(" Jira password  ? ");
            password = Console.ReadLine();
            Console.WriteLine("  ");

            var base64String = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{user}:{password}"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64String);

            var response = await client.PostAsync(url, data);

            // It would be better to make sure this request actually made it through

            string result = await response.Content.ReadAsStringAsync();

            //close out the client
            client.Dispose();

            // Write to console the result of the request
            // -----------------------------------------------------------------------------
            Console.WriteLine(result);
            Console.WriteLine("  ");

            JObject Ob;
            Ob = JObject.Parse(result);

            //write the result sous forme groupée in a file 
            // write the result in a json formated file
            //----------------------------------------------------------------------------
            //ecriture dans un fichier des données au format Json
            // Get the current directory.

            string dir = Directory.GetCurrentDirectory();
            string path = dir + "/Response.json";
            if (File.Exists(path))
            {   File.Delete(path);
            }
            using (var tw = new StreamWriter(path, true))
            {   tw.WriteLine(result.ToString());
                tw.Close();
            }
            Console.WriteLine("json formated file : created {0} ", path);
            Console.WriteLine("--------------------------------------------------------------");

            //write the result sous forme groupée in a file 
            // write the result in a text formated file
            //----------------------------------------------------------------------------
            //ecriture dans un fichier des données au format string

            string path1 = dir + "/Response.txt";
            if (File.Exists(path1))
            {   File.Delete(path1);
            }
            using (var tw1 = new StreamWriter(path1, true))
            {   tw1.WriteLine(Ob.ToString());
                tw1.Close();
            }
            Console.WriteLine("text formated file created {0} ", path1);
            Console.WriteLine("----------------------------------------------------------");




        }
    }
}
