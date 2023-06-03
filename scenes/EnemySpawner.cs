using Godot;
using System;

public class EnemySpawner : Spatial
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Export]
    PackedScene enemyScene;
    Enemy enemy;
    Timer timer;
    RandomNumberGenerator random = new RandomNumberGenerator();
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        random = new RandomNumberGenerator();
        random.Randomize();
        timer = GetNode<Timer>("Timer");
        timer.WaitTime = (Mathf.Abs((int)random.Randi()) % 10 + 5);
        GD.Print(timer.WaitTime);
        timer.Start();
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        
    }

    public void _on_Timer_timeout()
    {
        random = new RandomNumberGenerator();
        enemy = (Enemy)enemyScene.Instance();
        enemy.Scale = new Vector3(8.0f, 8.0f, 0.0f);
        GetParent().AddChild(enemy);
        enemy.Translation = this.GlobalTranslation;
        Translation += new Vector3(random.Randf()*20, 0.0f, 0.0f);
        timer.Start();
    }
}
