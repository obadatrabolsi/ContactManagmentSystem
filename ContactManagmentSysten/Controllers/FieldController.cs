using CMS.Api.Controllers.Base;
using CMS.Core.Models;
using CMS.Domain.Requests.Fields;
using CMS.Services.ExtendedFields;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Api.Controllers
{
    public class FieldController : BaseApiController
    {
        private readonly IExtendedFieldService _extendedFieldService;

        public FieldController(IExtendedFieldService extendedFieldService)
        {
            _extendedFieldService=extendedFieldService;
        }

        [HttpPost("")]
        public async Task<ApiResponse> CreateAsync(CreateFieldRequest model)
        {
            await _extendedFieldService.CreateAsync(model);

            return ApiResponse.Success();
        }
    }
}
