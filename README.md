# Unity Utilties
A collection of utility files and classes for working with Unity projects. 

## Installation
Assuming you're in your Unity project's root project, run the following:

`git submodule add git@github.com:byr0n3/unity-utilities.git Assets/Scripts/Utilities`

## `Byrone.Utilities`
Contains global utilities and classes.

#### Event
Custom event class for subscribing, unsubscribing and invoking events.

#### Flusher
Class for keeping track of [`System.Action`](https://learn.microsoft.com/en-us/dotnet/api/system.action)'s with the functionality to invoke/`Flush` all of them (useful for keeping track of `Event` unsubscribe functions).

#### RandomUtilities
Helper functions for randomization and such.

#### RecyclePool
Custom pooling system.

## `Byrone.Utilities.Extensions`
Extension methods for various classes
