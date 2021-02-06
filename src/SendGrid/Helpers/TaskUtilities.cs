using System;
using System.Threading;
using System.Threading.Tasks;

namespace SendGrid.Helpers
{
    internal static class TaskUtilities
    {
        public static Task Delay(TimeSpan delay, CancellationToken cancellationToken = default)
        {
#if NET40
            return TaskEx.Delay(delay, cancellationToken);
#else
            return Task.Delay(delay, cancellationToken);
#endif
        }
    }
}