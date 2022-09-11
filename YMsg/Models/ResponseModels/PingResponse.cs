namespace YMsg.Models.ResponseModels;

public class PingResponse
{
    public DateTime CurrentServerDateTime { get; set; }
    
    public string Message { get; set; }
    
    public string CacheDbVersion { get; set; }
}