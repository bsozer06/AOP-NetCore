using Autofac;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntitiyFramework;

namespace Business.DependencyResolvers.Autofac
{
    /// <summary>
    /// Autofac 'de duzenlenen dependency inversion 'lar.
    /// </summary>
    public class AutofactBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductManager>().As<IProductService>();
            builder.RegisterType<EfProductDal>().As<IProductDal>();

            builder.RegisterType<CategoryManager>().As<ICategoryService>();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>();
        }
    }
}
