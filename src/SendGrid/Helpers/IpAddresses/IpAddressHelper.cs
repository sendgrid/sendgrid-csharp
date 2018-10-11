using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SendGrid.SendGridClient;

namespace SendGrid.Helpers.IpAddresses
{
    /// <summary>
    /// A helper for getting IP addresses
    /// </summary>
    public class IpAddressHelper
    {
        private readonly ISendGridClient sendGridClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="IpAddressHelper"/> class.
        /// </summary>
        /// <param name="sendGridClient">SendGrid client to access the API</param>
        public IpAddressHelper(ISendGridClient sendGridClient)
        {
            this.sendGridClient = sendGridClient;
        }

        /// <summary>
        /// Gets the list of unassigned IPs on your account (The IPs that do not have a subuser assigned to them)
        /// </summary>
        /// <returns>A list of IP addresses</returns>
        public async Task<IList<string>> GetUnassignedIpsAsync()
        {
            var response = await this.sendGridClient.RequestAsync(
                Method.GET,
                null,
                urlPath: "ips");

            dynamic result = JArray.Parse(await response.Body.ReadAsStringAsync());
            return ((IEnumerable<dynamic>)result)
                .Where(ip => !((IEnumerable<dynamic>)ip.subusers).Any())
                .Select(ip => (string)ip.ip).ToList();
        }
    }
}
