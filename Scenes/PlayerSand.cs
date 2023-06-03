using Godot;
using System;

public class PlayerSand : KinematicBody
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	[Export]
	float speed;
	[Export]
	float mouseSens;
	[Export]
	Resource playerStats;
	float health;
	Vector3 moveVector;
	Camera camera;
	public Player_UI playerUI;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		playerUI = GetNode<Player_UI>("Player_UI");
		if (playerStats is PlayerStats stats) 
		{
			playerUI.healthBar.Value = stats.health;
			playerUI.oxygenBar.Value = stats.oxygenLvl;
			playerUI.collectedWaterBar.Value = stats.waterQt;

			GD.Print($"Water: {playerUI.collectedWaterBar.Value}");
			GD.Print($"HP: {playerUI.healthBar.Value}");
			GD.Print($"Oxygen: {playerUI.oxygenBar.Value}");
		}
		GD.Print("PLAYER_UI FOUND");
		camera = GetNode<Camera>("Camera");
		Input.MouseMode = Input.MouseModeEnum.Captured;
	}

	public override void _Input(InputEvent @event)
	{
		if(@event is InputEventMouseMotion mouseMotion)
		{
			camera.RotateX(Mathf.Deg2Rad(mouseMotion.Relative.y*mouseSens*-1));
			camera.RotationDegrees = new Vector3(Mathf.Clamp(camera.RotationDegrees.x, -75.0f, 75.0f), 0.0f, 0.0f);
			this.RotateY(Mathf.Deg2Rad(mouseMotion.Relative.x*mouseSens*-1));
		}
	}
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		if(Input.IsActionPressed("ui_cancel"))
			GetTree().Quit();
	}

	public override void _PhysicsProcess(float delta)
	{
		moveVector = new Vector3(0.0f, 0.0f, 0.0f);
		if(Input.IsActionPressed("ui_up"))
		{
			moveVector -= camera.GlobalTransform.basis.z;
			moveVector.y = 0.0f;
		}
		if(Input.IsActionPressed("ui_down"))
		{
			moveVector += camera.GlobalTransform.basis.z;
			moveVector.y = 0.0f;
		}
		if(Input.IsActionPressed("ui_right"))
			moveVector += camera.GlobalTransform.basis.x;
		if(Input.IsActionPressed("ui_left"))
			moveVector -= camera.GlobalTransform.basis.x;
		if(Input.IsActionPressed("up"))
			moveVector += camera.GlobalTransform.basis.y;
		if(Input.IsActionPressed("down"))
			moveVector -= camera.GlobalTransform.basis.y;
		moveVector.Normalized();

		MoveAndSlide(moveVector*speed);
	}
}
