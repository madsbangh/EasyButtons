# Easy buttons for the Unity default inspector
[![openupm](https://img.shields.io/npm/v/com.madsbangh.easybuttons?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.madsbangh.easybuttons/)

These tiny scripts add the ability to quickly show a button in the inspector for any method.

## Installation

### OpenUPM

Once you have the OpenUPM cli, run the following command:

```openupm install com.madsbangh.easybuttons```

Or if you don't have it, add the scoped registry to manifest.json with the desired version: 
```json
  "scopedRegistries": [
    {
      "name": "package.openupm.com",
      "url": "https://package.openupm.com",
      "scopes": [
        "com.madsbangh.easybuttons",
        "com.openupm"
      ]
    }
  ],
  "dependencies": {
    "com.madsbangh.easybuttons": "1.4.0"
  }
```

### Git URL

Project supports Unity Package Manager. To install the project as a Git package do the following:

1. In Unity, open **Window** -> **Package Manager**.
2. Press the **+** button, choose "**Add package from git URL...**"
3. Enter "https://github.com/madsbangh/EasyButtons.git#upm" and press **Add**.

### Unity Package
Alternatively, you can add the code directly to the project:

1. Clone the repo or download the latest release.
2. Add the EasyButtons folder to your Unity project or import the .unitypackage

## How To Use
1. Add the Button attribute to a method.

   ```csharp
   using EasyButtons; // 1. Import the namespace
   using UnityEngine;
   
   public class ButtonsExample : MonoBehaviour
   {
       // 2. Add the Button attribute to any method.
   	[Button]
   	public void SayHello()
       {
           Debug.Log("Hello");
       }
   }
   ```
   
2. You should now see a button at the top of the component with the same name as the method:

   ![Button in the inspector](/Images/inspector.png)

   ![Result](/Images/console.png)

3. Add the Button attribute to a method with parameters.

   ```csharp
   using EasyButtons;
   using UnityEngine;
   
   public class Test : MonoBehaviour
   {
       [Button]
       public void ButtonWithParameters(string message, int number)
       {
           Debug.Log($"Your message #{number}: '{message}'");
       }
   }
   ```

4. You can now enter parameter values and invoke the method in the inspector:

   ![Button with parameters](/Images/parameters.png)

## Attribute Options

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

## Custom Editors

If you want to show buttons in a custom editor, you can use the **ButtonsDrawer** class defined in EasyButtons.Editor.

Instantiate ButtonsDrawer in OnEnable if possible, then draw the buttons with help of the DrawButtons method, as in the example:

```csharp
[CustomEditor(typeof(Example))]
public class CustomEditor : ObjectEditor
{   
    private ButtonsDrawer _buttonsDrawer;

    private void OnEnable()
    {
        _buttonsDrawer = new ButtonsDrawer(target);
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        _buttonsDrawer.DrawButtons(targets);
    }
}
```

You can also draw only specific buttons:

```csharp
// Draw only the button called "Custom Editor Example"
_buttonsDrawers.Buttons.First(button => button.DisplayName == "Custom Editor Example").Draw(targets);
```

You can search for a specific button using its ***DisplayName*** or ***Method*** (MethodInfo object the button is attached to.)

## Notes
- Older versions of Unity might not understand the reference to EasyButtons runtime in the EasyButtons editor assembly definition. If you experience issues, you can re-add the reference, or remove the asmdefs completely.
