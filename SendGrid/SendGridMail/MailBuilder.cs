using Smtpapi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace SendGridMail {
    public class MailBuilder {
        private SendGrid sendgrid;
        private bool? enableFlag;

        public static MailBuilder Create() {
            var mailBuilder = new MailBuilder();
            mailBuilder.sendgrid = SendGrid.GetInstance();
            return new MailBuilder();
        }
        public SendGrid Build() {
            if (string.IsNullOrEmpty(sendgrid.Html) && string.IsNullOrEmpty(sendgrid.Text)) {
                throw new InvalidOperationException("Mail does not contain a body.");
            }
            if (sendgrid.To.Length == 0) { throw new InvalidOperationException("Mail does not have any recipients."); }
            if (sendgrid.From == null) { throw new InvalidOperationException("Mail does not have a valid sender's email address."); }
            if (string.IsNullOrEmpty(sendgrid.Subject)) { throw new InvalidOperationException("Mail does not have a subject."); }
            return sendgrid;
        }

        public MailBuilder Enable {
            get {
                enableFlag = true;
                return this;
            }
        }
        public MailBuilder Disable {
            get {
                enableFlag = false;
                return this;
            }
        }

        public MailBuilder From(MailAddress address) {
            sendgrid.From = address;
            return this;
        }
        public MailBuilder From(string email) {
            sendgrid.From = new MailAddress(email);
            return this;
        }
        public MailBuilder From(string email, string displayName) {
            sendgrid.From = new MailAddress(email, displayName);
            return this;
        }
        public MailBuilder To(MailAddress address) {
            return this.To(address.Address, address.DisplayName);
        }
        public MailBuilder To(string email) {
            sendgrid.AddTo(email);
            return this;
        }
        public MailBuilder To(string email, string displayName) {
            sendgrid.AddTo(string.Format("{0} <{1}>", displayName, email));
            return this;
        }
        public MailBuilder Cc(MailAddress address) {
            return this.Cc(address.Address, address.DisplayName);
        }
        public MailBuilder Cc(string email) {
            sendgrid.AddCc(email);
            return this;
        }
        public MailBuilder Cc(string email, string displayName) {
            sendgrid.AddCc(string.Format("{0} <{1}>", displayName, email));
            return this;
        }
        public MailBuilder Bcc(MailAddress address) {
            return this.Bcc(address.Address, address.DisplayName);
        }
        public MailBuilder Bcc(string email, string displayName) {
            sendgrid.AddBcc(string.Format("{0} <{1}>", displayName, email));
            return this;
        }

        public MailBuilder Subject(string subject) {
            sendgrid.Subject = subject;
            return this;
        }
        public MailBuilder Html(string html) {
            sendgrid.Html = html;
            return this;
        }
        public MailBuilder Text(string text) {
            sendgrid.Text = text;
            return this;
        }
        
        public MailBuilder WithSubstitution(string replacementTag, List<string> substitutionValues) {
            sendgrid.AddSubstitution(replacementTag, substitutionValues);
            return this;
        }
        public MailBuilder WithUniqueArgs(IDictionary<string, string> identifiers) {
            sendgrid.AddUniqueArgs(identifiers);
            return this;
        }
        public MailBuilder WithCategory(string category) {
            sendgrid.SetCategory(category);
            return this;
        }
        public MailBuilder WithCategories(IEnumerable<string> categories) {
            sendgrid.SetCategories(categories);
            return this;
        }
        public MailBuilder WithAttachment(Stream stream, string name) {
            sendgrid.AddAttachment(stream, name);
            return this;
        }
        public MailBuilder WithAttachment(string filePath) {
            sendgrid.AddAttachment(filePath);
            return this;
        }
        public MailBuilder WithHeaders(IDictionary<string, string> headers) {
            sendgrid.AddHeaders(headers);
            return this;
        }

        /// <summary>
        /// Preface with Enable or Disable to set the status of the Gravatar filter
        /// </summary>
        /// <returns></returns>
        public MailBuilder Gravatar() {
            if (!enableFlag.HasValue) { throw new InvalidOperationException("Builder requires 'Enable' or 'Disable' before this method."); }
            if (enableFlag.Value) { sendgrid.EnableGravatar(); }
            else { sendgrid.DisableGravatar(); }
            enableFlag = null;
            return this;
        }
 
        /// <summary>
        /// Preface with Enable or Disable to set the status of the Open Tracking filter
        /// </summary>
        /// <returns></returns>
        public MailBuilder OpenTracking() {
            if (!enableFlag.HasValue) { throw new InvalidOperationException("Builder requires 'Enable' or 'Disable' before this method."); }
            if (enableFlag.Value) { sendgrid.EnableOpenTracking(); }
            else { sendgrid.DisableOpenTracking(); }
            enableFlag = null;
            return this;
        }

        /// <summary>
        /// Preface with Enable or Disable to set the status of the Click Tracking filter
        /// </summary>
        /// <param name="includePlainText"></param>
        /// <returns></returns>
        public MailBuilder ClickTracking(bool includePlainText = false) {
            if (!enableFlag.HasValue) { throw new InvalidOperationException("Builder requires 'Enable' or 'Disable' before this method."); }
            if (enableFlag.Value) { sendgrid.EnableClickTracking(includePlainText); }
            else { sendgrid.DisableClickTracking(); }
            enableFlag = null;
            return this;
        }

        /// <summary>
        /// Preface with Enable or Disable to set the status of the Spam Checking filter
        /// </summary>
        /// <param name="score"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public MailBuilder SpamCheck(int score = 5, string url = null) {
            if (!enableFlag.HasValue) { throw new InvalidOperationException("Builder requires 'Enable' or 'Disable' before this method."); }
            if (enableFlag.Value) { sendgrid.EnableSpamCheck(score, url); }
            else { sendgrid.DisableSpamCheck(); }
            enableFlag = null;
            return this;
        }

        /// <summary>
        /// When prefaced with Disable, turns off the Unsubscribe Tracking filter
        /// </summary>
        /// <returns></returns>
        public MailBuilder Unsubscribe() {
            if (!enableFlag.HasValue) { throw new InvalidOperationException("Builder requires 'Enable' or 'Disable' before this method."); }
            if (enableFlag.Value) { throw new ArgumentException("Arguments need to be specified to enable this item."); }
            else { sendgrid.DisableUnsubscribe(); }
            enableFlag = null;
            return this;
        }

        /// <summary>
        /// When prefaced with Enable, turns on the Unsubscribe Tracking filter
        /// </summary>
        /// <param name="text"></param>
        /// <param name="html"></param>
        /// <returns></returns>
        public MailBuilder Unsubscribe(string text, string html) {
            if (!enableFlag.HasValue) { throw new InvalidOperationException("Builder requires 'Enable' or 'Disable' before this method."); }
            if (enableFlag.Value) { sendgrid.EnableUnsubscribe(text, html); }
            else { sendgrid.DisableUnsubscribe(); }
            enableFlag = null;
            return this;
        }

        /// <summary>
        /// When prefaced with Enable, turns on the Unsubscribe Tracking filter
        /// </summary>
        /// <param name="replace"></param>
        /// <returns></returns>
        public MailBuilder Unsubscribe(string replace) {
            if (!enableFlag.HasValue) { throw new InvalidOperationException("Builder requires 'Enable' or 'Disable' before this method."); }
            if (enableFlag.Value) { sendgrid.EnableUnsubscribe(replace); }
            else { sendgrid.DisableUnsubscribe(); }
            enableFlag = null;
            return this;
        }

        /// <summary>
        /// Preface with Enable or Disable to set the status of the Footer filter
        /// </summary>
        /// <param name="text"></param>
        /// <param name="html"></param>
        /// <returns></returns>
        public MailBuilder Footer(string text = null, string html = null) {
            if (!enableFlag.HasValue) { throw new InvalidOperationException("Builder requires 'Enable' or 'Disable' before this method."); }
            if (enableFlag.Value) { sendgrid.EnableFooter(text, html); }
            else { sendgrid.DisableFooter(); }
            enableFlag = null;
            return this;
        }

        /// <summary>
        /// When prefaced with Disable, turns off the Google Analytics filter
        /// </summary>
        /// <returns></returns>
        public MailBuilder GoogleAnalytics() {
            if (!enableFlag.HasValue) { throw new InvalidOperationException("Builder requires 'Enable' or 'Disable' before this method."); }
            if (enableFlag.Value) { throw new ArgumentException("Arguments need to be specified to enable this item."); }
            else { sendgrid.DisableGoogleAnalytics(); }
            enableFlag = null;
            return this;
        }

        /// <summary>
        /// When prefaced with Enable, turns on the Google Analytics filter
        /// </summary>
        /// <param name="source"></param>
        /// <param name="medium"></param>
        /// <param name="term"></param>
        /// <param name="content"></param>
        /// <param name="campaign"></param>
        /// <returns></returns>
        public MailBuilder GoogleAnalytics(string source, string medium, string term, string content = null, string campaign = null) {
            if (!enableFlag.HasValue) { throw new InvalidOperationException("Builder requires 'Enable' or 'Disable' before this method."); }
            if (enableFlag.Value) { sendgrid.EnableGoogleAnalytics(source, medium, term, content, campaign); }
            else { sendgrid.DisableGoogleAnalytics(); }
            enableFlag = null;
            return this;
        }

        /// <summary>
        /// When prefaced with Disable, turns off the Template filter
        /// </summary>
        /// <returns></returns>
        public MailBuilder Template() {
            if (!enableFlag.HasValue) { throw new InvalidOperationException("Builder requires 'Enable' or 'Disable' before this method."); }
            if (enableFlag.Value) { throw new ArgumentException("Arguments need to be specified to enable this item."); }
            else { sendgrid.DisableTemplate(); }
            enableFlag = null;
            return this;
        }

        /// <summary>
        /// When prefaced with Enable, turns on the Template filter
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public MailBuilder Template(string html) {
            if (!enableFlag.HasValue) { throw new InvalidOperationException("Builder requires 'Enable' or 'Disable' before this method."); }
            if (enableFlag.Value) { sendgrid.EnableTemplate(html); }
            else { sendgrid.DisableTemplate(); }
            enableFlag = null;
            return this;
        }

        /// <summary>
        /// When prefaced with Disable, turns off the Bcc filter
        /// </summary>
        /// <returns></returns>
        public MailBuilder Bcc() {
            if (!enableFlag.HasValue) { throw new InvalidOperationException("Builder requires 'Enable' or 'Disable' before this method."); }
            if (enableFlag.Value) { throw new ArgumentException("Arguments need to be specified to enable this item."); }
            else { sendgrid.DisableBcc(); }
            enableFlag = null;
            return this;
        }

        /// <summary>
        /// When prefaced with Enable, turns on the Bcc filter.  Otherwise, sets the Bcc address
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public MailBuilder Bcc(string email) {
            if (!enableFlag.HasValue) { sendgrid.AddBcc(email); }
            else if (enableFlag.Value) { sendgrid.EnableBcc(email); }
            else { sendgrid.DisableBcc(); }
            enableFlag = null;
            return this;
        }

        /// <summary>
        /// Preface with Enable or Disable to set the status of the Bypass List Management filter
        /// </summary>
        /// <returns></returns>
        public MailBuilder BypassListManagement() {
            if (!enableFlag.HasValue) { throw new InvalidOperationException("Builder requires 'Enable' or 'Disable' before this method."); }
            if (enableFlag.Value) { sendgrid.EnableBypassListManagement(); }
            else { sendgrid.DisableBypassListManagement(); }
            enableFlag = null;
            return this;
        }


    }
}
