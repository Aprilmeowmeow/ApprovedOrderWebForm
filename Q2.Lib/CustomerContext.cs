using CAP.Core;
using System.Data;

namespace Q2.Lib
{
    /// <summary>
    /// 
    /// </summary>
    public partial class CustomerContext : DalContext<CustomerModel>
    {
        public bool SetCustEnable(string id, bool enable)
        {
            string query = @"UPDATE [Customer] SET [Enable] = @Enable WHERE ([ID] = @ID)";
            this.DAO.CommandText = query;
            this.DAO.CommandType = CommandType.Text;
            this.DAO.ClearParameters();
            this.DAO.AddParameter("Enable", enable);
            this.DAO.AddParameter("ID", id);
            return this.DAO.ExecuteNonQuery();
        }
    }
}
