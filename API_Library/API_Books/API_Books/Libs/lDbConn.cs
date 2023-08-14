namespace API_Books.Libs
{
    public class lDbConn
    {
        public string conStringDBLibrary()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var config = builder.Build();
            //var configPass = lc.decrypt(config.GetSection("configPass:passwordDB").Value.ToString());
            var configDB = config.GetSection("DbContextSettings:ConnectionString_DB").Value.ToString();

            //var repDB = configDB.Replace("{database}", database);
            //var repPass = repDB.Replace("{pass}", configPass);
            return configDB;
        }
    }
}
