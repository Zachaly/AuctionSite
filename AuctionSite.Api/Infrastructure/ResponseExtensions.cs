using AuctionSite.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace AuctionSite.Api.Infrastructure
{
    public static class ResponseExtensions
    {
        public static ActionResult<ResponseModel> ReturnNoContentOrBadRequest(this ResponseModel model)
        {
            if (!model.Success)
                return new BadRequestObjectResult(model);

            return new NoContentResult();
        }

        public static ActionResult<ResponseModel> ReturnCreatedOrBadRequest(this ResponseModel model, string location)
        {
            if (!model.Success)
                return new BadRequestObjectResult(model);

            return new CreatedResult(location, model);
        }

        public static ActionResult<DataResponseModel<T>> ReturnOkOrBadRequest<T>(this DataResponseModel<T> model)
        {
            if (!model.Success)
                return new BadRequestObjectResult(model);

            return new OkObjectResult(model);
        }

        public static ActionResult<DataResponseModel<T>> ReturnOkOrNotFound<T>(this DataResponseModel<T> model)
        {
            if (!model.Success)
                return new NotFoundObjectResult(model);

            return new OkObjectResult(model);
        }
    }
}
