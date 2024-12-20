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
            // Проверяет успешную генерацию резюме по шаблону 1, когда указаны корректные параметры "login" и "password".
            var request = new RestRequest("/pattern1/1", Method.Get);
            request.AddParameter("login", "testuser");
            request.AddParameter("password", "testpassword");

            var response = client.Execute(request);

            Assert.AreEqual((int)HttpStatusCode.OK, (int)response.StatusCode);
            Assert.IsTrue(response.Content.Contains("<html"));
        }

        [TestMethod]
        public void Test_GenerateResume_Pattern2_Success()
        {
            // Проверяет успешную генерацию резюме по шаблону 2, когда указаны корректные параметры "login" и "password".
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
            // Проверяет, что запрос к шаблону 3 возвращает ошибку 401 (Unauthorized), если параметры авторизации не переданы.
            var request = new RestRequest("/pattern3/1", Method.Get);

            var response = client.Execute(request);

            Assert.AreEqual((int)HttpStatusCode.Unauthorized, (int)response.StatusCode);
            Assert.IsTrue(response.Content.Contains("User not found"));
        }

        [TestMethod]
        public void Test_GenerateResumeImagePdf_Success()
        {
            // Проверяет успешное создание PDF-резюме с изображением, когда используется корректный идентификатор шаблона и пользователя.
            var request = new RestRequest("/pattern_image_pdf/1/1", Method.Get);

            var response = client.Execute(request);

            Assert.AreEqual((int)HttpStatusCode.OK, (int)response.StatusCode);
            Assert.IsTrue(response.Content.Contains("%PDF")); 
        }

        [TestMethod]
        public void Test_GenerateResumeImagePdf_Failure()
        {
            // Проверяет, что запрос на генерацию PDF-резюме возвращает ошибку 404 (NotFound), если передан некорректный идентификатор шаблона или пользователя.
            var request = new RestRequest("/pattern_image_pdf/999/1", Method.Get);

            var response = client.Execute(request);

            Assert.AreEqual((int)HttpStatusCode.NotFound, (int)response.StatusCode);
            Assert.IsTrue(response.Content.Contains("Failed to fetch PDF"));
        }
    }
}
