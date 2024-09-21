using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoWebAPISalesDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        public static List<string> regions = new();
        
        [HttpGet]
        public List<string> GetAll()
        {
            if (regions.Count == 0)
            {
                return new List<string> { "No regions in list... yet" };
            } 
            else
            {
                return regions;
            }
        }


        private bool ValidIndex(int idx)
        {
            bool valid = true;
            if (idx < 0 || idx >= regions.Count)
            {
                valid = false;
            }
            return valid;
        }


        [HttpGet("{idx}")]
        public string GetRegionByIdx(int idx)
        {
            if(!ValidIndex(idx))
            {
                return "No Region at that index postion, Try again!";
            }
            else
            {
                return regions[idx];
            }
        }


        [HttpPost]
        public string AddRegion(string region)
        {
            regions.Add(region);
            return "Region Added!";
        }


        [HttpPut]
        public string UpdateRegion(int idx, string newRegion)
        {
            if (!ValidIndex(idx))
            {
                return "No Region at that index position, Try again!";
            }
            else
            {
                regions[idx] = newRegion;
                return "Region updated!";
            }
        }


        [HttpDelete("{idx}")]
        public string RemoveRegion(int idx, string region)
        {
            if (!ValidIndex(idx))
            {
                return "No Region at that index position, Try again!";
            } 
            else
            {
                regions.RemoveAt(idx);
                return "Region removed!";
            }
        }


    }
}
