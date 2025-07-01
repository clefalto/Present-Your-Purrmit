![](https://lh7-rt.googleusercontent.com/docsz/AD_4nXcM030zBiaXD-OiJhLH8jFBwFWac_tCqz1JTVvFK13xWOxxM-VufK0ON3MuFabFjZJ3bsLK6mxO1WYXc9BBnwAGJ1RdsFdbd1lHVdYuShx1fK_OnZawf0ZQTpQa6dZgljVz9oiS2Q?key=Uq_QqO-eit5CNMKxitTt-w)
<p align="center">
  Present Your Purrmit
  Team 5
  Alexis Manalastas, Benedict Ignacio, Garett Hammerle
</p>


# Introduction

_Present Your Pummit_ is a comedy simulation game inspired by titles such as _Papers, Please_ and _VR Job Simulator_. In this game, the player takes on the role of a border inspector in the feline-filled country of Meowzkovia. The player meets  NPCs seeking entry and inspects their paperwork for validity, cross-referencing dates and expiry, checking for proper vaccinations, etc. This project was developed as a virtual reality (VR) experience using the Unity game engine.

Our project explores the problem space of designing engaging, entertaining, and replayable gameplay by capitalizing on the immersive capabilities of VR. Virtual reality plays a key role by introducing a 3D, hands-on spin to the job simulation genre by allowing players to physically interact with paperwork and engage with NPCs. The VR experience also transports the user directly to the office environment, further enhancing immersion and enjoyment.


# Design Overview

The game loop of _Present Your Purrmit_ is summarized below:

1. Cat NPC greets the player with a few lines of dialogue

2. NPC gives the player their “papers” – an identification and permit

3. Player can choose to approve or reject the permit

4. NPC responds according to player choice, with a bit of dialogue

5. NPC walks out of the room, the direction depends on whether they were rejected or accepted

6. Repeat with subsequent NPCs

Before the first NPC greets the player, they receive instructions from their “boss,” serving as a tutorial for the player to get a sense of the game loop and how to identify correct and incorrect permits. 

In order to determine whether they should accept or reject any given NPC, the player can look at a few details on their permit:

- The expiration date, which can be compared with the current in-game day (August 12th, 2088 in the demo)

- The NPCs vaccinations; the player is instructed to reject any candidate without the “Meowlaria” vaccine

The player’s choice for each NPC does not have much effect on their gameplay. The ending only changes if they accept every NPC, in which case their boss fires them. In a full version, the gameplay (for subsequent days) should change depending on which NPC’s the player accepts. 


# Implementation Details

Our project uses the Unity game engine, along with the Meta XR packages for adding VR integrations. 


### Interactions

For object interactions, we used the Meta XR Interactions SDK which includes prefabs for making objects which can be grabbed using the Meta Quest’s hand tracking features. Hand tracking allows for a higher level of immersion for our players than controllers (and it’s also more fun). The player can pick up many objects on their desk, including the NPCs permits, the accept and reject stamps, a coffee cup, a mouse and keyboard, and a few crumpled pieces of paper. These are also physics objects, so the player can throw them around and see them collide. This uses Unity physics under the hood, so no extra scripting needed to be done. 

We added some small object interactions on the player’s desktop and in their office. The player can “drink” their coffee by tilting the cup, and refill it by putting it under the coffeemaker. The office fan also spins which adds a bit of realism. 


### NPC Behavior

Our NPCs are represented in the game world via flat images. This allows us to express their personality while avoiding having to 3D model them. 

NPCs are scripted to move from one location to another using coroutines, all while translating up and down as if they’re walking. When they get to their initial location, they will start saying their lines of dialogue, which are given in a text file. Each line of the text file is said every 3 seconds by the NPC, but this value can be changed per NPC. When the end of the text file is read, their dialogue is finished, and they will give the player their permit. 

NPCs are sent into the room by the NPCManager script, which contains a list of NPCs to send. Each NPC sends a signal to this script when they leave the room, which sends in the next one. 


### Permit Behavior

Permits can be stamped by the player. This is done with a simple Unity physics collision with the permit and the player’s stamp. Depending on the stamp, the permit’s material will be swapped to either the “Approved” or “Rejected” one. This also changes the Permit script’s state to that of the respective stamp. The player can pass the permit back to the NPC by throwing it at them. There’s a box collider in front of their desk which will detect when a permit overlaps with it. This will then call a function on the PaperManager script to despawn the permit and give the “verdict” to the NPCManager. 


# Challenges

### Time Constraints

For the most part, we implemented all the features necessary for our gameplay loop. However, due to scope creep, we had to cut back on some extraneous features that would’ve increased the complexity of our gameplay loop. One such feature includes a metal detector that detects any contraband that an NPC may smuggle in during inspection. However, these extra features will be something that we will plan to implement in the future (see Future Work). For the duration of the project, we wanted to focus mainly on implementing gameplay content and building the VR environment for our demo.

In addition to having to simplify our gameplay loop, we also imported most of our decorative 3D assets instead of creating custom assets through 3D modeling tools such as Blender. Doing this ensured that we would only have to create the essential 2D and 3D assets by ourselves, leaving more time to focus on adding more gameplay content.


### Technical Issues

During our development process, we also ran into issues with Unity, particularly with its physics engine and building process. While we were testing our game, we had to redesign our permit model such that it wouldn’t clip through the table or fly off once we interacted with it. Although we mostly sanded this bug off during development, players could potentially still have the permits clip through the table, though it is now more difficult to do so. For the most part, these bugs do not detract from the gameplay experience.

Another massive issue we came across during development was building the application on different devices. Most of our development was through our laptops rather than the PCs provided during lab sessions. Although we could easily work on features online using Github, this also meant that we would have to fork and build the game on our own devices, which usually took a long time. This slowed down our development process, especially during testing.

On top of that, we wanted to also implement cross-compatibility between different Meta Quest devices. This meant having to implement features that could work across the Meta Quest 2, the Meta Quest 3, and the VR simulator. A particularly difficult task for us was working on game controls, either with the controllers or hand tracking. Despite this slowing down our testing process, by the end we were able to make our game compatible with as many devices as possible.


# Results

Our accomplishments for this project include:

- A complete 5-minute demo!

- One full in-game day

- Six fully-functional NPCs

- Fun interactables in the office

- Successful branching dialogue and multiple ending capabilities

[Present Your Purrmit - Trailer](https://drive.google.com/file/d/12qGQZnALAV1v4O3HuDUID1Ee1a_L4D6c/view?usp=drive_link)

[Present Your Purrmit - Gameplay Demo](https://drive.google.com/file/d/1pKmv6LJz0OpSF7qpMEoT8QWmHWHVQk9p/view?usp=drive_link)

![](https://lh7-rt.googleusercontent.com/docsz/AD_4nXfZJtixDxrolDIsDHipQ3jN0XDJ8OpYmS16WS4_Ql4pyRfOePSmA9K95Dthf8uodf_rr0JtyXJ2MMe8rIRkur44dQ0SvlyDcK-p04WE8ITzKDxEZGXqBZdSR23x-mhss2z-2kqr?key=Uq_QqO-eit5CNMKxitTt-w "holding permit.gif")![](https://lh7-rt.googleusercontent.com/docsz/AD_4nXfBnfwonIDdHY4FkQJhnghUWu7p7q3Cbt3i19c419UpV0gNebSraFOuMr6dL0rTPw2B4oAIDsgMTLHGrR8vyVhnOYoOnOJ7qI_HT6KKMkFLfK46J0VJvEGUVjCS1ApNOqPoBDbi_Q?key=Uq_QqO-eit5CNMKxitTt-w "hand demo.gif")![](https://lh7-rt.googleusercontent.com/docsz/AD_4nXeX8SUVUg1lT3RV2JUI4dOcoZguaL0iJrIY3cZ5gOjFJn_a5vqm6ynPkQCwAh71phbInv24itDGHZXsp-DDaLzmGgEtaEABOnmsejlPO4rYOjPjr9ZpIx8V1JDSwqtGocgvSPhy?key=Uq_QqO-eit5CNMKxitTt-w "stamping.gif")


# Future Work

To expand and refine the user experience, we plan to implement several new features to enhance gameplay depth and polish.


### Main Menu & UI Improvements:

A full menu system that allows for players to create saves, load previous saves, pause gameplay, access settings, admire collectables, and review previously-earned endings is integral to clean game design.


### Full Week of Gameplay (5 Days)

A full week of gameplay lets the user participate in an in-depth narrative experience, which can include branching events based on previous player decisions. For example, accepting or rejecting certain NPCs could cause unique events to trigger in later days.


### Enhancements to gameplay

Additional gameplay enhancements can be added to create a more engaging experience without feeling repetitive as total playtime increases. These gameplay enhancements include:

- Increasing complexity of paperwork as the week progresses 

  - Passports from foreign countries

  - Vaccine records

  - Diplomat permits

* Extra office interactions

  - Phone to receive calls from boss

  - Interactable computer

- Metal Detector

  - Weight identification paper to check for discrepancies

  - Metal detector that player can hold and wave over NPC to find contraband

* Collectables to represent user progress
