using Microsoft.AspNetCore.Mvc;
using ScriptEase.OnlinePharmacy.Data.Interfaces.Interfaces;

namespace ScriptEase.OnlinePharmacy.WebApi.Controllers
{
 
    [ApiController]
    public class BaseController<T, TR> : ControllerBase where TR : IBaseRepository<T>
    {
        private readonly TR _repository;

        public BaseController(TR repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var entities = await _repository.GetAllAsync();
            return Ok(entities);
        }

        // Implement other CRUD operations
    }
}
