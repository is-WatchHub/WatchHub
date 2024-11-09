namespace UserManagementApplication.Dtos.Outgoing;

public class GetUserResultDto(string id)
{
    public string Id { get; set; } = id;
    public string? Login { get; set; }
    public string? Email { get; set; }
}