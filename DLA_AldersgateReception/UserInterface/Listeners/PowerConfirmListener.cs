using proAV.Core.UserInterface.Listeners;
using UserinterfaceJoinConstants;

namespace DLA_AldersgateReception.UserInterface.Listeners {
	public class PowerConfirmListener : VtProSubpageListenerBase<AldersgateReceptionUserInterface> {
		public PowerConfirmListener(AldersgateReceptionUserInterface userinterface_) 
			: base(userinterface_, Tsw560.Pages.Home, Tsw560.HomeSubpages.PowerOffConfirm){
		}

		protected override void OnShow() {
		}

		protected override void OnHide() {
		}
	}
}