using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApp3;
using WebApp3.Models;

namespace WebApp3_test
{
    [TestClass]
    public class PersonneRestControllerTest
    {
        private static WebApplicationFactory<Startup> _factory;

        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            _factory = new WebApplicationFactory<Startup>();
        }

        [TestMethod]
        public async Task A_GetPersonsTest()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/personnesrest");

            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task B_PostPersonTest()
        {
            var client = _factory.CreateClient();

            Personne p = new Personne
            {
                Nom = "Wick",
                Prenom = "John"
            };

            var response = await client.PostAsync("api/personnesrest",
               new StringContent(JsonConvert.SerializeObject(p), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var resp = response.Content.ReadAsStringAsync().Result;

            var responseData = JsonConvert.DeserializeObject<Personne>(resp);
            Assert.IsNotNull(responseData);

            var nom = "Wick";
            Assert.IsTrue(nom == responseData.Nom);
        }

        [TestMethod]
        public async Task C_UpdatePersonTest()
        {
            var client = _factory.CreateClient();

            Personne p = new Personne
            {
                Num = 8,
                Nom = "John",
                Prenom = "Doe",
            };

            var response = await client.PutAsync("api/personnesrest/8", new StringContent(JsonConvert.SerializeObject(p),
                Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();

            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);

        }

        [TestMethod]
        public async Task D_GetPersonByIdTest()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("api/personnesrest/8");

            response.EnsureSuccessStatusCode();


            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "Il faut absolument que le HttpStatusCode soit 200");
            Assert.AreEqual("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());


            Assert.AreNotEqual("text/plain; charset=utf-8", response.Content.Headers.ContentType?.ToString()
                , "Il faut absolument que les HttpStatusCode soient differents");

            var json = await response.Content.ReadAsStringAsync();

            Assert.AreEqual("{\"num\":8,\"nom\":\"John\",\"prenom\":\"Doe\"}", json);

            var resp = response.Content.ReadAsStringAsync().Result;
            var responseData = JsonConvert.DeserializeObject<Personne>(resp);

            Assert.IsNotNull(responseData);

            var nom = "John";

            Assert.IsFalse(nom == responseData.Prenom);
            Assert.IsTrue(nom != responseData.Prenom);

            Assert.IsTrue(nom == responseData.Nom);

        }

        [TestMethod]
        public async Task E_DeletePersonByIdTest()
        {
            var client = _factory.CreateClient();

            var response = await client.DeleteAsync("api/personnesrest/8");

            response.EnsureSuccessStatusCode();

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            _factory.Dispose();
        }
    }
}
