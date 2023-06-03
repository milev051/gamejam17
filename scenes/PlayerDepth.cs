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
	[Export]
	public Resource playerStats;
	Vector3 moveVector;
	Camera camera;
	Harpoon harpoon;
	AnimationPlayer animPlayer;
	public Player_UI playerUI;
	PackedScene nextLvl;
	Timer timer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		playerUI = GetNode<Player_UI>("Player_UI");
		nextLvl = (PackedScene)ResourceLoader.Load("res://Scenes/SandLevel.tscn");
		camera = GetNode<Camera>("Camera");
		harpoon = camera.GetNode<Harpoon>("Harpoon");
		animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		timer = GetNode<Timer>("Timer");
		timer.Start();
		if (playerStats is PlayerStats stats) 
		{
			playerUI.healthBar.Value = stats.health;
			playerUI.collectedWaterBar.Value = stats.waterQt;
		}
		playerUI.oxygenBar.Value = 1000;
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
		if (playerUI.oxygenBar.Value <= 0.0f) 
		{
			playerUI.oxygenBar.Value = 1000;
			playerUI.healthBar.Value = 1000;
			if (playerStats is PlayerStats stats)
			{
				stats.health = (int)playerUI.healthBar.Value;
				stats.oxygenLvl = (int)playerUI.oxygenBar.Value;
				stats.waterQt = (int)playerUI.collectedWaterBar.Value;
			}
			GetTree().ChangeSceneTo(nextLvl);
		}
			
		//JEBITE SE SVI U PICKU MATERINU
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

		if(Input.IsActionPressed("ui_accept") && harpoon.canShoot)
		{
			harpoon.shoot();
		}

		MoveAndSlide(moveVector*speed);
	}

	public void damage(float damage)
	{
		playerUI.UpdateHealthBar(-100);
		animPlayer.Play("damage");
		if(playerUI.healthBar.Value <= 0.0f)
			GetTree().ReloadCurrentScene();
	}

	public void _on_Timer_timeout()
	{
		playerUI.UpdateOxygenBar(-10);
		GD.Print(playerUI.oxygenBar.Value);
		timer.Start();
	}
}