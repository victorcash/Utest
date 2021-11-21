using System;
using UnityEngine;

namespace StarterAssets
{
    public class UICanvasControllerInput : MonoBehaviour
    {
        public UIVirtualJoystick leftJoystick;
        public UIVirtualJoystick rightJoystick;
        public UIVirtualButton buttonA;
        public UIVirtualButton buttonB;
        private IPlayable iPlayable;

        public void Init()
        {
            leftJoystick.joystickOutputEvent.AddListener(VirtualMoveInput);
            rightJoystick.joystickOutputEvent.AddListener(VirtualLookInput);
            buttonA.buttonStateOutputEvent.AddListener(VirtualJumpInput);
            buttonB.buttonStateOutputEvent.AddListener(VirtualSprintInput);
            Services.GameStates.AddOnGameModeChangedListener(OnGameModeChanged);
        }

        private void OnGameModeChanged(GameMode gameMode)
        {
            if(gameMode == GameMode.Edit)
            {
                iPlayable = null;
            }
            if (gameMode == GameMode.Play)
            {
                iPlayable = Services.Element.GetActivePlayable();
            }
        }

        public void VirtualMoveInput(Vector2 virtualMoveDirection)
        {
            iPlayable?.JoyStickLeft(virtualMoveDirection);
        }

        public void VirtualLookInput(Vector2 virtualLookDirection)
        {
            iPlayable?.JoyStickRight(virtualLookDirection);
        }

        public void VirtualJumpInput(bool virtualJumpState)
        {
            iPlayable?.JoyPadA();
        }

        public void VirtualSprintInput(bool virtualSprintState)
        {
            iPlayable?.JoyPadB();
        }
    }
}
