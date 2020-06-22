using Crestron.SimplSharp;
using Crestron.SimplSharp.Reflection;
using Crestron.SimplSharpPro.DM;
using proAV.Core;

namespace DLA_AldersgateReception {
	public class ControlSystem : ProAvControlSystem {
		public static ProjectConfig Configs;
		public static HdMd4x14kE Switcher { get; set; }
		public static RoomState Room { get; set; }

		public ControlSystem() : base(Assembly.GetExecutingAssembly(), 20) { }

		protected override void StartProgram() {
			CrestronConsole.PrintLine("GO!");
			Configs = new ProjectConfig();
			Configs.GetEmbeddedConfigs(Assembly.GetExecutingAssembly());
			Configs.UpdateConfigs();

			var initialiser = SystemInitialiser.Create();
			initialiser();
		}
	}
}