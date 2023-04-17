using InitialProject.Serializer;

namespace InitialProject.Domain.Models
{
    public class Notification: ISerializable
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Message { get; set; }
        public bool HasRead { get; set; }

        public Notification() { }

        public Notification(int userId, string message, bool hasRead)
        {
            UserId = userId;
            Message = message;
            HasRead = hasRead;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), UserId.ToString(), Message, HasRead.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            UserId = int.Parse(values[1]);
            Message = values[2];
            HasRead = bool.Parse(values[3]);
        }
    }
}
