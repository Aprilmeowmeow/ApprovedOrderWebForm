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
    [YosDCS("Customer")]
    public partial class Customer : ICommandEntity
    {
        /// <summary>
        /// 
        /// </summary>
        [YosDPS(DbType.Guid, FieldName = "UID", Size = 16, IsPrimaryKey = true)]
        public Guid UID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [YosDPS(DbType.String, FieldName = "ID", Size = 20)]
        public String ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [YosDPS(DbType.String, FieldName = "Name", Size = 50)]
        public String Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [YosDPS(DbType.String, FieldName = "Country", Size = 3)]
        public String Country { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [YosDPS(DbType.String, FieldName = "City", Size = 50)]
        public String City { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [YosDPS(DbType.String, FieldName = "State", Size = 2)]
        public String State { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [YosDPS(DbType.String, FieldName = "Address", Size = 100)]
        public String Address { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [YosDPS(DbType.String, FieldName = "Zip", Size = 20)]
        public String Zip { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [YosDPS(DbType.Int32, FieldName = "Status", Size = 4)]
        public Int32? Status { get; set; }

    }
}
