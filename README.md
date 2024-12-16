# Station Seven

Station Seven is a civilization-building game on Mars where the goal is to construct a self-sustaining research lab. Built by Ethan Pham using Unity3D.

The player is faced with managing three different stats: energy, oxygen, and food. In order for the mission to be a success, the colony must be able to sustain 100 people within 80 turns, where 1 person requires a net gain of 5 in all stats. Build structures to generate and store these stats.

## Implementation Details

The level, a cube in space with hills and mountains, is procedurally generated with a modified Perlin noise map combined with a falloff map. The buildable space within the level is a grid-based system that takes into account the elevation of each tile when determining buildable space. When a building is placed, all tiles it is placed upon are marked as unbuildable and prevent other buildings from using that space.

The game is turned based, using a system to record changes in resources and committing them when the turn ends. A calculation based on those resources controls whether the player finishes the game.

NPCs are instantiated every third building, and randomly pathfind to different buildings to simulate performing work. Pathfinding utilizes the Unity NavMesh system, and pathfind destinations are randomly selected from all placed buildings on the map.

## Installation

Clone the repository and open in Unity 2022.3.
