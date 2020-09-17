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
    public static DebugCommand KILL_PLAYER;
    public static DebugCommand KILL_ENEMIES;
    public static DebugCommand<string> EFFECT;
    public static DebugCommand<string> SPAWN;

    private PlayerHealth playerHealth;
    private PlayerController playerController;
    private PlayerEffects playerEffects;
    private LevelRunner levelRunner;

    public List<DebugCommandBase> commandList;
    public List<GameObject> spawnItems;

    private void Start() {
        settings = FindObjectOfType<GameSettings>();
        playerHealth = FindObjectOfType<PlayerHealth>();
        playerController = FindObjectOfType<PlayerController>();
        playerEffects = FindObjectOfType<PlayerEffects>();
        levelRunner = FindObjectOfType<LevelRunner>();

        playerHomePosition = playerController.gameObject.transform.position;

        INCREASE_HEALTH = new DebugCommand<int>("heal", "Heals the player by x health", "heal <heal amount>", (value) => {
            playerHealth.IncreaseHealth(value);
        });
        DECREASE_HEALTH = new DebugCommand<int>("damage", "Damages the player by x health", "damage <damage amount>", (value) => {
            playerHealth.DecreaseHealth(value);
        });
        HELP = new DebugCommand("help", "Shows a list of commands", "help", () => {
            showHelp = true;
        });
        HOME = new DebugCommand("home", "Teleports player to home", "home", () => {
            playerController.gameObject.transform.position = playerHomePosition;
        });
        KILL_PLAYER = new DebugCommand("kill_player", "Instantly kills the player", "kill_player", () => {
            playerHealth.DecreaseHealth(playerHealth.MAXHEALTH);
        });
        KILL_ENEMIES = new DebugCommand("kill_enemies", "Instantly kills all enemies", "kill_enemies", () => {
            foreach (EnemyHealth enemy in FindObjectsOfType<EnemyHealth>()) {
                enemy.InstantKillEnemy();
                levelRunner.DecreaseNumEnemy();
            }
        });
        EFFECT = new DebugCommand<string>("effect", "Gives the player speed, jump, shield, or health", "effect <type>", (value) => {
            SetEffect(value);
        });
        SPAWN = new DebugCommand<string>("spawn", "Spawns a speed, jump, shield, or health pickup near the player", "spawn <type>", (value) => {
            SpawnItem(value);
        });

        commandList = new List<DebugCommandBase> {
            INCREASE_HEALTH,
            DECREASE_HEALTH,
            HELP,
            HOME,
            KILL_PLAYER,
            KILL_ENEMIES,
            EFFECT,
            SPAWN
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
        }
    }

    void OnGUI() {
        if (!showConsole) { return; }

        float y = Screen.height - 30f;

        if (showHelp) {
            GUI.Box(new Rect(0, y, Screen.width, 100), "");

            Rect viewport = new Rect(0, 0, Screen.width - 30, 20 * commandList.Count);

            scroll = GUI.BeginScrollView(new Rect(0, y + 5f, Screen.width, 90), scroll, viewport);

            for (int i = 0; i < commandList.Count; i++) {
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

    private void SetEffect(string value) {
        if (!playerEffects.shield && !playerEffects.speed && !playerEffects.jump) {
            if (value == "jump") {
                playerEffects.ActivateJump();
            }
            else if (value == "speed") {
                playerEffects.ActivateSpeed();
            }
            else if (value == "shield") {
                playerEffects.ActivateShield();
            }
        }

        if (value == "health") {
            playerEffects.ActivateHealth();
        }
    }

    private void SpawnItem(string value) {
        if (value == "jump") {
            Instantiate(spawnItems[0], playerController.gameObject.transform.position, Quaternion.identity);
        }
        else if (value == "speed") {
            Instantiate(spawnItems[1], playerController.gameObject.transform.position, Quaternion.identity);
        }
        else if (value == "shield") {
            Instantiate(spawnItems[2], playerController.gameObject.transform.position, Quaternion.identity);
        }
        else if (value == "health") {
            Instantiate(spawnItems[3], playerController.gameObject.transform.position, Quaternion.identity);
        }
    }

    private void HandleInput() {
        string[] properties = input.Split(' ');

        for (int i = 0; i < commandList.Count; i++) {
            DebugCommandBase commandBase = commandList[i] as DebugCommandBase;

            if (properties[0].Equals(commandBase.commandId)) {
                //if (input.Contains(commandBase.commandId)) {
                if (commandList[i] is DebugCommand command) {
                    command.Invoke();
                }
                else if (commandList[i] is DebugCommand<int> commandInt) {
                    commandInt.Invoke(int.Parse(properties[1]));
                }
                else if (commandList[i] is DebugCommand<string> commandString) {
                    commandString.Invoke(properties[1]);
                }
            }
        }
    }
}
