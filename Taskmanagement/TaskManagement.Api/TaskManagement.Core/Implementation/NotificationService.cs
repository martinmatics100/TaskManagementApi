using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Core.Implementation
{
    public class NotificationService
    {
        public void SendNotification(string userId, string message)
        {
            Console.WriteLine($"Sending notification to User ID: {userId}");
            Console.WriteLine($"Message: {message}");
        }
    }
}
