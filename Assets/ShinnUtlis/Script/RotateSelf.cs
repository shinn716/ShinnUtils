using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShinnUtil{

	public class RotateSelf : MonoBehaviour {

		[Header("Rotate Self Setting")]
		[SerializeField] float px;
		[SerializeField] float py;
		[SerializeField] float pz;
		[SerializeField] float speed = .5f;

		[Header("Random Rotate")]
		[SerializeField] bool RandSt = false;
		[SerializeField] Vector2 RotatePxRange;
		[SerializeField] Vector2 RotatePyRange;
		[SerializeField] Vector2 RotatePzRange;

		[Header("Noise Rotate")]
		[SerializeField] bool NoiseSt = false;

		float NoiseSeed1;
		float NoiseSeed2;
		float NoiseSeed3;

		void Start(){
			if(RandSt){
				px = Random.Range (RotatePxRange.x, RotatePxRange.y);
				py = Random.Range (RotatePyRange.x, RotatePyRange.y);
				pz = Random.Range (RotatePzRange.x, RotatePzRange.y);
			}

			if (NoiseSt) {
				NoiseSeed1 = Random.value;
				NoiseSeed2 = Random.value;
				NoiseSeed3 = Random.value;
			}
		}

		void FixedUpdate () {

			if (NoiseSt) {
				transform.Rotate (new Vector3 (	px * Mathf.PerlinNoise (Time.time * speed, NoiseSeed1),
												py * Mathf.PerlinNoise (Time.time * speed, NoiseSeed2),
												pz * Mathf.PerlinNoise (Time.time * speed, NoiseSeed3)
				));
			}
			else
				transform.Rotate (new Vector3(px, py, pz) * speed);

		}

	}
}