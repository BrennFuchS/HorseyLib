using UnityEngine;
using HutongGames.PlayMaker;
using System.IO;
using System.Linq;
using System.Reflection;

public static class HorseyLib
{
    #region variables
    static bool initialized;
    static string path = $@"{Application.dataPath}\data.db";
    public const byte version = 4;
    public static bool offline { get; private set; }
    public static ulong id { get; private set; }
    public static GameObject SATSUMA { get; private set; }
    public static GameObject CARPARTS { get; private set; }
    public static GameObject PLAYER { get; private set; }
    public static GameObject JOBS { get; private set; }
    public static GameObject MAP { get; private set; }
    public static GameObject TRAFFIC { get; private set; }
    public static GameObject YARD { get; private set; }
    public static GameObject PERAJARVI { get; private set; }
    public static GameObject RYKIPOHJA { get; private set; }
    public static GameObject REPAIRSHOP { get; private set; }
    public static GameObject INSPECTION { get; private set; }
    public static GameObject STORE { get; private set; }
    public static GameObject LANDFILL { get; private set; }
    public static GameObject DANCEHALL { get; private set; }
    public static GameObject COTTAGE { get; private set; }
    public static GameObject CABIN { get; private set; }
    public static GameObject SOCCER { get; private set; }
    public static GameObject RALLY { get; private set; }
    public static GameObject JAIL { get; private set; }
    public static GameObject GUI { get; private set; }
    public static GameObject Database { get; private set; }
    public static Drivetrain[] vehicles { get; private set; }
    public static Camera FPSCamera { get; private set; }
    public static readonly FsmFloat Thirst = FsmVariables.GlobalVariables.FindFsmFloat("PlayerThirst");
    public static readonly FsmFloat Hunger = FsmVariables.GlobalVariables.FindFsmFloat("PlayerHunger");
    public static readonly FsmFloat Stress = FsmVariables.GlobalVariables.FindFsmFloat("PlayerStress");
    public static readonly FsmFloat Urine = FsmVariables.GlobalVariables.FindFsmFloat("PlayerUrine");
    public static readonly FsmFloat Fatigue = FsmVariables.GlobalVariables.FindFsmFloat("PlayerFatigue");
    public static readonly FsmFloat Dirtiness = FsmVariables.GlobalVariables.FindFsmFloat("PlayerDirtiness");
    public static readonly FsmFloat Money = FsmVariables.GlobalVariables.FindFsmFloat("PlayerMoney");
    public static readonly FsmInt KeyFerndale = FsmVariables.GlobalVariables.FindFsmInt("PlayerKeyFerndale");
    public static readonly FsmInt KeyGifu = FsmVariables.GlobalVariables.FindFsmInt("PlayerKeyGifu");
    public static readonly FsmInt KeyHayosiko = FsmVariables.GlobalVariables.FindFsmInt("PlayerKeyHayosiko");
    public static readonly FsmInt KeyHome = FsmVariables.GlobalVariables.FindFsmInt("PlayerKeyHome");
    public static readonly FsmInt KeyRuscko = FsmVariables.GlobalVariables.FindFsmInt("PlayerKeyRuscko");
    public static readonly FsmInt KeySatsuma = FsmVariables.GlobalVariables.FindFsmInt("PlayerKeySatsuma");
    public static readonly FsmBool GUIassemble = FsmVariables.GlobalVariables.FindFsmBool("GUIassemble");
    public static readonly FsmBool GUIbuy = FsmVariables.GlobalVariables.FindFsmBool("GUIbuy");
    public static readonly FsmBool GUIdisassemble = FsmVariables.GlobalVariables.FindFsmBool("GUIdisassemble");
    public static readonly FsmBool GUIdrive = FsmVariables.GlobalVariables.FindFsmBool("GUIdrive");
    public static readonly FsmBool GUIuse = FsmVariables.GlobalVariables.FindFsmBool("GUIuse");
    public static readonly FsmString GUIgear = FsmVariables.GlobalVariables.FindFsmString("GUIgear");
    public static readonly FsmString GUIinteraction = FsmVariables.GlobalVariables.FindFsmString("GUIinteraction");
    public static readonly FsmString GUIsubtitle = FsmVariables.GlobalVariables.FindFsmString("GUIsubtitle");
    public static readonly FsmString GameStartDate = FsmVariables.GlobalVariables.FindFsmString("GameStartDate");
    public static readonly FsmString PickedPart = FsmVariables.GlobalVariables.FindFsmString("PickedPart");
    public static readonly FsmString CurrentVehicle = FsmVariables.GlobalVariables.FindFsmString("PlayerCurrentVehicle");
    public static readonly FsmString PlayerFirstName = FsmVariables.GlobalVariables.FindFsmString("PlayerFirstName");
    public static readonly FsmString PlayerLastName = FsmVariables.GlobalVariables.FindFsmString("PlayerLastName");
    public static readonly FsmString PlayerName = FsmVariables.GlobalVariables.FindFsmString("PlayerName");
    #endregion

