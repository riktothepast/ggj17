using UnityEngine;
using System.Collections;
using InControl;

public class PlayerController : PlayerActionSet
{
    public PlayerAction Action1;
    public PlayerAction Action2;
    public PlayerAction Action3;
    public PlayerAction Action4;
    public PlayerAction RightBumper;
    public PlayerAction LeftBumper;
    public PlayerAction Start;
    public PlayerAction Left;
    public PlayerAction Right;
    public PlayerAction Up;
    public PlayerAction Down;
    public PlayerTwoAxisAction Move;

    public PlayerController()
		{
            Action1 = CreatePlayerAction("Action1");
            Action2 = CreatePlayerAction("Action2");
            Action3 = CreatePlayerAction("Action3");
            Action4 = CreatePlayerAction("Action4");
            RightBumper = CreatePlayerAction("RightBumper");
            LeftBumper = CreatePlayerAction("LeftBumper");
            Start = CreatePlayerAction("Start");
			Left = CreatePlayerAction( "Move Left" );
			Right = CreatePlayerAction( "Move Right" );
			Up = CreatePlayerAction( "Move Up" );
			Down = CreatePlayerAction( "Move Down" );
			Move = CreateTwoAxisPlayerAction( Left, Right, Down, Up );
		}

    public static PlayerController CreateWithDefaultBindings()
    {
        var playerController = new PlayerController();

        playerController.Action1.AddDefaultBinding(Key.S);
        playerController.Action1.AddDefaultBinding(InputControlType.Action1);

        playerController.Action2.AddDefaultBinding(Key.D);
        playerController.Action2.AddDefaultBinding(InputControlType.Action2);

        playerController.Action3.AddDefaultBinding(Key.A);
        playerController.Action3.AddDefaultBinding(InputControlType.Action3);

        playerController.Action4.AddDefaultBinding(Key.W);
        playerController.Action4.AddDefaultBinding(InputControlType.Action4);

        playerController.RightBumper.AddDefaultBinding(Key.E);
        playerController.RightBumper.AddDefaultBinding(InputControlType.RightBumper);

        playerController.LeftBumper.AddDefaultBinding(Key.Q);
        playerController.LeftBumper.AddDefaultBinding(InputControlType.LeftBumper);

        playerController.Start.AddDefaultBinding(Key.Return);
        playerController.Start.AddDefaultBinding(InputControlType.Command);

        playerController.Up.AddDefaultBinding(Key.UpArrow);
        playerController.Down.AddDefaultBinding(Key.DownArrow);
        playerController.Left.AddDefaultBinding(Key.LeftArrow);
        playerController.Right.AddDefaultBinding(Key.RightArrow);

        playerController.Left.AddDefaultBinding(InputControlType.LeftStickLeft);
        playerController.Right.AddDefaultBinding(InputControlType.LeftStickRight);
        playerController.Up.AddDefaultBinding(InputControlType.LeftStickUp);
        playerController.Down.AddDefaultBinding(InputControlType.LeftStickDown);

        playerController.Left.AddDefaultBinding(InputControlType.DPadLeft);
        playerController.Right.AddDefaultBinding(InputControlType.DPadRight);
        playerController.Up.AddDefaultBinding(InputControlType.DPadUp);
        playerController.Down.AddDefaultBinding(InputControlType.DPadDown);
        playerController.ListenOptions.IncludeUnknownControllers = true;
        playerController.ListenOptions.MaxAllowedBindings = 3;

        playerController.ListenOptions.OnBindingFound = (action, binding) =>
        {
            if (binding == new KeyBindingSource(Key.Escape))
            {
                action.StopListeningForBinding();
                return false;
            }
            return true;
        };

        playerController.ListenOptions.OnBindingAdded += (action, binding) =>
        {
            Debug.Log("Binding added... " + binding.DeviceName + ": " + binding.Name);
        };

        playerController.ListenOptions.OnBindingRejected += (action, binding, reason) =>
        {
            Debug.Log("Binding rejected... " + reason);
        };

        return playerController;
    }
}
