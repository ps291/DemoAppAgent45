using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
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
            GetMonGoDBCall();
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
                
            string ConnStr = @"Data Source=.;Initial Catalog=AgentDB;Integrated Security=SSPI;";
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
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GetMonGoDBCall()
        {
            try
            {
                var dbClient = new MongoClient("mongodb://127.0.0.1:27017");
                var dbList = dbClient.ListDatabases().ToList();

                Console.WriteLine("The list of databases are:");

                foreach (var item in dbList)
                {
                    Console.WriteLine(item);
                }
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
                var data = wcfServiceClient.AgentData();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        private void GetException()
        {
            try
            {
                var c = 0;
                var data = 1 / c;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}