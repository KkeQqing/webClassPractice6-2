using System.ComponentModel;

namespace Yb.Model.Enums
{
    #region 数据审核状态枚举 - EnumCheckStatus
    /// <summary>
    /// 数据审核状态枚举
    /// </summary>
    public enum EnumCheckStatus
    {
        /// <summary>
        /// 未提交
        /// </summary>
        [Description("未提交")]
        UnCommit = 0,

        /// <summary>
        /// 提交待审核
        /// </summary>
        [Description("待审核")]
        UnCheck = 1,

        /// <summary>
        /// 审核不通过，待修改
        /// </summary>
        [Description("审核不通过")]
        CheckFailed = 2,

        /// <summary>
        /// 审核通过
        /// </summary>
        [Description("审核通过")]
        CheckSuccess = 3,
    }
    #endregion

    #region 性别枚举 - EnumSex
    /// <summary>
    /// 性别枚举
    /// </summary>
    public enum EnumSex
    {
        /// <summary>
        /// 女
        /// </summary>
        [Description("女")]
        Female = 0,

        /// <summary>
        /// 男
        /// </summary>
        [Description("男")]
        Male = 1
    }
    #endregion

    #region 菜单类型枚举 - EnumMenuType
    /// <summary>
    /// 菜单类型枚举
    /// </summary>
    public enum EnumMenuType
    {
        /// <summary>
        /// 系统菜单
        /// </summary>
        [Description("系统菜单")]
        Sys = 0,

        /// <summary>
        /// 前端顶部菜单
        /// </summary>
        [Description("前端顶部菜单")]
        Top = 1,

        /// <summary>
        /// 其他菜单
        /// </summary>
        [Description("其他菜单")]
        Other = 2
    }
    #endregion
}