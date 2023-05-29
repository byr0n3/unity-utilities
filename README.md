# Unity Utilties
A collection of utility files and classes for working with Unity projects. 

## Installation
Add the following Git URL as a package dependency:

`git@github.com:byr0n3/unity-utilities.git`

If you get a `permission denied` error in your console, try adding the package using the following URL:

`https://github.com/byr0n3/unity-utilities.git`

## `Byrone.Utilities`
Contains global utilities and classes.

#### Event
Custom event class for subscribing, unsubscribing and invoking events.

#### Flusher
Class for keeping track of [`System.Action`](https://learn.microsoft.com/en-us/dotnet/api/system.action)'s with the functionality to invoke/`Flush` all of them (useful for keeping track of `Event` unsubscribe functions).

#### RecyclePool
Custom pooling system.

#### StringBuilder2
Custom `StringBuilder` optimized for performance and minimum GC allocation.

## `Byrone.Utilities.Extensions`
Extension methods for various classes
