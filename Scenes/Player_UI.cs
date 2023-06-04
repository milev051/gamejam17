using Godot;
using System;

public class Player_UI : CanvasLayer
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
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
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        // if (isAlive && !inWater)
        // 	oxygenBar.Value += 2;

        // if(healthBar.Value > 0)
        // 	oxygenBar.Value = oxygenBar.Value + 2;
    }

    public void UpdateHealthBar(int value)
    {
        // Player cannot get any health if is dead or has maximum health
        if (0 < healthBar.Value && healthBar.Value <= maximumBarLimit)
            healthBar.Value = healthBar.Value + value;

        // Player cannot have more than 100 Health
        if (healthBar.Value > maximumBarLimit)
            healthBar.Value = maximumBarLimit;
        if (oxygenBar.Value > maximumBarLimit)
            oxygenBar.Value = maximumBarLimit;
        if (collectedWaterBar.Value > maximumBarLimit)
            collectedWaterBar.Value = maximumBarLimit;

        // Check if game is over
        if (healthBar.Value <= 0)
            isAlive = false;
    }

    public void UpdateOxygenBar(int value)
    {
        // Player cannot get any health if is dead or has maximum
        if (0 <= oxygenBar.Value && oxygenBar.Value <= maximumBarLimit)
            oxygenBar.Value = oxygenBar.Value + value;

        // Player cannot have more than maximum
        else if (oxygenBar.Value > maximumBarLimit)
            oxygenBar.Value = maximumBarLimit;


        if (oxygenBar.Value < 0)
            oxygenBar.Value = 0;

        // If there is no any oxygen decrease health
        if (oxygenBar.Value <= 0)
            UpdateHealthBar(value);
    }

    public void UpdateCollectedWaterBar(int value)
    {
        // Player cannot get any health if is dead or has maximum
        if (0 <= collectedWaterBar.Value && collectedWaterBar.Value <= maximumBarLimit)
            collectedWaterBar.Value = collectedWaterBar.Value + value;

        if (collectedWaterBar.Value < 0)
            collectedWaterBar.Value = 0;

        // Player cannot have more than maximum
        else if (collectedWaterBar.Value > maximumBarLimit)
            collectedWaterBar.Value = maximumBarLimit;
    }
}
