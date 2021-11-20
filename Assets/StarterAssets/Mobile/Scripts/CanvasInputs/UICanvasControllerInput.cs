using UnityEngine;

namespace StarterAssets
{
    public class UICanvasControllerInput : MonoBehaviour
    {
        public UIVirtualJoystick leftJoystick;
        public UIVirtualJoystick rightJoystick;
        public UIVirtualButton buttonA;
        public UIVirtualButton buttonB;

        [Header("Output")]
        public StarterAssetsInputs starterAssetsInputs;

        private void Awake()
        {
            leftJoystick.joystickOutputEvent.AddListener(VirtualMoveInput);
            rightJoystick.joystickOutputEvent.AddListener(VirtualLookInput);
            buttonA.buttonStateOutputEvent.AddListener(VirtualJumpInput);
            buttonB.buttonStateOutputEvent.AddListener(VirtualSprintInput);
        }
        public void SetControlTarget(StarterAssetsInputs inputs)
        {
            starterAssetsInputs = inputs;
        }
        public void VirtualMoveInput(Vector2 virtualMoveDirection)
        {
            starterAssetsInputs?.MoveInput(virtualMoveDirection);
        }

        public void VirtualLookInput(Vector2 virtualLookDirection)
        {
            starterAssetsInputs?.LookInput(virtualLookDirection);
        }

        public void VirtualJumpInput(bool virtualJumpState)
        {
            starterAssetsInputs?.JumpInput(virtualJumpState);
        }

        public void VirtualSprintInput(bool virtualSprintState)
        {
            starterAssetsInputs?.SprintInput(virtualSprintState);
        }
    }
}
