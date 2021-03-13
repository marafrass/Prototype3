using UnityEngine;
using UnityEngine.EventSystems;

namespace AC
{

	/**
	 * This component acts as a click handler for Unity UI Buttons, and is added automatically by UISlot.
	 */
	public class UISlotClick : MonoBehaviour, IPointerClickHandler, ISelectHandler
	{

		#region Variables

		private AC.Menu menu;
		private MenuElement menuElement;
		private int slot;
		private bool reactToRightClick;

		#endregion


		#region UnityStandards

		private void Update ()
		{
			if (reactToRightClick && menuElement)
			{
				if (KickStarter.playerInput && KickStarter.playerInput.InputGetButtonDown ("InteractionB"))
				{
					if (KickStarter.playerMenus.IsEventSystemSelectingObject(gameObject))
					{
						menuElement.ProcessClick (menu, slot, MouseState.RightClick);
					}
				}
			}
		}


		/** Implementation of IPointerClickHandler */
		public void OnPointerClick (PointerEventData eventData)
		{
			if (reactToRightClick && menuElement)
			{
				if (eventData.button == PointerEventData.InputButton.Right)
				{
					menuElement.ProcessClick (menu, slot, MouseState.RightClick);
				}
			}
		}


		/** Implementation of ISelectHandler */
		public void OnSelect (BaseEventData eventData)
		{
			if (menuElement == null) return;

			if (menu.CanCurrentlyKeyboardControl (KickStarter.stateHandler.gameState))
			{
				KickStarter.sceneSettings.PlayDefaultSound (menuElement.hoverSound, false);
				KickStarter.eventManager.Call_OnMouseOverMenuElement (menu, menuElement, slot);
			}
		}

		#endregion


		#region PublicFunctions

		/**
		 * <summary>Syncs the component to a slot within a menu.</summary>
		 * <param name = "_menu">The Menu that the Button is linked to</param>
		 * <param name = "_element">The MenuElement within _menu that the Button is linked to</param>
		 * <param name = "_slot">The index number of the slot within _element that the Button is linked to</param>
		 * <param name = "_reactToRightClick">If True, the component will listen and react to right-clicks</param>
		 */
		public void Setup (AC.Menu _menu, MenuElement _element, int _slot, bool _reactToRightClick)
		{
			if (_menu == null)
			{
				return;
			}

			menu = _menu;
			menuElement = _element;
			slot = _slot;
			reactToRightClick = _reactToRightClick;
		}

		#endregion

	}

}