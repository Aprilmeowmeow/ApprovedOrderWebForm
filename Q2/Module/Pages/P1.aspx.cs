using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ext.Net;
using Newtonsoft.Json;
using Q2.Lib;

namespace Q2.Web.UI
{
    public partial class P1 : PageBase
    {
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
            List<ApprovedOrder> ar = new List<ApprovedOrder>();
            using (Q2.Lib.OrderContext order = new Q2.Lib.OrderContext())
            {
                MainStore.DataSource = order.GetApproveOrder();
                MainStore.DataBind();
            }
            using (Q2.Lib.CustomerContext customer = new CustomerContext())
            {
                Store1.DataSource = customer.Maintain.RetrieveCollection("ID != '0'").ToList();
                Store1.DataBind();
            }
                //using (Q2.Lib.CustomerContext cust = new Q2.Lib.CustomerContext())
                //{
                //    var customers= cust.Maintain.RetrieveCollection(" ID != '0'").ToList();
                //    Store1.DataSource = customers;
                //    Store1.DataBind();
                    
                //    var orders = order.Maintain.RetrieveCollection(" Status > 0 ").ToList();
                    
                //    foreach (var o in orders)
                //    {

                //        ar.Add(new ApprovedOrder()
                //        {
                //            ID = o.ID,
                //            Customer_ID = o.Customer_ID,
                //            TotalAmount = o.TotalAmount,
                //            Status = o.Status,
                //            Order_Date = o.Order_Date,
                //            Sales_Name = o.Sales_Name,
                //            Approved_Date = o.Approved_Date,
                //            Enable = (from customer in customers where customer.ID == o.Customer_ID select customer.Enable).FirstOrDefault()
                //        }) ; 
                //    }
                //    MainStore.DataSource = ar;
                //    MainStore.DataBind();
                //}
        }
        
      
        protected void SaveCust(object sender, DirectEventArgs e)
        {
            string custJson = e.ExtraParams["model"];
            var model = JsonConvert.DeserializeObject<List<Customer>>(custJson);
            foreach(var m in model)
            {
                if (String.IsNullOrEmpty(m.ID))
                {
                    e.Success = false;
                }
                else
                    e.Success = UpdateEnable(m.ID, m.Enable);
            }
        }

        private bool UpdateEnable(string id, bool enable)
        {
            if(id == null)
            {
                return false;
            }
            using (Q2.Lib.CustomerContext mgr = new Q2.Lib.CustomerContext())
            {
                return mgr.SetCustEnable(id, enable);
            }
        }

        protected void SubmitBtnClick(object sender, DirectEventArgs e)
        {
            RowSelectionModel sm = this.MainGrid.GetSelectionModel() as RowSelectionModel;
            if (sm.SelectedRows.Count == 0)
            {
                e.Success = false;
                e.ErrorMessage = "error";
                return;
            }
            
            foreach (SelectedRow row in sm.SelectedRows)
            {
                e.Success=ExecuteApprove( row.RecordID);
            }

            if(e.Success == false)
            {
                e.ErrorMessage = "Approved Failure";
            }
        }
    }
}

