using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Models;

public partial class DbTextingGameContext : DbContext
{
    public DbTextingGameContext()
    {
    }

    public DbTextingGameContext(DbContextOptions<DbTextingGameContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblMessage> TblMessages { get; set; }

    public virtual DbSet<TblRoom> TblRooms { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }

    public virtual DbSet<TblUserRoom> TblUserRooms { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server = DESKTOP-UQON0I1; Database=db_TextingGame; Integrated Security=true; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblMessage>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__tbl_Mess__C87C0C9CEC2B6E19");

            entity.ToTable("tbl_Messages");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Message).HasColumnType("text");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.TblMessageCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbl_Messa__Creat__31EC6D26");

            entity.HasOne(d => d.Room).WithMany(p => p.TblMessages)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("FK__tbl_Messa__RoomI__300424B4");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.TblMessageUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbl_Messa__Updat__32E0915F");

            entity.HasOne(d => d.User).WithMany(p => p.TblMessageUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__tbl_Messa__UserI__30F848ED");
        });

        modelBuilder.Entity<TblRoom>(entity =>
        {
            entity.HasKey(e => e.RoomId).HasName("PK__tbl_Room__32863939A59A412D");

            entity.ToTable("tbl_Rooms");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.RoomName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.TblRoomCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbl_Rooms__Creat__267ABA7A");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.TblRoomUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbl_Rooms__Updat__276EDEB3");
        });

        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__tbl_User__1788CC4CF159B501");

            entity.ToTable("tbl_User");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EmailId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MobileNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblUserRoom>(entity =>
        {
            entity.HasKey(e => e.UserRoomId).HasName("PK__tbl_User__152B95B65D55E230");

            entity.ToTable("tbl_User_Rooms");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.TblUserRoomCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbl_User___Creat__2C3393D0");

            entity.HasOne(d => d.Room).WithMany(p => p.TblUserRooms)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("FK__tbl_User___RoomI__2A4B4B5E");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.TblUserRoomUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbl_User___Updat__2D27B809");

            entity.HasOne(d => d.User).WithMany(p => p.TblUserRoomUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__tbl_User___UserI__2B3F6F97");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
