using proAV.Core.UserInterface.Listeners;
using UserinterfaceJoinConstants;

namespace DLA_AldersgateReception.UserInterface.Listeners {
	public class WelcomeListener : VtProPageListenerBase<AldersgateReceptionUserInterface> {
		public WelcomeListener(AldersgateReceptionUserInterface userinterface_) : base(userinterface_, Tsw560.Pages.Welcome) {
		}

		protected override void OnShow() {
		}
	}
}