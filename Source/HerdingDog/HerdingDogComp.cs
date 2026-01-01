using System;
using Verse;
using RimWorld;

public class HerdingDogComp : ThingComp
{
    public bool isGrazing = false;          // Si el perro está pastando
    public Zone_Pasture currentPasture;     // Pastura asignada

    public HerdingDogComp() { }

    public override void PostExposeData()
    {
        base.PostExposeData();
        Scribe_Values.Look(ref isGrazing, "isGrazing");
        Scribe_References.Look(ref currentPasture, "currentPasture");
    }

    public void StartGrazing(Zone_Pasture pasture)
    {
        currentPasture = pasture;
        isGrazing = true;
        // Puedes añadir código extra para mover al perro al área de pastoreo
    }

    public void ReturnToCorral(Zone_Pasture corral)
    {
        currentPasture = corral;
        isGrazing = false;
        // Código para mover al perro de vuelta al corral
    }
}
