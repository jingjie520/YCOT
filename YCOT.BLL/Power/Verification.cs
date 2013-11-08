using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YCOT.BLL.Power
{
    /// <summary>
    /// 权力验证
    /// </summary>
    public class Verification
    {
        public static bool ControllerActionByUserName(string UserName, string ControllerName, string ActionName, ref string ResultInfo)
        {
            var Result = false;
            if (string.IsNullOrWhiteSpace(UserName))
            {
                Result = ControllerAction(null, ControllerName, ActionName, ref ResultInfo);
            }
            else
            {

                using (var data = new DAL.YCOT_DataEntities())
                {
                    var userModel = data.Admins.FirstOrDefault(c => c.UserName == UserName);
                    if (userModel != null)
                    {
                        return ControllerAction(userModel.id, ControllerName, ActionName, ref ResultInfo);
                    }
                    else
                    {
                        ResultInfo = "当前用户不存在.";
                    }
                }
            }

            return Result;

        }



        /// <summary>
        /// 控制器权限验证
        /// </summary>
        /// <param name="AdminID">管理员编号</param>
        /// <param name="ControllerName">控制器名称</param>
        /// <param name="ActionName">Action名称</param>
        /// <param name="ResultInfo">返回信息</param>
        /// <returns></returns>
        public static bool ControllerAction(int? AdminID, string ControllerName, string ActionName, ref string ResultInfo)
        {
            var Result = false;

            //取控制器配置
            using (var data = new DAL.YCOT_DataEntities())
            {
                var acaModel = data.Auth_ControllerActions.FirstOrDefault(c => c.Name == ActionName && c.ControllerName == ControllerName && c.IsController == false);
                if (acaModel == null)
                {
                    acaModel = data.Auth_ControllerActions.FirstOrDefault(c => c.Name == ControllerName && c.IsController == true);
                    if (acaModel == null)
                    {
                        ResultInfo = "当前Action不受权限系统管理.";
                        return true;
                    }
                }




                if (acaModel.IsAllowedNoneRoles.Value)
                {
                    ResultInfo = "当前Action允许匿名用户访问.";
                    return true;
                }

                if (!AdminID.HasValue)
                {
                    ResultInfo = "当前Action不允许匿名用户访问.";
                    return false;
                }


                if (acaModel.IsAllowedAllRoles.Value)
                {
                    ResultInfo = "当前Action允许所有用户访问.";
                    return true;
                }


                //开始接口权限


                //1.1 检查拒绝权限
                if (acaModel.Auth_AdminControllerAction.Any(c => c.AdminID == AdminID && c.IsRefuse == true))
                {
                    ResultInfo = "当前Action拒绝该用户访问.";
                    return false;
                }
                //1.2 检查组拒绝权限
                //      得到用户所属组
                //var aarList = data.Auth_AdminRole.Where(c=>c.AdminID == AdminID);
                var acarList = from aar in data.Auth_AdminRole
                               join acar in data.Auth_ControllerActionRole
                               on aar.RoleID equals acar.RoleID
                               where aar.AdminID == AdminID && acar.ControllerActionID == acaModel.ID
                               select acar;

                if (acarList.Any(c => c.IsRefuse.Value == false))
                {
                    ResultInfo = "当前Action拒绝该用户组访问.";
                    return false;
                }

                //1.3 允许组
                if (acarList.Any(c => c.IsRefuse.Value == true))
                {
                    ResultInfo = "当前Action允许该用户组访问.";
                    return true;
                }

                //1.4 允许用户
                if (acaModel.Auth_AdminControllerAction.Any(c => c.AdminID == AdminID && c.IsRefuse == false))
                {
                    ResultInfo = "当前Action允许该用户访问.";
                    return true;
                }
            }

            ResultInfo = "当前Action没有该用户的权限设置";
            return Result;
        }
    }
}
