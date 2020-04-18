namespace Covid_19.CoreAPI.Models {
    public static class Env {
        public static string dataDirectory => "./data";
        public static string statisticalVietNamPath => $"{dataDirectory}/StatisticalVietNam.json";
        public static string statisticalGlobalPath => $"{dataDirectory}/StatisticalGlobal.json";
        public static string statisticalProvincesPath => $"{dataDirectory}/StatisticalProvinces.json";
        public static string statisticalPatientsPath => $"{dataDirectory}/StatisticalPatients.json";
        public static string timelinesPath => $"{dataDirectory}/Timelines.json";
    }
}