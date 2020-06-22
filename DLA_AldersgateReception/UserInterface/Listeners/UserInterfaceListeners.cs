using proAV.Core.Interfaces.UserInterface;

namespace DLA_AldersgateReception.UserInterface.Listeners {
	public class UserInterfaceListeners {
		public static void Create(AldersgateReceptionUserInterface userInterface_) {
			userInterface_.Listeners.AddRange(new IListenerBase[] {
				new MenuListener(userInterface_),
				new HardKeysListener(userInterface_),
				new FooterListener(userInterface_),
				new PowerConfirmListener(userInterface_),
				new WelcomeListener(userInterface_)
			});
		}
	}
}