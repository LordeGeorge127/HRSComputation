namespace HRSCompute
{
    public class EmployeeListPagination<T>:List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; set; }

        public EmployeeListPagination(List<T> items, int count,int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            //TotalPages = count / (double)pageSize;
            TotalPages = (int)Math.Ceiling(count/(double)pageSize);
            this.AddRange(items);
        }
        //enable or disable page buttons
        public bool isPreviousPageAvailable => PageIndex > 1;
        public bool isNextPageAvailable => PageIndex < TotalPages;
        public static EmployeeListPagination<T> Create (IList<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count;
            var items = source.Skip((pageIndex -1) * pageSize).Take(pageSize).ToList();   
            return new EmployeeListPagination<T>(items, count, pageIndex, pageSize);
        }

    }
}
