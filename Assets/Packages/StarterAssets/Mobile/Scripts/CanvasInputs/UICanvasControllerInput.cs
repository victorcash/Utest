using UnityEngine;

namespace StarterAssets
{
    public class UICanvasControllerInput : MonoBehaviour
    {
        public UIVirtualJoystick leftJoystick;
        public UIVirtualJoystick rightJoystick;
        public UIVirtualButton buttonA;
        public UIVirtualButton buttonB;

        public GamePlayElementBehaviour playable;
        public IPlayable iPlayable;

        [Header("Output")]
        public StarterAssetsInputs starterAssetsInputs;

        private void Awake()
        {
            leftJoystick.joystickOutputEvent.AddListener(VirtualMoveInput);
            rightJoystick.joystickOutputEvent.AddListener(VirtualLookInput);
            buttonA.buttonStateOutputEvent.AddListener(VirtualJumpInput);
            buttonB.buttonStateOutputEvent.AddListener(VirtualSprintInput);

            iPlayable = (IPlayable)playable;
        }
        public void SetControlTarget(StarterAssetsInputs inputs)
        {
            starterAssetsInputs = inputs;
        }
        public void SetControlTarget(IPlayable iPlayable)
        {
            this.iPlayable = iPlayable;
        }
        public void VirtualMoveInput(Vector2 virtualMoveDirection)
        {
            starterAssetsInputs?.MoveInput(virtualMoveDirection);
            iPlayable?.JoyStickLeft(virtualMoveDirection);
        }

        public void VirtualLookInput(Vector2 virtualLookDirection)
        {
            starterAssetsInputs?.LookInput(virtualLookDirection);
            iPlayable?.JoyStickRight(virtualLookDirection);
        }

        public void VirtualJumpInput(bool virtualJumpState)
        {
            starterAssetsInputs?.JumpInput(virtualJumpState);
            iPlayable?.JoyPadA();
        }

        public void VirtualSprintInput(bool virtualSprintState)
        {
            starterAssetsInputs?.SprintInput(virtualSprintState);
            iPlayable?.JoyPadB();
        }
    }
}
