# HerdingDogMod
RimWorld mod - herding dogs for grazing
# Mod Herding Dog para RimWorld 1.6

Este mod añade perros pastores que pueden autorizar el pastoreo de animales fuera de los corrales usando zonas de pastoreo, según un horario programable o cuando detecten que los animales tienen hambre.

## Características

- **Sistema de Zonas**: Usa zonas permitidas del juego (sin forzar movimiento)
- **Horario Programable**: Sistema simple de 24 horas (pastar/corral)
- **Detección Automática de Hambre**: El sistema detecta animales hambrientos y los saca automáticamente
- **Perro Pastor**: Requiere un perro entrenado en "Pastoreo" para autorizar el sistema
- **Alimentación Natural**: Facilita la crianza de ganado sin necesidad de plantar cultivos

## Estructura del Mod

```
HerdingDog/
├─ About/
│  ├─ About.xml (obligatorio)
│  └─ About.html (descripción visual)
├─ Defs/
│  ├─ ThingDefs/
│  │  ├─ Races_Animal_HerdingDog.xml (perro pastor)
│  │  └─ HerdingAnimalComp.xml (componentes base)
│  └─ TrainableDefs/
│     └─ Trainables_HerdingDog.xml (entrenamiento de pastoreo)
├─ Source/
│  ├─ HerdingDog/
│  │  ├─ HerdingDogMod.cs (clase base del mod)
│  │  ├─ CompPastureAnimal.cs (componente de pastoreo)
│  │  ├─ CompPastureSchedule.cs (horario)
│  │  ├─ PastureUtility.cs (utilidades)
│  │  └─ PastureManager.cs (lógica principal)
│  └─ HerdingDog.csproj (proyecto C#)
└─ Assemblies/
   └─ HerdingDog.dll (generado al compilar)
```

## Instrucciones de Compilación

1. **Abrir el proyecto**:
   - Abre `Source/HerdingDog.csproj` en Visual Studio
   - Asegúrate de tener .NET Framework 4.7.2

2. **Configurar referencias**:
   - Establece la variable de entorno `RimWorldDir` apuntando a la carpeta de instalación
   - O modifica manualmente las rutas en `.csproj` para que apunten a:
     - `RimWorldWin64_Data\Managed\Assembly-CSharp.dll`
     - `RimWorldWin64_Data\Managed\0Harmony.dll`
     - `RimWorldWin64_Data\Managed\UnityEngine.CoreModule.dll`

3. **Compilar**:
   - Selecciona "Release"
   - Compila (Build > Build Solution)
   - El DLL se generará en `Assemblies/HerdingDog.dll`

## Uso (Conceptual)

El sistema funciona automáticamente una vez configurado:

1. **Obtener un Perro Pastor**: Aparece como nueva raza en el juego
2. **Entrenar al Perro**: Entrénalo en "Pastoreo" (nuevo entrenamiento)
3. **Configurar Zonas**: 
   - Asigna zona de corral a los animales
   - Crea zona de pastoreo con hierba natural
4. **Configurar Horario**: Por defecto pastorea de 6 AM a 6 PM
5. **Detección Automática**: Si un animal tiene hambre (< 40%), sale automáticamente

## Cómo Funciona

- **No fuerza movimiento**: Solo cambia zonas permitidas (`animal.playerSettings.AreaRestriction`)
- **Prioridad de hambre**: La hambre tiene prioridad sobre el horario
- **Retorno automático**: Los animales vuelven al corral cuando están llenos (> 85%) o termina el horario
- **Requiere perro entrenado**: Sin perro, no hay pastoreo (balance)

## Limitaciones Actuales

- No hay interfaz gráfica para configurar el horario (se planea)
- Los componentes se aplican mediante `HerdingAnimalBase` (se necesita aplicar a animales específicos)
- El `PastureManager` se registra como GameComponent (puede necesitar ajustes)

## Notas Técnicas

- El mod requiere RimWorld 1.6
- Usa el sistema de zonas vanilla (compatible y estable)
- El código está en C# y usa la API de RimWorld
- `PastureManager` es un GameComponent que se ejecuta cada 250 ticks

## Desarrollo Futuro

- Interfaz gráfica para configurar horario
- Aplicación automática de componentes a animales comunes
- Opciones configurables (umbrales de hambre, etc.)
- Mejoras en la detección de hierba