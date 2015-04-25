namespace AssemblyLine.Common.Configuration
{
    public interface IConfigurationProvider
    {
        string Get(string name);
    }
}