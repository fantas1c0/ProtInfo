using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AplProt.Labs;

namespace AplProt.Controllers
{
    public class UserPostModel 
    {
        public int Id_Labs { get;  }
        public string KeyCode { get; set; }
        public string TextMessage { get; set; }

    }

    public class RandomKeyModel
    {
        public int Id_Labs { get; set; }
        public int Length { get; set; }
    }

    [Route("api/")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        public List<ILabs> Labs { get; set; } = new(){
            new Laba1(), 
            new Laba2()
        };

        [HttpGet("list_labs")]
        public IActionResult GetItems()
        {
            Dictionary<int, string> items = new();
            for (int i = 0; i < Labs.Count; ++i) {
                items.Add(i, Labs[i].name);    
            }
            return Ok(items);
        }


        [HttpPost("result_encrypt")]
        public IActionResult SetItem([FromBody] UserPostModel model) {
            if (model.Id_Labs < 0 && model.Id_Labs >= Labs.Count) { 
                return  BadRequest("error: Id_Labs not valid!");
            }

            var lab = Labs[model.Id_Labs];

            var keyCheck = lab.Validate_key_code(model.TextMessage ?? "", model.KeyCode ?? "");
            var msgCheck = lab.Validate_text_message(model.TextMessage ?? "");

            string encrypted = lab.Encrypt(model.TextMessage ?? "", model.KeyCode ?? "");
            //string encrypted = lab.RandomKey(10);
            return Ok(new
            {
                result = $"Ляба: {lab.name}\n" +
                         $"Ключ: {keyCheck.status} — {keyCheck.message}\n" +
                         $"Текст: {msgCheck.status} — {msgCheck.message}\n" +
                         $"Шифр: {encrypted}",
                keyStatus = keyCheck.status.ToString(),
                keyMessage = keyCheck.message,
                textStatus = msgCheck.status.ToString(),
                textMessage = msgCheck.message,
                encrypted
            });
            //return Ok($"Получено: {model.KeyCode}, {model.TextMessage}");

        }

        [HttpPost("random_key")]
        public IActionResult RandomKey([FromBody] RandomKeyModel model)
        {
            if (model.Id_Labs < 0 || model.Id_Labs >= Labs.Count)
                return BadRequest(new { error = "Id_Labs not valid!" });

            if (model.Length <= 0)
                return BadRequest(new { error = "Length must be > 0" });

            var key = Labs[model.Id_Labs].RandomKey(model.Length);
            return Ok(new { key });
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
