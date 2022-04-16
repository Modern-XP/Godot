using System;
using Godot;

public class Chunk : Spatial {
	public static OpenSimplexNoise Noise;
	public static Vector3 ChunkScale;

	public Vector3 ChunkPosition;
	public float[,,] Data;
	public int DivisionCount;

	public MeshInstance ChunkMesh;
	public StaticBody ChunkStaticBody;
	public CollisionShape ChunkCollisionShape;

	public Chunk(Vector3 Position, int Divisions) {
		ChunkPosition = Position;
		DivisionCount = Divisions;
		Data = new float[DivisionCount+2,DivisionCount+2,DivisionCount+2];
	}

	public override void _EnterTree() {
		
	}

	public override void _Ready() {
		Vector3 NoisePosition;
		Vector3 ChunkCubeDimensions = Vector3(
			Data.GetLength(0)-1,
			Data.GetLength(1)-1,
			Data.GetLength(2)-1
		);
		for (int i = 0; i < Data.GetLength(0); i++) {
			for (int j = 0; j < Data.GetLength(1); j++) {
				for (int k = 0; k < Data.GetLength(2); k++) {
				NoisePosition = (new Vector3(i,j,k) / ChunkCubeDimensions) + ChunkPosition;
				Data[i,j,k] = Noise.GetValue3dv(NoisePosition * ChunkScale);
				}
			}
		}
	}

	public void UpdateTerrain(float[,,] NewData) {
		for (int i = 0; i < Data.GetLength(0); i++) {
			for (int j = 0; j < Data.GetLength(1); j++) {
				for (int k = 0; k < Data.GetLength(2); k++) {
				Data[i,j,k] = NewData[i,j,k];
				}
			}
		}
		UpdateMeshes();
	}

	public void UpdateMeshes() {
		Godot.Collections.Array MeshData = new Godot.Collections.Array();
		MeshData.Resize(9);
		MeshData[0] = Vertices; // Vertices //
		MeshData[8] = Triangles; // Indices //

		if (((Vector3[])MeshData[0]).LongLength <= 0) { return; }

		ArrayMesh ChunkArrMesh = new ArrayMesh();
		ChunkArrMesh.AddSurfaceFromArrays(Mesh.PrimitiveType.Triangles, MeshData);

		ChunkCollider.Shape = ChunkArrMesh.CreateTrimeshShape();
		ChunkMesh.Mesh = ChunkArrMesh;
		ChunkMesh.SetSurfaceMaterial(0, ChunkMat);
	}
}
