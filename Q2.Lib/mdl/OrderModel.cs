using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Yos.Studio.ORM;

namespace Q2.Lib
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable()]
    public partial class OrderModel : Order
    {
        
    }


    [Serializable()]
    public partial class OrderModelCollection : Collection<OrderModel>
    {
        public OrderModelCollection() : base() { }
        public OrderModelCollection(IEnumerable<OrderModel> collection) : base(collection) { }


    }
}
