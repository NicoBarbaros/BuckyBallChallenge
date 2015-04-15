using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

namespace playerController
{

	[RequireComponent(typeof (PlayerController))]
	public class Platformer2DController : MonoBehaviour
	{
		private PlayerController mCharacter;
		private bool mJump;

		public string jumpKey;
		public string moveKeys;
		
		private void Awake()
		{
			mCharacter = GetComponent<PlayerController>();
		}
		
		
		private void Update()
		{
			if (!mJump)
			{
				// Read the jump input in Update so button presses aren't missed.

				mJump = CrossPlatformInputManager.GetButtonDown(jumpKey);

			}
		}
		
		
		private void FixedUpdate()
		{
			// Read the inputs.
			bool crouch = Input.GetKey(KeyCode.LeftControl);
			float h = CrossPlatformInputManager.GetAxis(moveKeys);
			Debug.Log ("2e");
			// Pass all parameters to the character control script.
			mCharacter.Move(h, crouch, mJump);
			mJump = false;
		}
	}
}