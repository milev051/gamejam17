using Godot;
using System;

public class Player_UI : CanvasLayer
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
<<<<<<< HEAD
    public TextureProgress healthBar;
    public TextureProgress oxygenBar;
    public TextureProgress collectedWaterBar;
    int maximumBarLimit = 1000;
    bool isAlive = true;
    bool inWater = false;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        healthBar = GetNode<Control>("Control").GetNode<TextureProgress>("HealthBar");
        oxygenBar = GetNode<Control>("Control").GetNode<TextureProgress>("OxygenBar");
        collectedWaterBar = GetNode<Control>("Control").GetNode<TextureProgress>("CollectedWaterBar");
=======
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
>>>>>>> cc57a5cf11a56ec3b087b36db0fa4ebbf5f0145b
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
<<<<<<< HEAD
        if (isAlive && !inWater)
            oxygenBar += 2;

        if(healthBar.Value > 0)
            oxygenBar.Value = oxygenBar.Value + 2;
=======
        // Ako je na kopnu
        // If (nije u vodi i nije mrtav)
        if(HealthBar.Value > 0)
            OxygenBar.Value = OxygenBar.Value + 2;
>>>>>>> cc57a5cf11a56ec3b087b36db0fa4ebbf5f0145b

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
<<<<<<< HEAD
        if (0 < healthBar.Value && healthBar.Value <= maximumBarLimit)
            healthBar.Value = healthBar.Value + value;

        // Player cannot have more than 100 Health
        else if (healthBar.Value > maximumBarLimit)
            healthBar.Value = maximumBarLimit;

        // Check if game is over
        if (HealthBar.Value <= 0)
             isAlive = false;
=======
        if (0 < HealthBar.Value && HealthBar.Value <= MaximumBarLimit)
            HealthBar.Value = HealthBar.Value + value;

        // Player cannot have more than 100 Health
        else if (HealthBar.Value > MaximumBarLimit)
            HealthBar.Value = MaximumBarLimit;

        // Check if game is over
        // if (HealthBar.Value <= 0)
        //     Game over
>>>>>>> cc57a5cf11a56ec3b087b36db0fa4ebbf5f0145b
    }

    public void UpdateOxygenBar(int value)
    {
<<<<<<< HEAD
        // Player cannot get any health if is dead or has maximum
        if (0 < oxygenBar.Value && oxygenBar.Value <= maximumBarLimit)
            oxygenBar.Value = oxygenBar.Value + value;

        // Player cannot have more than maximum
        else if (oxygenBar.Value > maximumBarLimit)
            oxygenBar.Value = maximumBarLimit;

        // If there is no any oxygen decrease health
        if (oxygenBar.Value <= 0)
=======
        // Player cannot get any health if is dead or has maximum health
        if (0 < OxygenBar.Value && OxygenBar.Value <= MaximumBarLimit)
            OxygenBar.Value = OxygenBar.Value + value;

        // Player cannot have more than 100 Health
        else if (OxygenBar.Value > MaximumBarLimit)
            OxygenBar.Value = MaximumBarLimit;

        // If there is no any oxygen decrease health
        if (OxygenBar.Value <= 0)
>>>>>>> cc57a5cf11a56ec3b087b36db0fa4ebbf5f0145b
            UpdateHealthBar(value);
    }

    public void UpdateCollectedWaterBar(int value)
    {
<<<<<<< HEAD
        // Player cannot get any health if is dead or has maximum
        if (0 < collectedWaterBar.Value && collectedWaterBar.Value <= maximumBarLimit)
            collectedWaterBar.Value = collectedWaterBar.Value + value;

        // Player cannot have more than maximum
        else if (collectedWaterBar.Value > maximumBarLimit)
            collectedWaterBar.Value = maximumBarLimit;
=======
        // Player cannot get any health if is dead or has maximum health
        if (0 < CollectedWaterBar.Value && CollectedWaterBar.Value <= MaximumBarLimit)
            CollectedWaterBar.Value = CollectedWaterBar.Value + value;

        // Player cannot have more than 100 Health
        else if (CollectedWaterBar.Value > MaximumBarLimit)
            CollectedWaterBar.Value = MaximumBarLimit;
>>>>>>> cc57a5cf11a56ec3b087b36db0fa4ebbf5f0145b
    }
}
