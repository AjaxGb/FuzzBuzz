using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physics2DMovement : MonoBehaviour {

	public Rigidbody2D rb;
	public float maxXSpeed = 350;
	public float maxXAccel = 500;
	public float maxYSpeed = 300;
	public float maxYAccel = 100;

	public bool impulseX;
	public bool impulseY;

	void Awake() {
		if (!rb) rb = GetComponent<Rigidbody2D>();
	}
	
	public void Move(float xAxis, float yAxis) {
		float targetXSpeed = xAxis * maxXSpeed;
		if (!impulseX) targetXSpeed *= Time.deltaTime;

		float deltaXSpeed = targetXSpeed - rb.velocity.x;

		float maxXAccelTick = maxXAccel;
		if (!impulseX) maxXAccelTick *= Time.deltaTime;
		rb.velocity += Vector2.right * Mathf.Clamp(deltaXSpeed, -maxXAccelTick, maxXAccelTick);

		if (yAxis != 0) {
			float targetYSpeed = yAxis * maxXSpeed;
			if (!impulseY) targetYSpeed *= Time.deltaTime;

			float deltaYSpeed = targetYSpeed - rb.velocity.y;

			float maxYAccelTick = maxYAccel;
			if (!impulseY) maxYAccelTick *= Time.deltaTime;
			rb.velocity += Vector2.up * Mathf.Min(deltaYSpeed, maxYAccelTick);
		}
	}
}
