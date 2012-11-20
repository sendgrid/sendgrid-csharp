using System;
using System.Collections.Generic;

namespace SendGridMail.WebApi
{
    public interface IBlockApi
    {
        String GetBlocks(Int32? days, DateTime? startDate, DateTime? endDate, String email);

        void DeleteBlocks(String email);
    }
}
