//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace YCOT.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Auth_ControllerActions
    {
        public Auth_ControllerActions()
        {
            this.Auth_AdminControllerAction = new HashSet<Auth_AdminControllerAction>();
            this.Auth_ControllerActionRole = new HashSet<Auth_ControllerActionRole>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public Nullable<bool> IsController { get; set; }
        public Nullable<bool> IsAllowedNoneRoles { get; set; }
        public Nullable<bool> IsAllowedAllRoles { get; set; }
        public string ControllerName { get; set; }
    
        public virtual ICollection<Auth_AdminControllerAction> Auth_AdminControllerAction { get; set; }
        public virtual ICollection<Auth_ControllerActionRole> Auth_ControllerActionRole { get; set; }
    }
}
