using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Exception;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private IProductDal _productDal;
        // başka servisi kullanacaksan onun dal 'ını cagirma , direk onun servisini cagir !!!
        private ICategoryService _categoryService;


        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }

        [ValidationAspect(typeof(ProductValidator), Priority = 1)]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Product product)
        {
            #region 2. way to validaiton in aop
            //ValidationTool.Validate(new ProductValidator(), product);
            #endregion

            IResult result = BusinessRules.Run
                (
                    CheckIfProductNameExists(product.ProductName),
                    CheckIfCategoryIsEnabled()
                );

            if (result != null)
            {
                return result; 
            }

            _productDal.Add(product);

            return new SuccessResult(Messages.ProductAdded); 
        }

        private IResult CheckIfProductNameExists(string productName)
        {
            var result = _productDal.GetList(p => p.ProductName == productName).Any();

            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }

            return new SuccessResult();
        }
        private IResult CheckIfCategoryIsEnabled()
        {
            var result = _categoryService.GetList();
            if (result.Data.Count < 10)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return new SuccessResult();
        }


        [TransactionScopeAspect]
        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new SuccessResult(Messages.ProductUpdated);

        }

        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccessResult(Messages.ProductDeleted);

        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductID == productId));
        }

        [PerformanceAspect(5)]
        public IDataResult<List<Product>> GetList()
        {
            Thread.Sleep(5150);
            return new SuccessDataResult<List<Product>>(_productDal.GetList().ToList());
        }

        //[SecuredOperation("Product.List, Admin")]
        [CacheAspect(10)]
        [LogAspect(typeof(FileLogger))]
        //[LogAspect(typeof(DatabaseLogger))]
        public IDataResult<List<Product>> GetListByCategory(int categoryId)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetList(p => p.CategoryID == categoryId).ToList());
        }

       
    }
}
