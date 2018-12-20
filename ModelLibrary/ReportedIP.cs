namespace ModelLibrary
{
    /// <summary>
    /// 已反應名單
    /// </summary>
    public class ReportedIP
    {
        public string IP { get; set; }

        public string NumReports { get; set; }

        public string MostRecentReport { get; set; }

        public int Public { get; set; }

        public string CountryCode { get; set; }

        public int IsWhitelisted { get; set; }

        public int? abuseConfidenceScore { get; set; }
    }
}