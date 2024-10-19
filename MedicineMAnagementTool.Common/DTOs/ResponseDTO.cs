namespace MedicineMAnagementTool.Common.DTOs
{
    public class ResponseDTO<T> : StatusDTO
    {
        public int Count { get; set; }
        public List<T> ListGeneric { get; set; }
    }
}
