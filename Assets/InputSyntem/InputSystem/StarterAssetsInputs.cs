using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool aim;
		public bool shoot;
		public bool pause;
		public bool pickup;
		public bool openBook;
		public bool deletesave;
		public bool cheat1, cheat2, cheat3, cheat4;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}
		
		public void OnAim(InputValue value)
		{
			AimInput(value.isPressed);
		}
		
		public void OnShoot(InputValue value)
		{
			ShootInput(value.isPressed);
		}
        public void OnPause(InputValue value)
        {
            PauseInput(value.isPressed);
        }

        public void OnPickUp(InputValue value)
        {
            PickUpInput(value.isPressed);
        }
		
		public void OnBook(InputValue value)
        {
            BookInput(value.isPressed);
        }
		
		public void OnDS(InputValue value)
        {
            DSInput(value.isPressed);
        }
		public void OnCheat1(InputValue value)
        {
            OnCheat1Input(value.isPressed);
        }
		public void OnCheat2(InputValue value)
        {
            OnCheat2Input(value.isPressed);
        }
		public void OnCheat3(InputValue value)
        {
            OnCheat3Input(value.isPressed);
        }
		public void OnCheat4(InputValue value)
        {
            OnCheat4Input(value.isPressed);
        }
#endif


        public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}
		
		public void AimInput(bool newAimState)
		{
			aim = newAimState;
		}
		
		public void ShootInput(bool newShootState)
		{
			shoot = newShootState;
		}

		public void PauseInput(bool newPauseState)
		{
			pause = newPauseState;
		}

        public void PickUpInput(bool newPickUpState)
        {
            pickup = newPickUpState;
        }
		
		public void BookInput(bool newBookState)
        {
            openBook = newBookState;
        }
		
		public void DSInput(bool newDSState)
        {
            deletesave = newDSState;
        }
		public void OnCheat1Input(bool newCheat1State)
        {
            cheat1 = newCheat1State;
        }
		public void OnCheat2Input(bool newCheat2State)
        {
            cheat2 = newCheat2State;
        }
		public void OnCheat3Input(bool newCheat3State)
        {
            cheat3 = newCheat3State;
        }
		public void OnCheat4Input(bool newCheat4State)
        {
            cheat4 = newCheat4State;
        }

        public void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		public void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}