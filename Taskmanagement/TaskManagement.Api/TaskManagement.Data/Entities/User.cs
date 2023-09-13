using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Data.Entities
{
    public class User : BaseEntity
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<CustomTask> CreatedTasks { get; set; }
        public List<Notification> Notifications { get; set; }
    }
}
