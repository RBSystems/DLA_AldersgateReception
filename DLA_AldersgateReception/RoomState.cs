using System.Linq;
using proAV.Core;
using proAV.Core.Enumeration;
using proAV.Core.Interfaces.Device.Agnostic;

namespace DLA_AldersgateReception {
	public class RoomState {
		public bool Power {
			get { return _power; }
			set {
				if (value) {
					PowerOn();
				}
				else {
					PowerOff();
				}
			}
		}
		private bool _power;

		private void PowerOn() {
			_power = true;
			var display = ProAvControlSystem.Displays.FirstOrDefault();
			if (display != null) {
				display.Power = true;
				display.InputSource = (int)InputSourceCatalog.HDMI1;
				var volumeControl = display as IVolume;
				if (volumeControl != null) {
					volumeControl.Volume = 25;
				}
			}
		}

		private void PowerOff() {
			_power = false;
			var display = ProAvControlSystem.Displays.FirstOrDefault();
			if (display != null) {
				display.Power = false;
			}
		}
	}
}