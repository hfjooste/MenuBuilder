namespace ThirdPixelGames.MenuBuilder
{
    using UnityEngine;
    using UnityEngine.InputSystem;
    
    /// <summary>
    /// Monitor for input device changes and update the UI when needed
    /// </summary>
    public class MenuDeviceMonitor : MonoBehaviour
    {
        #region Public Variables
        /// <summary>
        /// A list of all keyboard indicators
        /// </summary>
        [Header("References")]
        [Tooltip("A list of all keyboard indicators")]
        public GameObject[] keyboardIndicators;

        /// <summary>
        /// A list of all Xbox indicators
        /// </summary>
        [Tooltip("A list of all Xbox indicators")]
        public GameObject[] xboxIndicators;

        /// <summary>
        /// A list of all PlayStation indicators
        /// </summary>
        [Tooltip("A list of all PlayStation indicators")]
        public GameObject[] playStationIndicators;

        /// <summary>
        /// A list of all Switch indicators
        /// </summary>
        [Tooltip("A list of all Switch indicators")]
        public GameObject[] nintendoSwitchIndicators;

        /// <summary>
        /// A list of all generic controller indicators
        /// </summary>
        [Tooltip("A list of all generic controller indicators")]
        public GameObject[] genericControllerIndicators;
        #endregion
        
        #region Private Variables
        /// <summary>
        /// A reference to the MenuInput component in the scene
        /// </summary>
        private MenuInput _input;
        #endregion
        
        #region Unity Methods
        /// <summary>
        /// Awake is called when the script instance is being loaded
        /// </summary>
        private void Awake()
        {
            // Find a reference to the MenuInput component
            _input = FindObjectOfType<MenuInput>();
            
            // Check if we've found a MenuInput component
            if (_input == null)
            {
                // Log this error if we can't find the required MenuInput component
                Debug.LogError("MenuInput not found");
            }
        }
        
        /// <summary>
        /// This function is called when the object becomes enabled and active
        /// </summary>
        private void OnDisable()
        {
            // Remove all the input listeners
            _input.onInputReceived.RemoveListener(OnInputReceived);
        }

        /// <summary>
        /// This function is called when the behaviour becomes disabled
        /// </summary>
        private void OnEnable()
        {
            // Add an input listener
            _input.onInputReceived.AddListener(OnInputReceived);
            
            // Hide all the indicators
            ToggleIndicatorVisibility(ref keyboardIndicators, false);
            ToggleIndicatorVisibility(ref xboxIndicators, false);
            ToggleIndicatorVisibility(ref playStationIndicators, false);
            ToggleIndicatorVisibility(ref nintendoSwitchIndicators, false);
            ToggleIndicatorVisibility(ref genericControllerIndicators, false);
        }
        #endregion
        
        #region Private Methods

        /// <summary>
        /// Invoked after receiving any input event from the Input System
        /// </summary>
        /// <param name="inputDevice">The current input device used by the user</param>
        private void OnInputReceived(InputDevice inputDevice)
        {
            // Check if we're using a keyboard
            if (inputDevice.name.ToLower().Contains("keyboard"))
            {
                // Show the keyboard indicators
                UpdateIndicators(ref keyboardIndicators);
                return;
            }
            
            // Check if we're using an Xbox controller
            if (inputDevice.name.ToLower().Contains("xbox"))
            {
                // Show the Xbox indicators
                UpdateIndicators(ref xboxIndicators);
                return;
            }
            
            // Check if we're using a PlayStation controller
            if (inputDevice.name.ToLower().Contains("playstation") || 
                inputDevice.name.ToLower().Contains("ps"))
            {
                // Show the PlayStation indicators
                UpdateIndicators(ref playStationIndicators);
                return;
            }

            // Check if we're using an Switch controller
            if (inputDevice.name.ToLower().Contains("nintendo") ||
                inputDevice.name.ToLower().Contains("switch") ||
                inputDevice.name.ToLower().Contains("joycon") ||
                inputDevice.name.ToLower().Contains("joy-con"))
            {
                // Show the Switch indicators
                UpdateIndicators(ref nintendoSwitchIndicators);
                return;
            }
            
            // Show the generic controller indicators
            UpdateIndicators(ref genericControllerIndicators);
        }

        /// <summary>
        /// Update the visible input indicators
        /// </summary>
        /// <param name="visibleIndicators">The indicators that should be visible</param>
        private void UpdateIndicators(ref GameObject[] visibleIndicators)
        {
            // Hide all the indicators
            ToggleIndicatorVisibility(ref keyboardIndicators, false);
            ToggleIndicatorVisibility(ref xboxIndicators, false);
            ToggleIndicatorVisibility(ref playStationIndicators, false);
            ToggleIndicatorVisibility(ref nintendoSwitchIndicators, false);
            ToggleIndicatorVisibility(ref genericControllerIndicators, false);

            // Show the correct indicators
            ToggleIndicatorVisibility(ref visibleIndicators, true);
        }

        /// <summary>
        /// Update the input indicators' visibility
        /// </summary>
        /// <param name="indicators">The indicators to update</param>
        /// <param name="isVisible">Is the indicators supposed to be visible?</param>
        private void ToggleIndicatorVisibility(ref GameObject[] indicators, bool isVisible)
        {
            // Check if we have indicators to use
            if (indicators == null || indicators.Length <= 0)
            {
                // Return if we don't have indicators
                return;
            }

            // Loop through all the indicators
            foreach (var indicator in indicators)
            {
                // Update the indicator's visibility
                indicator.SetActive(isVisible);
            }
        }
        #endregion
    }
}