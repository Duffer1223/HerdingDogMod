using RimWorld;
using Verse;

namespace HerdingDog
{
    public class HerdingDogMod : Mod
    {
        public HerdingDogMod(ModContentPack content) : base(content)
        {
            Log.Message("[HerdingDog] Mod loaded");
        }
    }

    [StaticConstructorOnStartup]
    public static class HerdingDogInit
    {
        static HerdingDogInit()
        {
            Log.Message("[HerdingDog] Static constructor initialized");
            
            // El PastureManager se registra automáticamente cuando se crea un nuevo Game
            // mediante el constructor GameComponent(Game game)
        }
    }
}

