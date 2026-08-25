using Loan_Management_System_Business.Dtos.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Business.Interfaces
{
    public interface INotificationService
    {
        Task<NotificationDto?> GetByIdAsync(
    int notificationId);

        Task<List<NotificationDto>> GetAllAsync();

        Task<List<NotificationDto>>
            GetByUserIdAsync(
                string userId);

        Task<NotificationDto>
            CreateAsync(
                NotificationDto model);

        Task<NotificationDto?>
            UpdateAsync(
                int notificationId,
                NotificationDto model);

        Task<bool> DeleteAsync(
            int notificationId);
    }
}
