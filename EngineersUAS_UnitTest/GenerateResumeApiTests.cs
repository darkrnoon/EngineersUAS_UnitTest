using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System.Net;

namespace EngineersUAS_UnitTest
{
    [TestClass]
    public class GenerateResumeApiTests
    {
        private const string BaseUrl = "http://127.0.0.1:5000";
        private RestClient client;

        [TestInitialize]
        public void Setup()
        {
            client = new RestClient(BaseUrl);
        }

        [TestMethod]
        public void Test_GenerateResume_Pattern1_Success()
        {
            var request = new RestRequest("/pattern1/1", Method.Get);
            request.AddParameter("login", "testuser");
            request.AddParameter("password", "testpassword");

            var response = client.Execute(request);

            Assert.AreEqual((int)HttpStatusCode.OK, (int)response.StatusCode);
            Assert.IsTrue(response.Content.Contains("<html")); // Проверяем, что HTML-шаблон возвращён
        }

        [TestMethod]
        public void Test_GenerateResume_Pattern2_Success()
        {
            var request = new RestRequest("/pattern2/1", Method.Get);
            request.AddParameter("login", "testuser");
            request.AddParameter("password", "testpassword");

            var response = client.Execute(request);

            Assert.AreEqual((int)HttpStatusCode.OK, (int)response.StatusCode);
            Assert.IsTrue(response.Content.Contains("<html"));
        }

        [TestMethod]
        public void Test_GenerateResume_Pattern3_Unauthorized()
        {
            var request = new RestRequest("/pattern3/1", Method.Get);

            var response = client.Execute(request);

            Assert.AreEqual((int)HttpStatusCode.Unauthorized, (int)response.StatusCode);
            Assert.IsTrue(response.Content.Contains("User not found"));
        }

        [TestMethod]
        public void Test_GenerateResumeImagePdf_Success()
        {
            var request = new RestRequest("/pattern_image_pdf/1/1", Method.Get);

            var response = client.Execute(request);

            Assert.AreEqual((int)HttpStatusCode.OK, (int)response.StatusCode);
            Assert.IsTrue(response.Content.Contains("%PDF")); // Проверяем, что PDF возвращён
        }

        [TestMethod]
        public void Test_GenerateResumeImagePdf_Failure()
        {
            var request = new RestRequest("/pattern_image_pdf/999/1", Method.Get);

            var response = client.Execute(request);

            Assert.AreEqual((int)HttpStatusCode.NotFound, (int)response.StatusCode);
            Assert.IsTrue(response.Content.Contains("Failed to fetch PDF"));
        }
    }
}
