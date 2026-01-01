using RimWorld;
using Verse;

namespace HerdingDog
{
    public class CompPastureSchedule : ThingComp
    {
        // Horario simple: true = pastar, false = corral
        private bool[] schedule = new bool[24];

        public override void Initialize(CompProperties props)
        {
            base.Initialize(props);
            // Inicializar horario por defecto: pastar de 6 AM a 6 PM
            for (int i = 0; i < 24; i++)
            {
                schedule[i] = (i >= 6 && i < 18);
            }
        }

        public override void PostExposeData()
        {
            base.PostExposeData();
            
            // Guardar y cargar el horario
            for (int i = 0; i < 24; i++)
            {
                bool value = schedule[i];
                Scribe_Values.Look(ref value, "schedule_" + i, i >= 6 && i < 18);
                if (Scribe.mode == LoadSaveMode.LoadingVars)
                {
                    schedule[i] = value;
                }
            }
        }

        public bool ShouldGrazeNow(int hour)
        {
            if (hour < 0 || hour >= 24) return false;
            return schedule[hour];
        }

        public void SetSchedule(int hour, bool shouldGraze)
        {
            if (hour >= 0 && hour < 24)
            {
                schedule[hour] = shouldGraze;
            }
        }

        public bool GetSchedule(int hour)
        {
            if (hour >= 0 && hour < 24)
            {
                return schedule[hour];
            }
            return false;
        }
    }
}

