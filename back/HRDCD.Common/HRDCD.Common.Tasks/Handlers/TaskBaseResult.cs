namespace HRDCD.Common.Tasks.Handlers;

public abstract class TaskBaseResult
{
    public string ErrorMessage { get; set; }
    
    public bool IsSuccess { get; set; }
}