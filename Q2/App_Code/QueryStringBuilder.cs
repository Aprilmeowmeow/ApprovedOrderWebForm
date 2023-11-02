using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Web;

namespace Q2.Web
{
    /// <summary>
    /// 處理網址QueryString的類別 (created by Yos 2011)
    /// </summary>
    public sealed class QueryStringBuilder
    {
        #region Contructors

        /// <summary>
        /// 建構子
        /// </summary>
        public QueryStringBuilder() : this(false) { }

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="current">是否複製目前網址</param>
        public QueryStringBuilder(bool current)
        {
            _queryStringCollection = new NameValueCollection();
            _context = System.Web.HttpContext.Current;

            if (current)
            { 
                if (_context != null)
                {
                    _queryStringCollection.Add(_context.Request.QueryString);

                    _baseUrl = _context.Request.Url.AbsoluteUri;

                    if (_queryStringCollection.Count > 0)
                        _baseUrl = _baseUrl.Substring(0, _baseUrl.LastIndexOf('?'));
                }
            }

        }

        #endregion

        #region Methods

        /// <summary>
        /// 加入對應名稱和值至QueryString
        /// </summary>
        /// <param name="name">名稱</param>
        /// <param name="value">值</param>
        public void Add(string name, string value)
        {
            _queryStringCollection.Add(name, value);
        }
        /// <summary>
        /// 移除QueryString中對應名稱的 Key & Value
        /// </summary>
        /// <param name="name">名稱</param>
        public void Remove(string name)
        {
            _queryStringCollection.Remove(name);
        }
        /// <summary>
        /// 清空目前QueryString
        /// </summary>
        public void Clear()
        {
            _queryStringCollection.Clear();
        }
        /// <summary>
        /// 判斷QueryString裡是否有對應名稱的項目
        /// </summary>
        /// <param name="name">名稱</param>
        /// <returns></returns>
        public bool HaveKey(string name)
        {
            foreach (string keyItem in _queryStringCollection.AllKeys)
            {
                if (keyItem == name)
                    return true;
            }

            return false;
        }
        /// <summary>
        /// 判斷QueryString裡是否有對應值的項目
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        public bool HaveValue(string value)
        {
            foreach (string keyItem in _queryStringCollection.AllKeys)
            {
                foreach (string valueItem in _queryStringCollection.GetValues(keyItem))
                {
                    if (valueItem == value)
                        return true;
                }
            }

            return false;
        }
        /// <summary>
        /// 將目前結果組合成字串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
           
            List<string> qsList = new List<string>();
            foreach (string key in _queryStringCollection.AllKeys)
            {
                qsList.Add(String.Format("{0}={1}", key, HttpUtility.UrlEncode(_queryStringCollection[key])));
            }

            string resultUrl = _baseUrl + (qsList.Count > 0 ? "?" + String.Join("&", qsList.ToArray()) : "");

            return resultUrl;
        }

        #endregion

        #region Properties
        public string BaseUrl
        {
            get { return this._baseUrl; }
            set { this._baseUrl = value; }
        }
        #endregion

        #region Fields
        private HttpContext _context;
        private NameValueCollection _queryStringCollection;
        private string _baseUrl;
        #endregion
    }
     
}
