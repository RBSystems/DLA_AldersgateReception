using System;
using System.Collections.Generic;
using System.Linq;
using Crestron.SimplSharpPro.DM;
using DLA_AldersgateReception.UserInterface;
using DLA_AldersgateReception.UserInterface.Listeners;
using proAV.Core;
using proAV.Core.Devices.Displays;
using proAV.Core.Extensions;
using proAV.Core.Interfaces.API;
using proAV.Core.UserInterface;
using proAV.Core.Utilities;

namespace DLA_AldersgateReception {
	public class SystemInitialiser {
		public static Action Create() {
			var init = new SystemInitialiser();
			var initActions = new List<Action> {
				init.CreateTouchPanels,
				init.CreateDisplay,
				init.CreateSwitcher,
				init.CreateRoomState
			};

			var newActionQueue = new ActionQueue("SystemInitialiser", initActions);
			newActionQueue.AddFeedbackPath(new ConsolePercentageFeedback());
			newActionQueue.Start();
			return Initialise;
		}

		private static void Initialise() {
			ProAvControlSystem.CrestronNativeDevices.RegisterAllDevices();
			foreach (var display in ProAvControlSystem.Displays.OfType<IInitialise>()) {
				display.Initialise();
			}
			ProAvControlSystem.Userinterfaces.InitialiseAll();
		}

		private void CreateTouchPanels() {
			this.PrintFunctionName("CreateTouchPanels");
			if (ControlSystem.Configs.Tsw560 != null) {
				var uiSigHandlers = TouchpanelFactory.Create(ControlSystem.Configs.Config.Touchpanels);
				foreach (var uiSigHandler in uiSigHandlers) {
					var userInterface = new AldersgateReceptionUserInterface(ControlSystem.Configs.Tsw560, uiSigHandler);
					ProAvControlSystem.Userinterfaces.Add(userInterface);
				}
			}
			foreach (var userInterface in ProAvControlSystem.Userinterfaces.OfType<AldersgateReceptionUserInterface>()) {
				UserInterfaceListeners.Create(userInterface);
			}
		}

		private void CreateDisplay() {
			this.PrintFunctionName("CreateDisplay");
			var displayCollection = DisplayFactory.CreateDisplays(ControlSystem.Configs.Config.Displays);
			ProAvControlSystem.Displays.Add(displayCollection);
		}

		private void CreateSwitcher() {
			this.PrintFunctionName("CreateSwitcher");
			var config = ControlSystem.Configs.Config.Switcher;
			var switcher = new HdMd4x14kE((uint)config.Connection.Port, config.Connection.Address, ProAvControlSystem.ControlSystem);
			ProAvControlSystem.CrestronNativeDevices.Add(config.Id, switcher);
		}

		private void CreateRoomState() {
			this.PrintFunctionName("CreateRoomState");
			var newRoomState = new RoomState();
			ControlSystem.Room = newRoomState;
		}
	}
}