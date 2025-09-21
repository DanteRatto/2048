using Godot;

namespace Utilities;

public partial class SwipeDetector : Node
{
    private const float swipeThreshold = 125.0f; // minimum distance for a swipe
    private const float diagonalSwipeThreshold = 25.0f; // maximum distance for a swipe in the diagonal direction

    private readonly InputEventAction inputEventAction = new() { Pressed = true };

    private Vector2? startPosition;

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventScreenTouch touchEvent)
        {
            if (touchEvent.Pressed) startPosition = touchEvent.Position;
            else DetectSwipeDirection(touchEvent.Position);
        }
    }

    private void DetectSwipeDirection(Vector2 endPosition)
    {
        if (startPosition == null) return;
        var swipeDelta = endPosition - startPosition.Value;
        if (swipeDelta.Length() < swipeThreshold) return; // too short
        if (Mathf.Abs(swipeDelta.X) > Mathf.Abs(swipeDelta.Y)) // horizontal swipe
        {
            if (Mathf.Abs(swipeDelta.Y) > diagonalSwipeThreshold) return; // too diagonal
            inputEventAction.Action = swipeDelta.X > 0 ? "ui_right" : "ui_left";
        }
        else // vertical swipe
        {
            if (Mathf.Abs(swipeDelta.X) > diagonalSwipeThreshold) return; // too diagonal
            inputEventAction.Action = swipeDelta.Y > 0 ? "ui_down" : "ui_up";
        }
        Input.ParseInputEvent(inputEventAction); // send event
        startPosition = null;
    }
}
