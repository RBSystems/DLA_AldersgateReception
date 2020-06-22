using System;
using System.Linq;
using Crestron.SimplSharpPro;
using Crestron.SimplSharpPro.UI;
using proAV.Core.CustomEventArgs;
using proAV.Core.Enumeration;
using proAV.Core.UserInterface.Listeners;
using proAV.Core.UserInterface.VisionToolsObjects;
using Tsw560 = UserinterfaceJoinConstants.Tsw560;

namespace DLA_AldersgateReception.UserInterface.Listeners {
	public class HardKeysListener : ListenerBase<AldersgateReceptionUserInterface> {
		private readonly BasicButton _power;
		private readonly BasicButton _home;
		private readonly BasicButton _raise;
		private readonly BasicButton _lower;

		public HardKeysListener(AldersgateReceptionUserInterface userinterface_)
			: base(userinterface_) {
			var touchPanel = userinterface_.Touchpanel as TswX60BaseClass;
			if (touchPanel != null) {
				touchPanel.OnlineStatusChange += TouchPanelOnOnlineStatusChange;
				SetHardKeys(touchPanel);
				_power = new BasicButton(userinterface_.UiCore.JoinCollection) {
					PressJoinNumber = 2001
				};
				_home = new BasicButton(userinterface_.UiCore.JoinCollection) {
					PressJoinNumber = 2002
				};
				_raise = new BasicButton(userinterface_.UiCore.JoinCollection) {
					PressJoinNumber = 2004
				};
				_lower = new BasicButton(userinterface_.UiCore.JoinCollection) {
					PressJoinNumber = 2005
				};

				_power.TouchAction += PowerOnTouchAction;
				_home.TouchAction += HomeOnTouchAction;
				_raise.TouchAction += RaiseOnTouchAction;
				_raise.HoldTime = 300;
				_lower.TouchAction += LowerOnTouchAction;
				_lower.HoldTime = 300;
			}
		}

		private void TouchPanelOnOnlineStatusChange(GenericBase currentDevice_, OnlineOfflineEventArgs args_) {
			SetHardKeys(currentDevice_);
		}

		private void SetHardKeys(GenericBase device_) {
			var touchPanel = device_ as TswX60BaseClass;
			if (touchPanel != null) {
				touchPanel.ExtenderHardButtonReservedSigs.Use();
				touchPanel.ExtenderHardButtonReservedSigs.Brightness.UShortValue = ushort.MaxValue;
				touchPanel.ExtenderHardButtonReservedSigs.TurnButton1BackLightOn();
				touchPanel.ExtenderHardButtonReservedSigs.TurnButton2BackLightOn();
				touchPanel.ExtenderHardButtonReservedSigs.TurnButton3BackLightOff();
				touchPanel.ExtenderHardButtonReservedSigs.TurnButton4BackLightOn();
				touchPanel.ExtenderHardButtonReservedSigs.TurnButton5BackLightOn();
			}
		}

		private void PowerOnTouchAction(object sender_, TouchActionEventArgs touchActionEventArgs_) {
			switch (touchActionEventArgs_.Action) {
				case TouchType.PressAndRelease: {
						var homePage = Userinterface.Pages.PageControl(Tsw560.Pages.Home);
						if (homePage != null) {
							if (ControlSystem.Room.Power) {
								var powerConfirmationPage = Userinterface.Pages.GetSubPageInstanceControl(Tsw560.Pages.Home, Tsw560.HomeSubpages.PowerOffConfirm);
								powerConfirmationPage.Show();
							}
						}
					}
					break;
			}
		}

		private void HomeOnTouchAction(object sender_, TouchActionEventArgs touchActionEventArgs_) {
			switch (touchActionEventArgs_.Action) {
				case TouchType.PressAndRelease: {
						var powerConfirmationPage = Userinterface.Pages.GetSubPageInstanceControl(Tsw560.Pages.Home, Tsw560.HomeSubpages.PowerOffConfirm);
						powerConfirmationPage.Hide();
						if (!ControlSystem.Room.Power) {
							Userinterface.Pages.PageControl(Tsw560.Pages.Home).Show();
							ControlSystem.Room.Power = true;
						}
					}
					break;
			}
		}

		private FooterListener Footer {
			get { return Userinterface.Listeners.FirstOrDefault(x_ => x_.GetType() == typeof (FooterListener)) as FooterListener; }
		}

		private void RaiseOnTouchAction(object sender_, TouchActionEventArgs touchActionEventArgs_) {
			Footer.VolumeUp(sender_, touchActionEventArgs_);
		}

		private void LowerOnTouchAction(object sender_, TouchActionEventArgs touchActionEventArgs_) {
			Footer.VolumeDown(sender_, touchActionEventArgs_);
		}
	}
}