using NHibernatePoc.Core.Services;

namespace NHibernatePoc.Core.Tests.Services
{
    [TestFixture]
    public class NotificationServiceTests
    {
        private NotificationService _notificationService;
        private StringWriter _stringWriter;

        [SetUp]
        public void Setup()
        {
            _notificationService = new NotificationService();
            _stringWriter = new StringWriter();
            Console.SetOut(_stringWriter);  // Redirect console output to the StringWriter
        }

        [TearDown]
        public void TearDown()
        {
            _stringWriter.Dispose();
        }

        [Test]
        public void NotifyInventoryLow_PrintsCorrectMessage()
        {
            int partId = 1;
            int availableQuantity = 5;

            _notificationService.NotifyInventoryLow(partId, availableQuantity);
            var output = _stringWriter.ToString().Trim();

            Assert.AreEqual($"Alert: Inventory low for part ID {partId}. Only {availableQuantity} items left.", output);
        }

        [Test]
        public void NotifyOrderStatusChanged_PrintsCorrectMessage()
        {
            int orderId = 1;
            string newStatus = "Shipped";

            _notificationService.NotifyOrderStatusChanged(orderId, newStatus);
            var output = _stringWriter.ToString().Trim();

            Assert.AreEqual($"Notification: Order {orderId} status changed to {newStatus}.", output);
        }
    }
}
