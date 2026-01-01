using HarmonyLib;
using Verse;

[StaticConstructorOnStartup]
public static class HerdingDogHarmony
{
    static HerdingDogHarmony()
    {
        var harmony = new Harmony("com.tumod.herdingdog");
        harmony.PatchAll();
    }
}
