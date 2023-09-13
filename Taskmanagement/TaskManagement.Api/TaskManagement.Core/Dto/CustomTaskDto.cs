using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Data.EntityEnums;

namespace TaskManagement.Core.Dto
{
    public class CustomTaskDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; }
        public string ProjectId { get; set; }
        public string CreatedByUserId { get; set; }
    }
}
