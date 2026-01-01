using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace HerdingDog
{
    public static class PastureUtility
    {
        // Detectar si hay un perro pastor entrenado disponible
        public static bool HasTrainedHerdingDog(Map map, Faction faction)
        {
            if (map == null || faction == null) return false;

            foreach (Pawn pawn in map.mapPawns.SpawnedPawnsInFaction(faction))
            {
                if (IsHerdingDog(pawn) && IsTrainedForHerding(pawn))
                {
                    return true;
                }
            }
            return false;
        }

        // Verificar si un animal es un perro pastor
        public static bool IsHerdingDog(Pawn pawn)
        {
            if (pawn == null || !pawn.RaceProps.Animal) return false;
            
            // Verificar si es un perro (o el defName específico HerdingDog)
            if (pawn.def.defName == "HerdingDog" || (pawn.def.race != null && pawn.def.race.body?.defName == "Dog"))
            {
                TrainableDef herdingDef = DefDatabase<TrainableDef>.GetNamedSilentFail("Pastoreo");
                if (herdingDef != null)
                {
                    // Verificar si la raza puede aprender este trainable
                    return pawn.RaceProps.specialTrainables != null &&
                           pawn.RaceProps.specialTrainables.Contains(herdingDef);
                }
            }
            
            return false;
        }

        // Verificar si el perro está entrenado para pastorear
        public static bool IsTrainedForHerding(Pawn dog)
        {
            if (dog == null || !IsHerdingDog(dog)) return false;
            
            TrainableDef herdingDef = DefDatabase<TrainableDef>.GetNamedSilentFail("Pastoreo");
            if (herdingDef != null && dog.training != null)
            {
                return dog.training.HasLearned(herdingDef);
            }
            
            return false;
        }

        // Verificar si un animal tiene hambre
        public static bool IsAnimalHungry(Pawn animal, float threshold = 0.4f)
        {
            if (animal == null || animal.needs?.food == null) return false;
            return animal.needs.food.CurLevelPercentage < threshold;
        }

        // Verificar si un animal está lleno
        public static bool IsAnimalFull(Pawn animal, float threshold = 0.85f)
        {
            if (animal == null || animal.needs?.food == null) return false;
            return animal.needs.food.CurLevelPercentage >= threshold;
        }

        // Obtener el componente CompPastureAnimal
        public static CompPastureAnimal GetPastureComp(Pawn animal)
        {
            return animal?.GetComp<CompPastureAnimal>();
        }

        // Verificar si hay suficiente hierba en la zona de pastoreo
        public static bool HasEnoughGrass(Area pastureArea, Map map)
        {
            if (pastureArea == null || map == null) return false;

            int grassCount = 0;
            foreach (IntVec3 cell in pastureArea.ActiveCells)
            {
                if (cell.InBounds(map))
                {
                    Plant plant = cell.GetPlant(map);
                    if (plant != null && plant.def.plant != null && 
                        (plant.def.plant.harvestedThingDef == ThingDefOf.Hay || 
                         plant.def == ThingDefOf.Plant_Grass))
                    {
                        grassCount++;
                    }
                }
            }
            
            // Considerar suficiente si hay al menos 10 celdas con hierba
            return grassCount >= 10;
        }
    }
}

