using Godot;
using System;

public class PlayerDepth : KinematicBody
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	[Export]
	float speed;
	[Export]
	float mouseSens;
	float health;
	Vector3 moveVector;
	Camera camera;
	Harpoon harpoon;
	AnimationPlayer animPlayer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		camera = GetNode<Camera>("Camera");
		harpoon = camera.GetNode<Harpoon>("Harpoon");
		animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		health = 100.0f;
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

		if(Input.IsActionPressed("ui_accept"))
		{
			harpoon.shoot();
		}

		MoveAndSlide(moveVector*speed);
	}

	public void damage(float damage)
	{
		health -= damage;
		animPlayer.Play("damage");
		if(health < 0.0f)
			GetTree().ReloadCurrentScene();
	}
}
