# HorseyLib
### Contains all helper methods & properties

<br>

Name | Type | Path
-|-|-
SATSUMA | GameObject | `SATSUMA(557kg, 248)`
CARPARTS | GameObject | `CARPARTS`
PLAYER | GameObject | `PLAYER`
JOBS | GameObject | `JOBS`
MAP | GameObject | `MAP`
TRAFFIC | GameObject | `TRAFFIC`
YARD | GameObject | `YARD`
PERAJARVI | GameObject | `PERAJARVI`
RYKIPOHJA | GameObject | `RYKIPOHJA`
REPAIRSHOP | GameObject | `REPAIRSHOP`
INSPECTION | GameObject | `INSPECTION`
STORE | GameObject | `STORE`
LANDFILL | GameObject | `LANDFILL`
DANCEHALL | GameObject | `DANCEHALL`
COTTAGE | GameObject | `COTTAGE`
CABIN | GameObject | `CABIN`
SOCCER | GameObject | `SOCCER`
RALLY | GameObject | `RALLY`
JAIL | GameObject | `JAIL`
Database | GameObject | `Database`
FPSCamera | Camera | `PLAYER/Pivot/AnimPivot/Camera/FPSCamera/FPSCamera`
vehicles | Drivetrain[] | `Object.FindObjectsOfType<Drivetrain>()`
Thirst | FsmFloat | `Globals/PlayerThirst`
Hunger | FsmFloat | `Globals/PlayerHunger`
Stress | FsmFloat | `Globals/PlayerStress`
Urine | FsmFloat | `Globals/PlayerUrine`
Fatigue | FsmFloat | `Globals/PlayerFatigue`
Dirtiness | FsmFloat | `Globals/PlayerDirtiness`
Money | FsmFloat | `Globals/PlayerMoney`
KeyFerndale | FsmInt | `Globals/PlayerKeyFerndale`
KeyGifu | FsmInt | `Globals/PlayerKeyGifu`
KeyHayosiko | FsmInt | `Globals/PlayerKeyHayosiko`
KeyHome | FsmInt | `Globals/PlayerKeyHome`
KeyRuscko | FsmInt | `Globals/PlayerKeyRuscko`
KeySatsuma | FsmInt | `Globals/PlayerKeySatsuma`
KeyFerndale | FsmInt | `Globals/PlayerKeyFerndale`
GUIassemble | FsmBool | `Globals/GUIassemble`
GUIbuy | FsmBool | `Globals/GUIbuy`
GUIdisassemble | FsmBool | `Globals/GUIdisassemble`
GUIdrive | FsmBool | `Globals/GUIdrive`
GUIuse | FsmBool | `Globals/GUIuse`
GUIgear | FsmString | `Globals/GUIgear`
GUIinteraction | FsmString | `Globals/GUIinteraction`
GUIsubtitle | FsmString | `Globals/GUIsubtitle`
GameStartDate | FsmString | `Globals/GameStartDate`
PickedPart | FsmString | `Globals/PickedPart`
CurrentVehicle | FsmString | `Globals/PlayerCurrentVehicle`
PlayerFirstName | FsmString | `Globals/PlayerFirstName`
PlayerLastName | FsmString | `Globals/PlayerLastName`
PlayerName | FsmString | `Globals/PlayerName`

<br>

Name | Returns | Params | Summary
-|-|-|-
init | void | None | Initializes variables, Call this OnLoad()
checkVersion | bool | `int expectedVersion` | If the library is up to date with the expected version

<br>

# Interactable
### A class to make interaction easier

<br>

<i>All methods are only called at a max distance of 1 meter away just like the game does</i>

Name | Called
-|-
mouseEnter() | Called when the player's cursor first moves over the object
mouseOver | Called when the player's cursor is over the object
mouseExit | Called when the player's cursor is exits object
scrollUp | Called when the player scrolls up and moused over
scrollDown | Called when the player scrolls down and moused over
use | Called when the player presses the use key and moused over
lClick | Called when the player presses the left mouse button moused over
lHold | Called when the player holds the left mouse button moused over
lRelease | Called when the player releases the left mouse button or mouses off
rClick | Called when the player presses the right mouse button moused over
rHold | Called when the player holds the right mouse button moused over
rRelease | Called when the player releases the right mouse button or mouses off

<br>

## Example

```cs
class InteractableExample : Interactable
{
	public override void mouseEnter() => HorseyLib.GUIuse.value = true;

	public override void lClick() => ModConsole.Print("Interactable clicked");

	public override void mouseExit() => HorseyLib.GUIuse.value = false;
}
```

<br>

# SaveBytes
### Saves classes with BinaryFormatters

<br>

Can save any class marked as `System.Serializable`, or any of the exceptions below
- UnityEngine.Color
- UnityEngine.Quaternion
- UnityEngine.Vector2
- UnityEngine.Vector3
- UnityEngine.Vector4
- UnityEngine.GameObject
- UnityEngine.Transform

<i>These classes are returned as s[classname] since the default aren't serializable</i>

<br>

Class | Method | Summary
-|-|-
sColor | `get()` | Gets stored `Color`
sQuaternion | `get()` | Gets stored `Quaternion`
sVector2 | `get()` | Gets stored `Vector2`
sVector3 | `get()` | Gets stored `Vector3`
sVector4 | `get()` | Gets stored `Vector4`
sGameObject | `apply(GameObject)` | Applies data to `GameObject`
sTransform | `apply(GameObject)` | Applies data to `Transform`

<br>

Name | Returns | Params
-|-|-
save | void | `string saveFile` Save location <br> `object[] data` Data to save
load | object[] | `string saveFile` Save location <br> `object[] ifFail` Data to return if there is an error

<br>

## Example

```cs
SaveBytes.save(saveFile, new object[]
{
	myColor,
	myQuaternion,
	myVector2,
	myVector3,
	myVector4,
	myGameObject,
	myTransform
});

var data = SaveBytes.load(saveFile);
if (data == null) return; // failed to load save

myColor = ((sColor)data[0]).get();
myQuaternion = ((sQuaternion)data[1]).get();
myVector2 = ((sVector2)data[2]).get();
myVector3 = ((sVector3)data[3]).get();
myVector4 = ((sVector4)data[4]).get();
((sGameObject)data[5]).apply(myGameObject);
((sTransform)data[6]).apply(myTransform);
```