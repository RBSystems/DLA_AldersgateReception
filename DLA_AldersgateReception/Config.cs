using Newtonsoft.Json;
using proAV.Core.Config;

namespace DLA_AldersgateReception {
	public class Config : ConfigDto {
		[JsonProperty("switcher")]
		public DeviceConfig Switcher { get; set; }
	}
}