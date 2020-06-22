using System;
using Crestron.SimplSharpPro.DM;
using proAV.Core;
using proAV.Core.CustomEventArgs;
using proAV.Core.Enumeration;
using proAV.Core.Interfaces.API;
using proAV.Core.UserInterface.Listeners;
using proAV.Core.UserInterface.VisionToolsObjects;
using UserinterfaceJoinConstants;

namespace DLA_AldersgateReception.UserInterface.Listeners {
	public class HomeListener : VtProPageListenerBase<AldersgateReceptionUserInterface> {
		private readonly AdvancedButton _laptop;
		private readonly AdvancedButton _pc;
		private readonly AdvancedButton _wireless;

		private HdMd4x14kE Switcher {
			get { return ProAvControlSystem.CrestronNativeDevices[ControlSystem.Configs.Config.Switcher.Id] as HdMd4x14kE; }
		}

		public HomeListener(AldersgateReceptionUserInterface userinterface_)
			: base(userinterface_, Tsw560.Pages.Home) {
			_laptop = Userinterface.Pages.GetObjectControl<AdvancedButton>(Tsw560.Pages.Home, Tsw560.HomeControls.Laptop);
			_pc = Userinterface.Pages.GetObjectControl<AdvancedButton>(Tsw560.Pages.Home, Tsw560.HomeControls.PC);
			_wireless = Userinterface.Pages.GetObjectControl<AdvancedButton>(Tsw560.Pages.Home, Tsw560.HomeControls.Wireless);

			DrawButton(_laptop, 1);
			DrawButton(_pc, 2);
			DrawButton(_wireless, 3);

			_laptop.TouchAction += SourceTouchAction;
			_pc.TouchAction += SourceTouchAction;
			_wireless.TouchAction += SourceTouchAction;
		}

		private void SourceTouchAction(object sender_, TouchActionEventArgs touchActionEventArgs_) {
			if (touchActionEventArgs_.Action == TouchType.PressAndRelease) {
				var button = sender_ as AdvancedButton;
				if (button != null) {
					var routeAction = button.UserSpecifiedObject as Action<uint>;
					if (routeAction != null) {
						routeAction((uint) button.UserSpecifiedNumber);
						_laptop.Selected = button.UserSpecifiedNumber == _laptop.UserSpecifiedNumber;
						_pc.Selected = button.UserSpecifiedNumber == _pc.UserSpecifiedNumber;
						_wireless.Selected = button.UserSpecifiedNumber == _wireless.UserSpecifiedNumber;
					}
				}
			}
		}

		private void DrawButton(IUserSpecifiedValues button_, int inputId_) {
			button_.UserSpecifiedObject = new Action<uint>(RouteSource);
			button_.UserSpecifiedNumber = inputId_;
		}

		private void RouteSource(uint inputId_) {
			var videoInput = Switcher.Inputs[inputId_];
			if (Switcher.Outputs[1].VideoOutFeedback != videoInput) {
				Switcher.Outputs[1].VideoOut = videoInput;
			}
		}

		protected override void OnShow() { }
	}
}