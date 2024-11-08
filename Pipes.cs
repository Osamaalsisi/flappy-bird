using Godot;
using System;

public partial class Pipes : Node2D
{
    [Export] private float scrollSpeed = -220;
    [Export] private float OffScreenCoordinate = -500;

    public delegate void voidChannelResource();
    public event voidChannelResource onPlayerHit;

    // New signal to be emitted when the bird passes through
    [Signal]
    public delegate void ScoreUpdatedEventHandler();

    private Area2D _scoreArea;

    public override void _Ready()
    {
        // Find the ScoreArea and connect the signal
        _scoreArea = GetNode<Area2D>("ScoreArea");
        _scoreArea.BodyEntered += OnScoreAreaBodyEntered;

        onPlayerHit += StopMovement;
    }

    public override void _Process(double delta)
    {
        var position = Position;
        position.X += (float)delta * scrollSpeed;

        if (Position.X < OffScreenCoordinate)
        {
            onPlayerHit -= StopMovement;
            QueueFree();
        }
        Position = position;
    }

    private void StopMovement()
    {
        scrollSpeed = 0;
    }

    // This function is triggered when the bird passes through the ScoreArea
   private void OnScoreAreaBodyEntered(Node2D body)
{
    if (body.Name == "Bird")  // Make sure the bird is what triggered the event
    {
        EmitSignal(nameof(ScoreUpdated));
    }
}

}
