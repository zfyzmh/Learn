using System;

namespace algorithm.使用自定义特性自动判断属性是否更改
{
    public class PropertyChangelog<T>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public PropertyChangelog()
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        /// <param name="oldValue">旧值</param>
        /// <param name="newValue">新值</param>
        public PropertyChangelog(string propertyName, string oldValue, string newValue)
        {
            PropertyName = propertyName;
            OldValue = oldValue;
            NewValue = newValue;
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="className">类名</param>
        /// <param name="propertyName">属性名称</param>
        /// <param name="oldValue">旧值</param>
        /// <param name="newValue">新值</param>
        /// <param name="changedTime">修改时间</param>
        public PropertyChangelog(string className, string propertyName, string oldValue, string newValue, DateTime changedTime)
            : this(propertyName, oldValue, newValue)
        {
            ClassName = className;
            ChangedTime = changedTime;
        }

        /// <summary>
        /// 类名称
        /// </summary>
        public string ClassName { get; set; } = typeof(T).FullName;

        /// <summary>
        /// 属性名称
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// 旧值
        /// </summary>
        public string OldValue { get; set; }

        /// <summary>
        /// 新值
        /// </summary>
        public string NewValue { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ChangedTime { get; set; } = DateTime.Now;
    }
}