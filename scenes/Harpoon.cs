using Godot;
using System;

public class Harpoon : Spatial
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    Sprite sprite;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        sprite = GetNode<CanvasLayer>("CanvasLayer").GetNode<Control>("Control").GetNode<Sprite>("Sprite");
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public void shoot()
    {
        sprite.Modulate = new Color(0.0f, 0.0f, 0.0f, 1.0f);
    }
}
