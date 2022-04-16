using System;
using Godot;

public class ChunkGenerator {
	[Export] int RenderDistance = 2;
	// This array is to edit to fix seams along the edges of chunks.
	public float[,,] Data;

	public override void _Ready() {
		PackedScene ChunkScene = (PackedScene)(GD.Load("res://Scenes/Chunk.tscn"));

		Chunk.BaseNoise = new OpenSimplexNoise();
		Chunk.BaseNoise.Octaves = 0;
		Chunk.BaseNoise.Seed = WorldSeed;

		Chunk.ChunkScale = Vector3.One*5;
		Chunk.DivCount = 4;

		Chunk CurrentChunk;

		int FullRenderDistance = (RenderDistance * 2) + 1;
		int FullDataSize = (FullRenderDistance * (Chunk.DivCount + 1)) + 1;

		Data = new float[FullDataSize,FullDataSize,FullDataSize];

		// Here are some index variables we can use later.
		int Index1, Index2, Index3;

		for (int i = -RenderDistance; i <= RenderDistance; i++) {
			for (int k = -RenderDistance; k <= RenderDistance; k++) {
				for (int j = -RenderDistance; j <= RenderDistance; j++) {
					// This is to create a new chunk.
					CurrentChunk = (Chunk)ChunkScene.Instance();

					// Here, we'll set those index variables.
					Index1 = (i + RenderDistance) * (Chunk.DivCount + 1);
					Index2 = (j + RenderDistance) * (Chunk.DivCount + 1);
					Index3 = (k + RenderDistance) * (Chunk.DivCount + 1);

					// We loop through the chunk's data and add it to the big array.
					for (int x = 0; x < CurrentChunk.Data.GetLength(0); x++) {
						for (int y = 0; y < CurrentChunk.Data.GetLength(1); y++) {
							for (int z = 0; z < CurrentChunk.Data.GetLength(2); z++) {
								Data[Index1 + x,Index2 + y,Index3 + z] = CurrentChunk.Data[x,y,z];
							}
						}
					}
					// Optionally, you can name the chunks.
					// I name them by their position index, but it could really be anything.
					CurrentChunk.Name = $"Chunk {CurrentChunk.ChunkPosition/CurrentChunk.ChunkScale}"
					AddChild(CurrentChunk);
				}
			}
		}
	}

	public static void EditChunk(Vector3 EditPosition, float EditValue = 1f, float Range = 1f) {
		// You can edit the terrain by many means.
		int IndexA, IndexB, IndexC;
		IndexA = Godot.Mathf.RoundToInt(EditPosition.x+((Chunk.DivCount+1)*(RenderDistance+0.5f)));
		IndexB = Godot.Mathf.RoundToInt(EditPosition.y+((Chunk.DivCount+1)*(RenderDistance+0.5f)));
		IndexC = Godot.Mathf.RoundToInt(EditPosition.z+((Chunk.DivCount+1)*(RenderDistance+0.5f)));
		
		Vector3 MinRange = ((EditPosition-(Vector3.One*Radius))+((ChunkSubdivisionCount+Vector3.One)*(RenderDistance+0.5f))).Round();
		Vector3 MaxRange = ((EditPosition+(Vector3.One*Radius))+((ChunkSubdivisionCount+Vector3.One)*(RenderDistance+0.5f))).Round();

		Vector3 ChunkIndex = (EditPosition / Chunk.ChunkScale).Round();
		Chunk CurrentChunk = GetNodeOrNull<Chunk>();
		if (CurrentChunk is Chunk) {
			CurrentChunk.UpdateTerrain();
		}
	}
}
