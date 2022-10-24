namespace UserManager.Shared.Response;

public class BaseResult
{
    public bool IsSuccess { get; set; } = false;
    
    public string? Message { get; set; }
}