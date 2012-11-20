using System;
using System.Collections.Generic;
using System.Linq;

namespace SendGridMail.WebApi
{
    public interface IBounceApi
    {
        List<Bounce> GetBounces(Boolean includeDate, Int32? days, DateTime? startDate, DateTime? endDate, Int32? limit, Int32? offset, BounceType type, string email);

        void DeleteBounces(DateTime? startDate, DateTime? endDate, BounceType type, string email);
    }
}