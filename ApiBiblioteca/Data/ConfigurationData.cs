namespace ApiBiblioteca.Data
{
    public class ConfigurationData
    {
            public ConfigurationData(string connectionString) => ConnectionString = connectionString;

            public string ConnectionString { get; set; }

    }
}
