namespace HRDCD.Order.Tasks.Handlers;

public abstract class TaskMultipleResult<TResultEntity> : TaskBaseResult
{
    public IList<TResultEntity> Results { get; set; }
    
    public int Total { get; set; }
    
    public int PageNumber { get; set; }
    
    public int PageSize { get; set; }
}