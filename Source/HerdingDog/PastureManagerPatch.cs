using HarmonyLib;
using HerdingDog;
using Verse;

[HarmonyPatch(typeof(Map), "MapPreInit")]
public static class Patch_MapPreInit
{
    static void Postfix(Map __instance)
    {
        if (__instance.GetComponent<PastureManager>() == null)
        {
            __instance.components.Add(new PastureManager(__instance));
            Log.Message("PastureManager registrado en el mapa.");
        }
    }
}
