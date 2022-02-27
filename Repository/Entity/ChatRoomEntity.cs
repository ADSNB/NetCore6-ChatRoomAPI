using Repository.Entity.Interfaces;

namespace Repository.Entity
{
    public class ChatRoomEntity : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}