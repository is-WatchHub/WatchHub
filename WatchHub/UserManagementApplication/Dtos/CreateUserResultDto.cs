namespace UserManagementApplication.Dtos.Results;

public class CreateUserResultDto(bool isSuccess, IEnumerable<string> errors)
{
    public bool IsSuccess { get; init; } = isSuccess;
    public IEnumerable<string> Errors { get; } = errors;
}