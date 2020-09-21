using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugController : MonoBehaviour {
    public static DebugController Instance;

    private bool showConsole = false;
    private bool showHelp = false;
    private bool showExtraHelp = false;

    private string input = "";
    private string lastInput = "";
    private GameSettings settings;
    private Vector2 scroll;
    private Vector3 playerHomePosition;
    private DebugCommandBase extraHelp;

    public static DebugCommand FULL_HEAL;
    public static DebugCommand<int> INCREASE_HEALTH;
    public static DebugCommand<int> DECREASE_HEALTH;
    public static DebugCommand<string> HELP;
    public static DebugCommand HOME;
    public static DebugCommand KILL_PLAYER;
    public static DebugCommand KILL_ENEMIES;
    public static DebugCommand<string> EFFECT;
    public static DebugCommand<string, int> SPAWN;
    public static DebugCommand<float> TIME;
    public static DebugCommand<string> TOGGLE;
    public static DebugCommand CLEAR;

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
        if (Instance == null) {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this) {
            Destroy(gameObject);
        }

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
        if (Input.GetKeyDown(KeyCode.BackQuote)) {
            showConsole = !showConsole;
            settings.isPaused = !settings.isPaused;
        }

        if (!showConsole && (showHelp || showExtraHelp)) {
            Clear();
        }
    }

    private void SetupCommands() {
        FULL_HEAL = new DebugCommand("full_heal", "Heals the player to full health", "full_heal", null, () => {
            playerHealth.IncreaseHealth(playerHealth.MAXHEALTH);
        });
        INCREASE_HEALTH = new DebugCommand<int>("heal", "Heals the player by x health", "heal <heal amount>", null, (value) => {
            playerHealth.IncreaseHealth(value);
        });
        DECREASE_HEALTH = new DebugCommand<int>("damage", "Damages the player by x health", "damage <damage amount>", null, (value) => {
            playerHealth.DecreaseHealth(value);
        });
        HELP = new DebugCommand<string>("help", "Shows a list of commands", "help", null, (value) => {
            if (value != null) {
                HandleExtraHelp(value);
            }
            else {
                showHelp = true;
                showExtraHelp = false;
            }
        });
        HOME = new DebugCommand("home", "Teleports player to home", "home", null, () => {
            playerController.gameObject.transform.position = playerHomePosition;
        });
        KILL_PLAYER = new DebugCommand("kill_player", "Instantly kills the player", "kill_player", null, () => {
            playerHealth.DecreaseHealth(playerHealth.MAXHEALTH);
        });
        KILL_ENEMIES = new DebugCommand("kill_enemies", "Instantly kills all enemies", "kill_enemies", null, () => {
            foreach (EnemyHealth enemy in FindObjectsOfType<EnemyHealth>()) {
                enemy.InstantKillEnemy();
                levelRunner.DecreaseNumEnemy();
            }
        });
        EFFECT = new DebugCommand<string>("effect", "Gives the player an effect", "effect <type>", "Allows user to give themselves the effect of [speed, jump, shield, or health]", (value) => {
            SetEffect(value);
        });
        SPAWN = new DebugCommand<string, int>("spawn", "Spawns things near the player", "spawn <type> <num>", "Allows user to spawn a pickup [speed, jump, shield, health], weapon [rocket, bomb], enemy, fish, or bird", (command, numberMod) => {
            SpawnItem(command, numberMod);
        });
        TIME = new DebugCommand<float>("time", "Sets the scale of time [0.25, 0.5, 1, 1.5, 2]", "time <num>", "Speed up or slow down game [0.25, 0.5, 1, 1.5, 2] [default: 1]", (value) => {
            if (value == 0.25 || value == 0.5 || value == 1 || value == 1.5 || value == 2) {
                settings.defaultTimeScale = value;
            }
        });
        TOGGLE = new DebugCommand<string>("toggle", "Toggles a game setting. Help for more info", "toggle <type>", "Toggle setting: [grass, water, seasweed, footprints, moons, clouds, atmosphere, crates, vfx, daylight_cycle]", (value) => {
            ToggleSetting(value);
        });
        CLEAR = new DebugCommand("clear", "Clears the help screen", "clear", null, () => {
            Clear();
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
            SPAWN,
            TIME,
            TOGGLE,
            CLEAR
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
        else if (showExtraHelp) {
            y = Screen.height - 30f - 30f;

            GUI.Box(new Rect(0, y, Screen.width, 30), "");

            string description;

            if (extraHelp.extraDescription != null) {
                description = extraHelp.extraDescription;
            }
            else {
                description = extraHelp.commandDescription;
            }

            string label = $"{extraHelp.commandFormat} - {description}";

            Rect labelRect = new Rect(5, y + 5, Screen.width - 20f, 30);

            GUI.Label(labelRect, label);
        }

        y = Screen.height - 30f;

        GUI.Box(new Rect(0, y, Screen.width, 30), "");
        GUI.backgroundColor = new Color(0, 0, 0, 0);

        if (showConsole && Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.UpArrow) {
            input = lastInput;
        }

        if (showConsole && Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return) {
            lastInput = input;
            HandleInput();
            input = "";
        }
        else {
            GUI.SetNextControlName("MyTextField");
            input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20), input);
            GUI.FocusControl("MyTextField");
            if (input != null && input.Contains("`")) {
                input = "";
                showConsole = false;
                settings.isPaused = false;
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
                    string value = properties.Length >= 2 ? properties[1] : null;

                    commandString.Invoke(value);
                }
                else if (commandList[i] is DebugCommand<float> commandFloat) {
                    float number = properties.Length >= 2 ? float.Parse(properties[1]) : 1f;

                    commandFloat.Invoke(number);
                }
                else if (commandList[i] is DebugCommand<string, int> commandStringInt) {
                    int number = properties.Length >= 3 ? int.Parse(properties[2]) : 1;

                    commandStringInt.Invoke(properties[1], number);
                }
            }
        }
    }

    private void HandleExtraHelp(string value) {
        for (int i = 0; i < commandList.Count; i++) {
            DebugCommandBase commandBase = commandList[i] as DebugCommandBase;

            if (value.Equals(commandBase.commandId)) {
                extraHelp = commandBase;
                showExtraHelp = true;
                showHelp = false;
            }
        }
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

    private void Clear() {
        showHelp = false;
        showExtraHelp = false;
    }

    private void ToggleSetting(string value) {
        if (value == "grass") {
            settings.useGrass = !settings.useGrass;
        }
        else if (value == "water") {
            settings.useWater = !settings.useWater;
        }
        else if (value == "seaweed") {
            settings.useSeaweed = !settings.useSeaweed;
        }
        else if (value == "footprints") {
            settings.useFootPrints = !settings.useFootPrints;
        }
        else if (value == "moons") {
            settings.useMoons = !settings.useMoons;
        }
        else if (value == "clouds") {
            settings.useClouds = !settings.useClouds;
        }
        else if (value == "atmosphere") {
            settings.useAtmosphere = !settings.useAtmosphere;
        }
        else if (value == "crates") {
            settings.useCrates = !settings.useCrates;
        }
        else if (value == "vfx") {
            settings.useVFX = !settings.useVFX;
        }
        else if (value == "daylight_cycle") {
            settings.daylightCycle = !settings.daylightCycle;
        }

        levelRunner.UpdateSettings();
    }
}
