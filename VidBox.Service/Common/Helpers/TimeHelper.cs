using VidBox.Domain.Constants;

namespace VidBox.Service.Common.Helpers
{
    public class TimeHelper
    {
        public static DateTime GetDateTime()
        {
            var dtTime = DateTime.UtcNow;
            dtTime.AddHours(TimeConstants.UTC);
            return dtTime;
        }
    }
}
