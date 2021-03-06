﻿using System;
using System.Net.Http;
using System.Text;
using WebApiClient.Contexts;

namespace WebApiClient.Attributes
{
    /// <summary>
    /// 表示将参数的文本内容作为请求内容
    /// </summary>
    public class RawStringContentAttribute : HttpContentAttribute, IEncodingable
    {
        /// <summary>
        /// 媒体类型
        /// </summary>
        private readonly string mediaType;

        /// <summary>
        /// 编码方式
        /// </summary>
        private Encoding encoding = System.Text.Encoding.UTF8;

        /// <summary>
        /// 获取或设置编码名称
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        public string Encoding
        {
            get
            {
                return this.encoding.WebName;
            }
            set
            {
                this.encoding = System.Text.Encoding.GetEncoding(value);
            }
        }

        /// <summary>
        /// 将参数的文本内容作为请求内容
        /// </summary>
        /// <param name="mediaType">媒体类型</param>
        /// <exception cref="ArgumentNullException"></exception>
        public RawStringContentAttribute(string mediaType)
        {
            if (string.IsNullOrEmpty(mediaType))
            {
                throw new ArgumentNullException(nameof(mediaType));
            }
            this.mediaType = mediaType;
        }

        /// <summary>
        /// 设置参数到http请求内容
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="parameter">特性关联的参数</param>
        protected override void SetHttpContent(ApiActionContext context, ApiParameterDescriptor parameter)
        {
            var content = parameter.ToString();
            context.RequestMessage.Content = new StringContent(content ?? string.Empty, this.encoding, this.mediaType);
        }
    }
}
