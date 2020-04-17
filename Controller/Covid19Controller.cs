using System.Collections.Generic;
using System.Threading.Tasks;
using Covid_19.CoreAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Covid_19.CoreAPI.Controller {
    [ApiController, Route("[controller]")]
    public class Covid19Controller : ControllerBase {
        [HttpGet("Timelines")]
        public async Task<ActionResult<List<Timeline>>> GetTimelinesAsync() {
            var html = await Helper.GetHTMLAsync("https://ncov.moh.gov.vn/");

            return await Timeline.GetTimelinesAsync(html);
        }

        [HttpGet("Global")]
        public async Task<ActionResult<Statistical>> GetStatisticalGlobalAsync() {
            var html = await Helper.GetHTMLAsync("https://ncov.moh.gov.vn/");
            var result = await Statistical.GetVietNamGlobalAsync(html);

            return result.Global;
        }

        [HttpGet("VietNam")]
        public async Task<ActionResult<Statistical>> GetStatisticalVietNamAsync() {
            var html = await Helper.GetHTMLAsync("https://ncov.moh.gov.vn/");
            var result = await Statistical.GetVietNamGlobalAsync(html);

            return result.VietNam;
        }

        [HttpGet("Provinces")]
        public async Task<ActionResult<List<Statistical>>> GetStatisticalProvincesAsync() {
            var html = await Helper.GetHTMLAsync("https://ncov.moh.gov.vn/");

            return await Statistical.GetProvincesAsync(html);
        }

        [HttpGet("Patients")]
        public async Task<ActionResult<List<Patient>>> GetStatisticalPatientsAsync() {
            var html = await Helper.GetHTMLAsync("https://ncov.moh.gov.vn/");

            return await Patient.GetPatientsAsync(html);
        }
    }
}