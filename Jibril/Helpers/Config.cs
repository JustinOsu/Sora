using Shared.Helpers;

namespace Jibril.Helpers
{
    public class Config : IConfig
    {
        public Shared.Helpers.MySql MySql { get; set; }
        public Redis Redis { get; set; }
        public Osu Osu { get; set; }
        public Server Server { get; set; }
    }

    public struct Server
    {
        public short Port;
    }
    
    public struct Osu
    {
        public string OsuAPIKey { get; set; }
    }
}