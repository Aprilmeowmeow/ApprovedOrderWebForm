using System;
using System.ComponentModel;
using System.Data;
using Yos.Studio.ORM;

namespace Q2.Lib
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable()]
    [YosDCS("ApprovedOrder")]
    public partial class ApprovedOrder : ICommandEntity
    {
        /// <summary>
        /// 
        /// </summary>

        public string ID { get; set; }
        public String Customer_ID { get; set; }
        public Decimal TotalAmount { get; set; }
        public Int32 Status { get; set; }
        public DateTime Order_Date { get; set; }
        public String Sales_Name { get; set; }
        public DateTime Approved_Date { get; set; }
        public int Enable { get; set; }

    }
}
