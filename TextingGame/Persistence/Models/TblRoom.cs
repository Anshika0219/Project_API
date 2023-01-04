using System;
using System.Collections.Generic;

namespace Persistence.Models;

public partial class TblRoom
{
    public int RoomId { get; set; }

    public string RoomName { get; set; } = null!;

    public int? NumOfPeopele { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public int CreatedBy { get; set; }

    public int UpdatedBy { get; set; }

    public virtual TblUser CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<TblMessage> TblMessages { get; } = new List<TblMessage>();

    public virtual ICollection<TblUserRoom> TblUserRooms { get; } = new List<TblUserRoom>();

    public virtual TblUser UpdatedByNavigation { get; set; } = null!;
}
