namespace HRDCD.Common.DataModel.Queue;

/// <inheritdoc cref="IOrderMessage"/>
public class OrderMessage : IOrderMessage
{
    /// <inheritdoc cref="IOrderMessage.OrderNumber"/>
    public string OrderNumber { get; set; }
    
    /// <inheritdoc cref="IOrderMessage.OrderName"/>
    public string OrderName { get; set; }
    
    /// <inheritdoc cref="IOrderMessage.OrderDescription"/>
    public string OrderDescription { get; set; }
}