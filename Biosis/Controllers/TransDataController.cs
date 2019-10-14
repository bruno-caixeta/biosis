using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biosis.BusinessLayer.Implementation;
using Biosis.BusinessLayer.Interface;
using Biosis.Model.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Biosis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransDataController : Controller
    {

        private readonly IAnalysisDataExtract _analysisDataExtract;
        private readonly ITransCalculations _transCalculations;
        private readonly ITransDataBusinessLayer _transDataBusinessLayer;
        private readonly IResearchBusinessLayer _researchBusinessLayer;

        public TransDataController(IAnalysisDataExtract analysisDataExtract, ITransCalculations transCalculations, ITransDataBusinessLayer transDataBusinessLayer, IResearchBusinessLayer researchBusinessLayer)
        {
            _analysisDataExtract = analysisDataExtract;
            _transCalculations = transCalculations;
            _transDataBusinessLayer = transDataBusinessLayer;
            _researchBusinessLayer = researchBusinessLayer; 
        }        

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] AnalysisFileDTO analysisFileDTO)
        {
            try
            {                
                var result = _analysisDataExtract.ExtractValues(analysisFileDTO);
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

        [HttpGet("Control")]
        public IActionResult GetControls()
        {
            try
            {
                var result = _transDataBusinessLayer.GetControls();
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

        [HttpGet("report/{researchId}")]
        public IActionResult GetReport(Guid researchId)
        {
            try
            {
                var research = _researchBusinessLayer.GetFullResearch(researchId);
                if (research == null)
                {
                    return NotFound("Pesquisa não encontrada");
                }
                var controle = _transDataBusinessLayer.GetTransData(research.ControlId);
                if (controle == null)
                {
                    return NotFound("Dados de controle não encontrados");
                }
                var memoryStream = _transCalculations.GeneratePdfReport(controle, research);
                return File(memoryStream.ToArray(), "application/octet-stream", "research.pdf");
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
