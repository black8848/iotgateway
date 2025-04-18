using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WalkingTec.Mvvm.Core
{
    /// <summary>
    /// DataPrivilege
    /// </summary>
    [Table("DataPrivileges")]
    public class DataPrivilege : BasePoco, ITenant
    {
        [Display(Name = "_Admin.User")]
        public string UserCode { get; set; }

        [Display(Name = "_Admin.Group")]
        public string GroupCode { get; set; }

        [Required(ErrorMessage = "Validate.{0}required")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Display(Name = "_Admin.TableName")]
        public string TableName { get; set; }

        public string RelateId { get; set; }

        [Display(Name = "_Admin.Domain")]
        public string Domain { get; set; }

        [Display(Name = "_Admin.Tenant")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        public string TenantCode { get; set; }
    }
}