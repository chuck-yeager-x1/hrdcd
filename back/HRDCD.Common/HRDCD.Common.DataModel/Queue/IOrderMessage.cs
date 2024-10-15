namespace HRDCD.Common.DataModel.Queue;

public interface IOrderMessage
{
    string OrderNumber { get; set; }

    string OrderName { get; set; }
    
    string OrderDescription { get; set; }
}