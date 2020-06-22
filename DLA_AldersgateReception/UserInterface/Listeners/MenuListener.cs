using proAV.Core.UserInterface.Listeners;
using UserinterfaceJoinConstants;

namespace DLA_AldersgateReception.UserInterface.Listeners {
	public class MenuListener : VtProPageListenerBase<AldersgateReceptionUserInterface> {
		public MenuListener(AldersgateReceptionUserInterface userinterface_)
			: base(userinterface_, Tsw560.Pages.Home) {
		}

		protected override void OnShow() {
		}
	}
}