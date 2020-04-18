namespace Covid_19.CoreAPI.Models {
    public static class Env {
        public static string DataDirectory => "./data";
        public static string StatisticalVietNamPath => $"{DataDirectory}/StatisticalVietNam.json";
        public static string StatisticalGlobalPath => $"{DataDirectory}/StatisticalGlobal.json";
        public static string StatisticalProvincesPath => $"{DataDirectory}/StatisticalProvinces.json";
        public static string StatisticalPatientsPath => $"{DataDirectory}/StatisticalPatients.json";
        public static string TimelinesPath => $"{DataDirectory}/Timelines.json";
    }
}