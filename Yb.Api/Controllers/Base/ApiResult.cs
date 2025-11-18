using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Yb.Api.Controllers.Base
{
    /// <summary>
    /// Web Api 返回结果对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResult<T>
    {
        /// <summary>
        /// 是否获取成功
        /// </summary>
        public bool Success { set; get; }

        /// <summary>
        /// 代码
        /// </summary>
        public int Code { set; get; }

        /// <summary>
        /// 结果
        /// </summary>
        public T Result { set; get; }

        /// <summary>
        /// 错误信息，支持HTML
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Error { set; get; }

        /// <summary>
        /// 消息（可替代 Error）
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Msg { set; get; }

        /// <summary>
        /// 模型错误信息
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<ApiResultModelError> ModelErrors { get; set; }

        /// <summary>
        /// 扩展信息
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public dynamic extra { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="result"></param>
        /// <param name="success"></param>
        public ApiResult(T result, bool success = true)
        {
            Result = result;
            Success = success;
        }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public ApiResult() { }

        /// <summary>
        /// 隐式转换：ApiResult -> string (取 Error)
        /// </summary>
        public static implicit operator string(ApiResult<T> model)
        {
            return model?.Error;
        }

        /// <summary>
        /// 隐式转换：string -> ApiResult (作为错误信息)
        /// </summary>
        public static implicit operator ApiResult<T>(string msg)
        {
            return new ApiResult<T>() { Error = msg };
        }

        /// <summary>
        /// 隐式转换：ApiResult -> T (取 Result)
        /// </summary>
        public static implicit operator T(ApiResult<T> model)
        {
            if (model == null)
                return default(T);
            return model.Result;
        }

        /// <summary>
        /// 隐式转换：T -> ApiResult (成功包装)
        /// </summary>
        public static implicit operator ApiResult<T>(T t)
        {
            return new ApiResult<T>() { Result = t, Success = true };
        }

        /// <summary>
        /// 隐式转换：List<ApiResultModelError> -> ApiResult (模型验证错误)
        /// </summary>
        public static implicit operator ApiResult<T>(List<ApiResultModelError> errors)
        {
            return new ApiResult<T>()
            {
                ModelErrors = errors,
                Error = errors == null ? "" : string.Join(",", errors.SelectMany(o => o.errors))
            };
        }
    }

    /// <summary>
    /// 模型校验错误信息
    /// </summary>
    public class ApiResultModelError
    {
        /// <summary>
        /// 字段名（key）
        /// </summary>
        public string key { get; set; }

        /// <summary>
        /// 错误消息列表
        /// </summary>
        public List<string> errors { get; set; }
    }

    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class ApiResultEx
    {
        /// <summary>
        /// 将任意对象转为 ApiResult
        /// </summary>
        public static ApiResult<T> ToApiResult<T>(this T t, bool success = true, int code = 0, string error = null)
        {
            if (t == null)
            {
                success = false;
                return new ApiResult<T>(default(T), success)
                {
                    Error = "未能返回数据对象"
                };
            }

            return new ApiResult<T>(t, success) { Code = code, Error = error };
        }
    }
}