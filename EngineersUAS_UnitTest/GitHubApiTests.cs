using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System.Net;

namespace EngineersUAS_UnitTest
{
    [TestClass]
    public class GitHubApiTests
    {
        private const string BaseUrl = "http://127.0.0.1:5000";
        private RestClient client;

        [TestInitialize]
        public void Setup()
        {
            client = new RestClient(BaseUrl);
        }

        [TestMethod]
        public void Test_GetRepos_Success()
        {
            var request = new RestRequest("/repos", Method.Get);
            request.AddParameter("github_url", "https://github.com/testuser");

            var response = client.Execute(request);

            Assert.AreEqual((int)HttpStatusCode.OK, (int)response.StatusCode);
            Assert.IsTrue(response.Content.Contains("name"));
        }

        [TestMethod]
        public void Test_GetRepos_MissingGitHubUrl()
        {
            var request = new RestRequest("/repos", Method.Get);

            var response = client.Execute(request);

            Assert.AreEqual((int)HttpStatusCode.BadRequest, (int)response.StatusCode);
            Assert.IsTrue(response.Content.Contains("GitHub URL обязателен"));
        }

        [TestMethod]
        public void Test_AddRepos_Success()
        {
            var request = new RestRequest("/add_repos", Method.Post);
            request.AddParameter("github_url", "https://github.com/testuser");
            request.AddParameter("id_resume", 1);

            var response = client.Execute(request);

            Assert.AreEqual((int)HttpStatusCode.OK, (int)response.StatusCode);
            Assert.IsTrue(response.Content.Contains("generated_description"));
        }

        [TestMethod]
        public void Test_AddRepos_MissingParameters()
        {
            var request = new RestRequest("/add_repos", Method.Post);

            var response = client.Execute(request);

            Assert.AreEqual((int)HttpStatusCode.BadRequest, (int)response.StatusCode);
            Assert.IsTrue(response.Content.Contains("GitHub URL и resume_id обязательны"));
        }

        [TestMethod]
        public void Test_GetRepos_EmptyResult()
        {
            var request = new RestRequest("/repos", Method.Get);
            request.AddParameter("github_url", "https://github.com/nonexistentuser");

            var response = client.Execute(request);

            Assert.AreEqual((int)HttpStatusCode.OK, (int)response.StatusCode);
            Assert.IsTrue(response.Content.Contains("[]")); // Ожидается пустой массив
        }

        [TestMethod]
        public void Test_AddRepos_InvalidUrl()
        {
            var request = new RestRequest("/add_repos", Method.Post);
            request.AddParameter("github_url", "invalid_url");
            request.AddParameter("id_resume", 1);

            var response = client.Execute(request);

            Assert.AreEqual((int)HttpStatusCode.BadRequest, (int)response.StatusCode);
            Assert.IsTrue(response.Content.Contains("Неверный формат URL"));
        }

        [TestMethod]
        public void Test_AddRepos_Unauthorized()
        {
            var request = new RestRequest("/add_repos", Method.Post);
            request.AddParameter("github_url", "https://github.com/testuser");
            request.AddParameter("id_resume", 1);

            // Предположим, что для доступа требуется токен
            request.AddHeader("Authorization", "Bearer invalid_token");

            var response = client.Execute(request);

            Assert.AreEqual((int)HttpStatusCode.Unauthorized, (int)response.StatusCode);
            Assert.IsTrue(response.Content.Contains("Unauthorized"));
        }
    }
}
