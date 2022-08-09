using MongoDB.Bson;
using MongoDB.Driver;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace PrakharTesting45.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult HttpCall()
        {
            ViewBag.Message = "HTTP Call done Successfully";
            return View();
        }

        public ActionResult SQLCall()
        {
            ViewBag.Message = "SQL Call done Successfully";
            GetSQLDBCall();
            return View();
        }

        public ActionResult WCFCall()
        {
            ViewBag.Message = "WCF Call done Successfully";
            GetWCFCall();
            return View();
        }
        public ActionResult WebAPICall()
        {
            ViewBag.Message = "WebAPI Call done Successfully";
            GetWebAPICall();
            return View();
        }
        public ActionResult MongoDBCall()
        {
            ViewBag.Message = "MondoDB Call done Successfully";
            ViewBag.Result = GetMonGoDBCall();
            return View();
        }
        public ActionResult RaiseException()
        {
            ViewBag.Message = "Exception Call done Successfully";
            GetException();
            return View();
        }
        public SqlConnection Conn;
        private void GetSQLDBCall()
        {
            try
            {
                var ConnStr = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString;
                Conn = new SqlConnection(ConnStr);
            string SqlString = "SELECT * FROM TR_Agent WHERE ID = 1;";

            SqlDataAdapter sda = new SqlDataAdapter(SqlString, Conn);

            DataTable dt = new DataTable();

                Conn.Open();
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Conn.Close();
            }
        }

        private void GetWebAPICall()
        {
            try
            {
                using (var client = new HttpClient())
                {
                client.BaseAddress = new Uri("https://gorest.co.in/public/v2/");
                //HTTP GET
                var responseTask = client.GetAsync("users");
                responseTask.Wait();

                var result = responseTask.Result;
                }

                // Function and Action
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                string url = "https://services.odata.org/TripPinRESTierService/ResetDataSource";
                string strResult = string.Empty;
                HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(url);
                webrequest.Method = "POST";
                webrequest.ContentType = "application/x-www-form-urlencoded";
                HttpWebResponse webresponse = (HttpWebResponse)webrequest.GetResponse();


                string url1 = "https://services.odata.org/TripPinRESTierService/GetNearestAirport(lat = 33, lon = -118)";
                string strResult1 = string.Empty;
                HttpWebRequest webrequest1 = (HttpWebRequest)WebRequest.Create(url);
                webrequest.Method = "GET";
                webrequest.ContentType = "application/x-www-form-urlencoded";
                HttpWebResponse webresponse1 = (HttpWebResponse)webrequest.GetResponse();

                //Querying Data
                string url2 = "https://services.odata.org/TripPinRESTierService/Me/Friends?$filter=Friends/any(f:f/FirstName eq 'Scott')";
                string strResult2 = string.Empty;
                HttpWebRequest webrequest2 = (HttpWebRequest)WebRequest.Create(url);
                webrequest.Method = "GET";
                webrequest.ContentType = "application/x-www-form-urlencoded";
                HttpWebResponse webresponse2 = (HttpWebResponse)webrequest.GetResponse();

                //Querying Data
                string url3 = "https://services.odata.org/TripPinRESTierService/Airports?$filter=contains(Location/Address, 'San Francisco')";
                string strResult3 = string.Empty;
                HttpWebRequest webrequest3 = (HttpWebRequest)WebRequest.Create(url);
                webrequest.Method = "GET";
                webrequest.ContentType = "application/x-www-form-urlencoded";
                HttpWebResponse webresponse3 = (HttpWebResponse)webrequest.GetResponse();

                //Request Etag
                string url4 = "https://services.odata.org/TripPinRESTierService/Airlines";
                string strResult4 = string.Empty;
                HttpWebRequest webrequest4 = (HttpWebRequest)WebRequest.Create(url);
                webrequest.Method = "GET";
                webrequest.ContentType = "application/x-www-form-urlencoded";
                HttpWebResponse webresponse4 = (HttpWebResponse)webrequest.GetResponse();

                //Delete Data
                string url5 = "https://services.odata.org/TripPinRESTierService/People('russellwhyte')";
                string strResult5 = string.Empty;
                HttpWebRequest webrequest5 = (HttpWebRequest)WebRequest.Create(url);
                webrequest.Method = "DELETE";
                webrequest.ContentType = "application/x-www-form-urlencoded";
                HttpWebResponse webresponse5 = (HttpWebResponse)webrequest.GetResponse();
            
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GetMonGoDBCall()
        {
            try
            {
                var ConnMongoDB = System.Configuration.ConfigurationManager.ConnectionStrings["MongoDBConnectionString"].ConnectionString;
                var dbClient = new MongoClient(ConnMongoDB);
                var dbList = dbClient.ListDatabases().ToList();


                Console.WriteLine("The list of databases are:");
                // List of database
                foreach (var item in dbList)
                {
                    Console.WriteLine(item);
                }
                // Call Atlas MongoDB Dataset
                var mongo_database = dbClient.GetDatabase("sample_weatherdata");
                var collection = mongo_database.GetCollection<BsonDocument>("data");
                var records = collection.Find(FilterDefinition<BsonDocument>.Empty).Limit(2).ToList();

                // Call Atlas DB stats
                IMongoDatabase db = dbClient.GetDatabase("sample_weatherdata");
                var command = new BsonDocument { { "dbstats", 1 } };
                var result = db.RunCommand<BsonDocument>(command);


                return result.ToJson();
            }
            catch (Exception ex)
            {
                throw ex;
            }
}

        private void GetWCFCall()
        {
            try
            {
                AgentDBService.WcfServiceClient wcfServiceClient = new AgentDBService.WcfServiceClient();
                //update
                var updatedata = wcfServiceClient.UpdateAgentData("123");
                //Get
                var data = wcfServiceClient.AgentData();

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        private void GetException()
        {
                var c = 0;
                var data = 1 / c;
            
        }
    }
}