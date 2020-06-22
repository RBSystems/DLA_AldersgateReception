using proAV.Core.Attributes;
using proAV.Core.Config.VtPro;
using proAV.Core.Framework;

namespace DLA_AldersgateReception {
	public class ProjectConfig : ConfigContainer {
		[ConfigName("config.json")]
		public Config Config { get; set; }

		[ConfigName("Tsw560.json")]
		public VtProEnvironment Tsw560 { get; set; }
	}
}