using Microsoft.AspNetCore.Mvc.Filters;

namespace WebAPI_CRUD.Exception
{
    public class CustomException : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
