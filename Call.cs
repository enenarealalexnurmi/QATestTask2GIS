using System.Collections.Generic;

namespace RegionsAPI.Data
{
    public class Call
    {
        public ErrorData Error { get; set; }
        public int Total { get; set; }
        public List<Region> Items { get; set; }
    }
}
