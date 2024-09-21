//using DemoWebAPIEFSalesDb.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace DemoWebAPIEFSalesDb.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]

//    public class DemoRegionsController : ControllerBase
//    {
//        // define dbContext
//        private readonly SalesDBContext _salesDBContext;

//        public DemoRegionsController(SalesDBContext salesDBContext)
//        {
//            _salesDBContext = salesDBContext;
//        }

//        // GetRegions  Task -> object type
//        public async Task<ActionResult<IEnumerable<Region>>> GetRegions()
//        {
//            return await _salesDBContext.Regions.ToListAsync();
//        }
//    }
//}
