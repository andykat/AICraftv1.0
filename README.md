# AICraftv1.0
1v1 Strategy a.i. game
## Objective 
Kill the opponents nexus

##Game Summary
Game occurs on a 5x8 map, with players' bases on opposite sides.<br />
Gold is earned every frame, and can be spent to spawn units.<br />
Units are able to move and attack, whoever kills the opponent's nexus wins<br />
Units can only be controlled by a set of commands given every frame<br />

# The numbers Alan, what do they mean??
all numbers are still in testing.
###Nexus
Generates 5 gold/frame<br />
200 Health<br />
May be given attack in the future<br />

##Spawnable Units
###Zergling
50 Gold<br />
50 Health<br />
0.2 movement speed<br />
0.2 attack range<br />
10 attack damage<br />

###Marine
80 Gold<br />
50 health<br />
0.1 movement speed<br />
0.6 attack range<br />
10 attack damage<br />

#Commands
Commands are sent from a C# class inheriting the AI interface.<br />
Each Unit can only perform 1 command each frame (i.e. you cannot move and attack at the same time).<br />
##Command 1: Spawn [Spawns a unit next to the nexus]
Type (type of unit to spawn)

##Command 2: Move [moves a unit in a certain direction at a distance equal to the unit's movement speed]
ID (unit to be moved)
Rotato (direction of movement)

##Command 3: Attack [unit attacks another unit. If enemy is out of range, the attack will fail]
ID (unit that will attack)
E_ID (unit being attacked)
