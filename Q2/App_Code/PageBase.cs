using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text; 

namespace Q2.Web.UI
{
    public abstract class PageBase : System.Web.UI.Page
    {
        public PageBase() { }

        /// <summary>
        /// 將資料序列化成JSON格式字串並回傳
        /// </summary>
        /// <param name="value">資料物件</param>
        /// <returns></returns>
        protected virtual string SerializeObject(object value)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(value);
        }
        /// <summary>
        /// 將資料序列化成JSON格式字串並回傳
        /// </summary>
        /// <param name="value">資料物件</param>
        /// <returns></returns>
        protected virtual string SerializeObject(object value, Newtonsoft.Json.JsonSerializerSettings settings)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(value, settings);
        }
        /// <summary>
        /// 將JSON格式字串反序列化成物件並回傳
        /// </summary>
        /// <typeparam name="T">物件型別</typeparam>
        /// <param name="json">JSON格式字串</param>
        /// <returns></returns>
        protected virtual T DeserializeObject<T>(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
        }
         
        #region Query String

        /// <summary>
        /// 取得參數值
        /// </summary>
        /// <typeparam name="T">要轉換的型別</typeparam>
        /// <param name="key">參數名稱</param>
        /// <returns></returns>
        protected T GetParameter<T>(string key)
        {
            return this.GetParameter<T>(key, false);
        }
        /// <summary>
        /// 取得參數值
        /// </summary>
        /// <typeparam name="T">要轉換的型別</typeparam>
        /// <param name="key">參數名稱</param>
        /// <param name="isDecode">是否解碼(使用DES)</param>
        /// <returns></returns>
        protected T GetParameter<T>(string key, bool isDecode)
        {
            if (this.Context.Request[key] != null)
            {
                try
                {
                    string v = this.Context.Request[key];

                    if (isDecode)
                        v = CAP.Core.Utility.DESDecode(v);

                    return (T)Convert.ChangeType(v, typeof(T));
                }
                catch { }
            }

            return default(T);
        }
        /// <summary>
        /// 取得表單參數值
        /// </summary>
        /// <typeparam name="T">要轉換的型別</typeparam>
        /// <param name="key">參數名稱</param>
        /// <returns></returns>
        protected T GetFormParameter<T>(string key)
        {
            if (this.Context.Request.Form[key] != null)
            {
                try
                {
                    return (T)Convert.ChangeType(this.Request.Form[key], typeof(T));
                }
                catch { }
            }

            return default(T);
        }
        /// <summary>
        /// 取得網址參數值
        /// </summary>
        /// <typeparam name="T">要轉換的型別</typeparam>
        /// <param name="key">參數名稱</param>
        /// <returns></returns>
        protected T GetQueryStringParameter<T>(string key)
        {
            if (this.Context.Request.QueryString[key] != null)
            {
                try
                {
                    return (T)Convert.ChangeType(this.Request.QueryString[key], typeof(T));
                }
                catch { }
            }

            return default(T);
        }

        #endregion

        #region Redirect
        /// <summary>
        /// 網頁導向(將原頁的ＱueryString一併加入網址字串)
        /// </summary>
        /// <param name="url">網址</param>
        /// <param name="queryStrings">附加參數(兩個一組，第一個為參數名，第二個為參數值)</param>
        protected virtual void Redirect(string url, params string[] queryStrings)
        {
            redirectMethod(url, false, queryStrings);
        }
        /// <summary>
        /// 網頁導向
        /// </summary>
        /// <param name="url">網址</param>
        /// <param name="isClear">是否清除原頁網址的ＱueryString</param>
        /// <param name="queryStrings">附加參數(兩個一組，第一個為參數名，第二個為參數值)</param>
        protected virtual void Redirect(string url, bool isClear, params string[] queryStrings)
        {
            redirectMethod(url, isClear, queryStrings);
        }
        /// <summary>
        /// 網頁導向
        /// </summary>
        /// <param name="url">網址</param>
        /// <param name="isClear">是否清除原頁網址的ＱueryString</param>
        /// <param name="queryStrings">附加參數(兩個一組，第一個為參數名，第二個為參數值)</param>
        private void redirectMethod(string url, bool isClear, params string[] queryStrings)
        {
            Response.Redirect(this.CombineUrl(url, isClear, queryStrings));
        }
        protected string CombineUrl(string url, params string[] queryStrings)
        {
            return this.CombineUrl(url, false, queryStrings);
        }
        /// <summary>
        /// 結合網址與參數
        /// </summary>
        /// <param name="url">網址</param>
        /// <param name="isClear">是否清空舊有參數</param>
        /// <param name="queryStrings">附加參數(兩個一組，第一個為參數名，第二個為參數值)</param>
        /// <returns></returns>
        protected string CombineUrl(string url, bool isClear, params string[] queryStrings)
        {
            QueryStringBuilder builder = new QueryStringBuilder();
            builder.BaseUrl = url;

            if (isClear)
                builder.Clear();

            for (int index = 0; index < queryStrings.Length; index += 2)
            {
                builder.Add(queryStrings[index], queryStrings.Length > index + 1 ? queryStrings[index + 1] : "");
            }

            return builder.ToString();
        }

        #endregion
         
        #region Property

         private string m_Title = String.Empty;
        /// <summary>
        /// 存取Title
        /// </summary>
        public new string Title
        {
            set
            {
                base.Title = value;
                m_Title = value;

                MasterPage master = this.Master;
                HtmlTitle htmlTitle = null;

                while (master != null)
                {
                    htmlTitle = master.FindControl("Page_Title") as HtmlTitle;
                    if (htmlTitle != null)
                    {
                        htmlTitle.Text = value;
                        break;
                    }

                    master = master.Master;
                }

                this.SetLabel4TitleText(value);

            }
            get
            {
                return m_Title; //base.Title; 
            }
        }

        protected bool TitleAreaVisible
        {
            get
            {
                HtmlGenericControl div = this.FindDiv4Title();

                if (div != null)
                    return div.Visible;

                return false;
            }
            set
            {
                HtmlGenericControl div = this.FindDiv4Title();

                if (div != null)
                    div.Visible = value;
            }
        }

        protected virtual void SetLabel4TitleText(string text)
        {
            Label labelTitle = this.FindLabel4Title();

            if (labelTitle != null)
                labelTitle.Text = text;
        }

        protected virtual Label FindLabel4Title()
        {
            MasterPage master = null;
            Label labelTitle = null;
            ContentPlaceHolder formPlaceHolder = null;

            master = this.Master;
            while (master != null)
            {
                labelTitle = master.FindControl("Label_Title") as Label;
                if (labelTitle == null)
                {
                    formPlaceHolder = master.FindControl("FormPlaceHolder1") as ContentPlaceHolder;

                    if (formPlaceHolder != null)
                        labelTitle = formPlaceHolder.FindControl("Label_Title") as Label;
                }

                if (labelTitle != null)
                    break;

                master = master.Master;
            }

            return labelTitle;
        }

        protected virtual HtmlGenericControl FindDiv4Title()
        {
            MasterPage master = null;
            HtmlGenericControl divTitle = null;
            ContentPlaceHolder formPlaceHolder = null;

            master = this.Master;
            while (master != null)
            {
                divTitle = master.FindControl("MainTitleArea") as HtmlGenericControl;
                if (divTitle == null)
                {
                    formPlaceHolder = master.FindControl("FormPlaceHolder1") as ContentPlaceHolder;

                    if (formPlaceHolder != null)
                        divTitle = formPlaceHolder.FindControl("MainTitleArea") as HtmlGenericControl;
                }

                if (divTitle != null)
                    break;

                master = master.Master;
            }

            return divTitle;
        }

        #endregion

        #region Script

        /// <summary>
        /// 將script註冊到頁面上
        /// </summary>
        /// <param name="script">Javascript</param>
        protected virtual void DoPageScript(string script)
        {
            string _out = String.Format("<script type='text/javascript'>{0};</script>", script);
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), _out);
        }
        /// <summary>
        /// 將alert script註冊到頁面上
        /// </summary>
        /// <param name="alertMessage">要顯示的訊息</param>
        protected virtual void DoPageScriptAlert(string alertMessage)
        {
            string _out = String.Format("<script type='text/javascript'>alert('{0}');</script>", alertMessage);
            Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), _out);
        }
 
        #endregion

         
    }
}