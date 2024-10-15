namespace HRDCD.Order.Sender;

public interface IMessageSender
{
    Task SendMessageAsync(string orderNumber, string orderName, string orderDescription);
}