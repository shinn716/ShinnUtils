using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shinn{

	public class RotateSelf : MonoBehaviour
    {

        public enum Type
        {
            Constant,
            Random,
            Noise
        }
        public void SetRotation()
        {
            switch (type)
            {
                case Type.Constant:
                    transform.Rotate(new Vector3(px, py, pz) * speed);
                    break;
                case Type.Random:
                    transform.Rotate(new Vector3(px, py, pz) * speed);
                    break;
                case Type.Noise:
                    transform.Rotate(new Vector3(
                                BaseValue.x + (speed * Mathf.PerlinNoise(Time.time * NoiseSpeed, noiseSeed.x)),
                                BaseValue.y + (speed * Mathf.PerlinNoise(Time.time * NoiseSpeed, noiseSeed.y)),
                                BaseValue.z + (speed * Mathf.PerlinNoise(Time.time * NoiseSpeed, noiseSeed.z))
                    ));
                    break;
                default:
                    break;
            }
        }
        public Type type;
        
        [SerializeField, Range(0, 3)] float speed = .5f;
        [SerializeField] bool FreezeRotX = false;
        [SerializeField] bool FreezeRotY = false;
        [SerializeField] bool FreezeRotZ = false;
        
        [Header("Constant")]
		[SerializeField, Range(-3, 3)] float px;
		[SerializeField, Range(-3, 3)] float py;
		[SerializeField, Range(-3, 3)] float pz;		

		[Header("Random")]
		[SerializeField] Vector2 RotatePxRange = Vector2.zero;
		[SerializeField] Vector2 RotatePyRange = Vector2.zero;
		[SerializeField] Vector2 RotatePzRange = Vector2.zero;

        [Header("Noise")]
        [SerializeField] Vector3 BaseValue = Vector3.zero;
        [SerializeField, Range(0, 1)] float NoiseSpeed = 1;

        private Vector3 noiseSeed;
        private Vector3 rotOrigin;

        private void Start()
        {
            rotOrigin = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z);

            if (type == Type.Random)
            {
                px = Random.Range(RotatePxRange.x, RotatePxRange.y);
                py = Random.Range(RotatePyRange.x, RotatePyRange.y);
                pz = Random.Range(RotatePzRange.x, RotatePzRange.y);
            }
            else if (type == Type.Noise)
                noiseSeed = new Vector3(Random.value, Random.value, Random.value);
        }

        private void FixedUpdate () {

            // Set rotation
            SetRotation();
            
            // Freeze rotation
            transform.localEulerAngles = FreezeRotX ? new Vector3(rotOrigin.x, transform.localEulerAngles.y, transform.localEulerAngles.z) : transform.localEulerAngles;
            transform.localEulerAngles = FreezeRotY ? new Vector3(transform.localEulerAngles.x, rotOrigin.y, transform.localEulerAngles.z) : transform.localEulerAngles;
            transform.localEulerAngles = FreezeRotZ ? new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, rotOrigin.z) : transform.localEulerAngles;
        }

    }
}
