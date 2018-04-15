using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter))]
public class ShSkin : MonoBehaviour {

	Vector3[] vertices;

	public Vector3[] GetMeshVertices{
		get { return vertices;}
	}

	void Awake () {
		Mesh mesh = GetComponent<MeshFilter> ().mesh;
		vertices = mesh.vertices;
	}

}
