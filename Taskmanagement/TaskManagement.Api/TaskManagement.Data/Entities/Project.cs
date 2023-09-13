using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Data.Entities
{
    public class Project : BaseEntity
    {
        public string? ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<CustomTask> Task { get; set; }
    }
}
