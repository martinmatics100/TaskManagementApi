using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Data.EntityEnums;

namespace TaskManagement.Core.Dto
{
    public class NotificationDto
    {
       
        public NotificationType Type { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }

        public string UserId { get; set; }

        public string? TaskId { get; set; }
    }
}
