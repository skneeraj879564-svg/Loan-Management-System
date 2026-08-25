using Loan_Management_System_Data.Data;
using Loan_Management_System_Data.Models;
using Loan_Management_System_Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Data.Repositories.Implementations
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationDbContext _context;

        public NotificationRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }

        // =========================
        // GET BY ID
        // =========================

        public async Task<Notification?> GetByIdAsync(
            int notificationId)
        {
            return await _context.Notifications
                .FirstOrDefaultAsync(
                    x => x.NotificationId == notificationId);
        }

        // =========================
        // GET ALL
        // =========================

        public async Task<List<Notification>> GetAllAsync()
        {
            return await _context.Notifications
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();
        }

        // =========================
        // GET BY USER ID
        // =========================

        public async Task<List<Notification>>
            GetByUserIdAsync(string userId)
        {
            return await _context.Notifications
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();
        }

        // =========================
        // ADD
        // =========================

        public async Task<Notification> AddAsync(
            Notification notification)
        {
            await _context.Notifications.AddAsync(
                notification);

            await _context.SaveChangesAsync();

            return notification;
        }

        // =========================
        // UPDATE
        // =========================

        public async Task<Notification> UpdateAsync(
            Notification notification)
        {
            _context.Notifications.Update(
                notification);

            await _context.SaveChangesAsync();

            return notification;
        }

        // =========================
        // DELETE
        // =========================

        public async Task<bool> DeleteAsync(
            int notificationId)
        {
            var notification =
                await _context.Notifications
                    .FirstOrDefaultAsync(
                        x => x.NotificationId ==
                             notificationId);

            if (notification == null)
            {
                return false;
            }

            _context.Notifications.Remove(
                notification);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
