# Menu Builder for Unity

## License
You are free to do whatever you want with this library. You're allowed to use this for commercial projects, edit the source code and even redistribute the source code. No attribution is required (but appreciated)

## Requirements
- Unity 2020.3.11f1
- Unity Input System

## Features
This tool allows you to create complex menus without writing a single line of code. It includes to following features
<ol>
    <li>Keyboard and controller support using the Unity Input System</li>
    <li>Vertical, Horizontal and Tabbed style menus</li>
    <li>Wrap-around when exceeding the start/end positions</li>
    <li>Activated, deactivated, selected and canceled events that can be handled inside the editor</li>
</ol>

## Setting up the input
![Input System](https://i.imgur.com/cbsHqZ4.png)
This library includes demo input actions that should be perfect for any game. If you want to create your own, you can follow these steps:
<ol>
    <li>Right click in the project and select Create > Input Actions</li>
    <li>Use <b>InputActions</b> as the name</li>
    <li>Open the new Input Actions file</li>
    <li>Create a new control scheme</li>
    <li>Create a <b>Menu</b> Action Map</li>
    <li>
        Create the following actions:
        <ul>
            <li><b>Vertical</b> (1D Axis)</li>
            <li><b>Horizontal</b> (1D Axis composite)</li>
            <li><b>Tabs</b> (1D Axis composite)</li>
            <li><b>Select</b> (Button binding)</li>
            <li><b>Cancel</b> (Button binding)</li>
        </ul>
    </li>
    <li>Specify the key bindings for each action</li>
</ol>

<i>Please refer to the official Unity documentation for more information: https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/QuickStartGuide.html</i>

## Handling input inside the scene
![Menu Input](https://i.imgur.com/lhuG8YP.png)
<ol>
    <li>Add a new empty game object to your scene</li>
    <li>Add a <b>Menu Input</b> component to this new game object</li>
</ol>

This <b>Menu Input</b> component will read the input from the Unity Input System (using the InputActions file) and invoke the correct events when needed

## Creating a menu
![Menu](https://i.imgur.com/2VzC3pt.png)
<ol>
    <li>Add a new empty game object to your scene</li>
    <li>Add a <b>Menu</b> component to this new game object</li>
    <li>
        Set the properties of the menu
        <ul>
            <li><b>Active:</b> A boolean value indicating if the menu can receive input events</li>
            <li><b>Menu Style:</b> The type of menu you're using (Vertical, Horizontal or Tabbed)</li>
            <li><b>Reset Index After Loading:</b> Reset the selected index after loading the menu (or disabling and enabling it)</li>
            <li><b>Wrap Around:</b> Enable/disable the wrap-around feature when exceeding the start/end position of the menu</li>
            <li><b>Menu Items:</b> A list of all menu items managed by this menu</li>
        </ul>
    </li>
    <li>Set up the canceled event (optional)</li>
</ol>

## Creating a menu item
![Menu Item](https://i.imgur.com/N03Q9cd.png)
<ol>
    <li>Add a new empty game object to your scene</li>
    <li>Add a <b>Menu Item</b> component to this new game object</li>
    <li>Set up the activated event (optional). This is invoked when the menu item is active</li>
    <li>Set up the deactivated event (optional). This is invoked when the menu item is deactivated</li>
    <li>Set up the selected event (optional). This is invoked when the menu item is selected</li>
    <li>Design the menu item</li>
</ol>

<i>Remember to add the menu item to the menu. Otherwise no events will be invoked</i>

## Demo
![Demo Scene](https://i.imgur.com/IAc8pEM.png)
This library includes a demo scene that shows you how to create vertical, horizontal and tabbed menus. It also includes different menus using different features available in the library. There's a <b>Input</b> directory that contains an <b>InputActions</b> file that should be perfect for your game