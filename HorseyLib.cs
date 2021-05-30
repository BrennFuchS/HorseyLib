using UnityEngine;
using HutongGames.PlayMaker;
using System;
using Steamworks;

public static class HorseyLib
{
    static bool initialized;
    public const int version = 2;
    public static bool steamOffline { get; private set; }
    public static ulong steamID { get; private set; }
    public static bool osWindows { get; private set; }
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
        vehicles = GameObject.FindObjectsOfType<Drivetrain>();

        FPSCamera.gameObject.AddComponent<InteractableHandler>().cam = FPSCamera;

        osWindows = Environment.OSVersion.Platform == PlatformID.Win32NT;
        try { steamID = SteamUser.GetSteamID().m_SteamID; }
        catch { steamOffline = true; }
    }

    /// <summary>If the library is up to date with the expected version</summary>
    public static bool checkVersion(int expectedVersion) => version >= expectedVersion;

    /// <summary>Check only if the user has a specific steamID</summary>
    public static bool hasID(params ulong[] steamIDs)
    {
        for (var i = 0; i < steamIDs.Length; i++)
            if (steamIDs[i] == steamID) return true;
        return false;
    }

    /// <summary>Advanced steamID check to avoid bypasses</summary>
    public static bool isUser(bool offlineOK, params ulong[] steamIDs)
    {
        if (!offlineOK && steamOffline) return true;
        if (osWindows)
        {
            var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Amistech\My Summer Car");
            if (key != null)
            {
                var val = key.GetValue("ID");
                if (val != null)
                {
                    var id = ulong.Parse((string)val);
                    for (var i = 0; i < steamIDs.Length; i++)
                        if (steamIDs[i] == id)
                        {
                            key.Close();
                            return true;
                        }
                }
                key.Close();
            }
        }
        if (hasID(steamIDs))
        {
            if (osWindows)
            {
                var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Amistech\My Summer Car", true);
                key.SetValue("ID", steamID);
                key.Close();
            }
            return true;
        }
        return false;
    }
}