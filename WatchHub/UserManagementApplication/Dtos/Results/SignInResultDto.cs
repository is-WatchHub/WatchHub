namespace UserManagementApplication.Dtos.Results;

public class SignInResultDto
{
    public SignInResultDto(bool isSucceeded, bool isLockedOut, bool isNotAllowed)
    {
        IsSucceeded = isSucceeded;
        IsLockedOut = isLockedOut;
        IsNotAllowed = isNotAllowed;
    }

    public bool IsSucceeded { get; }
    public bool IsLockedOut { get; }
    public bool IsNotAllowed { get; }
}