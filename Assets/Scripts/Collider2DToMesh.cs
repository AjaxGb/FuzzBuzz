using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[ExecuteInEditMode]
public class Collider2DToMesh : MonoBehaviour {
	PolygonCollider2D coll;
	MeshFilter meshFilter;

	void Start() {
		coll = GetComponent<PolygonCollider2D>();
		meshFilter = GetComponent<MeshFilter>();

		UpdateMesh();
	}

#if UNITY_EDITOR
	public bool updateMesh;

	void Update() {
		if (updateMesh) {
			UpdateMesh();
			updateMesh = false;
		}
	}
#endif

	public void UpdateMesh() {
		Mesh mesh = new Mesh();
		Vector2[] points = coll.points;
		Vector3[] vertices = new Vector3[points.Length];
		Vector2[] uv = new Vector2[points.Length];

		for (int i = 0; i < points.Length; i++) {
			vertices[i] = points[i];
			uv[i] = points[i];
		}

		Triangulator tr = new Triangulator(points);
		int[] triangles = tr.Triangulate();

		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.uv = uv;

		meshFilter.mesh = mesh;
	}
}