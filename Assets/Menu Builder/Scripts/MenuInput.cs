namespace ThirdPixelGames.MenuBuilder
{
    using UnityEngine;
    using UnityEngine.Events;

    /// <summary>
    /// Responsible for reading input from the Input System and invoke the correct input events
    /// </summary>
    public class MenuInput : MonoBehaviour
    {
        #region Public Events
        /// <summary>
        /// Invoked whenever we receive an up input event from the Input System
        /// </summary>
        [HideInInspector] public UnityEvent onMenuUp = new UnityEvent();

        /// <summary>
        /// Invoked whenever we receive a down input event from the Input System
        /// </summary>
        [HideInInspector] public UnityEvent onMenuDown = new UnityEvent();

        /// <summary>
        /// Invoked whenever we receive a left input event from the Input System
        /// </summary>
        [HideInInspector] public UnityEvent onMenuLeft = new UnityEvent();

        /// <summary>
        /// Invoked whenever we receive a right input event from the Input System
        /// </summary>
        [HideInInspector] public UnityEvent onMenuRight = new UnityEvent();

        /// <summary>
        /// Invoked whenever we receive a selected input event from the Input System
        /// </summary>
        [HideInInspector] public UnityEvent onMenuSelected = new UnityEvent();

        /// <summary>
        /// Invoked whenever we receive a canceled input event from the Input System
        /// </summary>
        [HideInInspector] public UnityEvent onMenuCanceled = new UnityEvent();
        #endregion

        #region Private Variables
        /// <summary>
        /// A reference to the input actions managed by the input system
        /// </summary>
        private InputActions _input;
        #endregion

        #region Unity Methods
        /// <summary>
        /// This function is called when the object becomes enabled and active
        /// </summary>
        private void OnEnable() => Init();

        /// <summary>
        /// This function is called when the behaviour becomes disabled
        /// </summary>
        private void OnDisable() => DisableInput();
        #endregion

        #region Private Methods
        /// <summary>
        /// Disable the input actions and stop receiving input from the Input System
        /// </summary>
        private void DisableInput()
        {
            // Check if we have input to disable
            if (_input == null)
            {
                // Already disabled
                return;
            }

            // Disable the input
            _input.Disable();
            
            // Dispose the input
            _input.Dispose();
            
            // Set the reference to null
            _input = null;
        }

        /// <summary>
        /// Enable the input actions and start receiving input from the Input System
        /// </summary>
        private void Init()
        {
            // Disable the input (if needed) before we initialize it
            DisableInput();

            // Create a new InputActions object
            _input = new InputActions();
            
            // Enable the input
            _input.Enable();

            // Monitor for input events received from the Input System
            _input.Menu.Vertical.performed += input => OnMenuVerticalPerformed(input.ReadValue<float>());
            _input.Menu.Horizontal.performed += input => OnMenuHorizontalPerformed(input.ReadValue<float>());
            _input.Menu.Select.performed += _ => onMenuSelected?.Invoke();
            _input.Menu.Cancel.performed += _ => onMenuCanceled?.Invoke();
        }

        /// <summary>
        /// Invoke the correct vertical input event based on the input received from the Input System
        /// </summary>
        /// <param name="input">The input received from the Input System</param>
        private void OnMenuVerticalPerformed(float input)
        {
            // Check if we have a value to process
            if (input == 0)
            {
                // Ignore the input if we don't have a value
                return;
            }

            // Check for a positive value
            if (input > 0)
            {
                // Invoke the menu up event
                onMenuUp?.Invoke();
            }
            else
            {
                // Invoke the menu down event
                onMenuDown?.Invoke();
            }
        }

        /// <summary>
        /// Invoke the correct horizontal input event based on the input received from the Input System
        /// </summary>
        /// <param name="input">The input received from the Input System</param>
        private void OnMenuHorizontalPerformed(float input)
        {
            // Check if we have a value to process
            if (input == 0)
            {
                // Ignore the input if we don't have a value
                return;
            }

            // Check for a positive value
            if (input > 0)
            {
                // Invoke the menu right event
                onMenuRight?.Invoke();
            }
            else
            {
                // Invoke the menu left event
                onMenuLeft?.Invoke();
            }
        }
        #endregion
    }
}