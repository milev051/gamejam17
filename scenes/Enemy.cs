using Godot;
using System;

public class Enemy : KinematicBody
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    Vector3 moveVector;
    float speed;
    bool visible1, visible2;
    PlayerDepth player;
    Sprite3D sprite;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        player = GetParent().GetNode<PlayerDepth>("Player");
        sprite = GetNode<Sprite3D>("Sprite3D");
        speed = 2.0f;
        visible1 = true;
        visible2 = true;
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }

    public override void _PhysicsProcess(float delta)
    {
        LookAt(player.Translation, new Vector3(0.0f, 1.0f, 1.0f));
        moveVector = (player.Translation - GlobalTransform.origin).Normalized();
        float distance = moveVector.DistanceTo(player.Translation);
        if(distance > 20.0f)
            Visible = false;
        else if(distance < 20.0f && distance > 10.0f)
        {
            sprite.Modulate = new Color(1.0f, 1.0f, 1.0f, 0.5f);
            Visible = true;
        }
        else if(distance < 10.0f)
            sprite.Modulate = new Color(1.0f, 1.0f, 1.0f, 1.0f);

        MoveAndSlide(moveVector*speed);
    }
}
