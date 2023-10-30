namespace cotr.backend.Model
{
    public class Jwt
    {
        public string AccessKey { get; set; }
        public string RefreshKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public double DurationInMinutesAccess { get; set; }
        public double DurationInDaysRefresh { get; set; }

        public Jwt(string accessKey, string refreshkey, string issuer, string audience, double durationInMinutesAccess, double durationInDaysRefresh)
        {
            AccessKey = accessKey;
            RefreshKey = refreshkey;
            Issuer = issuer;
            Audience = audience;
            DurationInMinutesAccess = durationInMinutesAccess;
            DurationInDaysRefresh = durationInDaysRefresh;
        }
    }
}
