// WTM默认页面 Wtm buidin page
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;

namespace WalkingTec.Mvvm.Mvc.Admin.ViewModels.FrameworkRoleVMs
{
    public class FrameworkRoleVM : BaseCRUDVM<FrameworkRole>
    {
        public override DuplicatedInfo<FrameworkRole> SetDuplicatedCheck()
        {
            var rv = CreateFieldsInfo(SimpleField(x => x.RoleName));
            rv.AddGroup(SimpleField(x => x.RoleCode));
            return rv;
        }

        public override void DoAdd()
        {
            base.DoAdd();
            Wtm.RemoveRoleCache(Wtm.LoginUserInfo.CurrentTenant).Wait();
        }

        public override void DoEdit(bool updateAllFields = false)
        {
            if (FC.ContainsKey("Entity.RoleCode"))
            {
                FC.Remove("Entity.RoleCode");
            }
            base.DoEdit(updateAllFields);
            Wtm.RemoveRoleCache(Wtm.LoginUserInfo.CurrentTenant).Wait();
        }

        public override async Task DoDeleteAsync()
        {
            using (var tran = DC.BeginTransaction())
            {
                try
                {
                    await base.DoDeleteAsync();
                    var ur = DC.Set<FrameworkUserRole>().Where(x => x.RoleCode == Entity.RoleCode);
                    DC.Set<FrameworkUserRole>().RemoveRange(ur);
                    DC.SaveChanges();
                    tran.Commit();
                    await Wtm.RemoveUserCacheByRole(Entity.RoleCode);
                    await Wtm.RemoveRoleCache(Wtm.LoginUserInfo.CurrentTenant);
                }
                catch
                {
                    tran.Rollback();
                }
            }
        }
    }
}