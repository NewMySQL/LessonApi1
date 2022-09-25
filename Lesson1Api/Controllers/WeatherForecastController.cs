using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Lesson1Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("/Test")]
        public IActionResult Get()
        {
            var weatherCast = Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })
                .ToArray();

            return Ok(weatherCast);
        }

        /// <summary>
        /// Тестовый метод Get2
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     Test:
        ///     Test
        /// 
        /// </remarks>
        /// <param name="id">Числовое значение</param>
        /// <param name="name">Наименование</param>
        /// <param name="lang"></param>
        /// <param name="animalname"></param>
        /// <param name="animalage"></param>
        /// <response code="200">
        /// Успех:
        /// 
        ///     [
        ///         {
        ///             "workTypeId": 6,
        ///             "name": "Public engineering",
        ///             "description": "Construction of public works based on general planning, instruction, and control (incl. repair, improvement, or demolition work)"
        ///         },
        /// 
        ///         {
        ///             "workTypeId": 7,
        ///             "name": "Construction engineering",
        ///             "description": "Construction of buildings based on general planning, instruction, and control (incl. repair, improvement, or demolition work)"
        ///         }
        ///     ]
        /// 
        /// </response>
        /// <response code="400">Не верные данные (ошибка валидации)</response>
        /// <returns></returns>
        [HttpPost("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Get2(
            [FromRoute] int? id,
            [Required, FromQuery] string name,
            [FromHeader, DefaultValue("en")] string? lang,
            [FromForm, Required] string animalname, [FromForm, Required] int animalage)
        {
            switch (lang)
            {
                case "en":
                    return Ok($"Hello {name}, id - {id}, animal - {animalname}:{animalage}");

                case "ru":
                default:
                    return Ok($"Привет {name}, id - {id}, animal - {animalname}:{animalage}");
            }
        }
    }

    public class Animal
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}