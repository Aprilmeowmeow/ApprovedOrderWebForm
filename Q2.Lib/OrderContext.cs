using System;
using System.ComponentModel;
using System.Data;
using Yos.Studio.ORM;
using Yos.Studio.ORM.Extensions;
using CAP.Core;

namespace Q2.Lib
{
    /// <summary>
    /// 
    /// </summary>
    public partial class OrderContext : DalContext<OrderModel>
    {
        public bool SetApprove(string ID, DateTime approvedDate)
        {
            string query = @"UPDATE [Order] SET [Status] = @Status , [Approved_Date] = @ApprovedDate WHERE ([ID] = @ID)";
            this.DAO.CommandText = query;
            this.DAO.CommandType = CommandType.Text;
            this.DAO.ClearParameters();
            this.DAO.AddParameter("Status", 5);
            this.DAO.AddParameter("ID", ID); 
            this.DAO.AddParameter("ApprovedDate", approvedDate); 
            return this.DAO.ExecuteNonQuery();
        }
    }
}
