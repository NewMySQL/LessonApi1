using Lesson1Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lesson1Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WorkstokController : ControllerBase
    {
        private readonly WorksTokContext DB;

        public WorkstokController(WorksTokContext db)
        {
            this.DB = db;
        }

        /// <summary>
        /// Получение списка типов работ
        /// </summary>
        /// <returns>
        /// 
        /// [
        ///     {
        ///         "workTypeId": 6,
        ///         "name": "Public engineering",
        ///         "description": "Construction of public works based on general planning, instruction, and control (incl. repair, improvement, or demolition work)"
        ///     },
        /// 
        ///     {
        ///         "workTypeId": 7,
        ///         "name": "Construction engineering",
        ///         "description": "Construction of buildings based on general planning, instruction, and control (incl. repair, improvement, or demolition work)"
        ///     }
        /// ]
        /// 
        /// </returns>
        [HttpGet]
        public IActionResult WorkTypes()
        {
            return Ok(DB.Worktypes
                .Select(x => new
                {
                    WorkTypeId = x.Id,
                    x.Name,
                    x.Description,
                })
                .ToList());
        }

        [HttpGet]
        public IActionResult Clients()
        {
            return Ok(DB.Clients
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                })
                .ToList());
        }


        [HttpGet]
        public IActionResult WorkRequest()
        {
            var workrequests = DB.Workrequests
                //.Include(x => x.Client)
                //.Include(x => x.WorkType)
                .Select(x => new
                {
                    x.Id,
                    x.CreateTime,
                    Client = new
                    {
                        x.Client.Id,
                        x.Client.Name
                    },
                    WorkType = new
                    {
                        WorkTypeId = x.WorkType.Id,
                        x.WorkType.Name,
                        x.WorkType.Description
                    }
                })
                .ToList();

            return Ok(workrequests);
        }
    }
}
