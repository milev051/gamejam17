using Godot;
using System;

public class LightEnemy : KinematicBody
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    Vector3 moveVector;
    float speed;
    float health;
    bool hit;
    PlayerDepth player;
    Sprite3D sprite;
    CSGSphere sphere;
    AnimationPlayer animPlayer;
    Timer timer;
    Area area;
    AudioStreamPlayer3D audioPlayer;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        player = GetParent().GetNode<PlayerDepth>("Player");
        sprite = GetNode<Sprite3D>("Sprite3D");
        sphere = GetNode<CSGSphere>("CSGSphere");
        animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        timer = GetNode<Timer>("Timer");
        area = GetNode<Area>("Area");
        audioPlayer = GetNode<AudioStreamPlayer3D>("AudioStreamPlayer3D");
        speed = 5.0f;
        sprite.Visible = false;
        sphere.Visible = true;
    }

    public override void _PhysicsProcess(float delta)
    {
        LookAt(player.Translation, new Vector3(0.0f, 1.0f, 1.0f));
        moveVector = (player.Translation - GlobalTransform.origin).Normalized();
        float distance = moveVector.DistanceTo(player.Translation);

        MoveAndSlide(moveVector * speed);
    }

    public void _on_Area_body_entered(Node body)
    {
        if (body.GetType() == typeof(PlayerDepth))
        {
            PlayerDepth bodyPlayer = (PlayerDepth)body;
            bodyPlayer.damage(10.0f);
            sprite.Visible = true;
            sphere.Visible = false;
            speed = 10.0f;
            timer.Start();
            audioPlayer.Play();
            area.SetDeferred("monitoring", false);
        }
    }

    public void _on_Timer_timeout()
    {
        speed = 5.0f;
        sprite.Visible = false;
        sphere.Visible = true;
        audioPlayer.Stop();
        area.SetDeferred("monitoring", true);
    }
}
