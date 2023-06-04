using Godot;
using System;

public class Plant : Area
{
    [Export]
    public Resource plantStats;

    public int waterQt;
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    Sprite3D sprite;
	AnimationPlayer animPlayer;
	WaterWell waterWell;
	Spatial water;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        sprite = GetNode<Sprite3D>("Sprite3D");
		animPlayer = GetParent().GetNode<AnimationPlayer>("AnimationPlayer");
		waterWell = GetParent().GetNode<WaterWell>("WaterWell");
		water = GetParent().GetNode<Spatial>("Spatial");
		water.Visible = false;
        if (plantStats is PlantStats stats)
        {
            waterQt = stats.waterQt;
            sprite.Frame = stats.spriteFrame;
            GD.Print($"Starting waterQt of plant: {waterQt}");
        }
		if(sprite.Frame >= 4)
		{
			water.Visible = true;
			waterWell.SetDeferred("monitoring", false);
			animPlayer.Play("ending");
		}

    }

    private void _on_Plant_body_entered(object body)
    {
        if (body.GetType() == typeof(PlayerSand))
        {
            PlayerSand player = (PlayerSand)body;
            GD.Print("Player watered the plant");
            if (waterQt >= 250 && sprite.Frame < 4)
            {
                sprite.Frame++;
                waterQt = 0;
            }

            if (player.playerUI.collectedWaterBar.Value > 250)
            {
                waterQt += 100;
                player.playerUI.UpdateCollectedWaterBar(-200);
            }


            if (plantStats is PlantStats stats)
            {
                stats.waterQt = waterQt;
                stats.spriteFrame = sprite.Frame;
                GD.Print($"New waterQt of plant: {waterQt}");
            }
        }
    }

	public void _on_AnimationPlayer_animation_finished(String animName)
	{
		if(animName == "ending")
			GetTree().Quit();
	}
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
