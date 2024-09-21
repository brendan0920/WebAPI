using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoWebAPISalesDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HobbiesController : ControllerBase
    {
        public static List<string> Hobbies = new List<string>
        {
            "Playing Piano",
            "Listening to Classical Music",
            "Watching a movie",
            "Working out"
        };

        [HttpGet]
        public List<string> GetAll()
        {
            return Hobbies;
        }

        private bool ValidIndex(int idx)
        {
            bool valid = true;
            if (idx < 0 || idx >= Hobbies.Count)
            {
                valid = false;
            }
            return valid;
        }

        [HttpGet("{idx}")]
        public string GetHobbyByIdx(int idx)
        {
            if (!ValidIndex(idx))
            {
                return "No Hobby at that index position, Try again!";
            }
            else
            {
                return Hobbies[idx];
            }
        }

        [HttpPost]
        public string AddHobby(string hobby)
        {
            Hobbies.Add(hobby);
            return "Hobby added!";
        }


        [HttpPut("{idx}")]
        public string UpdateHobby(int idx, string NewHobby)
        {
            if (!ValidIndex(idx))
            {
                return "No Hobby at that index position, Try again!";
            }
            else
            {
                Hobbies[idx] = NewHobby;
                return "Hobby updated!";
            }
        }

        [HttpDelete("{idx}")]
        public string RemoveHobby(int idx, string hobby)
        {
            if (!ValidIndex(idx))
            {
                return "No Hobby at that index position, Try again!";
            }
            else
            {
                Hobbies.RemoveAt(idx);
                return "Hobby removed!";
            }
        }

    }
}
