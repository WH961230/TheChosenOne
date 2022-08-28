using UnityEngine;

// This class is created for the example scene. There is no support for this script.
public class PlayerMovement : MonoBehaviour
{
	public float walkSpeed = 5f;        // The speed that the player will move at.
	public float runSpeed = 7.5f;        // The speed that the player will move at.

	Vector3 movement;                   // The vector to store the direction of the player's movement.
	Animator anim;                      // Reference to the animator component.
	Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
	float camRayLength = 100f;          // The length of the ray from the camera into the scene.

	private Transform gun, ball;

	void Awake()
	{
		// Set up references.
		anim = GetComponent<Animator>();
		playerRigidbody = GetComponent<Rigidbody>();
		gun = transform.Find("gun");
		ball = transform.Find("ball");
	}


	void FixedUpdate()
	{
		// Store the input axes.
		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");

		// Move the player around the scene.
		Move(h, v);

		// Turn the player to face the mouse cursor.
		Turning();

		GetComponent<CapsuleCollider>().enabled = !Input.GetMouseButton(1);
	}

	void Move(float h, float v)
	{
		// Set the movement vector based on the axis input.
		movement.Set(h, 0f, v);

		// Normalise the movement vector and make it proportional to the speed per second.
		movement = movement.normalized * (Input.GetKey(KeyCode.LeftShift)?runSpeed:walkSpeed) * Time.deltaTime;

		// Move the player to it's current position plus the movement.
		playerRigidbody.MovePosition(transform.position + movement);
	}

	void Turning()
	{
		// Create a ray from the mouse cursor on screen in the direction of the camera.
		Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

		// Create a RaycastHit variable to store information about what was hit by the ray.
		RaycastHit hit;

		// Perform the raycast and if it hits something on the floor layer...
		if (Physics.Raycast(camRay, out hit, camRayLength))
		{
			if (Vector3.Distance(hit.point, transform.position) < 2.2f)
				return;

			// Create a vector from the player to the point on the floor the raycast from the mouse hit.
			Vector3 playerToMouse = hit.point - gun.Find("muzzle").position;

			gun.localRotation = Quaternion.LookRotation(playerToMouse);
			gun.localRotation = Quaternion.Euler(gun.localRotation.eulerAngles.x, 0, 0);

			// Ensure the vector is entirely along the floor plane.
			playerToMouse.y = 0f;

			// Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
			Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

			// Set the player's rotation to this new rotation.
			playerRigidbody.MoveRotation(newRotation);
		}
	}
}
