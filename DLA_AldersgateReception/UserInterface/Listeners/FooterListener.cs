using System;
using System.Linq;
using Crestron.SimplSharpPro.CrestronThread;
using proAV.Core;
using proAV.Core.CustomEventArgs;
using proAV.Core.Devices.Displays;
using proAV.Core.Enumeration;
using proAV.Core.UserInterface.Listeners;
using proAV.Core.UserInterface.VisionToolsObjects;
using proAV.Core.Utilities;
using UserinterfaceJoinConstants;

namespace DLA_AldersgateReception.UserInterface.Listeners {
	public class FooterListener : ListenerBase<AldersgateReceptionUserInterface> {
		public AdvancedButton Raise;
		public AdvancedButton Lower;
		public FaderSlider Fader;
		private volatile bool _rampingActive;

		public FooterListener(AldersgateReceptionUserInterface userinterface_)
			: base(userinterface_) {
			Raise = Userinterface.Pages.GetControl<AdvancedButton>(Tsw560.FooterControls.SubpageId, Tsw560.FooterControls.VolumeRaise);
			Raise.HoldTime = 300;
			Lower = Userinterface.Pages.GetControl<AdvancedButton>(Tsw560.FooterControls.SubpageId, Tsw560.FooterControls.VolumeLower);
			Lower.HoldTime = 300;
			Fader = Userinterface.Pages.GetControl<FaderSlider>(Tsw560.FooterControls.SubpageId, Tsw560.FooterControls.VolumeFader);

			Fader.ProgramReceivedValueChange += FaderOnProgramReceivedValueChange;
			Fader.ProgramInstructedValueChange += FaderOnProgramInstructedValueChange;
			Raise.TouchAction += VolumeUp;
			Lower.TouchAction += VolumeDown;
		}

		private void FaderOnProgramReceivedValueChange(object sender_, UShortEventArgs uShortEventArgs_) {
			if (uShortEventArgs_.Value != Fader.Value) {
				UpdateDeviceVolume(uShortEventArgs_.Value);
				Fader.Value = uShortEventArgs_.Value;
			}
		}

		private void FaderOnProgramInstructedValueChange(object sender_, UShortEventArgs uShortEventArgs_) {
			if (uShortEventArgs_.Value != Fader.Value) {
				UpdateDeviceVolume(uShortEventArgs_.Value);
			}
		}

		public void VolumeUp(object _, TouchActionEventArgs touchArgs_) {
			switch (touchArgs_.Action) {
				case TouchType.Press:
					break;
				case TouchType.PressAndRelease:
					Fader.Value = StepUp(Fader.Value);
					UpdateDeviceVolume(Fader.Value);
					break;
				case TouchType.Hold:
					new Thread(x_ => VolumeRampingAction(StepUp), null);
					break;
				case TouchType.Release:
					_rampingActive = false;
					break;
			}
		}
		public void VolumeDown(object _, TouchActionEventArgs touchArgs_) {
			switch (touchArgs_.Action) {
				case TouchType.Press:
					break;
				case TouchType.PressAndRelease:
					Fader.Value = StepDown(Fader.Value);
					UpdateDeviceVolume(Fader.Value);
					break;
				case TouchType.Hold:
					new Thread(x_ => VolumeRampingAction(StepDown), null);
					break;
				case TouchType.Release:
					_rampingActive = false;
					break;
			}
		}

		private void UpdateDeviceVolume(ushort value_) {
			if (_rampingActive && value_ % 5 != 0) {
				return;
			}
			var display = ProAvControlSystem.Displays.FirstOrDefault() as Display;
			if (display != null) {
				display.Volume = value_;
			}
		}

		private object VolumeRampingAction(Func<ushort, ushort> stepFunction_) {
			try {
				_rampingActive = true;
				while (_rampingActive) {
					var currentValue = Fader.Value;
					var newValue = stepFunction_(currentValue);
					Fader.Value = newValue;
					UpdateDeviceVolume(newValue);
					Thread.Sleep(50);
					if (newValue >= 100 || newValue <= 0) {
						_rampingActive = false;
						break;
					}
				}
			}
			catch (Exception e) {
				this.Error("VolumeRampingAction", "Exiting ramping method with exception: {0}", e);
			}
			finally {
				UpdateDeviceVolume(Fader.Value);
			}

			return null;
		}

		private ushort StepUp(ushort currentValue_) {
			return (ushort)(currentValue_ >= 100 ? 100 : ++currentValue_);
		}

		private ushort StepDown(ushort currentValue_) {
			return (ushort)(currentValue_ <= 0 ? 0 : --currentValue_);
		}
	}
}