using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugController : MonoBehaviour {
    private bool showConsole = false;
    private bool showHelp = false;
    private string input;
    private GameSettings settings;
    private Vector2 scroll;
    private Vector3 playerHomePosition;

    public static DebugCommand<int> INCREASE_HEALTH;
    public static DebugCommand<int> DECREASE_HEALTH;
    public static DebugCommand HELP;
    public static DebugCommand HOME;

    private PlayerHealth playerHealth;
    private PlayerController playerController;

    public List<DebugCommandBase> commandList;

    private void Start() {
        settings = FindObjectOfType<GameSettings>();
        playerHealth = FindObjectOfType<PlayerHealth>();
        playerController = FindObjectOfType<PlayerController>();

        playerHomePosition = playerController.gameObject.transform.position;

        INCREASE_HEALTH = new DebugCommand<int>("heal", "Heals the player by x health", "heal <heal amount>", (x) => {
            playerHealth.IncreaseHealth(x);
        });
        DECREASE_HEALTH = new DebugCommand<int>("damage", "Damages the player by x health", "damage <damage amount>", (x) => {
            playerHealth.DecreaseHealth(x);
        });
        HELP = new DebugCommand("help", "Shows a list of commands", "help", () => {
            showHelp = true;
        });
        HOME = new DebugCommand("home", "Teleports player to home", "home", () => {
            playerController.gameObject.transform.position = playerHomePosition;
        });

        commandList = new List<DebugCommandBase> {
            INCREASE_HEALTH,
            DECREASE_HEALTH,
            HELP,
            HOME
        };
    }

    void Update() {
        if (Input.GetButtonDown("Console")) {
            showConsole = !showConsole;
            settings.isPaused = !settings.isPaused;
        }

        if (showConsole && Input.GetButtonDown("Submit")) {
            HandleInput();
            input = "";
            // showConsole = false;
            // settings.isPaused = false;
        }
    }

    void OnGUI() {
        if (!showConsole) { return; }

        float y = Screen.height - 30f;

        if (showHelp) {
            GUI.Box(new Rect(0, y, Screen.width, 100), "");
            
            Rect viewport = new Rect(0, 0, Screen.width - 30, 20 * commandList.Count);

            scroll = GUI.BeginScrollView(new Rect(0, y + 5f, Screen.width, 90), scroll, viewport);

            for (int i = 0; i < commandList.Count; i++)
            {
                DebugCommandBase command = commandList[i];

                string label = $"{command.commandFormat} - {command.commandDescription}";

                Rect labelRect = new Rect(5, 20 * i, viewport.width - 100, 20);

                GUI.Label(labelRect, label);
            }

            GUI.EndScrollView();

            y -= 100;
        }

        GUI.Box(new Rect(0, y, Screen.width, 30), "");
        GUI.backgroundColor = new Color(0, 0, 0, 0);
        input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20), input);
    }

    private void HandleInput() {
        string[] porperties = input.Split(' ');

        for (int i = 0; i < commandList.Count; i++)
        {
            DebugCommandBase commandBase = commandList[i] as DebugCommandBase;

            if (input.Contains(commandBase.commandId)) {
                if (commandList[i] is DebugCommand dc) {
                    dc.Invoke();
                }
                else if (commandList[i] is DebugCommand<int> dcInt) {
                    dcInt.Invoke(int.Parse(porperties[1]));
                }
            }
        }
    }
}
