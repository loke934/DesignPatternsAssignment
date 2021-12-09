# Design Patterns Assignment
**By Emilia Lokrantz**

The gradable assignment for the design patterns course at FutureGames. It's part of a little prototype game where you are a duck cleaning up trash.
You collect trash and craft waste bins that you throw the trash in. 
The objective is to craft and place a certain amount of waste bins before the trash overflows. 

## Design Patterns
My main focus was to test and use design patterns. There are a lot of improvements that could be made but this is something for me to continue building on as I learn more. 

### Singleton
Used in the `ItemInventory.cs` and the `Pool.cs` scripts as ItemInventory Instance and Pool Instance, set in the Awake method. The choice was because I wanted a single instance of them as well as a global accessor for other scripts to use. Maybe it wasn't necessary because I think maybe static methods could have been enough but I wanted to try to use the pattern. 
### Component and composition
The player is divided into components in an attempt to isolate related code, all scrips are found in `Assets/Scripts/Player`. The container being the player prefab in the scene and all the components are attatcehd to it. Coupling between the components when needed through refrences that get assigned with GetComponent method in Awake. Combining data into structs are in the `ItemSettings.cs` and `WasteSpawnSettings.cs`, nothing fancy, they are used to make lists to loop through when creating and spawning objects.
### Object Pool and Factory
`Pool.cs` in `Assets/Scripts/ObjectPool` is a simple object pool to hold the "trash-objects" with methods GetPoolObject, that retrieve an object from the pool, and  Return method. The objects are created in the `ItemFactory.cs`, initially creating all the objects and adding them to the object pool in CreatePoolItems method. If the pool gets empty the factory creates more with CreateItem method.
### Observer
The `ItemInventory.cs` uses events that invoke when UI changes need to be made, such as inventory amount or when crafting is possible. The delegates and events are declared in the beginning of the class. The observers are in the `UiInventory.cs`, all of them subscribe in the SubscribeToEvent method. I also use events when the winning condition is met to stop spawning (observer is found in 'WasteSpawner.cs' and on game over, again with UI as an observer.
