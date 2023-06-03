using Godot;
using System;

public class Plant : Area
{
	[Export]
	public Resource plantStats;

	public int waterQt;
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (plantStats is PlantStats stats)
		{
			waterQt = stats.waterQt;
			GD.Print($"Starting waterQt of plant: {waterQt}");
		}
	}

	private void _on_Plant_body_entered(object body)
	{
		if (body.GetType() == typeof(PlayerSand))
		{
			GD.Print("Player watered the plant");
			if (plantStats is PlantStats stats)
			{
				waterQt += 5;
				stats.waterQt = waterQt;
				GD.Print($"New waterQt of plant: {waterQt}");
			}
		}
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
