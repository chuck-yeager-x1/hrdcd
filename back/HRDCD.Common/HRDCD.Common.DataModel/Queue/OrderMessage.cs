namespace HRDCD.Common.DataModel.Queue;

public class OrderMessage : IOrderMessage
{
    public string OrderNumber { get; set; }
    public string OrderName { get; set; }
    public string OrderDescription { get; set; }
}