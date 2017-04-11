using System.Collections.Generic;

namespace NtAbcExam.FrameWork.Data
{
    public class ModelQuery
    {
        public int limit { get; set; }

        public int offset { get; set; }

        public string order { get; set; }

        public string ordername { get; set; }

        public string filters { get; set; }

        public List<ModelFilter> AllFilters { get; set; } = new List<ModelFilter>();
    }

    public class ModelSort
    {
        public string Name { get; set; }
        public string OrderType { get; set; }
    }

    public class ModelFilter
    {
        public string name { get; set; }
        public string value { get; set; }
        public string operType { get; set; }
    }
}
