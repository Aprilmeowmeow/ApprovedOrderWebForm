using System;
using System.Data;
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
       
        public DataTable GetApproveOrder()
        {
            var query = @"SELECT [Customer].[Enable], [Order].[ID], [Order].[Customer_ID], [Order].[TotalAmount], [Order].Status,
            [Order].[Order_Date], [Order].[Sales_Name], [Order].[Approved_Date]
            FROM [Order] INNER JOIN[Customer] ON Customer.ID = [Order].Customer_ID";
            this.DAO.CommandText = query;
            this.DAO.CommandType = CommandType.Text;
            this.DAO.ClearParameters();
            var data = this.DAO.GetDataTable();
            return data;
        }
    }
}

