using proAV.Core.UserInterface.Listeners;
using proAV.Core.UserInterface.VisionToolsObjects;
using UserinterfaceJoinConstants;

namespace DLA_AldersgateReception.UserInterface.Listeners {
	public class HeaderListener : ListenerBase<AldersgateReceptionUserInterface> {
		private readonly SimpleLabel _roomName;
		private readonly SimpleLabel _pageTitle;
		public HeaderListener(AldersgateReceptionUserInterface userinterface_) : base(userinterface_) {
			_roomName = Userinterface.Pages.GetControl<SimpleLabel>(Tsw560.HeaderControls.SubpageId, Tsw560.HeaderControls.RoomName);
			_pageTitle = Userinterface.Pages.GetControl<SimpleLabel>(Tsw560.HeaderControls.SubpageId, Tsw560.HeaderControls.PageTitle);

			_roomName.Set(Userinterface.CustomLabel);
			_pageTitle.Set("Wall Display Sources");
		}
	}
}