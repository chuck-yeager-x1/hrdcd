namespace HRDCD.Order.Tasks.Handlers;

public abstract class TaskSingleResult<TResultEntity> : TaskBaseResult
{
    public TResultEntity Result { get; set; }
}