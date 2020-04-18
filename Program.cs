using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Covid_19.CoreAPI.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace Covid_19.CoreAPI {
    public class Program {
        public static void Main(string[] args) {
            if (!Directory.Exists(Env.DataDirectory)) Directory.CreateDirectory(Env.DataDirectory);
            var updateTask = new Task(async() => {
                while (true) {
                    await Task.Factory.StartNew(async() => await UpdateDataAsync());

                    Thread.Sleep(TimeSpan.FromMinutes(1));
                }
            });
            updateTask.Start();

            CreateHostBuilder(args).Build().Run();
            Task.WaitAll(updateTask);
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => {
                webBuilder.UseStartup<Startup>();
            });

        private static async Task UpdateDataAsync() {
            var html = await Helper.GetHTMLAsync("https://ncov.moh.gov.vn/");

            var vietNamGlobalTask = Statistical.GetVietNamGlobalAsync(html);
            var provincesTask = Statistical.GetProvincesAsync(html);
            var patientsTask = Patient.GetPatientsAsync(html);
            var timelineTask = Timeline.GetTimelinesAsync(html);
            Task.WaitAll(vietNamGlobalTask, provincesTask, patientsTask, timelineTask);

            var(vietNam, global) = vietNamGlobalTask.Result;
            var provinces = provincesTask.Result;
            var patients = patientsTask.Result;
            var timelines = timelineTask.Result;

            if (File.Exists(Env.StatisticalGlobalPath)) {
                if (!global.Equals(Helper.DeserializeObjectFromFile<Statistical>(Env.StatisticalGlobalPath)))
                    Helper.SerializeObjectToFile(Env.StatisticalGlobalPath, global);
            } else Helper.SerializeObjectToFile(Env.StatisticalGlobalPath, global);
            if (File.Exists(Env.StatisticalVietNamPath)) {
                if (!vietNam.Equals(Helper.DeserializeObjectFromFile<Statistical>(Env.StatisticalVietNamPath)))
                    Helper.SerializeObjectToFile(Env.StatisticalVietNamPath, vietNam);
            } else Helper.SerializeObjectToFile(Env.StatisticalVietNamPath, vietNam);
            if (File.Exists(Env.StatisticalProvincesPath)) {
                if (!provinces.Equals(Helper.DeserializeObjectFromFile<List<Statistical>>(Env.StatisticalProvincesPath)))
                    Helper.SerializeObjectToFile(Env.StatisticalProvincesPath, provinces);
            } else Helper.SerializeObjectToFile(Env.StatisticalProvincesPath, provinces);
            if (File.Exists(Env.StatisticalPatientsPath)) {
                if (!patients.Equals(Helper.DeserializeObjectFromFile<List<Statistical>>(Env.StatisticalPatientsPath)))
                    Helper.SerializeObjectToFile(Env.StatisticalPatientsPath, patients);
            } else Helper.SerializeObjectToFile(Env.StatisticalPatientsPath, patients);
            if (File.Exists(Env.TimelinesPath)) {
                if (!timelines.Equals(Helper.DeserializeObjectFromFile<List<Timeline>>(Env.TimelinesPath)))
                    Helper.SerializeObjectToFile(Env.TimelinesPath, timelines);
            } else Helper.SerializeObjectToFile(Env.TimelinesPath, timelines);
        }
    }
}