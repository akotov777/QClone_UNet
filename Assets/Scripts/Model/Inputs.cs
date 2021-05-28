using UnityEngine;


public static class Inputs
{
    public static class Movement
    {
        public static bool IsActive = true;

        public static float GetVerticalAxis()
        {
            if(!IsActive)
                return 0.0f;
            return Input.GetAxis("Vertical");
        }

        public static float GetHorizontalAxis()
        {
            if (!IsActive)
                return 0.0f;
            return Input.GetAxis("Horizontal");
        }

        public static bool JumpButtonPressed()
        {
            if (!IsActive)
                return false;
            return Input.GetKeyDown(KeyCode.Space);
        }
    }

    public static class Firing
    {
        public static bool IsActive = true;

        public static bool FireButtonDown()
        {
            if (!IsActive)
                return false;
            return Input.GetKeyDown(KeyCode.Mouse0);
        }
    }

    public static class Looking
    {
        public static bool IsActive = true;

        public static float GetXAxis()
        {
            if (!IsActive)
                return 0.0f;
            return Input.GetAxis("Mouse X");
        }

        public static float GetYAxis()
        {
            if (!IsActive)
                return 0.0f;
            return Input.GetAxis("Mouse Y");
        }
    }
}