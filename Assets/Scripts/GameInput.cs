using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public static class GameInput
{
   
    public static bool JumpPressed()
    {
#if ENABLE_INPUT_SYSTEM
        Keyboard kb = Keyboard.current;
        Mouse mouse = Mouse.current;
        Touchscreen touch = Touchscreen.current;

        bool key = kb != null && (kb.spaceKey.wasPressedThisFrame
                                  || kb.upArrowKey.wasPressedThisFrame
                                  || kb.wKey.wasPressedThisFrame);
        bool click = mouse != null && mouse.leftButton.wasPressedThisFrame;
        bool tap = touch != null && touch.primaryTouch.press.wasPressedThisFrame;
        return key || click || tap;
#else
        return Input.GetKeyDown(KeyCode.Space)
               || Input.GetKeyDown(KeyCode.UpArrow)
               || Input.GetKeyDown(KeyCode.W)
               || Input.GetMouseButtonDown(0)
               || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began);
#endif
    }

    
    public static bool CrouchHeld()
    {
#if ENABLE_INPUT_SYSTEM
        Keyboard kb = Keyboard.current;
        return kb != null && (kb.downArrowKey.isPressed || kb.sKey.isPressed);
#else
        return Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
#endif
    }

 
    public static bool RestartPressed()
    {
#if ENABLE_INPUT_SYSTEM
        Keyboard kb = Keyboard.current;
        return kb != null && kb.rKey.wasPressedThisFrame;
#else
        return Input.GetKeyDown(KeyCode.R);
#endif
    }
}
