namespace PalTracker.ConfigurationEntities
{
    public class CloudFoundryEnvironment
    {
        public string Port { get; }
        public string MemoryLimit { get; }
        public string CfInstanceIndex { get; }
        public string CfInstanceAddr { get; }

        public CloudFoundryEnvironment(string port, string memoryLimit, string cfInstanceIndex, string cfInstanceAddr)
        {
            Port = port;
            MemoryLimit = memoryLimit;
            CfInstanceIndex = cfInstanceIndex;
            CfInstanceAddr = cfInstanceAddr;
        }
    }
}
