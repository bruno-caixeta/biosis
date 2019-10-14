using Biosis.BusinessLayer.Interface;
using Biosis.DataObject;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biosis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResearchController: Controller
    {

        private readonly IResearchBusinessLayer _researchBusinessLayer;
        public ResearchController(IResearchBusinessLayer researchBusinessLayer)
        {
            _researchBusinessLayer = researchBusinessLayer;
        }
        
        [HttpPost]
        public IActionResult Post([FromBody] ResearchDTO researchDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var research = _researchBusinessLayer.CreateResearch(researchDTO);
                return Json(research);

            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    return StatusCode(500, new { Error = ex.Message, InnerException = ex.InnerException.Message });
                }
                return StatusCode(500, new { Error = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var researches = _researchBusinessLayer.GetAllResearches();
                return Json(researches);

            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    return StatusCode(500, new { Error = ex.Message, InnerException = ex.InnerException.Message });
                }
                return StatusCode(500, new { Error = ex.Message });
            }
        }
    }
}
