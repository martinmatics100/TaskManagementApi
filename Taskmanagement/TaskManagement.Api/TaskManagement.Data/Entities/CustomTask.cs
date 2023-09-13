using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Data.EntityEnums;

namespace TaskManagement.Data.Entities
{
    public class CustomTask : BaseEntity
    {
        public string CustomTaskId { get; set; }
        public string Title { get; set; } 
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; }
        public string? ProjectId { get; set; }
        public Project Project { get; set; }
        public string CreatedByUserId { get; set; }
        public User CreatedByUser { get; set; }
        public List<Notification> Notifications { get; set; }
    }
}
