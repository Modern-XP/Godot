Here's an explanation for how marching cubes will work for a basic implementation.

The first thing we need is a way to turn data (in the form of floats) into 3D points for a mesh.
This can be done with the following function:

public void GenerateMesh(float[,,] InputData, out Vector3[] Vertices, out int[] Indices) {
	int MaxX = InputData.GetLength(0)-1;
	int MaxY = InputData.GetLength(1)-1;
	int MaxZ = InputData.GetLength(2)-1;

	Vector3 LerpA, LerpB, LerpC;
	for (int i = 0; i < MaxX; i++) {
		for (int j = 0; j < MaxY; j++) {
			for (int k = 0; k < MaxZ; k++) {
			}
		}
	}
}
