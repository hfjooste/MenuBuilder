namespace ThirdPixelGames.MenuBuilder
{
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.Serialization;
    
    /// <summary>
    /// A menu item that is managed by the parent menu
    /// </summary>
    public class MenuItem : MonoBehaviour
    {
        #region Public Events
        /// <summary>
        /// This event is invoked when the object becomes enabled and active
        /// </summary>
        public UnityEvent onEnable = new UnityEvent();
        
        /// <summary>
        /// This event is invoked when the behaviour becomes disabled
        /// </summary>
        public UnityEvent onDisable = new UnityEvent();
        
        /// <summary>
        /// Invoked whenever the item is activated (got focus)
        /// </summary>
        [FormerlySerializedAs("Activated")]
        public UnityEvent activated = new UnityEvent();
    
        /// <summary>
        /// Invoked whenever the item is deactivated (lost focus)
        /// </summary>
        [FormerlySerializedAs("Deactivated")]
        public UnityEvent deactivated = new UnityEvent();
    
        /// <summary>
        /// Invoked whenever the item is selected
        /// </summary>
        [FormerlySerializedAs("Selected")]
        public UnityEvent selected = new UnityEvent();
        #endregion
        
        #region Unity Methods
        /// <summary>
        /// This function is called when the object becomes enabled and active
        /// </summary>
        private void OnEnable() => onEnable?.Invoke();

        /// <summary>
        /// This function is called when the behaviour becomes disabled
        /// </summary>
        private void OnDisable() => onDisable?.Invoke();
        #endregion
    }
}