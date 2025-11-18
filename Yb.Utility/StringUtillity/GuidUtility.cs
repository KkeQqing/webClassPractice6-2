using System;

namespace Yb.Utility.StringUtility
{
    /// <summary>
    /// Guid 工具类
    /// </summary>
    public static class GuidUtility
    {
        /// <summary>
        /// 获取新的 GUID 字符串（无横线）
        /// </summary>
        /// <returns></returns>
        public static string GetID()
        {
            return Guid.NewGuid().ToString("N").ToUpper();
        }

        /// <summary>
        /// 获取 GUID 字符串（带横线）
        /// </summary>
        /// <returns></returns>
        public static string GetGuid()
        {
            return Guid.NewGuid().ToString();
        }
    }
}