using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physics2DMovement : MonoBehaviour {

	public Rigidbody2D rb;
	public float maxXSpeed = 350;
	public float maxXAccel = 500;
	public float maxYSpeed = 300;
	public float maxYAccel = 100;

	void Awake() {
		if (!rb) rb = GetComponent<Rigidbody2D>();
	}
	
	public void Move(float xAxis, float yAxis) {
		float targetXSpeed = xAxis * maxXSpeed * Time.deltaTime;
		float deltaXSpeed = targetXSpeed - rb.velocity.x;

		float maxXAccelTick = maxXAccel * Time.deltaTime;
		rb.velocity += Vector2.right * Mathf.Clamp(deltaXSpeed, -maxXAccelTick, maxXAccelTick);

		if (yAxis != 0) {
			float targetYSpeed = yAxis * maxXSpeed * Time.deltaTime;
			float deltaYSpeed = targetYSpeed - rb.velocity.y;
			rb.velocity += Vector2.up * Mathf.Min(deltaYSpeed, maxYAccel * Time.deltaTime);
		}
	}
}
