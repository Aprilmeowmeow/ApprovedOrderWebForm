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
    [YosDCS("User")]
    public partial class User : ICommandEntity 
    {
        /// <summary>
        /// 
        /// </summary>
        [YosDPS(DbType.String, FieldName = "UserId", Size = 20, IsPrimaryKey = true)] 
        public String UserId { get; set; }

        [YosDPS(DbType.Int32, FieldName = "Enable", Size = 20)]
        public int Enable { get; set; }

    }
}
