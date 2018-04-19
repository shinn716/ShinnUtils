using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter))]
public class ShMeshSkin : MonoBehaviour {

	Vector3[] vertices;
	bool draw = false;

	public Vector3[] GetMeshVertices{
		get { return vertices;}
	}

	void Awake () {

		Mesh mesh = GetComponent<MeshFilter> ().mesh;
		vertices = mesh.vertices;
		
	}

	void Start(){
		draw = true;

	}

	void OnDrawGizmos(){
		if (draw) {
			for (int i = 0; i < vertices.Length; i++) {
				Gizmos.DrawSphere (vertices [i], .1f);
				Gizmos.color = Color.red;
			}
		}
	}

}
