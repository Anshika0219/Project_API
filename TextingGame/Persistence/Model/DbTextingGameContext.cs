using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Model;

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
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<TblMessage>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__tbl_Mess__C87C0C9C88A4489B");

            entity.ToTable("tbl_Messages");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Message).HasColumnType("text");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.TblMessageCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbl_Messa__Creat__7E37BEF6");

            entity.HasOne(d => d.Room).WithMany(p => p.TblMessages)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("FK__tbl_Messa__RoomI__7C4F7684");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.TblMessageUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbl_Messa__Updat__7F2BE32F");

            entity.HasOne(d => d.User).WithMany(p => p.TblMessageUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__tbl_Messa__UserI__7D439ABD");
        });

        modelBuilder.Entity<TblRoom>(entity =>
        {
            entity.HasKey(e => e.RoomId).HasName("PK__tbl_Room__32863939AD1FD51C");

            entity.ToTable("tbl_Rooms");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.RoomName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.TblRoomCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbl_Rooms__Creat__72C60C4A");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.TblRoomUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbl_Rooms__Updat__73BA3083");
        });

        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__tbl_User__1788CC4CCB14C42E");

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
            entity.HasKey(e => e.UserRoomId).HasName("PK__tbl_User__152B95B6F8E3C84C");

            entity.ToTable("tbl_User_Rooms");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.TblUserRoomCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbl_User___Creat__787EE5A0");

            entity.HasOne(d => d.Room).WithMany(p => p.TblUserRooms)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("FK__tbl_User___RoomI__76969D2E");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.TblUserRoomUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbl_User___Updat__797309D9");

            entity.HasOne(d => d.User).WithMany(p => p.TblUserRoomUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__tbl_User___UserI__778AC167");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
