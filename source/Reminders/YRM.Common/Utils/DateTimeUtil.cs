using YRM.Common.Interfaces.Utils;

namespace YRM.Common.Utils
{
    public class DateTimeUtil : IDateTimeUtil
    {
        public DateTime GetUtcNow() => DateTime.UtcNow;
    }
}
