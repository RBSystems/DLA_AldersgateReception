using proAV.Core.Config.VtPro;
using proAV.Core.Interfaces.UserInterface;
using proAV.Core.UserInterface;

namespace DLA_AldersgateReception.UserInterface {
	public class AldersgateReceptionUserInterface : AutoUserInterface {
		public AldersgateReceptionUserInterface(VtProEnvironment environmentConfig_, IUiSigHandler uiCore_)
			: base(environmentConfig_, uiCore_) {
		}
	}
}