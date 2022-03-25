using Castle.DynamicProxy;
using System;

namespace Core.Utilities.Interceptors
{
    // bir metodun nasıl yorumlanacagini gosterir
    public abstract class MethodInterception : MethodInterceptionBaseAttribute
    {
        // metod calismadan once
        protected virtual void OnBefore(IInvocation invocation) { }

        // metod calistıktan sonra 
        protected virtual void OnAfter(IInvocation invocation) { }

        protected virtual void OnException(IInvocation invocation, System.Exception e) { }

        protected virtual void OnSuccess(IInvocation invocation) { }


        public override void Intercept(IInvocation invocation)
        {
            var isSucccess = true;

            OnBefore(invocation);

            try
            {
                // metodu calistirma islemi
                invocation.Proceed();
            }
            catch (Exception e)
            {
                isSucccess = false;
                OnException(invocation, e);
                throw;
            }
            finally
            {
                if (isSucccess)
                {
                    OnSuccess(invocation);
                }
            }
            OnAfter(invocation);
        }
    }
}
