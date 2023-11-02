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
    public partial class UserModel : User
    {
        
    }


    [Serializable()]
    public partial class UserModelCollection : Collection<UserModel>
    {
        public UserModelCollection() : base() { }
        public UserModelCollection(IEnumerable<UserModel> collection) : base(collection) { }


    }
}
