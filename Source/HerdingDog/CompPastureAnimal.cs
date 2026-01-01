using RimWorld;
using Verse;

namespace HerdingDog
{
    public class CompPastureAnimal : ThingComp
    {
        public Area corralArea;
        public Area pastureArea;
        public bool isGrazing;

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_References.Look(ref corralArea, "corralArea");
            Scribe_References.Look(ref pastureArea, "pastureArea");
            Scribe_Values.Look(ref isGrazing, "isGrazing", false);
        }

        public Pawn Pawn => parent as Pawn;
    }
}

