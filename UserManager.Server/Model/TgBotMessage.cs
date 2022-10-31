namespace UserManager.Server.Model;

public class TgBotMessage
{
    public string WebSite { get; set; } = "";

    public string Operator { get; set; } = "";

    public string Operation { get; set; } = "";

    public string UserEmail { get; set; } = "";

    public string Content { get; set; } = "";

    public override string ToString()
    {
        return $"网站: {WebSite}\n操作人: {Operator}\n用户: {UserEmail}\n操作: {Operation}\n{Content}";
    }

    public static TgBotMessage FromOperationLog(OperationLog log)
    {
        return new TgBotMessage()
        {
            WebSite = log.WebSite,
            Operator = log.Operator,
            Operation = log.Operation,
            UserEmail = log.UserEmail,
            Content = log.Content
        };
    }
    
}