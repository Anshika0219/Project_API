using System;
using System.Collections.Generic;

namespace Persistence.Model;

public partial class TblUser
{
    public int UserId { get; set; }

    public string? UserName { get; set; }

    public string EmailId { get; set; } = null!;

    public string? Password { get; set; }

    public string? MobileNo { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public virtual ICollection<TblMessage> TblMessageCreatedByNavigations { get; } = new List<TblMessage>();

    public virtual ICollection<TblMessage> TblMessageUpdatedByNavigations { get; } = new List<TblMessage>();

    public virtual ICollection<TblMessage> TblMessageUsers { get; } = new List<TblMessage>();

    public virtual ICollection<TblRoom> TblRoomCreatedByNavigations { get; } = new List<TblRoom>();

    public virtual ICollection<TblRoom> TblRoomUpdatedByNavigations { get; } = new List<TblRoom>();

    public virtual ICollection<TblUserRoom> TblUserRoomCreatedByNavigations { get; } = new List<TblUserRoom>();

    public virtual ICollection<TblUserRoom> TblUserRoomUpdatedByNavigations { get; } = new List<TblUserRoom>();

    public virtual ICollection<TblUserRoom> TblUserRoomUsers { get; } = new List<TblUserRoom>();
}
