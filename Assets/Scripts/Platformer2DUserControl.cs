using UnityEngine;

[RequireComponent(typeof(PlatformerCharacter2D))]
public class Platformer2DUserControl : MonoBehaviour 
{
	private PlatformerCharacter2D character;
    private bool jump;
	private bool warp;
	private float warpDelay = 1f;
	private float timeSinceWarp;
	
	void Awake()
	{
		character = GetComponent<PlatformerCharacter2D>();
		timeSinceWarp = warpDelay;
	}

    void Update ()
    {
        // Read the jump input in Update so button presses aren't missed.
		timeSinceWarp += Time.deltaTime;
#if CROSS_PLATFORM_INPUT
        if (CrossPlatformInput.GetButtonDown("Jump"))
		{
			jump = true;
		}
		if (CrossPlatformInput.GetButtonDown("Warp") && timeSinceWarp > warpDelay)
		{
			warp = true;
			timeSinceWarp = 0f;
		}
#else
		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
		}
		if (Input.GetButtonDown("Warp") && timeSinceWarp > warpDelay)
		{
			warp = true;
			timeSinceWarp = 0f;
		}
#endif

    }

	void FixedUpdate()
	{
		// Read the inputs.
		bool crouch = Input.GetKey(KeyCode.LeftControl);
		#if CROSS_PLATFORM_INPUT
		float h = CrossPlatformInput.GetAxis("Horizontal");
		#else
		float h = Input.GetAxis("Horizontal");
		#endif

		// Pass all parameters to the character control script.
		character.Move( h, crouch, jump, warp );

        // Reset the jump input once it has been used.
		jump = false;
		warp = false;
	}
}
