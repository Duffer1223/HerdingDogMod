using Verse;
using RimWorld;

public static class AnimalComponentManager
{
    public static void AddHerdingComponent(Pawn pawn)
    {
        if (pawn == null || !pawn.RaceProps.Animal)
            return;

        if (pawn.TryGetComp<HerdingDogComp>() != null)
            return;

        var comp = new HerdingDogComp();
        pawn.AllComps.Add(comp);

        Log.Message($"HerdingDogComp agregado a {pawn.Name}");
    }
}
