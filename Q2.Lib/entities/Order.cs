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
    [YosDCS("Order")]
    public partial class Order : ICommandEntity 
    {
        /// <summary>
        /// 
        /// </summary>
        [YosDPS(DbType.String, FieldName = "ID", Size = 20, IsPrimaryKey = true)] 
        public String ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [YosDPS(DbType.String, FieldName = "Customer_ID", Size = 20)] 
        public String Customer_ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [YosDPS(DbType.Decimal, FieldName = "TotalAmount", Size = 9)] 
        public Decimal TotalAmount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [YosDPS(DbType.Int32, FieldName = "Status", Size = 4)] 
        public Int32 Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [YosDPS(DbType.DateTime, FieldName = "Order_Date", Size = 8)] 
        public DateTime Order_Date { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [YosDPS(DbType.String, FieldName = "Sales_Name", Size = 100)] 
        public String Sales_Name { get; set; }
        
        [YosDPS(DbType.DateTime, FieldName = "Approved_Date")]
        public DateTime Approved_Date { get; set; }
    }
}
