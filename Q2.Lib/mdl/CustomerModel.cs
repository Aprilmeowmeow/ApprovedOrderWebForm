using System;
using System.Collections.Generic;
using Yos.Studio.ORM;

namespace Q2.Lib
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable()]
    public partial class CustomerModel : Customer
    {
        public CustomerModel() : this(false) { }
        public CustomerModel(bool IsNew)
        {
            if (IsNew)
                this.UID = Guid.NewGuid();

        }
    }

    

    [Serializable()]
    public partial class CustomerModelCollection : Collection<CustomerModel>
    {
        public CustomerModelCollection() : base() { }
        public CustomerModelCollection(IEnumerable<CustomerModel> collection) : base(collection) { }


    }
}
