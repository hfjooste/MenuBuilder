namespace ThirdPixelGames.MenuBuilder
{
    using UnityEngine;
    using UnityEngine.Events;

    public class Menu : MonoBehaviour
    {
        #region Public Variables
        /// <summary>
        /// A boolean value indicating if the menu should handle input events
        /// </summary>
        [Header("Properties")]
        [Tooltip("A boolean value indicating if the menu should handle input events")]
        public bool active = true;
        
        /// <summary>
        /// The type of menu (horizontal, vertical or tabs)
        /// </summary>
        [Tooltip("The type of menu (horizontal, vertical or tabs)")]
        public MenuStyle menuStyle;
        
        /// <summary>
        /// A boolean value indicating if the menu supports wrap-around input
        /// </summary>
        [Tooltip("A boolean value indicating if the menu supports wrap-around input")]
        public bool wrapAround = true;
        
        /// <summary>
        /// A reference to all the menu items managed by this menu
        /// </summary>
        [Tooltip("A reference to all the menu items managed by this menu")]
        public MenuItem[] menuItems;
        #endregion

        #region Public Events
        /// <summary>
        /// Invoked whenever the menu receives a cancel input event
        /// </summary>
        [Header("Events")]
        [Tooltip("Invoked whenever the menu receives a cancel input event")]
        public UnityEvent onCanceled = new UnityEvent();
        #endregion

        #region Private Variables
        /// <summary>
        /// A reference to the MenuInput component in the scene
        /// </summary>
        private MenuInput _input;
        
        /// <summary>
        /// The selected index of this menu
        /// </summary>
        private int _selectedIndex;
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
            // Disable the current menu
            active = false;

            // Remove all the input listeners
            _input.onMenuUp.RemoveListener(OnMenuUp);
            _input.onMenuDown.RemoveListener(OnMenuDown);
            _input.onMenuLeft.RemoveListener(OnMenuLeft);
            _input.onMenuRight.RemoveListener(OnMenuRight);
            _input.onMenuSelected.RemoveListener(OnMenuSelected);
            _input.onMenuCanceled.RemoveListener(OnMenuCanceled);
            _input.onMenuTabLeft.RemoveListener(OnMenuTabLeft);
            _input.onMenuTabRight.RemoveListener(OnMenuTabRight);
        }

        /// <summary>
        /// This function is called when the behaviour becomes disabled
        /// </summary>
        private void OnEnable()
        {
            // Enable the current menu
            active = true;

            // Add input listeners
            _input.onMenuUp.AddListener(OnMenuUp);
            _input.onMenuDown.AddListener(OnMenuDown);
            _input.onMenuLeft.AddListener(OnMenuLeft);
            _input.onMenuRight.AddListener(OnMenuRight);
            _input.onMenuSelected.AddListener(OnMenuSelected);
            _input.onMenuCanceled.AddListener(OnMenuCanceled);
            _input.onMenuTabLeft.AddListener(OnMenuTabLeft);
            _input.onMenuTabRight.AddListener(OnMenuTabRight);

            // Reset the selected index
            _selectedIndex = 0;
            
            // Update the menu items
            UpdateMenuItems();

            // Check if we're using a tabbed menu
            if (menuStyle == MenuStyle.Tabs)
            {
                // Select the item
                OnMenuSelected();
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Enable/Disable the menu
        /// </summary>
        /// <param name="isActive">A boolean value indicating if the menu is enabled/disabled</param>
        public void ToggleActive(bool isActive) => active = isActive;
        
        /// <summary>
        /// Invoked after receiving a selected event from the MenuInput component
        /// </summary>
        public void OnMenuSelected()
        {
            // Check if the menu is active
            if (!active)
            {
                // Don't select anything if the menu is not active
                return;
            }

            // Invoke the selected event on the current menu item
            menuItems[_selectedIndex].selected?.Invoke();
        }

        /// <summary>
        /// Invoked after receiving a canceled event from the MenuInput component
        /// </summary>
        public void OnMenuCanceled()
        {
            // Check if the menu is active
            if (!active)
            {
                // Don't cancel anything if the menu is not active
                return;
            }

            // Invoke the cancel event
            onCanceled?.Invoke();
        }
        #endregion
        
        #region Private Methods
        /// <summary>
        /// Invoked after receiving an up event from the MenuInput component
        /// </summary>
        private void OnMenuUp()
        {
            // Check if the menu is vertical
            if (menuStyle != MenuStyle.Vertical)
            {
                // Don't update the selection if we're not using a vertical menu
                return;
            }

            // Reduce the selected index
            UpdateSelection(-1);
        }

        /// <summary>
        /// Invoked after receiving a down event from the MenuInput component
        /// </summary>
        private void OnMenuDown()
        {
            // Check if the menu is vertical
            if (menuStyle != MenuStyle.Vertical)
            {
                // Don't update the selection if we're not using a vertical menu
                return;
            }

            // Increase the selected index
            UpdateSelection(1);
        }

        /// <summary>
        /// Invoked after receiving a left event from the MenuInput component
        /// </summary>
        private void OnMenuLeft()
        {
            // Check if the menu is horizontal
            if (menuStyle != MenuStyle.Horizontal)
            {
                // Don't update the selection if we're not using a horizontal menu
                return;
            }

            // Reduce the selected index
            UpdateSelection(-1);
        }

        /// <summary>
        /// Invoked after receiving a right event from the MenuInput component
        /// </summary>
        private void OnMenuRight()
        {
            // Check if the menu is horizontal
            if (menuStyle != MenuStyle.Horizontal)
            {
                // Don't update the selection if we're not using a horizontal menu
                return;
            }

            // Increase the selected index
            UpdateSelection(1);
        }
        
        /// <summary>
        /// Invoked after receiving a tab left event from the MenuInput component
        /// </summary>
        private void OnMenuTabLeft()
        {
            // Check if the menu is tabbed
            if (menuStyle != MenuStyle.Tabs)
            {
                // Don't update the selection if we're not using a tabbed menu
                return;
            }

            // Reduce the selected index
            UpdateSelection(-1);
            
            // Select the item
            OnMenuSelected();
        }

        /// <summary>
        /// Invoked after receiving a tab right event from the MenuInput component
        /// </summary>
        private void OnMenuTabRight()
        {
            // Check if the menu is tabbed
            if (menuStyle != MenuStyle.Tabs)
            {
                // Don't update the selection if we're not using a tabbed menu
                return;
            }

            // Reduce the selected index
            UpdateSelection(1);
            
            // Select the item
            OnMenuSelected();
        }

        /// <summary>
        /// Update the selected index
        /// </summary>
        /// <param name="value">The amount to increase/decrease</param>
        private void UpdateSelection(int value)
        {
            // Check if we have menu items to update
            if (menuItems == null || menuItems.Length <= 0)
            {
                // Don't update the selected index if there's no menu items linked to this menu
                Debug.LogWarning("Unable to update selected index. No menu items found");
                return;
            }

            // Check if the menu is active
            if (!active)
            {
                // Don't update the selected index if the menu is not active
                return;
            }

            // Update the selected index
            _selectedIndex += value;
            
            // Check if the wrap-around feature is disabled
            if (!wrapAround)
            {
                // Clamp the selected index
                _selectedIndex = Mathf.Clamp(_selectedIndex, 0, menuItems.Length - 1);
                
                // Update the menu items
                UpdateMenuItems();
                return;
            }

            // Check if we've reached the start of the menu
            if (_selectedIndex <= -1)
            {
                // Set the index to the last menu item
                _selectedIndex = menuItems.Length - 1;
            }
            // Check if we've reached the end of the menu
            else if (_selectedIndex >= menuItems.Length)
            {
                // Set the index to the first menu item
                _selectedIndex = 0;
            }

            // Update the menu items
            UpdateMenuItems();
        }

        /// <summary>
        /// Update all the menu items based on the selected index
        /// </summary>
        private void UpdateMenuItems()
        {
            // Check if we have menu items to update
            if (menuItems == null || menuItems.Length <= 0)
            {
                // Don't update the selected index if there's no menu items linked to this menu
                Debug.LogWarning("Unable to update menu items. No menu items found");
                return;
            }
            
            // Loop through all the menu items
            for (var i = 0; i < menuItems.Length; i++)
            {
                // Check if the menu item is selected
                if (i == _selectedIndex)
                {
                    // Invoke the activated event
                    menuItems[i].activated?.Invoke();
                }
                else
                {
                    // Invoke the deactivated event
                    menuItems[i].deactivated?.Invoke();
                }
            }
        }
        #endregion
    }
}