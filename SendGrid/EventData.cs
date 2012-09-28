using System;

namespace SendGrid
{
    public class EventData
    {

        /// <summary>
        /// Applies to all message types.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Applies to all message types.
        /// </summary>
        public string Event { get; set; }

        /// <summary>
        /// Applies to all message types.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Applies to Deferred and Delivered events.
        /// </summary>
        public string Response { get; set; }

        /// <summary>
        /// Applies to the Deferred event.
        /// </summary>
        public int Attempt { get; set; }

        /// <summary>
        /// Applies to the Bounce and Drop events
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// Applies to the Click event.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 3 digit status code. Applies to the Bounce event.
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Applies to the Bounce event.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int64 TimeStamp { get; set; }

    }
}
