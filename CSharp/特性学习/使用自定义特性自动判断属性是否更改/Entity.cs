using System;

namespace algorithm.使用自定义特性自动判断属性是否更改
{
    /// <summary>
    ///
    /// </summary>
    [PropertyChangeTracking]
    public class Entity
    {
        [PropertyChangeTracking(ignore: true)]
        public Guid Id { get; set; }

        [PropertyChangeTracking(displayName: "序号")]
        public string OId { get; set; }

        [PropertyChangeTracking(displayName: "第一个字段")]
        public string A { get; set; }

        public double B { get; set; }

        public bool C { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;
    }
}