using RimWorld;
using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace HerdingDog
{
    public class JobDriver_HerdingDog : JobDriver
    {
        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            // No se requieren reservas previas para este trabajo por ahora
            return true;
        }

        protected override IEnumerable<Toil> MakeNewToils()
        {
            this.FailOnDespawnedNullOrForbidden(TargetIndex.A);

            // Toil vacío por ahora, solo para probar que compila
            Toil wait = Toils_General.Wait(200);
            wait.defaultCompleteMode = ToilCompleteMode.Delay;
            yield return wait;
        }
    }
}
