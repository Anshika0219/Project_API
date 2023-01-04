using System;
using System.Collections.Generic;

namespace Persistence.Model;

public partial class TblUserRoom
{
    public int UserRoomId { get; set; }

    public int? RoomId { get; set; }

    public int? UserId { get; set; }

    public bool IsActive { get; set; }

    public int ImpersonatedUserId { get; set; }

    public double Score { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public int CreatedBy { get; set; }

    public int UpdatedBy { get; set; }

    public virtual TblUser CreatedByNavigation { get; set; } = null!;

    public virtual TblRoom? Room { get; set; }

    public virtual TblUser UpdatedByNavigation { get; set; } = null!;

    public virtual TblUser? User { get; set; }
}
