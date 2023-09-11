using VidBox.Service.Dtos.Notification;

namespace VidBox.Service.Interfaces;

public interface ISmsSender
{
    public Task<bool> SendAsync(SmsMessage smsMessage);
}
