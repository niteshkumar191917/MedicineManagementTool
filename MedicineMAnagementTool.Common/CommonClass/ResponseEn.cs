namespace MedicineMAnagementTool.Common.CommonClass
{
    public class ResponseEn<T>
    {
        public int Count { get; set; }
        public List<T> ListGeneric { get; set; }
    }
}
