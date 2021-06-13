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
    }
}