using Godot;
using System;

public class HealthBar : CanvasLayer
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    public int PlayerHealth;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }

    public void UpdateHealth(int value)
    {
        // Player cannot get any health if is dead or has maximum health
        if (0 < PlayerHealth && PlayerHealth <= 100)
            PlayerHealth += value;

        // Player cannot have more than 100 Health
        else if (PlayerHealth > 100)
            PlayerHealth = 100;

        // Check if game is over
        // if (PlayerHealth <= 0)
        //     Game over
    }
}
