namespace DLA_AldersgateReception.UserInterface.Listeners {
	public class UserInterfaceListeners {
		public static void Create(AldersgateReceptionUserInterface userInterface_) {
		//	userInterface_.Listeners.AddRange(new IListenerBase[] {
			userInterface_.Listeners.Add(new HomeListener(userInterface_));
			userInterface_.Listeners.Add(new HardKeysListener(userInterface_));
			userInterface_.Listeners.Add(new HeaderListener(userInterface_));
			userInterface_.Listeners.Add(new FooterListener(userInterface_));
			userInterface_.Listeners.Add(new PowerConfirmListener(userInterface_));
			userInterface_.Listeners.Add(new WelcomeListener(userInterface_));
		//	});
		}
	}
}