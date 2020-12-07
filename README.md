# Easy buttons for the Unity default inspector
These tiny scripts add the ability to quickly show a button in the inspector for any zero-argument method.

## How to use
### Install via Git URL
Project supports Unity Package Manager. To install the project as a Git package do the following:

1. In Unity, open **Window** -> **Package Manager**.
2. Press the **+** button, choose "**Add package from git URL...**"
3. Enter "https://github.com/madsbangh/EasyButtons.git#upm" and press **Add**.

### Add to project Assets
Alternatively you can add the code directly to the project:

1. Clone the repo or download the latest release.
2. Add the EasyButtons folder to your Unity project or import the .unitypackage

### Use the Button attribute
1. Add the Button attribute to a method

   ![Code example](/Images/example.png)
   
2. You should now see a button at the top of the component with the same name as the method

   ![Button in the inspector](/Images/inspector.png)

   ![Result](/Images/console.png)
   
   ### Attribute Options
   
   The `Button` attribute has different options that allow customizing the button look.
   
   ***Mode*** - indicates when the button is enabled. You can choose between the following options:
   
   - AlwaysEnabled - the button is enabled in edit mode and play mode.
   
   - EnabledInPlayMode - the button is only enabled in play mode.
   
   - DisabledInPlayMode - the button is only enabled in edit mode.
   
   ***Spacing*** - allows to have some space before or after the button. The following options can be used:
   
   - None - no spacing at all.
   
   - Before - adds space above the button.
   
   - After - adds space below the button.
   
   ***Expanded*** - whether to expand the parameters foldout by default. It only works with buttons that have parameters.
   
   ### Custom Editors
   
   If you want to show buttons in a custom editor, you need to derive it from the `ObjectEditor` class.
   
   First-of-all, if you use `OnEnable()` in the custom editor, mark it `protected override` instead of `private` and use `base.OnEnable()` inside, like this:
   
   ```csharp
   [CustomEditor(typeof(Example))]
   public class CustomEditor : ObjectEditor
   {
       // Make sure to override OnEnable instead of creating a new private one.
       protected override void OnEnable()
       {
           base.OnEnable();
   		// Do the rest of initialization.
       }
   }
   ```
   
   Then, you can draw all the buttons in `OnInspectorGUI` with help of the `DrawEasyButtons()` method, or draw only specific buttons in the wanted places, like this:
   
   ```csharp
   // Draw only the button called "Custom Editor Example"
   Buttons.First(button => button.DisplayName == "Custom Editor Example").Draw(targets);
   ```
   
   You can search for a specific button using its ***DisplayName*** or ***Method*** (MethodInfo object the button is attached to.)

## Notes
- Older versions of Unity might not understand the reference to EasyButtons runtime in the EasyButtons editor assembly definition. If you experience issues, you can re-add the reference, or remove the asmdefs completely.
