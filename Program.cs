using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Covid_19.CoreAPI.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Covid_19.CoreAPI {
    public class Program {
        public static void Main(string[] args) {
            if (!Directory.Exists(Env.DataDirectory)) Directory.CreateDirectory(Env.DataDirectory);
            var updateTask = new Task(async() => {
                while (true) {
                    try {
                        await Task.Factory.StartNew(async() => await UpdateDataAsync());
                    } catch (Exception e) {
                        Helper.LogError($"Covid-19.CoreAPI ERROR: Update task\n{e}");
                    }

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

            var(vietNam, global) = await Statistical.GetVietNamGlobalAsync(html);
            var provinces = await Statistical.GetProvincesAsync(html);
            var patients = await Patient.GetPatientsAsync(html);

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
        }
    }
}