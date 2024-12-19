using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Queue.Domain.Entites;

namespace Queue.Persistence;

public partial class QueuesDbContext : DbContext
{
    public QueuesDbContext()
    {
    }

    public QueuesDbContext(DbContextOptions<QueuesDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Domain.Entites.Action> Actions { get; set; }

    public virtual DbSet<ExceedingsTime> ExceedingsTimes { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<NotificationType> NotificationTypes { get; set; }

    public virtual DbSet<QueueType> QueueTypes { get; set; }

    public virtual DbSet<ReasonsForCancellation> ReasonsForCancellations { get; set; }

    public virtual DbSet<Record> Records { get; set; }

    public virtual DbSet<RecordStatus> RecordStatuses { get; set; }

    public virtual DbSet<Resource> Resources { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoleAccess> RoleAccesses { get; set; }

    public virtual DbSet<RoleResourceAction> RoleResourceActions { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserService> UserServices { get; set; }

    public virtual DbSet<UserWindow> UserWindows { get; set; }

    public virtual DbSet<Window> Windows { get; set; }

    public virtual DbSet<WindowStatus> WindowStatuses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=178.89.186.221,1434;Initial Catalog=aybolat_db;User ID=aybolat_user;Password=F5u!03hl9;MultipleActiveResultSets=True;Application Name=EntityFramework;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("aybolat_user");

        modelBuilder.Entity<Domain.Entites.Action>(entity =>
        {
            entity.HasKey(e => e.ActionId).HasName("PK__Actions__FFE3F4B95C310E07");

            entity.ToTable("Actions", "elec_queue");

            entity.HasIndex(e => e.Name, "UQ__Actions__737584F683EA737E").IsUnique();

            entity.Property(e => e.ActionId).HasColumnName("ActionID");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DescriptionEn).HasColumnName("Description_en");
            entity.Property(e => e.DescriptionKk).HasColumnName("Description_kk");
            entity.Property(e => e.DescriptionRu).HasColumnName("Description_ru");
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Actions)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Actions__Created__73501C2F");
        });

        

        modelBuilder.Entity<ExceedingsTime>(entity =>
        {
            entity.HasKey(e => e.ExceedingsTimeId).HasName("PK__Exceedin__5F7C66B8FC4DCB67");

            entity.ToTable("ExceedingsTimes", "elec_queue");

            entity.Property(e => e.ExceedingsTimeId).HasColumnName("ExceedingsTimeID");
            entity.Property(e => e.CanceledOn).HasColumnType("datetime");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.WindowId).HasColumnName("WindowID");

            entity.HasOne(d => d.Window).WithMany(p => p.ExceedingsTimes)
                .HasForeignKey(d => d.WindowId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Exceeding__Windo__093F5D4E");
        });


        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__20CF2E322B2AED00");

            entity.ToTable("Notifications", "elec_queue");

            entity.HasIndex(e => e.NameKk, "UQ__Notifica__33287F951D00D312").IsUnique();

            entity.HasIndex(e => e.NameRu, "UQ__Notifica__3328B6A4E2095102").IsUnique();

            entity.HasIndex(e => e.NameEn, "UQ__Notifica__332920C2AFCB7758").IsUnique();

            entity.Property(e => e.NotificationId).HasColumnName("NotificationID");
            entity.Property(e => e.ContentEn).HasColumnName("Content_en");
            entity.Property(e => e.ContentKk).HasColumnName("Content_kk");
            entity.Property(e => e.ContentRu).HasColumnName("Content_ru");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NameEn)
                .HasMaxLength(100)
                .HasColumnName("Name_en");
            entity.Property(e => e.NameKk)
                .HasMaxLength(100)
                .HasColumnName("Name_kk");
            entity.Property(e => e.NameRu)
                .HasMaxLength(100)
                .HasColumnName("Name_ru");
            entity.Property(e => e.NotificationTypeId).HasColumnName("NotificationTypeID");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Notificat__Creat__4B0D20AB");

            entity.HasOne(d => d.NotificationType).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.NotificationTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Notificat__Notif__4A18FC72");
        });

        modelBuilder.Entity<NotificationType>(entity =>
        {
            entity.HasKey(e => e.NotificationTypeId).HasName("PK__Notifica__299002A182D0E272");

            entity.ToTable("NotificationTypes", "elec_queue");

            entity.HasIndex(e => e.NameKk, "UQ__Notifica__33287F957D065CD8").IsUnique();

            entity.HasIndex(e => e.NameRu, "UQ__Notifica__3328B6A4B0974BAA").IsUnique();

            entity.HasIndex(e => e.NameEn, "UQ__Notifica__332920C2208CA233").IsUnique();

            entity.Property(e => e.NotificationTypeId).HasColumnName("NotificationTypeID");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DescriptionEn).HasColumnName("Description_en");
            entity.Property(e => e.DescriptionKk).HasColumnName("Description_kk");
            entity.Property(e => e.DescriptionRu).HasColumnName("Description_ru");
            entity.Property(e => e.NameEn)
                .HasMaxLength(100)
                .HasColumnName("Name_en");
            entity.Property(e => e.NameKk)
                .HasMaxLength(100)
                .HasColumnName("Name_kk");
            entity.Property(e => e.NameRu)
                .HasMaxLength(100)
                .HasColumnName("Name_ru");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.NotificationTypes)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Notificat__Creat__436BFEE3");
        });

        modelBuilder.Entity<QueueType>(entity =>
        {
            entity.HasKey(e => e.QueueTypeId).HasName("PK__QueueTyp__D612E5876DA96B94");

            entity.ToTable("QueueTypes", "elec_queue");

            entity.HasIndex(e => e.NameKk, "UQ__QueueTyp__33287F95178FDE75").IsUnique();

            entity.HasIndex(e => e.NameRu, "UQ__QueueTyp__3328B6A479912E5F").IsUnique();

            entity.HasIndex(e => e.NameEn, "UQ__QueueTyp__332920C2CB8BE7B3").IsUnique();

            entity.Property(e => e.QueueTypeId).HasColumnName("QueueTypeID");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DescriptionEn).HasColumnName("Description_en");
            entity.Property(e => e.DescriptionKk).HasColumnName("Description_kk");
            entity.Property(e => e.DescriptionRu).HasColumnName("Description_ru");
            entity.Property(e => e.NameEn)
                .HasMaxLength(100)
                .HasColumnName("Name_en");
            entity.Property(e => e.NameKk)
                .HasMaxLength(100)
                .HasColumnName("Name_kk");
            entity.Property(e => e.NameRu)
                .HasMaxLength(100)
                .HasColumnName("Name_ru");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.QueueTypes)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__QueueType__Creat__1699586C");
        });

        modelBuilder.Entity<ReasonsForCancellation>(entity =>
        {
            entity.HasKey(e => e.ReasonId).HasName("PK__ReasonsF__A4F8C0C73EDEF346");

            entity.ToTable("ReasonsForCancellation", "elec_queue");

            entity.Property(e => e.ReasonId).HasColumnName("ReasonID");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.RecordId).HasColumnName("RecordID");

            entity.HasOne(d => d.Record).WithMany(p => p.ReasonsForCancellations)
                .HasForeignKey(d => d.RecordId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ReasonsFo__Recor__38EE7070");
        });

        modelBuilder.Entity<Record>(entity =>
        {
            entity.HasKey(e => e.RecordId).HasName("PK__Records__FBDF78C9AB7EFB4C");

            entity.ToTable("Records", "elec_queue");

            entity.Property(e => e.RecordId).HasColumnName("RecordID");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.Iin)
                .HasMaxLength(12)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("IIN");
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.RecordStatusId).HasColumnName("RecordStatusID");
            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");
            entity.Property(e => e.StartTime).HasColumnType("datetime");
            entity.Property(e => e.Surname).HasMaxLength(100);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Records)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Records__Created__351DDF8C");

            entity.HasOne(d => d.RecordStatus).WithMany(p => p.Records)
                .HasForeignKey(d => d.RecordStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Records__RecordS__3335971A");

            entity.HasOne(d => d.Service).WithMany(p => p.Records)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Records__Service__3429BB53");
        });

        modelBuilder.Entity<RecordStatus>(entity =>
        {
            entity.HasKey(e => e.RecordStatusId).HasName("PK__RecordSt__964FE144F3902A70");

            entity.ToTable("RecordStatuses", "elec_queue");

            entity.HasIndex(e => e.NameKk, "UQ__RecordSt__33287F95699986F4").IsUnique();

            entity.HasIndex(e => e.NameRu, "UQ__RecordSt__3328B6A41FBEFB0C").IsUnique();

            entity.HasIndex(e => e.NameEn, "UQ__RecordSt__332920C28F74EBD5").IsUnique();

            entity.Property(e => e.RecordStatusId).HasColumnName("RecordStatusID");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DescriptionEn).HasColumnName("Description_en");
            entity.Property(e => e.DescriptionKk).HasColumnName("Description_kk");
            entity.Property(e => e.DescriptionRu).HasColumnName("Description_ru");
            entity.Property(e => e.NameEn)
                .HasMaxLength(100)
                .HasColumnName("Name_en");
            entity.Property(e => e.NameKk)
                .HasMaxLength(100)
                .HasColumnName("Name_kk");
            entity.Property(e => e.NameRu)
                .HasMaxLength(100)
                .HasColumnName("Name_ru");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.RecordStatuses)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__RecordSta__Creat__2D7CBDC4");
        });

        modelBuilder.Entity<Resource>(entity =>
        {
            entity.HasKey(e => e.ResourceId).HasName("PK__Resource__4ED1814F2837EB68");

            entity.ToTable("Resources", "elec_queue");

            entity.HasIndex(e => e.Name, "UQ__Resource__737584F64F03E5E3").IsUnique();

            entity.Property(e => e.ResourceId).HasColumnName("ResourceID");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DescriptionEn).HasColumnName("Description_en");
            entity.Property(e => e.DescriptionKk).HasColumnName("Description_kk");
            entity.Property(e => e.DescriptionRu).HasColumnName("Description_ru");
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Resources)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Resources__Creat__6E8B6712");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Reviews__74BC79AEE78378A8");

            entity.ToTable("Reviews", "elec_queue");

            entity.Property(e => e.ReviewId).HasColumnName("ReviewID");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.RecordId).HasColumnName("RecordID");

            entity.HasOne(d => d.Record).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.RecordId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reviews__RecordI__3CBF0154");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE3AAA788C23");

            entity.ToTable("Roles", "elec_queue");

            entity.HasIndex(e => e.NameKk, "UQ__Roles__33287F95C4ACE5D5").IsUnique();

            entity.HasIndex(e => e.NameRu, "UQ__Roles__3328B6A4BA0FE2C3").IsUnique();

            entity.HasIndex(e => e.NameEn, "UQ__Roles__332920C24B0D5AEA").IsUnique();

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DescriptionEn).HasColumnName("Description_en");
            entity.Property(e => e.DescriptionKk).HasColumnName("Description_kk");
            entity.Property(e => e.DescriptionRu).HasColumnName("Description_ru");
            entity.Property(e => e.NameEn)
                .HasMaxLength(100)
                .HasColumnName("Name_en");
            entity.Property(e => e.NameKk)
                .HasMaxLength(100)
                .HasColumnName("Name_kk");
            entity.Property(e => e.NameRu)
                .HasMaxLength(100)
                .HasColumnName("Name_ru");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Roles)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Roles__CreatedBy__640DD89F");
        });

        modelBuilder.Entity<RoleAccess>(entity =>
        {
            entity.HasKey(e => e.RoleAccessId).HasName("PK__RoleAcce__C1244FB463327F39");

            entity.ToTable("RoleAccesses", "elec_queue");

            entity.Property(e => e.RoleAccessId).HasColumnName("RoleAccessID");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DescriptionEn).HasColumnName("Description_en");
            entity.Property(e => e.DescriptionKk).HasColumnName("Description_kk");
            entity.Property(e => e.DescriptionRu).HasColumnName("Description_ru");
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.GivenByNavigation).WithMany(p => p.RoleAccessGivenByNavigations)
                .HasForeignKey(d => d.GivenBy)
                .HasConstraintName("FK__RoleAcces__Given__69C6B1F5");

            entity.HasOne(d => d.Role).WithMany(p => p.RoleAccesses)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RoleAcces__RoleI__68D28DBC");

            entity.HasOne(d => d.User).WithMany(p => p.RoleAccessUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RoleAcces__UserI__67DE6983");
        });

        modelBuilder.Entity<RoleResourceAction>(entity =>
        {
            entity.HasKey(e => e.RoleResourceActionId).HasName("PK__RoleReso__53EA627AA4821A9C");

            entity.ToTable("RoleResourceActions", "elec_queue");

            entity.Property(e => e.RoleResourceActionId).HasColumnName("RoleResourceActionID");
            entity.Property(e => e.ActionId).HasColumnName("ActionID");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DescriptionEn).HasColumnName("Description_en");
            entity.Property(e => e.DescriptionKk).HasColumnName("Description_kk");
            entity.Property(e => e.DescriptionRu).HasColumnName("Description_ru");
            entity.Property(e => e.ResourceId).HasColumnName("ResourceID");
            entity.Property(e => e.RoleId).HasColumnName("RoleID");

            entity.HasOne(d => d.Action).WithMany(p => p.RoleResourceActions)
                .HasForeignKey(d => d.ActionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RoleResou__Actio__7908F585");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.RoleResourceActions)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__RoleResou__Creat__79FD19BE");

            entity.HasOne(d => d.Resource).WithMany(p => p.RoleResourceActions)
                .HasForeignKey(d => d.ResourceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RoleResou__Resou__7814D14C");

            entity.HasOne(d => d.Role).WithMany(p => p.RoleResourceActions)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RoleResou__RoleI__7720AD13");
        });


        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__Services__C51BB0EA86E9D61C");

            entity.ToTable("Services", "elec_queue");

            entity.HasIndex(e => e.NameKk, "UQ__Services__33287F95951886C1").IsUnique();

            entity.HasIndex(e => e.NameRu, "UQ__Services__3328B6A40B4542B8").IsUnique();

            entity.HasIndex(e => e.NameEn, "UQ__Services__332920C23E0530A8").IsUnique();

            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DescriptionEn).HasColumnName("Description_en");
            entity.Property(e => e.DescriptionKk).HasColumnName("Description_kk");
            entity.Property(e => e.DescriptionRu).HasColumnName("Description_ru");
            entity.Property(e => e.NameEn)
                .HasMaxLength(100)
                .HasColumnName("Name_en");
            entity.Property(e => e.NameKk)
                .HasMaxLength(100)
                .HasColumnName("Name_kk");
            entity.Property(e => e.NameRu)
                .HasMaxLength(100)
                .HasColumnName("Name_ru");
            entity.Property(e => e.ParentserviceId).HasColumnName("ParentserviceID");
            entity.Property(e => e.QueueTypeId).HasColumnName("QueueTypeID");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Services)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Services__Create__1F2E9E6D");

            entity.HasOne(d => d.Parentservice).WithMany(p => p.InverseParentservice)
                .HasForeignKey(d => d.ParentserviceId)
                .HasConstraintName("FK__Services__Parent__1D4655FB");

            entity.HasOne(d => d.QueueType).WithMany(p => p.Services)
                .HasForeignKey(d => d.QueueTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Services__QueueT__1E3A7A34");
        });


        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC25F45487");

            entity.ToTable("Users", "elec_queue");

            entity.HasIndex(e => e.Login, "UQ__Users__5E55825B7C5DEA66").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.Login).HasMaxLength(100);
            entity.Property(e => e.Surname).HasMaxLength(100);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.InverseCreatedByNavigation)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Users__CreatedBy__51EF2864");
        });

        modelBuilder.Entity<UserService>(entity =>
        {
            entity.HasKey(e => e.UserServiceId).HasName("PK__UserServ__C737CAF91D933888");

            entity.ToTable("UserServices", "elec_queue");

            entity.Property(e => e.UserServiceId).HasColumnName("UserServiceID");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DescriptionEn).HasColumnName("Description_en");
            entity.Property(e => e.DescriptionKk).HasColumnName("Description_kk");
            entity.Property(e => e.DescriptionRu).HasColumnName("Description_ru");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.UserServiceCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__UserServi__Creat__25DB9BFC");

            entity.HasOne(d => d.Service).WithMany(p => p.UserServices)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserServi__Servi__24E777C3");

            entity.HasOne(d => d.User).WithMany(p => p.UserServiceUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserServi__UserI__23F3538A");
        });

        modelBuilder.Entity<UserWindow>(entity =>
        {
            entity.HasKey(e => e.UserWindowId).HasName("PK__UserWind__F78D89B32F140CEF");

            entity.ToTable("UserWindows", "elec_queue");

            entity.Property(e => e.UserWindowId).HasColumnName("UserWindowID");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.StartTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.WindowId).HasColumnName("WindowID");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.UserWindowCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__UserWindo__Creat__0FEC5ADD");

            entity.HasOne(d => d.User).WithMany(p => p.UserWindowUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserWindo__UserI__0E04126B");

            entity.HasOne(d => d.Window).WithMany(p => p.UserWindows)
                .HasForeignKey(d => d.WindowId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserWindo__Windo__0EF836A4");
        });

        modelBuilder.Entity<Window>(entity =>
        {
            entity.HasKey(e => e.WindowId).HasName("PK__Windows__1EEC640907AAF250");

            entity.ToTable("Windows", "elec_queue");

            entity.Property(e => e.WindowId).HasColumnName("WindowID");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.WindowStatusId).HasColumnName("WindowStatusID");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Windows)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Windows__Created__056ECC6A");

            entity.HasOne(d => d.WindowStatus).WithMany(p => p.Windows)
                .HasForeignKey(d => d.WindowStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Windows__WindowS__047AA831");
        });

        modelBuilder.Entity<WindowStatus>(entity =>
        {
            entity.HasKey(e => e.WindowStatusId).HasName("PK__WindowSt__830B9B2E077133F8");

            entity.ToTable("WindowStatuses", "elec_queue");

            entity.HasIndex(e => e.NameKk, "UQ__WindowSt__33287F95AFB185A9").IsUnique();

            entity.HasIndex(e => e.NameRu, "UQ__WindowSt__3328B6A483FADC98").IsUnique();

            entity.HasIndex(e => e.NameEn, "UQ__WindowSt__332920C21C9A84E2").IsUnique();

            entity.Property(e => e.WindowStatusId).HasColumnName("WindowStatusID");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DescriptionEn).HasColumnName("Description_en");
            entity.Property(e => e.DescriptionKk).HasColumnName("Description_kk");
            entity.Property(e => e.DescriptionRu).HasColumnName("Description_ru");
            entity.Property(e => e.NameEn)
                .HasMaxLength(100)
                .HasColumnName("Name_en");
            entity.Property(e => e.NameKk)
                .HasMaxLength(100)
                .HasColumnName("Name_kk");
            entity.Property(e => e.NameRu)
                .HasMaxLength(100)
                .HasColumnName("Name_ru");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.WindowStatuses)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__WindowSta__Creat__00AA174D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
