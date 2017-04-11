namespace NtAbcExam.FrameWork.Data
{
    public class CalcPageIndex
    {
        public static int GetPageIndex(int offset, int limit)
        {
            if (offset == 0 || limit == 0)
            {
                return 1;
            }

            var pageIndex = offset / limit;

            return pageIndex == 0 ? 1 : pageIndex + 1;
        }
    }
}
