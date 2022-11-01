using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManager.Server.Model;

[Table("OperationLog")]
public class OperationLog
{
    [Key]
    [Column("Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("UserName")]
    [StringLength(128)]
    public string Operator { get; set; } = "";
    
    [Column("CreateTime")]
    public DateTime CreateTime { get; set; } = DateTime.Now;

    [Column("OptionTable")]
    [StringLength(64)]
    public string OptionTable { get; set; } = "";

    [Column("UserEmail")]
    [StringLength(128)]
    public string UserEmail { get; set; } = "";

    [Column("Operation")]
    [StringLength(128)]
    public string Operation { get; set; } = "Update";

    [Column("OldValue")]
    [StringLength(256)]
    public string OldValue { get; set; } = "";

    [Column("NewValue")]
    [StringLength(256)]
    public string NewValue { get; set; } = "";

    [Column("Content")] 
    [StringLength(1024)]
    public string Content { get; set; } = "";

    [Column("WebSite")] 
    [StringLength(64)]
    public string WebSite { get; set; } = "";

    public const string UserTable = "user";

    public const string ShopTable = "shop";

    public const string BoughtTable = "bought";

    public OperationLog Copy()
    {
        return new OperationLog()
        {
            Id = Id,
            Operator = Operator,
            UserEmail = UserEmail,
            OptionTable = OptionTable,
            WebSite = WebSite
        };
    }

}