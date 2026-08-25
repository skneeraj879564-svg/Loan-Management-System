using Loan_Management_System_Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Data.Repositories.Interfaces
{
    public interface INotificationRepository
    {
        Task<Notification?> GetByIdAsync(
    int notificationId);

        Task<List<Notification>> GetAllAsync();

        Task<List<Notification>>
            GetByUserIdAsync(
                string userId);

        Task<Notification> AddAsync(
            Notification notification);

        Task<Notification> UpdateAsync(
            Notification notification);

        Task<bool> DeleteAsync(
            int notificationId);
    }
}
