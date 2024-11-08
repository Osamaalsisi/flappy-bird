using Godot;
using System;
using System.Drawing;

public partial class Main : Node2D
{
    [Export]
    public PackedScene pipescene { get; set; }

    private int _score = 0;           
    private Label _scoreLabel;         

    public override void _Ready()
    {
        GD.Print("Hello World!");
        _scoreLabel = GetNode<Label>("ScoreLabel");
        _scoreLabel.Text = _score.ToString();
    }

    public override void _Process(double delta)
    {
    }

    public void OnPipeSpawnerTimeout()
    {
        GD.Print("Go");

        Pipes pipe = pipescene.Instantiate<Pipes>();

        Vector2 spawnerPosition = GetNode<Marker2D>("PipeSpawner").Position;

        RandomNumberGenerator rng = new RandomNumberGenerator();
        rng.Randomize();

        float minY = -200;
        float maxY = 200;
        float randomY = rng.RandfRange(minY, maxY);

        pipe.Position = new Vector2(spawnerPosition.X, spawnerPosition.Y + randomY);

        pipe.ScoreUpdated += OnPipeScoreUpdated;

        AddChild(pipe);
    }
        

    private void OnPipeScoreUpdated()
    {
        _score++;
        _scoreLabel.Text = _score.ToString();

        
    }
}