namespace StockportGovUK.NetStandard.Abstractions.Caching
{
    public class CacheProviderConfiguration
    {
        public bool AllowCaching { get; set; }

        public double Timeout { get; set; }
    }
}