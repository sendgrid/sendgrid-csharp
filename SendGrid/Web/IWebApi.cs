using System;
using System.Collections.Generic;

namespace SendGridMail.Web
{
    interface IWebApi
    {
        String user { get; set; }
        String pass { get; set; }

        String GetBounces(int date, String days, DateTime start_date, DateTime end_date, int limit, int offset, int type, String email);
        void DeleteBounces(DateTime start_date, DateTime end_date, String type, String email);
        String GetBlocks(int days, DateTime start_date, DateTime end_date, String email);
        void DeleteBlocks(String email);
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
        void GetInvalidEmails(int date, int days, DateTime start_date, DateTime end_date, int limit, int offset, String email);
        void DeleteInvalidEmails(DateTime start_date, DateTime end_date, String email);
        String CountInvalidEmails(DateTime start_date, DateTime end_date);
        String GetProfile();
        void UpdateProfile(String First_name, String last_name, String address, String city, String state, String country, int zip, int phone, String website);
        void SetUsername(String username);
        void SetPassword(String password, String confpass);
        void SetEmail(String email);
        String GetSpamReports(int date, int days, DateTime start_date, DateTime end_date, int limit, int offset, String email);
        void DeleteSpamReports(DateTime start_date, DateTime end_date, String email);
        String GetStats(int days, DateTime start_date, DateTime end_date);
        String GetAggregateStats();
        String GetCategoryStats();
        String GetCategoryStats(String category, int days, DateTime start_date, DateTime end_date);
        String GetUnsubscribes(int date, int days, DateTime start_date, DateTime end_date, int limit, int offset, String email);
        void DeleteUnsubscribes(DateTime start_date, DateTime end_date, String email);
        void AddUnsubscribes(String email);
    }
}
