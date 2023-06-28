namespace RealEstate.API.Models
{
    public class Error
    {
        public string Message { get; set; }
        public List<string>? Errors { get; set;}
    }
}
