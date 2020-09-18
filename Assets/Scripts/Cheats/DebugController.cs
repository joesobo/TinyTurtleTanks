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

    public static DebugCommand FULL_HEAL;
    public static DebugCommand<int> INCREASE_HEALTH;
    public static DebugCommand<int> DECREASE_HEALTH;
    public static DebugCommand HELP;
    public static DebugCommand HOME;
    public static DebugCommand KILL_PLAYER;
    public static DebugCommand KILL_ENEMIES;
    public static DebugCommand<string> EFFECT;
    public static DebugCommand<string, int> SPAWN;

    private PlayerHealth playerHealth;
    private PlayerController playerController;
    private PlayerEffects playerEffects;
    private Enemy enemyBase;
    private LevelRunner levelRunner;
    private BoidSpawner boidSpawner;
    private BoidManager boidManager;

    public List<DebugCommandBase> commandList;
    public List<GameObject> spawnItems;

    private void Awake() {
        enemyBase = FindObjectOfType<Enemy>();
    }

    private void Start() {
        settings = FindObjectOfType<GameSettings>();
        playerHealth = FindObjectOfType<PlayerHealth>();
        playerController = FindObjectOfType<PlayerController>();
        playerEffects = FindObjectOfType<PlayerEffects>();
        levelRunner = FindObjectOfType<LevelRunner>();
        boidSpawner = FindObjectOfType<BoidSpawner>();
        boidManager = FindObjectOfType<BoidManager>();

        playerHomePosition = playerController.gameObject.transform.position;

        SetupCommands();
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

    private void SetupCommands() {
        FULL_HEAL = new DebugCommand("full_heal", "Heals the player to full health", "full_heal", () => {
            playerHealth.IncreaseHealth(playerHealth.MAXHEALTH);
        });
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
        SPAWN = new DebugCommand<string, int>("spawn", "Spawns things near the player", "spawn <type> <num>", (command, numberMod) => {
            SpawnItem(command, numberMod);
        });

        commandList = new List<DebugCommandBase> {
            FULL_HEAL,
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

    void OnGUI() {
        if (!showConsole) { return; }

        float y;

        if (showHelp) {
            y = Screen.height - 30f - 100f;

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
        }

        y = Screen.height - 30f;

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

    private void SpawnItem(string value, int num) {
        Vector3 playerPos = playerController.gameObject.transform.position;
        Vector3 offsetPos = new Vector3(playerPos.x, playerPos.y + 3, playerPos.z);

        for (int i = 0; i < num; i++) {
            if (value == "jump") {
                Instantiate(spawnItems[0], playerPos, Quaternion.identity);
            }
            else if (value == "speed") {
                Instantiate(spawnItems[1], playerPos, Quaternion.identity);
            }
            else if (value == "shield") {
                Instantiate(spawnItems[2], playerPos, Quaternion.identity);
            }
            else if (value == "health") {
                Instantiate(spawnItems[3], playerPos, Quaternion.identity);
            }
            else if (value == "bomb") {
                Instantiate(spawnItems[4], playerPos, Quaternion.identity);
            }
            else if (value == "rocket") {
                Instantiate(spawnItems[5], playerPos, Quaternion.identity);
            }
            else if (value == "enemy") {
                enemyBase.CreateEnemy(offsetPos);
            }
            else if (value == "fish") {
                Instantiate(spawnItems[6], offsetPos, Quaternion.identity);
            }
            else if (value == "bird") {
                boidSpawner.CreateBoid(offsetPos);
                boidManager.UpdateBirdSettings();
            }
        }
    }

    private void HandleInput() {
        string[] properties = input.Split(' ');

        for (int i = 0; i < commandList.Count; i++) {
            DebugCommandBase commandBase = commandList[i] as DebugCommandBase;

            if (properties[0].Equals(commandBase.commandId)) {
                if (commandList[i] is DebugCommand command) {
                    command.Invoke();
                }
                else if (commandList[i] is DebugCommand<int> commandInt) {
                    int number = properties.Length >= 2 ? int.Parse(properties[1]) : 1;

                    commandInt.Invoke(number);
                }
                else if (commandList[i] is DebugCommand<string> commandString) {
                    commandString.Invoke(properties[1]);
                }
                else if (commandList[i] is DebugCommand<string, int> commandStringInt) {
                    int number = properties.Length >= 3 ? int.Parse(properties[2]) : 1;

                    commandStringInt.Invoke(properties[1], number);
                }
            }
        }
    }
}
