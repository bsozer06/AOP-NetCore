using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntitiyFramework.Contexts;
using Entities.Concrete;

namespace DataAccess.Concrete.EntitiyFramework
{
    // Dependency injection işleminde IProductDal kullanılıyor.
    // Bu sayede efProductDal kullanılabilir hale geliyor kalıtıldığı için.
    public class EfProductDal : EfEntityRepositoryBase<Product,NorthwindContext>, IProductDal
    {

    }
}
