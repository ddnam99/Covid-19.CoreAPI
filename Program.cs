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
            if (!Directory.Exists(Env.dataDirectory)) Directory.CreateDirectory(Env.dataDirectory);
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

            if (File.Exists(Env.statisticalGlobalPath)) {
                if (!global.Equals(Helper.DeserializeObjectFromFile<Statistical>(Env.statisticalGlobalPath)))
                    Helper.SerializeObjectToFile(Env.statisticalGlobalPath, global);
            } else Helper.SerializeObjectToFile(Env.statisticalGlobalPath, global);
            if (File.Exists(Env.statisticalVietNamPath)) {
                if (!vietNam.Equals(Helper.DeserializeObjectFromFile<Statistical>(Env.statisticalVietNamPath)))
                    Helper.SerializeObjectToFile(Env.statisticalVietNamPath, vietNam);
            } else Helper.SerializeObjectToFile(Env.statisticalVietNamPath, vietNam);
            if (File.Exists(Env.statisticalProvincesPath)) {
                if (!provinces.Equals(Helper.DeserializeObjectFromFile<List<Statistical>>(Env.statisticalProvincesPath)))
                    Helper.SerializeObjectToFile(Env.statisticalProvincesPath, provinces);
            } else Helper.SerializeObjectToFile(Env.statisticalProvincesPath, provinces);
            if (File.Exists(Env.statisticalPatientsPath)) {
                if (!patients.Equals(Helper.DeserializeObjectFromFile<List<Statistical>>(Env.statisticalPatientsPath)))
                    Helper.SerializeObjectToFile(Env.statisticalPatientsPath, patients);
            } else Helper.SerializeObjectToFile(Env.statisticalPatientsPath, patients);
            if (File.Exists(Env.timelinesPath)) {
                if (!timelines.Equals(Helper.DeserializeObjectFromFile<List<Timeline>>(Env.timelinesPath)))
                    Helper.SerializeObjectToFile(Env.timelinesPath, timelines);
            } else Helper.SerializeObjectToFile(Env.timelinesPath, timelines);
        }
    }
}