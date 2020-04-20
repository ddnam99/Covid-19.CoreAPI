using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Covid_19.CoreAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Covid_19.CoreAPI.Controller {
    [ApiController, Route("[controller]")]
    public class Covid19Controller : ControllerBase {
        [HttpGet("Global")]
        public ActionResult<Statistical> GetStatisticalGlobal() {
            try {
                return Helper.DeserializeObjectFromFile<Statistical>(Env.StatisticalGlobalPath);
            } catch (Exception e) {
                Helper.LogError($"Covid-19.CoreAPI ERROR: {Request.Path}\n{e}");
                return StatusCode(500);
            }
        }

        [HttpGet("VietNam")]
        public ActionResult<Statistical> GetStatisticalVietNam() {
            try {
                return Helper.DeserializeObjectFromFile<Statistical>(Env.StatisticalVietNamPath);
            } catch (Exception e) {
                Helper.LogError($"Covid-19.CoreAPI ERROR: {Request.Path}\n{e}");
                return StatusCode(500);
            }
        }

        [HttpGet("Provinces")]
        public ActionResult<List<Statistical>> GetStatisticalProvinces() {
            try {
                return Helper.DeserializeObjectFromFile<List<Statistical>>(Env.StatisticalProvincesPath);
            } catch (Exception e) {
                Helper.LogError($"Covid-19.CoreAPI ERROR: {Request.Path}\n{e}");
                return StatusCode(500);
            }
        }

        [HttpGet("Patients")]
        public ActionResult<List<Patient>> GetStatisticalPatients() {
            try {
                return Helper.DeserializeObjectFromFile<List<Patient>>(Env.StatisticalPatientsPath);
            } catch (Exception e) {
                Helper.LogError($"Covid-19.CoreAPI ERROR: {Request.Path}\n{e}");
                return StatusCode(500);
            }
        }

        [HttpGet("Timelines/{index}")]
        public async Task<ActionResult<List<Timeline>>> GetTimelinesAsync(int index = 1) {
            try {
                return await Timeline.GetTimelinesAsync(index);
            } catch (Exception e) {
                Helper.LogError($"Covid-19.CoreAPI ERROR: {Request.Path}\n{e}");
                return StatusCode(500);
            }
        }

        [HttpGet("News/{index}")]
        public async Task<ActionResult<List<News>>> GetNewsAsync(int index = 1) {
            try {
                return await News.GetNewsAsync(index);
            } catch (Exception e) {
                Helper.LogError($"Covid-19.CoreAPI ERROR: {Request.Path}\n{e}");
                return StatusCode(500);
            }
        }

        [HttpGet("Videos/{index}")]
        public async Task<ActionResult<List<News>>> GetVideosAsync(int index = 1) {
            try {
                return await News.GetVideosAsync(index);
            } catch (Exception e) {
                Helper.LogError($"Covid-19.CoreAPI ERROR: {Request.Path}\n{e}");
                return StatusCode(500);
            }
        }

        [HttpGet("KhuyenCao/{index}")]
        public async Task<ActionResult<List<News>>> GetKhuyenCaoAsync(int index = 1) {
            try {
                return await News.GetKhuyenCaoAsync(index);
            } catch (Exception e) {
                Helper.LogError($"Covid-19.CoreAPI ERROR: {Request.Path}\n{e}");
                return StatusCode(500);
            }
        }

        [HttpGet("DieuCanBiet/{index}")]
        public async Task<ActionResult<List<News>>> GetDieuCanBietAsync(int index = 1) {
            try {
                return await News.GetDieuCanBietAsync(index);
            } catch (Exception e) {
                Helper.LogError($"Covid-19.CoreAPI ERROR: {Request.Path}\n{e}");
                return StatusCode(500);
            }
        }

        [HttpGet("HoTroTrongNganh/{index}")]
        public async Task<ActionResult<List<News>>> GetHoTroTrongNganhAsync(int index = 1) {
            try {
                return await News.GetHoTroTrongNganhAsync(index);
            } catch (Exception e) {
                Helper.LogError($"Covid-19.CoreAPI ERROR: {Request.Path}\n{e}");
                return StatusCode(500);
            }
        }

        [HttpGet("ChiDaoDieuHanh/{index}")]
        public async Task<ActionResult<List<News>>> GetChiDaoDieuHanhAsync(int index = 1) {
            try {
                return await News.GetChiDaoDieuHanhAsync(index);
            } catch (Exception e) {
                Helper.LogError($"Covid-19.CoreAPI ERROR: {Request.Path}\n{e}");
                return StatusCode(500);
            }
        }
    }
}