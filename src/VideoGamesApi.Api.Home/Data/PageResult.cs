using System.Collections.Generic;

namespace VideoGamesApi.Api.Home.Data
{
    public class PageResult<TItem>
    {
        public int PageIndex { get; set; }

        public int PageSIze { get; set; }

        public IEnumerable<TItem> Items { get; set; }

        public int CountItems { get; set; }
    }
}
