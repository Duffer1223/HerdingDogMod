using Verse;
using RimWorld;
using UnityEngine; // Necesario para Color

public class Zone_Pasture : Zone
{
    public Zone_Pasture(ZoneManager zoneManager, string name) : base(name, zoneManager)
    {
    }

    public override string GetInspectString()
    {
        return "Zona de pastoreo";
    }

    // Implementación obligatoria del miembro abstracto
    protected override Color NextZoneColor
    {
        get
        {
            return new Color(0f, 1f, 2f); // Cian claro, por ejemplo
        }
    }
}
