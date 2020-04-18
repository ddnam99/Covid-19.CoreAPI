using System.Collections.Generic;
using System.Threading.Tasks;
using Covid_19.CoreAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Covid_19.CoreAPI.Controller {
    [ApiController, Route("[controller]")]
    public class Covid19Controller : ControllerBase {
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

        [HttpGet("Timelines/{index}")]
        public async Task<ActionResult<List<Timeline>>> GetTimelinesAsync(int index = 1) {
            return await Timeline.GetTimelinesAsync(index);
        }

        [HttpGet("News/{index}")]
        public async Task<ActionResult<List<News>>> GetNewsAsync(int index = 1) {
            return await News.GetNewsAsync(index);
        }

        [HttpGet("Videos/{index}")]
        public async Task<ActionResult<List<News>>> GetVideosAsync(int index = 1) {
            return await News.GetVideosAsync(index);
        }

        [HttpGet("KhuyenCao/{index}")]
        public async Task<ActionResult<List<News>>> GetKhuyenCaoAsync(int index = 1) {
            return await News.GetKhuyenCaoAsync(index);
        }

        [HttpGet("DieuCanBiet/{index}")]
        public async Task<ActionResult<List<News>>> GetDieuCanBietAsync(int index = 1) {
            return await News.GetDieuCanBietAsync(index);
        }

        [HttpGet("HoTroTrongNganh/{index}")]
        public async Task<ActionResult<List<News>>> GetHoTroTrongNganhAsync(int index = 1) {
            return await News.GetHoTroTrongNganhAsync(index);
        }

        [HttpGet("ChiDaoDieuHanh/{index}")]
        public async Task<ActionResult<List<News>>> GetChiDaoDieuHanhAsync(int index = 1) {
            return await News.GetChiDaoDieuHanhAsync(index);
        }
    }
}