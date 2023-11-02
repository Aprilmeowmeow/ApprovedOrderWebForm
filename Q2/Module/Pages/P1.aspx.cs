using System;
using System.Collections.Generic;
using System.Text;
using CAP.Core.Org;
using Ext.Net;

namespace Q2.Web.UI
{
    public partial class P1 : PageBase
    {
        //private readonly SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MSSQL_DBconnect"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                this.Title = "Approve Order";

                this.loadData();
            }
        }

        [DirectMethod(Timeout = 30000, ShowMask = true)]
        public bool ExecuteApprove(string ID)
        {
            bool success;
            using (Q2.Lib.OrderContext mgr = new Q2.Lib.OrderContext())
            {
                var approvedDate = DateTime.Now;
                success = mgr.SetApprove(ID, approvedDate);
            }           
            this.loadData();
            return success;
        }

        private void loadData()
        {
            using (Q2.Lib.OrderContext mgr = new Q2.Lib.OrderContext())
            {
                MainStore.DataSource = mgr.Maintain.RetrieveCollection(" Status > 0 ");
                MainStore.DataBind();
                
            }           
        }
        
        public List<UserModel> FilterUser()
        {
            using(Q2.Lib.UserContext mgr = new Q2.Lib.UserContext())
            {
                List<UserModel> user = new List<UserModel>();
                return user;
            }
            
        }
        protected void Button1_Click(object sender, DirectEventArgs e)
        {
            StringBuilder result = new StringBuilder();
            string ids = string.Empty;
            RowSelectionModel sm = this.MainGrid.GetSelectionModel() as RowSelectionModel;

            foreach (SelectedRow row in sm.SelectedRows)
            {
                e.Success=ExecuteApprove( row.RecordID);
            }
        }
        protected void Show(object sender, DirectEventArgs e)
        {
            X.Js.Call("show");
      
        }

    }
}

