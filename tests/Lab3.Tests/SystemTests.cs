using Itmo.ObjectOrientedProgramming.Lab3.Loggers;
using Itmo.ObjectOrientedProgramming.Lab3.MessageEndpoints;
using Itmo.ObjectOrientedProgramming.Lab3.Messages;
using Itmo.ObjectOrientedProgramming.Lab3.Recipients;
using Moq;
using Xunit;

namespace Lab3.Tests;

public class SystemTests
{
    [Fact]
    public void Receive_Message_IsSavedAsUnread()
    {
        var mockLogger = new Mock<ILogger>();
        var user = new User("TestUser", mockLogger.Object);
        var userRecipient = new UserRecipient(user, minimumImportanceLevel: 1, mockLogger.Object);
        var message = new Message("Test", "Test Content", importanceLevel: 2);

        userRecipient.Receive(message);
        userRecipient.Send(message);

        Assert.Single(user.Messages);
        ReceivedMessage receivedMessage = user.Messages.First();
        Assert.True(receivedMessage.Status == MessageStatus.Unread);
        Assert.True(message == receivedMessage.Message);

        mockLogger.Verify(
            logger => logger.Log(
                $"Пользователь {user.Name} получил сообщение ID: {message.Id}, Описание: {message.Description}"),
            Times.Once);

        mockLogger.Verify(
            logger => logger.Log($"UserRecipient отправил сообщение ID: {message.Id}"),
            Times.Once);
    }

    [Fact]
    public void MarkAsRead_MessageUnread_ShouldChangeStatusToRead()
    {
        var mockLogger = new Mock<ILogger>();
        var user = new User("TestUser", mockLogger.Object);
        var userRecipient = new UserRecipient(user, minimumImportanceLevel: 1, mockLogger.Object);
        var message = new Message("Test", "Test Content", importanceLevel: 2);
        userRecipient.Receive(message);
        userRecipient.Send(message);

        user.MarkMessagesAsRead(message.Id);

        ReceivedMessage receivedMessage = user.Messages.First();
        Assert.True(receivedMessage.Status == MessageStatus.Read);

        mockLogger.Verify(
            logger => logger.Log($"Пользователь {user.Name} отметил сообщение ID: {message.Id} как прочитанное."),
            Times.Once);
    }

    [Fact]
    public void MarkAsRead_MessageAlreadyRead_ShouldThrowExceptionAndLogError()
    {
        var mockLogger = new Mock<ILogger>();
        var user = new User("TestUser", mockLogger.Object);
        var userRecipient = new UserRecipient(user, minimumImportanceLevel: 1, mockLogger.Object);
        var message = new Message("Test", "Test Content", importanceLevel: 2);
        userRecipient.Receive(message);
        userRecipient.Send(message);
        user.MarkMessagesAsRead(message.Id);

        Exception? exception = Record.Exception(() => user.MarkMessagesAsRead(message.Id));

        Assert.NotNull(exception);
        Assert.IsType<InvalidOperationException>(exception);
        Assert.Equal("Сообщение уже отмечено как прочитанное.", exception.Message);
    }

    [Fact]
    public void FilteredRecipient_ShouldNotReceiveMessageBelowImportance()
    {
        var mockLogger = new Mock<ILogger>();
        var mockRecipient = new Mock<IRecipient>();
        mockRecipient.SetupGet(r => r.MinimumImportanceLevel).Returns(5);
        var groupRecipient = new GroupRecipient(minimumImportanceLevel: 1, mockLogger.Object);
        groupRecipient.AddSubRecipient(mockRecipient.Object);
        var message = new Message(
            "Low Importance",
            "This is a low importance message.",
            importanceLevel: 3);

        groupRecipient.Receive(message);

        mockRecipient.Verify(r => r.Receive(It.IsAny<Message>()), Times.Never);

        mockLogger.Verify(
            logger => logger.Log(
                $"GroupRecipient получил сообщение ID: {message.Id}, Уровень важности: {message.ImportanceLevel}"),
            Times.Once);

        mockLogger.Verify(
            logger => logger.Log(It.Is<string>(s => s.Contains($"MockRecipient получил сообщение ID: {message.Id}"))),
            Times.Never);
    }

