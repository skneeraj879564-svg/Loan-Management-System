using Loan_Management_System_Business.Dtos.Notification;
using Loan_Management_System_Business.Interfaces;
using Loan_Management_System_Data.Models;
using Loan_Management_System_Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Business.Services
{
    public class NotificationService:INotificationService
    {
        private readonly INotificationRepository _repository;

        public NotificationService(
            INotificationRepository repository)
        {
            _repository = repository;
        }

        // =========================
        // GET BY ID
        // =========================

        public async Task<NotificationDto?>
            GetByIdAsync(int notificationId)
        {
            var notification =
                await _repository.GetByIdAsync(
                    notificationId);

            if (notification == null)
            {
                return null;
            }

            return MapToDto(notification);
        }

        // =========================
        // GET ALL
        // =========================

        public async Task<List<NotificationDto>>
            GetAllAsync()
        {
            var notifications =
                await _repository.GetAllAsync();

            return notifications
                .Select(MapToDto)
                .ToList();
        }

        // =========================
        // GET BY USER ID
        // =========================

        public async Task<List<NotificationDto>>
            GetByUserIdAsync(string userId)
        {
            var notifications =
                await _repository.GetByUserIdAsync(userId);

            return notifications
                .Select(MapToDto)
                .ToList();
        }

        // =========================
        // CREATE
        // =========================

        public async Task<NotificationDto>
            CreateAsync(NotificationDto model)
        {
            var notification = new Notification
            {
                UserId = model.UserId,
                Title = model.Title,
                Message = model.Message,
                Type = model.Type,
                IsRead = model.IsRead,
                CreatedDate = model.CreatedDate == default
                    ? DateTime.UtcNow
                    : model.CreatedDate,
                ReadDate = model.ReadDate
            };

            var result =
                await _repository.AddAsync(
                    notification);

            return MapToDto(result);
        }

        // =========================
        // UPDATE
        // =========================

        public async Task<NotificationDto?>
            UpdateAsync(
                int notificationId,
                NotificationDto model)
        {
            var notification =
                await _repository.GetByIdAsync(
                    notificationId);

            if (notification == null)
            {
                return null;
            }

            notification.UserId = model.UserId;
            notification.Title = model.Title;
            notification.Message = model.Message;
            notification.Type = model.Type;
            notification.IsRead = model.IsRead;
            notification.ReadDate = model.ReadDate;

            var result =
                await _repository.UpdateAsync(
                    notification);

            return MapToDto(result);
        }

        // =========================
        // DELETE
        // =========================

        public async Task<bool>
            DeleteAsync(int notificationId)
        {
            return await _repository.DeleteAsync(
                notificationId);
        }

        // =========================
        // MAPPING
        // =========================

        private static NotificationDto
            MapToDto(Notification notification)
        {
            return new NotificationDto
            {
                UserId = notification.UserId,
                Title = notification.Title,
                Message = notification.Message,
                Type = notification.Type,
                IsRead = notification.IsRead,
                CreatedDate = notification.CreatedDate,
                ReadDate = notification.ReadDate
            };
        }
    }
}
