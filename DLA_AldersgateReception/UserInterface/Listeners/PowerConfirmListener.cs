using System;
using proAV.Core.CustomEventArgs;
using proAV.Core.UserInterface.Listeners;
using proAV.Core.UserInterface.VisionToolsObjects;
using UserinterfaceJoinConstants;

namespace DLA_AldersgateReception.UserInterface.Listeners {
	public class PowerConfirmListener : VtProSubpageListenerBase<AldersgateReceptionUserInterface> {
		public AdvancedButton Confirm;
		public AdvancedButton Cancel;

		public PowerConfirmListener(AldersgateReceptionUserInterface userinterface_)
			: base(userinterface_, Tsw560.Pages.Home, Tsw560.HomeSubpages.PowerOffConfirm) {
			Confirm = Userinterface.Pages.GetControl<AdvancedButton>(Tsw560.PowerOffConfirmControls.SubpageId, Tsw560.PowerOffConfirmControls.Confirm);
			Cancel = Userinterface.Pages.GetControl<AdvancedButton>(Tsw560.PowerOffConfirmControls.SubpageId, Tsw560.PowerOffConfirmControls.Cancel);

			Confirm.PressAndRelease += ConfirmOnPressAndRelease;
			Cancel.PressAndRelease += CancelOnPressAndRelease;
		}

		private void ConfirmOnPressAndRelease(object sender_, TouchActionEventArgs touchActionEventArgs_) {
			var welcomePage = Userinterface.Pages.PageControl(Tsw560.Pages.Welcome);
			welcomePage.Show();
			Subpage.Hide();
			ControlSystem.Room.Power = false;
		}

		private void CancelOnPressAndRelease(object sender_, TouchActionEventArgs touchActionEventArgs_) {
			Subpage.Hide();
		}

		protected override void OnShow() {
		}

		protected override void OnHide() {
		}
	}
}