using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

namespace SendGrid
{

    /// <summary>
    /// Represents the additional functionality to add SendGrid specific mail headers
    /// </summary>
    public class Header : IHeader
    {

        #region Private Members

        private const string SendGridHeader = "X-Smtpapi";
        private readonly HeaderSettingsNode _settings;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the array of recipient addresses from the X-SMTPAPI header
        /// </summary>
        public IEnumerable<string> To
        {
            get
            {
                return _settings.GetArray("to");
            }
        }

        #endregion

        #region Constructors

        public Header()
        {
            _settings = new HeaderSettingsNode();
        }

        #endregion

        #region Methods

        /// <summary>
        /// This adds a substitution value to be used during the mail merge.  Substitutions
        /// will happen in order added, so calls to this should match calls to addTo in the mail message.
        /// </summary>
        /// <param name="tag">string to be replaced in the message</param>
        /// <param name="substitutions">substitutions to be made, one per recipient</param>
        public void AddSubVal(string tag, IEnumerable<string> substitutions)
        {
            var keys = new List<string> {"sub", tag};
            _settings.AddArray(keys, substitutions);
        }

        /// <summary>
        /// This adds the "to" array to the X-SMTPAPI header so that multiple recipients
        /// may be addressed in a single email. (but they each get their own email, instead of a single email with multiple TO: addressees)
        /// </summary>
        /// <param name="addresses">List of email addresses</param>
        public void AddTo(IEnumerable<string> addresses)
        {
            _settings.AddArray(new List<string> { "to" }, addresses);
        }

        /// <summary>
        /// This adds parameters and values that will be passed back through SendGrid's
        /// Event API if an event notification is triggered by this email.
        /// </summary>
        /// <param name="identifiers">parameter value pairs to be passed back on event notification</param>
        public void AddUniqueIdentifier(IDictionary<string, string> identifiers)
        {
            foreach (var key in identifiers.Keys)
            {
                var keys = new List<string> {"unique_args", key};
                var value = identifiers[key];
                _settings.AddSetting(keys, value);
            }
        }

        /// <summary>
        /// This sets the category for this email.  Statistics are stored on a per category
        /// basis, so this can be useful for tracking on a per group basis.
        /// </summary>
        /// <param name="category">categories applied to the message</param>
        public void SetCategory(string category)
        {
            var keys = new List<string> {"category"};
            _settings.AddSetting(keys, category);
        }

        /// <summary>
        /// Shortcut method for enabling a filter.
        /// </summary>
        /// <param name="filter">The name of the filter to enable</param>
        public void Enable(string filter)
        {
            AddFilterSetting(filter, new List<string>(){ "enable" }, "1");
        }

        /// <summary>
        /// Shortcut method for disabling a filter.
        /// </summary>
        /// <param name="filter">The name of the filter to disable</param>
        public void Disable(string filter)
        {
            AddFilterSetting(filter, new List<string>(){"enable"}, "0");
        }

        /// <summary>
        /// Allows you to specify a filter setting.  You can find a list of filters and settings here:
        /// http://docs.sendgrid.com/documentation/api/web-api/filtersettings/
        /// </summary>
        /// <param name="filter">The name of the filter to set</param>
        /// <param name="settings">The multipart name of the parameter being set</param>
        /// <param name="value">The value that the settings name will be assigning</param>
        public void AddFilterSetting(string filter, IEnumerable<string> settings, string value)
        {
            var keys = new List<string>() {"filters", filter, "settings" }.Concat(settings).ToList();
            _settings.AddSetting(keys, value);
        }

        /// <summary>
        /// Attaches the SendGrid headers to the MIME.
        /// </summary>
        /// <param name="mime">the MIME to which we are attaching</param>
        public void AddHeader(MailMessage mime)
        {
            mime.Headers.Add(SendGridHeader, AsJson());
        }

        /// <summary>
        /// Converts the filter settings into a JSON string.
        /// </summary>
        /// <returns>string representation of the SendGrid headers</returns>
        public string AsJson()
        {
            if(_settings.IsEmpty()) return "";
            return _settings.ToJson();
        }

        #endregion

        #region Internal HeaderSettingsNode

        internal class HeaderSettingsNode
        {
            private readonly Dictionary<string, HeaderSettingsNode> _branches;
            private IEnumerable<string> _array; 
            private string _leaf;

            public HeaderSettingsNode()
            {
                _branches = new Dictionary<string, HeaderSettingsNode>();
            }

            public void AddArray(List<string> keys, IEnumerable<string> value)
            {
                if (keys.Count == 0)
                {
                    _array = value;
                }
                else
                {
                    if (_leaf != null || _array != null)
                        throw new ArgumentException("Attempt to overwrite setting");

                    var key = keys.First();
                    if (!_branches.ContainsKey(key))
                        _branches[key] = new HeaderSettingsNode();

                    var remainingKeys = keys.Skip(1).ToList();
                    _branches[key].AddArray(remainingKeys, value);
                }
            }

            public void AddSetting(List<string> keys, string value)
            {
                if (keys.Count == 0)
                {
                    _leaf = value;
                }
                else
                {
                    if(_leaf != null || _array != null) 
                        throw new ArgumentException("Attempt to overwrite setting");
                    
                    var key = keys.First();
                    if (!_branches.ContainsKey(key))
                        _branches[key] = new HeaderSettingsNode();
                    
                    var remainingKeys = keys.Skip(1).ToList();
                    _branches[key].AddSetting(remainingKeys, value);
                }
            }

            public string GetSetting(params string[] keys)
            {
                return GetSetting(keys.ToList());
            }

            public string GetSetting(List<string> keys)
            {
                if (keys.Count == 0)
                    return _leaf;
                var key = keys.First();
                if(!_branches.ContainsKey(key))
                    throw new ArgumentException("Bad key path!");
                var remainingKeys = keys.Skip(1).ToList();
                return _branches[key].GetSetting(remainingKeys);
            }

            public IEnumerable<string> GetArray(params string[] keys)
            {
                return GetArray(keys.ToList());
            }

            public IEnumerable<string> GetArray(List<string> keys)
            {
                if (keys.Count == 0)
                    return _array;
                var key = keys.First();
                if (!_branches.ContainsKey(key))
                    throw new ArgumentException("Bad key path!");
                var remainingKeys = keys.Skip(1).ToList();
                return _branches[key].GetArray(remainingKeys);
            }

            public string GetLeaf()
            {
                return _leaf;
            }

            public string ToJson()
            {
                if (_branches.Count > 0)
                    return "{" + string.Join(",", _branches.Keys.Select(k => Utils.Serialize(k)  + " : " + _branches[k].ToJson())) + "}";
                if (_leaf != null)
                    return Utils.Serialize(_leaf);
                if (_array != null)
                    return "[" + string.Join(", ", _array.Select(i => Utils.Serialize(i))) + "]";
                return "{}";
            }

            public bool IsEmpty()
            {
                if (_leaf != null) return false;
                return _branches == null || _branches.Keys.Count == 0;
            }
        }

        #endregion
    }
}