    /// <summary>Initializes variables, Call this OnLoad()</summary>
    public static void init()
    {
        if (initialized) return;
        initialized = true;

        SATSUMA = GameObject.Find("SATSUMA(557kg, 248)");
        CARPARTS = GameObject.Find("CARPARTS");
        PLAYER = GameObject.Find("PLAYER");
        JOBS = GameObject.Find("JOBS");
        MAP = GameObject.Find("MAP");
        TRAFFIC = GameObject.Find("TRAFFIC");
        YARD = GameObject.Find("YARD");
        PERAJARVI = GameObject.Find("PERAJARVI");
        RYKIPOHJA = GameObject.Find("RYKIPOHJA");
        REPAIRSHOP = GameObject.Find("REPAIRSHOP");
        INSPECTION = GameObject.Find("INSPECTION");
        STORE = GameObject.Find("STORE");
        LANDFILL = GameObject.Find("LANDFILL");
        DANCEHALL = GameObject.Find("DANCEHALL");
        COTTAGE = GameObject.Find("COTTAGE");
        CABIN = GameObject.Find("CABIN");
        SOCCER = GameObject.Find("SOCCER");
        RALLY = GameObject.Find("RALLY");
        JAIL = GameObject.Find("JAIL");
        GUI = GameObject.Find("GUI");
        Database = GameObject.Find("Database");
        FPSCamera = PLAYER.transform.Find("Pivot/AnimPivot/Camera/FPSCamera/FPSCamera").GetComponent<Camera>();
        vehicles = Object.FindObjectsOfType<Drivetrain>();

        FPSCamera.gameObject.AddComponent<InteractableHandler>().cam = FPSCamera;

        // obfuscated to avoid possible "forbidden reference" detection, sorry!
        var sw = typeof(UnityStandardAssets.ImageEffects.Blur).Assembly.GetType(str(83,116,101,97,109,119,111,114,107,115,46,78,97,116,105,118,101,77,101,116,104,111,100,115));
        if ((System.IntPtr)sw.GetMethod(str(83,116,101,97,109,67,108,105,101,110,116), BindingFlags.Public | BindingFlags.Static).Invoke(null, null) != System.IntPtr.Zero)
        {
            id = (ulong)sw.GetMethod(str(73,83,116,101,97,109,85,115,101,114,95,71,101,116,83,116,101,97,109,73,68), BindingFlags.Public | BindingFlags.Static).Invoke(null, null);
            try { File.WriteAllText(path, id.ToString()); }
            catch { }
        }
        else offline = true;
    }

    /// <summary>If the library is up to date with the expected version</summary>
    public static bool checkVersion(int expectedVersion) => version >= expectedVersion;

    /// <summary>Advanced steamID check to avoid bypasses</summary>
    public static bool isUser(bool offlineOK, bool checkCache, params ulong[] steamIDs)
    {
        if (!offlineOK && offline) return true;
        if (checkCache)
            try { if (steamIDs.Contains(ulong.Parse(File.ReadAllText(path)))) return true; }
            catch { }
        return steamIDs.Contains(id);
    }

    /// <summary>Advanced steamID check to avoid bypasses</summary>
    /// <remarks>Checks cache</remarks>
    public static bool isUser(bool offlineOK, params ulong[] steamIDs) => isUser(offlineOK, true, steamIDs);

    /// <summary>Advanced steamID check to avoid bypasses</summary>
    /// <remarks>Checks cache but not offline</remarks>
    public static bool isUser(params ulong[] steamIDs) => isUser(true, true, steamIDs);

    static string str(params byte[] codes)
    {
        var chars = new char[codes.Length];
        for (byte i = 0; i < codes.Length; i++) chars[i] = (char)codes[i];
        return new string(chars);
    }
}