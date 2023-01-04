using Microsoft.EntityFrameworkCore;
using Persistence.Model;

namespace XUnitTesting
{
    public class DatabaseFixure : IDisposable
    {
        private static DbContextOptions<DbTextingGameContext> dbContextOptions = new DbContextOptionsBuilder<DbTextingGameContext>()
       .UseInMemoryDatabase(databaseName: "db_TextingGame")
        .Options;
        public DbTextingGameContext _context;

        public DatabaseFixure()
        {
            _context = new DbTextingGameContext(dbContextOptions);
            _context.Database.EnsureCreated();
            SeeDatabase();
        }
        public void SeeDatabase()
        {
            var user = new List<TblUser>()
            {
                new TblUser(){UserId = 1,UserName = "Anshika",EmailId = "anshika@gmail.com",Password = "anshi12",MobileNo = "7867765564",IsActive = true,CreatedDate = DateTime.Now,UpdatedDate = DateTime.Now},
                new TblUser(){UserId = 2,UserName = "Pavan",EmailId = "pavan@gmail.com",Password = "pavan12",MobileNo = "6523776845",IsActive = true,CreatedDate = DateTime.Now,UpdatedDate = DateTime.Now}
            };
            _context.TblUsers.AddRange(user);
            _context.SaveChanges();
            var user1 = new List<TblRoom>()
            {
                new TblRoom(){RoomId = 1,RoomName = "FunGame",CreatedDate = DateTime.Now,UpdatedDate = DateTime.Now,IsActive = true,CreatedBy = 2,UpdatedBy = 1},
                new TblRoom(){RoomId = 2,RoomName = "TextGame",CreatedDate = DateTime.Now,UpdatedDate= DateTime.Now,IsActive = true,CreatedBy = 2,UpdatedBy = 1}
            };
            _context.TblRooms.AddRange(user1);
            _context.SaveChanges();
            var user2 = new List<TblUserRoom>()
            {
                new TblUserRoom(){UserRoomId=1,UserId=1,RoomId=1,IsActive = true,CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,CreatedBy = 2, UpdatedBy = 1},
                new TblUserRoom(){UserRoomId=2,UserId=2,RoomId=1,IsActive = true,CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,CreatedBy = 2, UpdatedBy = 1}
            };
            _context.TblUserRooms.AddRange(user2);
            _context.SaveChanges();
            var message = new List<TblMessage>()
            {
              new TblMessage(){MessageId = 1,RoomId = 1,UserId = 2,Message = "Hyy",IsActive = true,CreatedDate = DateTime.Now,UpdatedDate = DateTime.Now,CreatedBy = 2,
                UpdatedBy = 1},
               new TblMessage(){MessageId = 2,RoomId = 1,UserId = 2, Message = "Hello",IsActive = true,CreatedDate = DateTime.Now,UpdatedDate = DateTime.Now,CreatedBy = 2,
                UpdatedBy = 2}
            };
            _context.TblMessages.AddRange(message);
            _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
