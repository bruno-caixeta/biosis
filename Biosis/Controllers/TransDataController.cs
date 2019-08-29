using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biosis.BusinessLayer.Interface;
using Biosis.Model.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Biosis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransDataController : Controller
    {

        private readonly IAnalysisDataExtract _analysisDataExtract;

        public TransDataController(IAnalysisDataExtract analysisDataExtract)
        {
            _analysisDataExtract = analysisDataExtract;
        }        

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] AnalysisFileDTO analysisFileDTO)
        {
            try
            {                
                var result = _analysisDataExtract.ExtractValues(analysisFileDTO.Base64);
                return Json(result);
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
