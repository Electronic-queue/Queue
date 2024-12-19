using System;
using System.Collections.Generic;

namespace Queue.Domain.Entites;

public partial class User
{
    public int UserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Surname { get; set; }

    public string Login { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<Action> Actions { get; set; } = new List<Action>();

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<User> InverseCreatedByNavigation { get; set; } = new List<User>();

    public virtual ICollection<NotificationType> NotificationTypes { get; set; } = new List<NotificationType>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<QueueType> QueueTypes { get; set; } = new List<QueueType>();

    public virtual ICollection<RecordStatus> RecordStatuses { get; set; } = new List<RecordStatus>();

    public virtual ICollection<Record> Records { get; set; } = new List<Record>();

    public virtual ICollection<Resource> Resources { get; set; } = new List<Resource>();

    public virtual ICollection<RoleAccess> RoleAccessGivenByNavigations { get; set; } = new List<RoleAccess>();

    public virtual ICollection<RoleAccess> RoleAccessUsers { get; set; } = new List<RoleAccess>();

    public virtual ICollection<RoleResourceAction> RoleResourceActions { get; set; } = new List<RoleResourceAction>();

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();

    public virtual ICollection<UserService> UserServiceCreatedByNavigations { get; set; } = new List<UserService>();

    public virtual ICollection<UserService> UserServiceUsers { get; set; } = new List<UserService>();

    public virtual ICollection<UserWindow> UserWindowCreatedByNavigations { get; set; } = new List<UserWindow>();

    public virtual ICollection<UserWindow> UserWindowUsers { get; set; } = new List<UserWindow>();

    public virtual ICollection<WindowStatus> WindowStatuses { get; set; } = new List<WindowStatus>();

    public virtual ICollection<Window> Windows { get; set; } = new List<Window>();

}
