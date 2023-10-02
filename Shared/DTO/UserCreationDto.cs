namespace Shared.DTO;

public class UserCreationDto
{
    public string UserName { get; }

    public UserCreationDto(string userName)
    {
        UserName = userName;
    }
}