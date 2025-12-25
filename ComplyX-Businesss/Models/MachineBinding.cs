namespace ComplyX_Businesss.Models
{
    public class MachineBinding
    {
        public int MachineBindingId { get; set; }
        public string MachineHash { get; set; }
        public string? CPUID { get; set; }
        public string?  MotherboardSerial {  get; set; }
        public string? MACAddresses { get; set; }
        public string? WindowsSID { get; set; }
        public DateTime FirstSeenAt { get; set; }
        public DateTime? LastSeenAt { get; set; }
        public int LicenseActivationId { get; set; }
        public virtual LicenseActivation? LicenseActivation { get; set; }
    }
}
