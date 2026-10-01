namespace DublinBikesWeb.Models
{
    public class DublinBikesData
    {
        public int number {get;set;}

        public string? contractName {get;set;}

        public string? name {get;set;}

        public string? address{get;set;}

        public PositionObject? position {get;set;}

        public bool banking {get;set;}

        public bool bonus {get;set;}

        public string? status {get;set;}

        public string? lastUpdate {get;set;}

        public bool connected {get;set;}

        public bool overflow {get;set;}

        public StandsObject? totalStands {get;set;}

        public StandsObject? mainStands { get; set; }

    }
}
