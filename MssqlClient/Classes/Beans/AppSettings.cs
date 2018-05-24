using System.Configuration;
namespace MssqlClient.Classes.Beans
{
    class AppSettings
    {
        Configuration _config;
        public AppSettings()
        {
            _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        }
        public string GetConnectionString(string key)
        {
            return _config.ConnectionStrings.ConnectionStrings[key].ConnectionString;
        }
        public void SaveConnectionString(string key, string value)
        {
            _config.ConnectionStrings.ConnectionStrings[key].ConnectionString = value;
            _config.ConnectionStrings.ConnectionStrings[key].ProviderName = "System.Data.EntityClient";
            _config.Save(ConfigurationSaveMode.Modified);
        }
    }
}
