using Crestron.SimplSharp;
using Crestron.SimplSharp.Reflection;
using proAV.Core;

namespace DLA_AldersgateReception {
	public class ControlSystem : ProAvControlSystem {
		public static ProjectConfig Configs;

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