    [Fact]
    public void Recipient_WithConfiguredLogger_ShouldLog_WhenMessageIsReceived()
    {
        var mockLogger = new Mock<ILogger>();
        var user = new User("TestUser", mockLogger.Object);
        var userRecipient = new UserRecipient(user, minimumImportanceLevel: 1, mockLogger.Object);
        var message = new Message("Test", "Test Content", importanceLevel: 2);

        userRecipient.Receive(message);
        userRecipient.Send(message);

        string expectedRecipientLog =
            $"UserRecipient получил сообщение ID: {message.Id}, Уровень важности: {message.ImportanceLevel}";
        string expectedRecipientLogSecond =
            $"UserRecipient отправил сообщение ID: {message.Id}";
        string userLog =
            $"Пользователь {user.Name} получил сообщение ID: {message.Id}, Описание: {message.Description}";

        mockLogger.Verify(
            logger => logger.Log(expectedRecipientLog),
            Times.Once);

        mockLogger.Verify(
            logger => logger.Log(expectedRecipientLogSecond),
            Times.Once);

        mockLogger.Verify(
            logger => logger.Log(userLog),
            Times.Once);

        mockLogger.Verify(
            logger => logger.Log(It.IsAny<string>()),
            Times.Exactly(3));
    }

    [Fact]
    public void Send_Message_ViaMessenger_ShouldInvokeMessengerPrintAndLog()
    {
        var mockLogger = new Mock<ILogger>();
        var mockMessenger = new Mock<IMessenger>();

        mockMessenger.Setup(m => m.Print(It.IsAny<Message>()));

        var messengerRecipient =
            new MessengerRecipient(mockMessenger.Object, minimumImportanceLevel: 1, mockLogger.Object);
        var message = new Message("Alert", "System is down", importanceLevel: 5);

        messengerRecipient.Receive(message);
        messengerRecipient.Send(message);

        mockMessenger.Verify(
            m => m.Print(It.Is<Message>(msg =>
                msg.Id == message.Id &&
                msg.Description == message.Description &&
                msg.Content == message.Content &&
                msg.ImportanceLevel == message.ImportanceLevel)),
            Times.Once);

        mockLogger.Verify(
            logger => logger.Log(
                $"MessengerRecipient получил сообщение ID: {message.Id}, Уровень важности: {message.ImportanceLevel}"),
            Times.Once);

        mockLogger.Verify(
            logger => logger.Log(
                $"MessengerRecipient отправил сообщение ID: {message.Id}"),
            Times.Once);
    }

    [Fact]
    public void Send_MessageWithLowImportance_UserReceivesOnce()
    {
        var loggerMock = new Mock<ILogger>();

        var topic = new Topic("TestTopic", loggerMock.Object);

        var user = new User("TestUser", loggerMock.Object);

        var recipient1 = new UserRecipient(user, minimumImportanceLevel: 1, loggerMock.Object);
        var recipient2 = new UserRecipient(user, minimumImportanceLevel: 5, loggerMock.Object);

        topic.AddRecipient(recipient1);
        topic.AddRecipient(recipient2);

        var message = new Message("TestDescription", "TestContent", importanceLevel: 3);

        topic.Send(message);

        foreach (IRecipient recipient in topic.Recipients)
        {
            if (recipient is UserRecipient userRecipient &&
                userRecipient.Messages.Contains(message))
            {
                userRecipient.Send(message);
            }
        }

        Assert.Single(user.Messages);

        ReceivedMessage receivedMessage = user.Messages.First();
        Assert.Equal(message.Description, receivedMessage.Message.Description);
        Assert.Equal(message.Content, receivedMessage.Message.Content);
        Assert.Equal(message.ImportanceLevel, receivedMessage.Message.ImportanceLevel);
        Assert.Equal(MessageStatus.Unread, receivedMessage.Status);

        loggerMock.Verify(logger => logger.Log(It.IsAny<string>()), Times.AtLeastOnce);
    }
}