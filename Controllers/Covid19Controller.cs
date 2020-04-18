using System.Collections.Generic;
using System.Threading.Tasks;
using Covid_19.CoreAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Covid_19.CoreAPI.Controller {
    [ApiController, Route("[controller]")]
    public class Covid19Controller : ControllerBase {
        [HttpGet("Timelines")]
        public ActionResult<List<Timeline>> GetTimelines() {
            return Helper.DeserializeObjectFromFile<List<Timeline>>(Env.TimelinesPath);
        }

        [HttpGet("Global")]
        public ActionResult<Statistical> GetStatisticalGlobal() {
            return Helper.DeserializeObjectFromFile<Statistical>(Env.StatisticalGlobalPath);
        }

        [HttpGet("VietNam")]
        public ActionResult<Statistical> GetStatisticalVietNam() {
            return Helper.DeserializeObjectFromFile<Statistical>(Env.StatisticalVietNamPath);
        }

        [HttpGet("Provinces")]
        public ActionResult<List<Statistical>> GetStatisticalProvinces() {
            return Helper.DeserializeObjectFromFile<List<Statistical>>(Env.StatisticalProvincesPath);
        }

        [HttpGet("Patients")]
        public ActionResult<List<Patient>> GetStatisticalPatients() {
            return Helper.DeserializeObjectFromFile<List<Patient>>(Env.StatisticalPatientsPath);
        }
    }
}