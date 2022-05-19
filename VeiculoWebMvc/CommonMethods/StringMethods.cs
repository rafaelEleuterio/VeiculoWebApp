namespace VeiculoWebMvc.CommonMethods
{
    public static class StringMethods
    {
        public static string GetInfoFromAppSettings(string nameOfInfo)
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build()
                .GetValue<string>(nameOfInfo);
        }
    }
}
