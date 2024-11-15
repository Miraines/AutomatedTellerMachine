using Itmo.ObjectOrientedProgramming.Lab3.Filters;
using Itmo.ObjectOrientedProgramming.Lab3.Loggers;
using Itmo.ObjectOrientedProgramming.Lab3.MessageEndpoints;
using Itmo.ObjectOrientedProgramming.Lab3.Messages;
using Itmo.ObjectOrientedProgramming.Lab3.Recipients;
using Itmo.ObjectOrientedProgramming.Lab3.Recipients.Implementations;
using Itmo.ObjectOrientedProgramming.Lab3.Topics;
using Itmo.ObjectOrientedProgramming.Lab3.Users;
using Moq;
using Xunit;

namespace Lab3.Tests;

public class SystemTests
{
    [Fact]
    public void Receive_Message_IsSavedAsUnread()
    {
        var user = new User("TestUser");
        var userRecipient = new UserRecipient(user);
        var message = new Message("Test", "Test Content", importanceLevel: 2);
        var topic = new Topic("TestTopic");
        topic.AddRecipient(userRecipient);
        topic.Send(message);

        Assert.Single(user.Messages);
        ReceivedMessage receivedMessage = user.Messages.First();
        Assert.Equal(MessageStatus.Unread, receivedMessage.Status);
        Assert.Equal(message, receivedMessage.Message);
    }

    [Fact]
    public void MarkAsRead_MessageUnread_ShouldChangeStatusToRead()
    {
        var user = new User("TestUser");
        var userRecipient = new UserRecipient(user);
        var message = new Message("Test", "Test Content", importanceLevel: 2);
        userRecipient.Receive(message);

        user.MarkMessagesAsRead(message.Id);

        ReceivedMessage receivedMessage = user.Messages.First();
        Assert.Equal(MessageStatus.Read, receivedMessage.Status);
    }

    [Fact]
    public void MarkAsRead_MessageAlreadyRead_ShouldThrowException()
    {
        var user = new User("TestUser");
        var userRecipient = new UserRecipient(user);
        var message = new Message("Test", "Test Content", importanceLevel: 2);
        userRecipient.Receive(message);
        user.MarkMessagesAsRead(message.Id);

        InvalidOperationException exception =
            Assert.Throws<InvalidOperationException>(() => user.MarkMessagesAsRead(message.Id));
        Assert.Equal("Сообщение уже отмечено как прочитанное.", exception.Message);
    }

    [Fact]
    public void FilteredRecipient_ShouldNotReceiveMessageBelowImportance()
    {
        var mockInnerRecipient = new Mock<IRecipient>();
        var filter = new ImportanceLevelFilter(minimumImportanceLevel: 5);
        var filteredRecipient = new FilteredRecipient(mockInnerRecipient.Object, filter);
        var message = new Message("Low Importance", "This is a low importance message.", importanceLevel: 3);

        filteredRecipient.Receive(message);

        mockInnerRecipient.Verify(r => r.Receive(It.IsAny<Message>()), Times.Never);
    }

    [Fact]
    public void LoggedRecipient_ShouldLog_WhenMessageIsReceived()
    {
        var mockInnerRecipient = new Mock<IRecipient>();
        var mockLogger = new Mock<ILogger>();
        var loggedRecipient = new LoggedRecipients(mockInnerRecipient.Object, mockLogger.Object);
        var message = new Message("Test", "Test Content", importanceLevel: 2);

        loggedRecipient.Receive(message);

        mockInnerRecipient.Verify(r => r.Receive(message), Times.Once);
    }

    [Fact]
    public void MessengerRecipient_ShouldInvokeMessengerPrint()
    {
        var mockMessenger = new Mock<IMessenger>();
        var messengerRecipient = new MessengerRecipient(mockMessenger.Object);
        var message = new Message("Alert", "System is down", importanceLevel: 5);

        messengerRecipient.Receive(message);

        mockMessenger.Verify(
            m => m.Print(It.Is<Message>(msg =>
                msg.Id == message.Id &&
                msg.Description == message.Description &&
                msg.Content == message.Content &&
                msg.ImportanceLevel == message.ImportanceLevel)),
            Times.Once);
    }

    [Fact]
    public void Send_MessageWithLowImportance_UserReceivesOnce()
    {
        var user = new User("TestUser");

        var userRecipient1 = new UserRecipient(user);
        var innerRecipient2 = new UserRecipient(user);
        var filter = new ImportanceLevelFilter(minimumImportanceLevel: 5);
        var filteredRecipient2 = new FilteredRecipient(innerRecipient2, filter);

        var groupRecipient = new GroupRecipient();
        groupRecipient.AddSubRecipient(userRecipient1);
        groupRecipient.AddSubRecipient(filteredRecipient2);

        var message = new Message("TestDescription", "TestContent", importanceLevel: 3);

        groupRecipient.Receive(message);

        Assert.Single(user.Messages);
        ReceivedMessage receivedMessage = user.Messages.First();
        Assert.Equal(message.Description, receivedMessage.Message.Description);
        Assert.Equal(message.Content, receivedMessage.Message.Content);
        Assert.Equal(message.ImportanceLevel, receivedMessage.Message.ImportanceLevel);
        Assert.Equal(MessageStatus.Unread, receivedMessage.Status);
    }
}