using Godot;
using System;

public partial class Bird : CharacterBody2D
{
// Konstant hopphastighet (kraft uppåt vid hopp)
	public const float JumpVelocity = -380;
	[Export] private int score = 0;

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Tillägga gravitet.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Hantera Hopp.
		if (Input.IsActionJustPressed("Jump"))
		{
		// Ställ in Y-hastigheten till hopphastigheten (negativ för att gå uppåt)
			velocity.Y = JumpVelocity;
		}
		Velocity = velocity;
		MoveAndSlide();
	}

	public void OnBirdCollied(Rid rid, Area2D area2D, int areaId, int shapeId){
		if (area2D.Name == "LowerPipeArea" || area2D.Name == "UpperPipeArea" || area2D.Name == "DeadEnd")                                                                                                     
        {
            GD.Print("Bird collided with a pipe!");
	       	RestartGame();
            QueueFree();
        }
		else if(area2D.Name =="ScoreArea"){

		}
		
		GD.Print(rid.Id);
		GD.Print(area2D.Name);
		GD.Print(areaId);
		GD.Print(shapeId);

	}
	private void RestartGame()
    {
        GetTree().ReloadCurrentScene();
    }

}
