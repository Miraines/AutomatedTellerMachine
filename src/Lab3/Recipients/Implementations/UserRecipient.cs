using Itmo.ObjectOrientedProgramming.Lab3.MessageEndpoints.Implementations;
using Itmo.ObjectOrientedProgramming.Lab3.Users;

namespace Itmo.ObjectOrientedProgramming.Lab3.Recipients.Implementations;

public class UserRecipient : Recipient
{
    public UserRecipient(User user) : base(new UserEndpoint(user)) { }
}