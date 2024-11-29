using Itmo.ObjectOrientedProgramming.Lab4.FileSystemApp.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystemApp.ResponsibilityChain;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystemApp.ResponsibilityChain.CommandHandles.ConnectionHandles;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystemApp.ResponsibilityChain.CommandHandles.TreeHandles;
using Moq;
using Xunit;

namespace Lab4.Tests;

public class ConnectionTests
{
    [Fact]
    public void ConnectCommand_WithValidPathAndLocalMode_ShouldUpdateCurrentPath()
    {
        var fileSystemState = new FileSystemState();
        var printerMock = new Mock<IPrint>();

        var connectionTypeHandler = new ConnectionTypeHandle();
        connectionTypeHandler
            .SetNext(new ConnectHandle(fileSystemState, printerMock.Object))
            .SetNext(new NullChainLink<CommandRequest>());

        var request = new CommandRequest("connect C:\\TestDir -m local"); // Mode is handled in ConnectionTypeHandle
        Directory.CreateDirectory("C:\\TestDir");

        CommandRequest? result = connectionTypeHandler.Handle(request);

        Assert.NotNull(result);
        Assert.Equal("C:\\TestDir", fileSystemState.CurrentPath); // Directly check the expected path
        printerMock.Verify(x => x.Print(It.IsAny<string>()), Times.Never);

        Directory.Delete("C:\\TestDir", true);
    }

    [Fact]
    public void TreeGoto_AfterConnect_ShouldChangeDirectory()
    {
        var fileSystemState = new FileSystemState();
        var printerMock = new Mock<IPrint>();

        var connectionTypeHandler = new ConnectionTypeHandle();
        connectionTypeHandler
            .SetNext(new ConnectHandle(fileSystemState, printerMock.Object))
            .SetNext(new TreeGotoHandle(fileSystemState, printerMock.Object))
            .SetNext(new NullChainLink<CommandRequest>());

        connectionTypeHandler.Handle(new CommandRequest("connect C:\\TestDir -m local"));

        connectionTypeHandler.Handle(new CommandRequest("tree goto C:\\NewDir"));

        Assert.Equal("C:\\NewDir", fileSystemState.CurrentPath);
        printerMock.Verify(x => x.Print(It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public void DisconnectCommand_ShouldClearCurrentPath()
    {
        var fileSystemState = new FileSystemState();
        var printerMock = new Mock<IPrint>();

        var connectionTypeHandler = new ConnectionTypeHandle();
        connectionTypeHandler
            .SetNext(new ConnectHandle(fileSystemState, printerMock.Object))
            .SetNext(new DisconnectHandle(fileSystemState, printerMock.Object))
            .SetNext(new NullChainLink<CommandRequest>());

        var connectRequest = new CommandRequest("connect C:\\TestDir -m local");
        Directory.CreateDirectory("C:\\TestDir");
        connectionTypeHandler.Handle(connectRequest);
        Assert.NotNull(fileSystemState.CurrentPath);

        var disconnectRequest = new CommandRequest("disconnect");
        connectionTypeHandler.Handle(disconnectRequest);

        Assert.NotNull(fileSystemState.CurrentPath);
        printerMock.Verify(x => x.Print(It.IsAny<string>()), Times.Never);
    }

    public class NullChainLink<TRequest> : ChainLink<TRequest> where TRequest : class
    {
        public override TRequest? Handle(TRequest request) => null;
    }
}