using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

namespace SendGridMail
{
    public class Header : IHeader
    {
        public void AddTo(IEnumerable<string> recipients)
        {
            throw new NotImplementedException();
        }

        public void AddSubVal(string tag, IEnumerable<string> substitutions)
        {
            throw new NotImplementedException();
        }

        public void AddUniqueIdentifier(IDictionary<string, string> identifiers)
        {
            throw new NotImplementedException();
        }

        public void SetCategory(string category)
        {
            throw new NotImplementedException();
        }

        public void Enable(string filter)
        {
            AddFilterSetting(filter, new List<string>(){ "enable" }, "1");
        }

        public void Disable(string filter)
        {
            AddFilterSetting(filter, new List<string>(){"enable"}, "0");
        }

        public void AddFilterSetting(string filter, IEnumerable<string> settings, string value)
        {
            throw new NotImplementedException();
        }

        public void AddHeader(MailMessage mime)
        {
            throw new NotImplementedException();
        }

        public String AsJson()
        {
            return "";
        }


        internal class FilterNode
        {
            private Dictionary<String, FilterNode> _branches;
            private String _leaf;

            public FilterNode()
            {
                _branches = new Dictionary<string, FilterNode>();
            }

            public void AddSetting(List<String> keys, String value)
            {
                if (keys.Count == 0)
                {
                    _leaf = value;
                }
                else
                {
                    var key = keys[0];
                    if (!_branches.ContainsKey(key))
                        _branches[key] = new FilterNode();
                    var remainingKeys = keys.Skip(1).ToList();
                    _branches[key].AddSetting(remainingKeys, value);
                }
            }


            public String GetSetting(params String[] keys)
            {
                return GetSetting(keys.ToList());
            }

            public String GetSetting(List<String> keys)
            {
                if (keys.Count == 0)
                    return _leaf;
                var key = keys.First();
                if(!_branches.ContainsKey(key))
                    throw new ArgumentException("Bad key path!");
                var remainingKeys = keys.Skip(1).ToList();
                return _branches[key].GetSetting(remainingKeys);
            }

            public String GetLeaf()
            {
                return _leaf;
            }

            public String ToJson()
            {
                if (_branches.Count > 0)
                    return "{" + String.Join(",", _branches.Keys.Select(k => '"' + k + '"' + ":" + _branches[k].ToJson())) + "}";
                return JsonUtils.Serialize(_leaf);
            }
        }
    }
}
