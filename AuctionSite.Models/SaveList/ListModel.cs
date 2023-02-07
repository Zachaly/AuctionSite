using AuctionSite.Models.ListStock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSite.Models.SaveList
{
    public class ListModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ListStockModel> Items { get; set; }
    }
}
