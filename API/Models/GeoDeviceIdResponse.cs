namespace API.Models
{
    public class GeoDeviceIdResponse
    {
        public List<GeoSystemRole>? SystemRoles { get; set; }
        public List<GeoSystemDetail>? SystemDetails { get; set; }

        public class GeoSystemRole
        {
            public string? Name { get; set; }
            public string? SystemId { get; set; }
            public List<string>? Roles { get; set; }
        }

        public class GeoDevice
        {
            public string? DeviceType { get; set; }
            public int SensorType { get; set; }
            public int NodeId { get; set; }
            public GeoVersionNumber? VersionNumber { get; set; }
            public int PairedTimestamp { get; set; }
            public string? PairingCode { get; set; }
            public bool UpgradeRequired { get; set; }
        }

        public class GeoSystemDetail
        {
            public string? Name { get; set; }
            public List<GeoDevice>? Devices { get; set; }
            public string? SystemId { get; set; }
        }

        public class GeoVersionNumber
        {
            public int Major { get; set; }
            public int Minor { get; set; }
        }
    }
}