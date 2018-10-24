using System.Collections.Generic;

namespace ModelLibrary
{
    /// <summary>
    /// 黑名單結果
    /// </summary>
    public class BlockResult
    {
        public string networkAddress { get; set; }

        public string netmask { get; set; }

        public string minAddress { get; set; }

        public string maxAddress { get; set; }

        public int numPossibleHosts { get; set; }

        public string addressSpaceDesc { get; set; }

        public List<ReportedIP> reportedIPs { get; set; }
    }
}