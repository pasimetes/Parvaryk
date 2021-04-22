using System.Collections.Generic;

namespace Parvaryk.Contracts.Models.Response
{
    public class PagedResponse<TItem>
    {
        public ICollection<TItem> Items { get; set; }

        public int TotalItems { get; set; }
    }
}
