using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TravelPlanner
{
    [TestFixture]
    public class HealthCheckControllerTests
        {
        private WebApplicationFactory<Startup> _factory;
        private HttpClient _client;

        [OneTimeSetUp]
        public void Setup()
            {
            _factory = new WebApplicationFactory<Startup>();
            _client = _factory.CreateClient();
            }

        [OneTimeTearDown]
        public void Teardown()
            {
            _client.Dispose();
            _factory.Dispose();
            }

        [Test]
        public async Task HealthCheck_ReturnsOk()
            {
            // Act
            var response = await _client.GetAsync("/healthcheck");

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            }
        }
    }
