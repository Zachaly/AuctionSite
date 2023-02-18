using AuctionSite.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSite.Database.Repository.Abstraction
{
    public interface IProductCategoryRepository
    {
        IEnumerable<T> GetProductCategories<T>(Func<ProductCategory, T> selector);
    }
}
