using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace UserManager.Server.Model
{
    public partial class sspanelContext : DbContext
    {
        public sspanelContext()
        {
        }

        public sspanelContext(DbContextOptions<sspanelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AliveIp> AliveIps { get; set; } = null!;
        public virtual DbSet<Announcement> Announcements { get; set; } = null!;
        public virtual DbSet<Auto> Autos { get; set; } = null!;
        public virtual DbSet<Blockip> Blockips { get; set; } = null!;
        public virtual DbSet<Bought> Boughts { get; set; } = null!;
        public virtual DbSet<Code> Codes { get; set; } = null!;
        public virtual DbSet<Coupon> Coupons { get; set; } = null!;
        public virtual DbSet<DetectBanLog> DetectBanLogs { get; set; } = null!;
        public virtual DbSet<DetectList> DetectLists { get; set; } = null!;
        public virtual DbSet<DetectLog> DetectLogs { get; set; } = null!;
        public virtual DbSet<DisconnectIp> DisconnectIps { get; set; } = null!;
        public virtual DbSet<EmailVerify> EmailVerifies { get; set; } = null!;
        public virtual DbSet<Gconfig> Gconfigs { get; set; } = null!;
        public virtual DbSet<Link> Links { get; set; } = null!;
        public virtual DbSet<LoginIp> LoginIps { get; set; } = null!;
        public virtual DbSet<Payback> Paybacks { get; set; } = null!;
        public virtual DbSet<Paylist> Paylists { get; set; } = null!;
        public virtual DbSet<RadiusBan> RadiusBans { get; set; } = null!;
        public virtual DbSet<Relay> Relays { get; set; } = null!;
        public virtual DbSet<Shop> Shops { get; set; } = null!;
        public virtual DbSet<SmsVerify> SmsVerifies { get; set; } = null!;
        public virtual DbSet<Speedtest> Speedtests { get; set; } = null!;
        public virtual DbSet<SsInviteCode> SsInviteCodes { get; set; } = null!;
        public virtual DbSet<SsNode> SsNodes { get; set; } = null!;
        public virtual DbSet<SsNodeInfo> SsNodeInfos { get; set; } = null!;
        public virtual DbSet<SsNodeOnlineLog> SsNodeOnlineLogs { get; set; } = null!;
        public virtual DbSet<SsPasswordReset> SsPasswordResets { get; set; } = null!;
        public virtual DbSet<TelegramSession> TelegramSessions { get; set; } = null!;
        public virtual DbSet<TelegramTask> TelegramTasks { get; set; } = null!;
        public virtual DbSet<Ticket> Tickets { get; set; } = null!;
        public virtual DbSet<Unblockip> Unblockips { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserSubscribeLog> UserSubscribeLogs { get; set; } = null!;
        public virtual DbSet<UserToken> UserTokens { get; set; } = null!;
        public virtual DbSet<UserTrafficLog> UserTrafficLogs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;port=3306;database=sspanel;user=root;password=orz.10089", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.28-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_bin")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<AliveIp>(entity =>
            {
                entity.ToTable("alive_ip");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Datetime).HasColumnName("datetime");

                entity.Property(e => e.Ip)
                    .HasColumnType("text")
                    .HasColumnName("ip");

                entity.Property(e => e.Nodeid).HasColumnName("nodeid");

                entity.Property(e => e.Userid).HasColumnName("userid");
            });

            modelBuilder.Entity<Announcement>(entity =>
            {
                entity.ToTable("announcement");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Content).HasColumnName("content");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.Property(e => e.Markdown).HasColumnName("markdown");
            });

            modelBuilder.Entity<Auto>(entity =>
            {
                entity.ToTable("auto");

                entity.UseCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Datetime).HasColumnName("datetime");

                entity.Property(e => e.Sign).HasColumnName("sign");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.Property(e => e.Value).HasColumnName("value");
            });

            modelBuilder.Entity<Blockip>(entity =>
            {
                entity.ToTable("blockip");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Datetime).HasColumnName("datetime");

                entity.Property(e => e.Ip)
                    .HasColumnType("text")
                    .HasColumnName("ip");

                entity.Property(e => e.Nodeid).HasColumnName("nodeid");
            });

            modelBuilder.Entity<Bought>(entity =>
            {
                entity.ToTable("bought");

                entity.UseCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Coupon)
                    .HasColumnType("text")
                    .HasColumnName("coupon");

                entity.Property(e => e.Datetime).HasColumnName("datetime");

                entity.Property(e => e.Price)
                    .HasPrecision(12, 2)
                    .HasColumnName("price");

                entity.Property(e => e.Renew).HasColumnName("renew");

                entity.Property(e => e.Shopid).HasColumnName("shopid");

                entity.Property(e => e.Userid).HasColumnName("userid");
            });

            modelBuilder.Entity<Code>(entity =>
            {
                entity.ToTable("code");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.Tradeno, "tradeno")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code1)
                    .HasColumnType("text")
                    .HasColumnName("code");

                entity.Property(e => e.Isused).HasColumnName("isused");

                entity.Property(e => e.Number)
                    .HasPrecision(11, 2)
                    .HasColumnName("number");

                entity.Property(e => e.Tradeno).HasColumnName("tradeno");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.Property(e => e.Usedatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("usedatetime");

                entity.Property(e => e.Userid).HasColumnName("userid");
            });

            modelBuilder.Entity<Coupon>(entity =>
            {
                entity.ToTable("coupon");

                entity.UseCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .HasColumnType("text")
                    .HasColumnName("code");

                entity.Property(e => e.Credit).HasColumnName("credit");

                entity.Property(e => e.Expire).HasColumnName("expire");

                entity.Property(e => e.Onetime).HasColumnName("onetime");

                entity.Property(e => e.Shop)
                    .HasColumnType("text")
                    .HasColumnName("shop");
            });

            modelBuilder.Entity<DetectBanLog>(entity =>
            {
                entity.ToTable("detect_ban_log");

                entity.HasComment("审计封禁日志")
                    .UseCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AllDetectNumber)
                    .HasColumnName("all_detect_number")
                    .HasComment("累计违规次数");

                entity.Property(e => e.BanTime)
                    .HasColumnName("ban_time")
                    .HasComment("本次封禁时长");

                entity.Property(e => e.DetectNumber)
                    .HasColumnName("detect_number")
                    .HasComment("本次违规次数");

                entity.Property(e => e.Email)
                    .HasMaxLength(32)
                    .HasColumnName("email")
                    .HasComment("用户邮箱");

                entity.Property(e => e.EndTime)
                    .HasColumnName("end_time")
                    .HasComment("统计结束时间");

                entity.Property(e => e.StartTime)
                    .HasColumnName("start_time")
                    .HasComment("统计开始时间");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasComment("用户 ID");

                entity.Property(e => e.UserName)
                    .HasMaxLength(128)
                    .HasColumnName("user_name")
                    .HasComment("用户名");
            });

            modelBuilder.Entity<DetectList>(entity =>
            {
                entity.ToTable("detect_list");

                entity.UseCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Regex).HasColumnName("regex");

                entity.Property(e => e.Text).HasColumnName("text");

                entity.Property(e => e.Type).HasColumnName("type");
            });

            modelBuilder.Entity<DetectLog>(entity =>
            {
                entity.ToTable("detect_log");

                entity.UseCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Datetime).HasColumnName("datetime");

                entity.Property(e => e.ListId).HasColumnName("list_id");

                entity.Property(e => e.NodeId).HasColumnName("node_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");
            });

            modelBuilder.Entity<DisconnectIp>(entity =>
            {
                entity.ToTable("disconnect_ip");

                entity.UseCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Datetime).HasColumnName("datetime");

                entity.Property(e => e.Ip)
                    .HasColumnType("text")
                    .HasColumnName("ip");

                entity.Property(e => e.Userid).HasColumnName("userid");
            });

            modelBuilder.Entity<EmailVerify>(entity =>
            {
                entity.ToTable("email_verify");

                entity.UseCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .HasColumnType("text")
                    .HasColumnName("code");

                entity.Property(e => e.Email)
                    .HasColumnType("text")
                    .HasColumnName("email");

                entity.Property(e => e.ExpireIn).HasColumnName("expire_in");

                entity.Property(e => e.Ip)
                    .HasColumnType("text")
                    .HasColumnName("ip");
            });

            modelBuilder.Entity<Gconfig>(entity =>
            {
                entity.HasKey(e => e.Key)
                    .HasName("PRIMARY");

                entity.ToTable("gconfig");

                entity.HasComment("网站配置")
                    .UseCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Key)
                    .HasMaxLength(50)
                    .HasColumnName("key")
                    .HasComment("配置名");

                entity.Property(e => e.Comment)
                    .HasColumnType("text")
                    .HasColumnName("comment")
                    .HasComment("配置描述");

                entity.Property(e => e.LastUpdate)
                    .HasColumnName("last_update")
                    .HasComment("修改时间");

                entity.Property(e => e.Name)
                    .HasMaxLength(128)
                    .HasColumnName("name")
                    .HasComment("配置名称");

                entity.Property(e => e.Oldvalue)
                    .HasColumnType("text")
                    .HasColumnName("oldvalue")
                    .HasComment("之前的配置值");

                entity.Property(e => e.OperatorEmail)
                    .HasMaxLength(32)
                    .HasColumnName("operator_email")
                    .HasComment("操作员邮箱");

                entity.Property(e => e.OperatorId)
                    .HasColumnName("operator_id")
                    .HasComment("操作员 ID");

                entity.Property(e => e.OperatorName)
                    .HasMaxLength(128)
                    .HasColumnName("operator_name")
                    .HasComment("操作员名称");

                entity.Property(e => e.Value)
                    .HasColumnType("text")
                    .HasColumnName("value")
                    .HasComment("配置值");
            });

            modelBuilder.Entity<Link>(entity =>
            {
                entity.ToTable("link");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasColumnType("text")
                    .HasColumnName("address");

                entity.Property(e => e.Geo).HasColumnName("geo");

                entity.Property(e => e.Ios).HasColumnName("ios");

                entity.Property(e => e.Isp)
                    .HasColumnType("text")
                    .HasColumnName("isp");

                entity.Property(e => e.Method)
                    .HasColumnType("text")
                    .HasColumnName("method");

                entity.Property(e => e.Port).HasColumnName("port");

                entity.Property(e => e.Token)
                    .HasColumnType("text")
                    .HasColumnName("token");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.Property(e => e.Userid).HasColumnName("userid");
            });

            modelBuilder.Entity<LoginIp>(entity =>
            {
                entity.ToTable("login_ip");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Datetime).HasColumnName("datetime");

                entity.Property(e => e.Ip)
                    .HasColumnType("text")
                    .HasColumnName("ip");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.Property(e => e.Userid).HasColumnName("userid");
            });

            modelBuilder.Entity<Payback>(entity =>
            {
                entity.ToTable("payback");

                entity.UseCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Datetime).HasColumnName("datetime");

                entity.Property(e => e.RefBy).HasColumnName("ref_by");

                entity.Property(e => e.RefGet)
                    .HasPrecision(12, 2)
                    .HasColumnName("ref_get");

                entity.Property(e => e.Total)
                    .HasPrecision(12, 2)
                    .HasColumnName("total");

                entity.Property(e => e.Userid).HasColumnName("userid");
            });

            modelBuilder.Entity<Paylist>(entity =>
            {
                entity.ToTable("paylist");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Autorenew).HasColumnName("autorenew");

                entity.Property(e => e.Datetime).HasColumnName("datetime");

                entity.Property(e => e.Shopid).HasColumnName("shopid");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Total)
                    .HasPrecision(12, 2)
                    .HasColumnName("total");

                entity.Property(e => e.Tradeno)
                    .HasColumnType("text")
                    .HasColumnName("tradeno");

                entity.Property(e => e.Userid).HasColumnName("userid");
            });

            modelBuilder.Entity<RadiusBan>(entity =>
            {
                entity.ToTable("radius_ban");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Userid).HasColumnName("userid");
            });

            modelBuilder.Entity<Relay>(entity =>
            {
                entity.ToTable("relay");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DistIp)
                    .HasColumnType("text")
                    .HasColumnName("dist_ip");

                entity.Property(e => e.DistNodeId).HasColumnName("dist_node_id");

                entity.Property(e => e.Port).HasColumnName("port");

                entity.Property(e => e.Priority).HasColumnName("priority");

                entity.Property(e => e.SourceNodeId).HasColumnName("source_node_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");
            });

            modelBuilder.Entity<Shop>(entity =>
            {
                entity.ToTable("shop");

                entity.UseCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AutoRenew).HasColumnName("auto_renew");

                entity.Property(e => e.AutoResetBandwidth).HasColumnName("auto_reset_bandwidth");

                entity.Property(e => e.Content)
                    .HasColumnType("text")
                    .HasColumnName("content");

                entity.Property(e => e.Name)
                    .HasColumnType("text")
                    .HasColumnName("name");

                entity.Property(e => e.Price)
                    .HasPrecision(12, 2)
                    .HasColumnName("price");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("'1'");
            });

            modelBuilder.Entity<SmsVerify>(entity =>
            {
                entity.ToTable("sms_verify");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.Id, "id")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .HasColumnType("text")
                    .HasColumnName("code");

                entity.Property(e => e.ExpireIn).HasColumnName("expire_in");

                entity.Property(e => e.Ip)
                    .HasColumnType("text")
                    .HasColumnName("ip");

                entity.Property(e => e.Phone).HasColumnName("phone");
            });

            modelBuilder.Entity<Speedtest>(entity =>
            {
                entity.ToTable("speedtest");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cmccdownload)
                    .HasColumnType("text")
                    .HasColumnName("cmccdownload");

                entity.Property(e => e.Cmccping)
                    .HasColumnType("text")
                    .HasColumnName("cmccping");

                entity.Property(e => e.Cmccupload)
                    .HasColumnType("text")
                    .HasColumnName("cmccupload");

                entity.Property(e => e.Datetime).HasColumnName("datetime");

                entity.Property(e => e.Nodeid).HasColumnName("nodeid");

                entity.Property(e => e.Telecomedownload)
                    .HasColumnType("text")
                    .HasColumnName("telecomedownload");

                entity.Property(e => e.Telecomeupload)
                    .HasColumnType("text")
                    .HasColumnName("telecomeupload");

                entity.Property(e => e.Telecomping)
                    .HasColumnType("text")
                    .HasColumnName("telecomping");

                entity.Property(e => e.Unicomdownload)
                    .HasColumnType("text")
                    .HasColumnName("unicomdownload");

                entity.Property(e => e.Unicomping)
                    .HasColumnType("text")
                    .HasColumnName("unicomping");

                entity.Property(e => e.Unicomupload)
                    .HasColumnType("text")
                    .HasColumnName("unicomupload");
            });

            modelBuilder.Entity<SsInviteCode>(entity =>
            {
                entity.ToTable("ss_invite_code");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.UserId, "user_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .HasMaxLength(128)
                    .HasColumnName("code");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("'2016-06-01 00:00:00'");

                entity.Property(e => e.UserId).HasColumnName("user_id");
            });

            modelBuilder.Entity<SsNode>(entity =>
            {
                entity.ToTable("ss_node");

                entity.UseCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BandwidthlimitResetday).HasColumnName("bandwidthlimit_resetday");

                entity.Property(e => e.CustomMethod).HasColumnName("custom_method");

                entity.Property(e => e.CustomRss).HasColumnName("custom_rss");

                entity.Property(e => e.Info)
                    .HasMaxLength(128)
                    .HasColumnName("info")
                    .HasDefaultValueSql("'无描述'");

                entity.Property(e => e.Method)
                    .HasMaxLength(64)
                    .HasColumnName("method");

                entity.Property(e => e.MuOnly)
                    .HasColumnName("mu_only")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Name)
                    .HasMaxLength(128)
                    .HasColumnName("name");

                entity.Property(e => e.NodeBandwidth).HasColumnName("node_bandwidth");

                entity.Property(e => e.NodeBandwidthLimit).HasColumnName("node_bandwidth_limit");

                entity.Property(e => e.NodeClass).HasColumnName("node_class");

                entity.Property(e => e.NodeConnector).HasColumnName("node_connector");

                entity.Property(e => e.NodeGroup).HasColumnName("node_group");

                entity.Property(e => e.NodeHeartbeat)
                    .HasColumnName("node_heartbeat")
                    .HasDefaultValueSql("'4070883661'");

                entity.Property(e => e.NodeIp)
                    .HasColumnType("text")
                    .HasColumnName("node_ip");

                entity.Property(e => e.NodeSpeedlimit)
                    .HasPrecision(12, 2)
                    .HasColumnName("node_speedlimit");

                entity.Property(e => e.Server)
                    .HasMaxLength(256)
                    .HasColumnName("server");

                entity.Property(e => e.Sort).HasColumnName("sort");

                entity.Property(e => e.Status)
                    .HasMaxLength(128)
                    .HasColumnName("status");

                entity.Property(e => e.TrafficRate)
                    .HasColumnName("traffic_rate")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Type).HasColumnName("type");
            });

            modelBuilder.Entity<SsNodeInfo>(entity =>
            {
                entity.ToTable("ss_node_info");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Load)
                    .HasMaxLength(32)
                    .HasColumnName("load");

                entity.Property(e => e.LogTime).HasColumnName("log_time");

                entity.Property(e => e.NodeId).HasColumnName("node_id");

                entity.Property(e => e.Uptime).HasColumnName("uptime");
            });

            modelBuilder.Entity<SsNodeOnlineLog>(entity =>
            {
                entity.ToTable("ss_node_online_log");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.LogTime).HasColumnName("log_time");

                entity.Property(e => e.NodeId).HasColumnName("node_id");

                entity.Property(e => e.OnlineUser).HasColumnName("online_user");
            });

            modelBuilder.Entity<SsPasswordReset>(entity =>
            {
                entity.ToTable("ss_password_reset");

                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasMaxLength(32)
                    .HasColumnName("email");

                entity.Property(e => e.ExpireTime).HasColumnName("expire_time");

                entity.Property(e => e.InitTime).HasColumnName("init_time");

                entity.Property(e => e.Token)
                    .HasMaxLength(128)
                    .HasColumnName("token");
            });

            modelBuilder.Entity<TelegramSession>(entity =>
            {
                entity.ToTable("telegram_session");

                entity.UseCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Datetime).HasColumnName("datetime");

                entity.Property(e => e.SessionContent)
                    .HasColumnType("text")
                    .HasColumnName("session_content");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.Property(e => e.UserId).HasColumnName("user_id");
            });

            modelBuilder.Entity<TelegramTask>(entity =>
            {
                entity.ToTable("telegram_tasks");

                entity.HasComment("Telegram 任务列表")
                    .UseCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Chatid)
                    .HasMaxLength(128)
                    .HasColumnName("chatid")
                    .HasDefaultValueSql("'0'")
                    .HasComment("Telegram Chat ID");

                entity.Property(e => e.Content)
                    .HasColumnType("text")
                    .HasColumnName("content")
                    .HasComment("任务详细内容");

                entity.Property(e => e.Datetime)
                    .HasColumnName("datetime")
                    .HasComment("任务产生时间");

                entity.Property(e => e.Executetime)
                    .HasColumnName("executetime")
                    .HasComment("任务执行时间");

                entity.Property(e => e.Messageid)
                    .HasMaxLength(128)
                    .HasColumnName("messageid")
                    .HasDefaultValueSql("'0'")
                    .HasComment("Telegram Message ID");

                entity.Property(e => e.Process)
                    .HasMaxLength(32)
                    .HasColumnName("process")
                    .HasComment("临时任务进度");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasComment("任务状态");

                entity.Property(e => e.Tguserid)
                    .HasMaxLength(32)
                    .HasColumnName("tguserid")
                    .HasDefaultValueSql("'0'")
                    .HasComment("Telegram User ID");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasComment("任务类型");

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .HasComment("网站用户 ID");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.ToTable("ticket");

                entity.UseCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Content).HasColumnName("content");

                entity.Property(e => e.Datetime).HasColumnName("datetime");

                entity.Property(e => e.Rootid).HasColumnName("rootid");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Title).HasColumnName("title");

                entity.Property(e => e.Userid).HasColumnName("userid");
            });

            modelBuilder.Entity<Unblockip>(entity =>
            {
                entity.ToTable("unblockip");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Datetime).HasColumnName("datetime");

                entity.Property(e => e.Ip)
                    .HasColumnType("text")
                    .HasColumnName("ip");

                entity.Property(e => e.Userid).HasColumnName("userid");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.Email, "email")
                    .IsUnique();

                entity.HasIndex(e => e.Id, "uid");

                entity.HasIndex(e => e.UserName, "user_name");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AllDetectNumber).HasColumnName("all_detect_number");

                entity.Property(e => e.AutoResetBandwidth)
                    .HasPrecision(12, 2)
                    .HasColumnName("auto_reset_bandwidth");

                entity.Property(e => e.AutoResetDay).HasColumnName("auto_reset_day");

                entity.Property(e => e.Class)
                    .HasColumnName("class")
                    .HasDefaultValueSql("'3'");

                entity.Property(e => e.ClassExpire)
                    .HasColumnType("datetime")
                    .HasColumnName("class_expire")
                    .HasDefaultValueSql("'2011-01-01 00:00:00'");

                entity.Property(e => e.D).HasColumnName("d");

                entity.Property(e => e.DetectBan).HasColumnName("detect_ban");

                entity.Property(e => e.DisconnectIp).HasColumnName("disconnect_ip");

                entity.Property(e => e.Discord).HasColumnName("discord");

                entity.Property(e => e.Email)
                    .HasMaxLength(32)
                    .HasColumnName("email");

                entity.Property(e => e.Enable)
                    .HasColumnName("enable")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.ExpireIn)
                    .HasColumnType("datetime")
                    .HasColumnName("expire_in")
                    .HasDefaultValueSql("'2099-06-04 00:05:00'");

                entity.Property(e => e.ExpireTime).HasColumnName("expire_time");

                entity.Property(e => e.ForbiddenIp).HasColumnName("forbidden_ip");

                entity.Property(e => e.ForbiddenPort).HasColumnName("forbidden_port");

                entity.Property(e => e.GaEnable).HasColumnName("ga_enable");

                entity.Property(e => e.GaToken)
                    .HasColumnType("text")
                    .HasColumnName("ga_token");

                entity.Property(e => e.GroupExpire)
                    .HasColumnType("datetime")
                    .HasColumnName("group_expire")
                    .HasDefaultValueSql("'1989-06-04 00:05:00'");

                entity.Property(e => e.ImType)
                    .HasColumnName("im_type")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.ImValue)
                    .HasColumnType("text")
                    .HasColumnName("im_value");

                entity.Property(e => e.InviteNum).HasColumnName("invite_num");

                entity.Property(e => e.IsAdmin).HasColumnName("is_admin");

                entity.Property(e => e.IsEmailVerify).HasColumnName("is_email_verify");

                entity.Property(e => e.IsHide).HasColumnName("is_hide");

                entity.Property(e => e.IsMultiUser).HasColumnName("is_multi_user");

                entity.Property(e => e.Lang)
                    .HasMaxLength(128)
                    .HasColumnName("lang")
                    .HasDefaultValueSql("'zh-cn'")
                    .HasComment("用户的语言");

                entity.Property(e => e.LastCheckInTime).HasColumnName("last_check_in_time");

                entity.Property(e => e.LastDayT).HasColumnName("last_day_t");

                entity.Property(e => e.LastDetectBanTime)
                    .HasColumnType("datetime")
                    .HasColumnName("last_detect_ban_time")
                    .HasDefaultValueSql("'1989-06-04 00:05:00'");

                entity.Property(e => e.LastGetGiftTime).HasColumnName("last_get_gift_time");

                entity.Property(e => e.LastRestPassTime).HasColumnName("last_rest_pass_time");

                entity.Property(e => e.Method)
                    .HasMaxLength(64)
                    .HasColumnName("method")
                    .HasDefaultValueSql("'rc4-md5'");

                entity.Property(e => e.Money)
                    .HasPrecision(12, 2)
                    .HasColumnName("money");

                entity.Property(e => e.NodeConnector).HasColumnName("node_connector");

                entity.Property(e => e.NodeGroup).HasColumnName("node_group");

                entity.Property(e => e.NodeSpeedlimit)
                    .HasPrecision(12, 2)
                    .HasColumnName("node_speedlimit");

                entity.Property(e => e.Obfs)
                    .HasMaxLength(128)
                    .HasColumnName("obfs")
                    .HasDefaultValueSql("'plain'");

                entity.Property(e => e.ObfsParam)
                    .HasMaxLength(128)
                    .HasColumnName("obfs_param");

                entity.Property(e => e.Pac).HasColumnName("pac");

                entity.Property(e => e.Pass)
                    .HasMaxLength(64)
                    .HasColumnName("pass");

                entity.Property(e => e.Passwd)
                    .HasMaxLength(16)
                    .HasColumnName("passwd");

                entity.Property(e => e.Phone).HasColumnName("phone");

                entity.Property(e => e.Plan)
                    .HasMaxLength(2)
                    .HasColumnName("plan")
                    .HasDefaultValueSql("'A'")
                    .UseCollation("utf8mb4_0900_ai_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.Port).HasColumnName("port");

                entity.Property(e => e.Protocol)
                    .HasMaxLength(128)
                    .HasColumnName("protocol")
                    .HasDefaultValueSql("'origin'");

                entity.Property(e => e.ProtocolParam)
                    .HasMaxLength(128)
                    .HasColumnName("protocol_param");

                entity.Property(e => e.RefBy).HasColumnName("ref_by");

                entity.Property(e => e.RegDate)
                    .HasColumnType("datetime")
                    .HasColumnName("reg_date");

                entity.Property(e => e.RegIp)
                    .HasMaxLength(128)
                    .HasColumnName("reg_ip")
                    .HasDefaultValueSql("'127.0.0.1'");

                entity.Property(e => e.Remark)
                    .HasColumnType("text")
                    .HasColumnName("remark");

                entity.Property(e => e.SendDailyMail).HasColumnName("sendDailyMail");

                entity.Property(e => e.Switch)
                    .HasColumnName("switch")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.T).HasColumnName("t");

                entity.Property(e => e.TelegramId).HasColumnName("telegram_id");

                entity.Property(e => e.Theme)
                    .HasColumnType("text")
                    .HasColumnName("theme");

                entity.Property(e => e.TransferEnable).HasColumnName("transfer_enable");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.U).HasColumnName("u");

                entity.Property(e => e.UserName)
                    .HasMaxLength(128)
                    .HasColumnName("user_name")
                    .UseCollation("utf8mb4_0900_ai_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.Uuid)
                    .HasColumnType("text")
                    .HasColumnName("uuid");
            });

            modelBuilder.Entity<UserSubscribeLog>(entity =>
            {
                entity.ToTable("user_subscribe_log");

                entity.HasComment("用户订阅日志")
                    .UseCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasMaxLength(32)
                    .HasColumnName("email")
                    .HasComment("用户邮箱");

                entity.Property(e => e.RequestIp)
                    .HasMaxLength(20)
                    .HasColumnName("request_ip")
                    .HasComment("请求 IP");

                entity.Property(e => e.RequestTime)
                    .HasColumnType("datetime")
                    .HasColumnName("request_time")
                    .HasComment("请求时间");

                entity.Property(e => e.RequestUserAgent)
                    .HasColumnType("text")
                    .HasColumnName("request_user_agent")
                    .HasComment("请求 UA 信息");

                entity.Property(e => e.SubscribeType)
                    .HasMaxLength(20)
                    .HasColumnName("subscribe_type")
                    .HasComment("获取的订阅类型");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasComment("用户 ID");

                entity.Property(e => e.UserName)
                    .HasMaxLength(128)
                    .HasColumnName("user_name")
                    .HasComment("用户名");
            });

            modelBuilder.Entity<UserToken>(entity =>
            {
                entity.ToTable("user_token");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateTime).HasColumnName("create_time");

                entity.Property(e => e.ExpireTime).HasColumnName("expire_time");

                entity.Property(e => e.Token)
                    .HasMaxLength(256)
                    .HasColumnName("token");

                entity.Property(e => e.UserId).HasColumnName("user_id");
            });

            modelBuilder.Entity<UserTrafficLog>(entity =>
            {
                entity.ToTable("user_traffic_log");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.D).HasColumnName("d");

                entity.Property(e => e.LogTime).HasColumnName("log_time");

                entity.Property(e => e.NodeId).HasColumnName("node_id");

                entity.Property(e => e.Rate).HasColumnName("rate");

                entity.Property(e => e.Traffic)
                    .HasMaxLength(32)
                    .HasColumnName("traffic");

                entity.Property(e => e.U).HasColumnName("u");

                entity.Property(e => e.UserId).HasColumnName("user_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
