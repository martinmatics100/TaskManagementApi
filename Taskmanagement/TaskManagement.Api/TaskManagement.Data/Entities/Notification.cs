using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Data.EntityEnums;

namespace TaskManagement.Data.Entities
{
    public class Notification : BaseEntity
    {
        public string NotificationId { get; set; }
        public NotificationType Type { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string? TaskId { get; set; }
        public CustomTask Task { get; set; }
        public bool IsRead { get; set; }

    }
}
