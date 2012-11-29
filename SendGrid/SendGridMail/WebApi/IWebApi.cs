using System;
using System.Collections.Generic;

namespace SendGridMail.WebApi
{
    interface IWebApi : IBounceApi, IBlockApi, IInvalidEmailApi, ISpamReportApi, IUnsubscribesApi
    {
        String user { get; set; }
        String pass { get; set; }

        String GetEmailParse(String hostname, String url);
        void SetEmailParse(String hostname, String url);
        void EditEmailParse(String hostname, String url);
        void DeleteEmailParse(String hostname);
        String GetNotificationUrl();
        void SetNotificationUrl(String url);
        void DeleteNotificationUrl();
        String GetFilter();
        void ActivateFilter(String name);
        void DeactivateFilter(String name);
        void SetupFilter(String user, String password, Dictionary<String, String> args);
        String GetFilterSettings(String name);
        String GetProfile();
        void UpdateProfile(String First_name, String last_name, String address, String city, String state, String country, int zip, int phone, String website);
        void SetUsername(String username);
        void SetPassword(String password, String confpass);
        void SetEmail(String email);
        String GetStats(int days, DateTime start_date, DateTime end_date);
        String GetAggregateStats();
        String GetCategoryStats();
        String GetCategoryStats(String category, int days, DateTime start_date, DateTime end_date);
    }
}
