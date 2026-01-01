using RimWorld;
using Verse;

namespace HerdingDog
{
    public class PastureManager : GameComponent
    {
        private const int CheckInterval = 250; // Verificar cada 250 ticks (~4 segundos)

        public PastureManager()
        {
        }

        public PastureManager(Game game)
        {
        }
        
        public override void ExposeData()
        {
            base.ExposeData();
            // No hay datos que guardar en este componente
        }

        public override void GameComponentTick()
        {
            base.GameComponentTick();

            // Solo verificar cada CheckInterval ticks
            if (Find.TickManager == null || Find.TickManager.TicksGame % CheckInterval != 0) return;

            // Procesar cada mapa
            foreach (Map map in Find.Maps)
            {
                if (map == null) continue;
                ProcessMap(map);
            }
        }

        private void ProcessMap(Map map)
        {
            // Obtener todos los animales con componente de pastoreo
            foreach (Pawn animal in map.mapPawns.AllPawnsSpawned)
            {
                if (animal == null || animal.Dead || !animal.RaceProps.Animal) continue;

                CompPastureAnimal comp = PastureUtility.GetPastureComp(animal);
                if (comp == null || comp.Pawn == null) continue;

                CompPastureSchedule scheduleComp = animal.GetComp<CompPastureSchedule>();
                
                // Verificar condiciones para salir al pastoreo
                if (!comp.isGrazing && ShouldStartGrazing(animal, comp, scheduleComp, map))
                {
                    StartGrazing(animal, comp);
                }
                // Verificar condiciones para volver al corral
                else if (comp.isGrazing && ShouldReturnToCorral(animal, comp, scheduleComp))
                {
                    ReturnToCorral(animal, comp);
                }
            }
        }

        private bool ShouldStartGrazing(Pawn animal, CompPastureAnimal comp, CompPastureSchedule scheduleComp, Map map)
        {
            // No hay zonas configuradas
            if (comp.corralArea == null || comp.pastureArea == null) return false;

            // No hay perro entrenado disponible
            if (!PastureUtility.HasTrainedHerdingDog(map, animal.Faction)) return false;

            // Verificar hambre (prioridad sobre horario)
            if (PastureUtility.IsAnimalHungry(animal, 0.4f))
            {
                // Verificar que haya hierba en la zona de pastoreo
                return PastureUtility.HasEnoughGrass(comp.pastureArea, map);
            }

            // Verificar horario
            if (scheduleComp != null)
            {
                int currentHour = GenLocalDate.HourOfDay(animal);
                if (scheduleComp.ShouldGrazeNow(currentHour))
                {
                    return PastureUtility.HasEnoughGrass(comp.pastureArea, map);
                }
            }

            return false;
        }

        private bool ShouldReturnToCorral(Pawn animal, CompPastureAnimal comp, CompPastureSchedule scheduleComp)
        {
            // Animal está lleno
            if (PastureUtility.IsAnimalFull(animal, 0.85f))
            {
                return true;
            }

            // Verificar horario
            if (scheduleComp != null)
            {
                int currentHour = GenLocalDate.HourOfDay(animal);
                if (!scheduleComp.ShouldGrazeNow(currentHour))
                {
                    return true;
                }
            }

            return false;
        }

        private void StartGrazing(Pawn animal, CompPastureAnimal comp)
        {
            if (animal == null || comp == null || comp.pastureArea == null) return;

            // Cambiar zona permitida a pastoreo
            if (animal.playerSettings != null)
            {
                animal.playerSettings.AreaRestrictionInPawnCurrentMap = comp.pastureArea;
                comp.isGrazing = true;
            }
        }

        private void ReturnToCorral(Pawn animal, CompPastureAnimal comp)
        {
            if (animal == null || comp == null || comp.corralArea == null) return;

            // Cambiar zona permitida de vuelta al corral
            if (animal.playerSettings != null)
            {
                animal.playerSettings.AreaRestrictionInPawnCurrentMap = comp.corralArea;
                comp.isGrazing = false;
            }
        }
    }
}

