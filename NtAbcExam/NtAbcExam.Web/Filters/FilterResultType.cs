using System.ComponentModel;

namespace NtAbcExam.Web.Filters
{
    public enum FilterResultType
    {
        [Description("返回视图")]
        ResultView = 0,
        [Description("返回json格式数据")]
        ResultJson = 1,
        [Description("返回普通文本格式数据")]
        ResultText = 2
    }
}