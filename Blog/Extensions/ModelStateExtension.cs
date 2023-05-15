using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Blog.Extensions
{
    public static class ModelStateExtension
    {
        public static List<string> GetErrors(this ModelStateDictionary modelState) // o this faz com que GetErrors() seja acessado no ModelState
        {
            var result = new List<string>();
            foreach (var item in modelState.Values)
            {
                //foreach (var error in item.Errors)
                //{
                //    result.Add(error.ErrorMessage);
                //}

                result.AddRange(item.Errors.Select(error => error.ErrorMessage)); // equivalente ao código comentado de cima
            }

            return result;
        }
    }
}
