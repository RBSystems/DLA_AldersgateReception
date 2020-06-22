using proAV.Core.CustomEventArgs;
using proAV.Core.UserInterface.Listeners;
using proAV.Core.UserInterface.VisionToolsObjects;
using UserinterfaceJoinConstants;

namespace DLA_AldersgateReception.UserInterface.Listeners {
	public class WelcomeListener : VtProPageListenerBase<AldersgateReceptionUserInterface> {
		private readonly BasicButton _start;
		
		public WelcomeListener(AldersgateReceptionUserInterface userinterface_) : base(userinterface_, Tsw560.Pages.Welcome) {
			_start = Userinterface.Pages.GetControl<BasicButton>(Tsw560.Pages.Welcome, Tsw560.WelcomeControls.Start);
			_start.PressAndRelease += StartOnPressAndRelease;
		}

		private void StartOnPressAndRelease(object sender_, TouchActionEventArgs touchActionEventArgs_) {
			Userinterface.Pages.PageControl(Tsw560.Pages.Home).Show();
			ControlSystem.Room.Power = true;
		}

		protected override void OnShow() { }
	}
}