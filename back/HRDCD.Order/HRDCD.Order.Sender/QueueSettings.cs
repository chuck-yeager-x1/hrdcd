namespace HRDCD.Order.Sender;

/// <summary>
/// Класс для хранения настроек очереди RabbitMQ из файла конфигурации.
/// </summary>
public class QueueSettings
{
    /// <summary>
    /// Возвращает или задает URL-адрес экземпляра RabbitMQ.
    /// </summary>
    public string Hostname { get; set; }
    
    /// <summary>
    /// Возвращает или задает имя пользователя для аутентификации.
    /// </summary>
    public string Username { get; set; }
    
    /// <summary>
    /// Возвращает или задает пароль для аутентификации.
    /// </summary>
    public string Password { get; set; }
    
    /// <summary>
    /// Возвращает или задает имя очереди.
    /// </summary>
    public string QueueName { get; set; }
}