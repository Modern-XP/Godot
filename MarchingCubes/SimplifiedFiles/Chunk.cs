using System;
using Godot;

public class Chunk : Spatial {
	// First, a chunk needs some variables.
	public static OpenSimplexNoise Noise;
	public static Vector3 ChunkScale;
	
	
	public override void _Ready() {
	}
	
	public void Update Meshes() {}
	
	public void UpdateTerrain() {}
}
