namespace LydFramework.WebApi.Middlewares.Options
{
    public class RateLimitOption
    {
        public DateTime StartTime { get; set; }
        public int Second { get; set; }
        public int Counter { get; set; }
        public int LimitCount { get; set; }
        public DateTime EndTime 
        { 
            get 
            { 
                return StartTime.AddSeconds(Second);
            } 
        }
        public TimeSpan Expiration
        {
            get
            {
                var ts = (EndTime - DateTime.Now).TotalMilliseconds;
                if(ts>0)
                    return TimeSpan.FromMilliseconds(ts);
                else
                    return TimeSpan.Zero;
            }
        }
        public bool IsLimit
        {
            get
            {
                return Counter > LimitCount;
            }
        }

    }
}
