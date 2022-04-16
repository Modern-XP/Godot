using System;
using Godot;

public class ChunkGenerator {
	[Export] int RenderDistance = 2;

	public override void _Ready() {
		PackedScene ChunkScene = (PackedScene)(GD.Load("res://Scenes/Chunk.tscn"));

		Chunk.BaseNoise = new OpenSimplexNoise();
		Chunk.BaseNoise.Octaves = 0;
		Chunk.BaseNoise.Seed = WorldSeed;

		Chunk.ChunkScale = Vector3.One*5;
		Chunk.DivCount = 4;

		Chunk CurrentChunk;

		for (int i = 0; i < ((RenderDistance * 2) + 1); i++) {
			for (int k = 0; k < ((RenderDistance * 2) + 1); k++) {
				for (int j = 0; j < ((RenderDistance * 2) + 1); j++) {
					CurrentChunk = (Chunk)ChunkScene.Instance();
				}
			}
		}
	}
}
