using Godot;
using System;

public class Player_UI : CanvasLayer
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    public TextureProgress HealthBar;
    public TextureProgress OxygenBar;
    public TextureProgress CollectedWaterBar;
    int MaximumBarLimit = 1000;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        HealthBar = GetNode<Control>("Control").GetNode<TextureProgress>("HealthBar");
        OxygenBar = GetNode<Control>("Control").GetNode<TextureProgress>("OxygenBar");
        CollectedWaterBar = GetNode<Control>("Control").GetNode<TextureProgress>("CollectedWaterBar");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        // Ako je na kopnu
        // If (nije u vodi i nije mrtav)
        if(HealthBar.Value > 0)
            OxygenBar.Value = OxygenBar.Value + 2;

        if (Input.IsActionPressed("ui_up"))
        {
            UpdateOxygenBar(10);
        }
        if (Input.IsActionPressed("ui_down"))
        {
            UpdateOxygenBar(-10);
        }
    }

    public void UpdateHealthBar(int value)
    {
        // Player cannot get any health if is dead or has maximum health
        if (0 < HealthBar.Value && HealthBar.Value <= MaximumBarLimit)
            HealthBar.Value = HealthBar.Value + value;

        // Player cannot have more than 100 Health
        else if (HealthBar.Value > MaximumBarLimit)
            HealthBar.Value = MaximumBarLimit;

        // Check if game is over
        // if (HealthBar.Value <= 0)
        //     Game over
    }

    public void UpdateOxygenBar(int value)
    {
        // Player cannot get any health if is dead or has maximum health
        if (0 < OxygenBar.Value && OxygenBar.Value <= MaximumBarLimit)
            OxygenBar.Value = OxygenBar.Value + value;

        // Player cannot have more than 100 Health
        else if (OxygenBar.Value > MaximumBarLimit)
            OxygenBar.Value = MaximumBarLimit;

        // If there is no any oxygen decrease health
        if (OxygenBar.Value <= 0)
            UpdateHealthBar(value);
    }

    public void UpdateCollectedWaterBar(int value)
    {
        // Player cannot get any health if is dead or has maximum health
        if (0 < CollectedWaterBar.Value && CollectedWaterBar.Value <= MaximumBarLimit)
            CollectedWaterBar.Value = CollectedWaterBar.Value + value;

        // Player cannot have more than 100 Health
        else if (CollectedWaterBar.Value > MaximumBarLimit)
            CollectedWaterBar.Value = MaximumBarLimit;
    }
}
