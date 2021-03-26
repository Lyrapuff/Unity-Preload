# SmallTail.Preload
Makes preloading singletons in Unity as easy as marking a class with an attribute

# Instalation
Via Unity Package Manager add from git url 

`https://github.com/SmallTailTeam/Unity-Preload.git`

# Usage
```csharp
[Preloaded("MapGrid")]
public class MapGrid : MonoBehaviour
...
```

And that's it, an empty object with this MonoBehaviour attached to it will be instantiated and DontDestroyOnLoaded as soon as the game starts.

Though be aware that this thing is not well tested yet, and perhaps will be extended in future to support multiple use cases.
