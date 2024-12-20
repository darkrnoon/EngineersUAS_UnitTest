using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System.Net;

namespace EngineersUAS_UnitTest
{
    [TestClass]
    public class AuthApiTests
    {
        private const string BaseUrl = "http://127.0.0.1:5000";
        private RestClient client;

        [TestInitialize]
        public void Setup()
        {
            client = new RestClient(BaseUrl);
        }

        [TestMethod]
        public void Test_Register_Success()
        {
            var request = new RestRequest("/register", Method.Post);
            request.AddJsonBody(new
            {
                email = "test@example.com",
                password = "password123",
                first_name = "John",
                last_name = "Doe",
                phone = "1234567890",
                role_id = 1
            });

            var response = client.Execute(request);

            Assert.AreEqual((int)HttpStatusCode.Created, (int)response.StatusCode);
            Assert.IsTrue(response.Content.Contains("Студент успешно зарегистрирован"));
        }

        [TestMethod]
        public void Test_Register_MissingFields()
        {
            var request = new RestRequest("/register", Method.Post);
            request.AddJsonBody(new { email = "test@example.com" });

            var response = client.Execute(request);

            Assert.AreEqual((int)HttpStatusCode.BadRequest, (int)response.StatusCode);
            Assert.IsTrue(response.Content.Contains("password is required"));
        }

        [TestMethod]
        public void Test_Login_Success()
        {
            var request = new RestRequest("/login", Method.Post);
            request.AddJsonBody(new { email = "test@example.com", password = "password123" });

            var response = client.Execute(request);

            Assert.AreEqual((int)HttpStatusCode.OK, (int)response.StatusCode);
            Assert.IsTrue(response.Content.Contains("token"));
        }

        [TestMethod]
        public void Test_Login_InvalidCredentials()
        {
            var request = new RestRequest("/login", Method.Post);
            request.AddJsonBody(new { email = "wrong@example.com", password = "wrongpassword" });

            var response = client.Execute(request);

            Assert.AreEqual((int)HttpStatusCode.Unauthorized, (int)response.StatusCode);
            Assert.IsTrue(response.Content.Contains("Неверные учетные данные"));
        }

        [TestMethod]
        public void Test_Login_MissingFields()
        {
            var request = new RestRequest("/login", Method.Post);
            request.AddJsonBody(new { email = "test@example.com" });

            var response = client.Execute(request);

            Assert.AreEqual((int)HttpStatusCode.BadRequest, (int)response.StatusCode);
            Assert.IsTrue(response.Content.Contains("password is required"));
        }
    }
}
