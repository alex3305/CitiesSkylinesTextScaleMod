namespace CitiesLargeText {

    using ColossalFramework.UI;
    using ICities;
    using UnityEngine;

    public class Scaling : LoadingExtensionBase {

        public override void OnLevelLoaded(LoadMode mode) {
            base.OnLevelLoaded(mode);

            var uiView = GameObject.FindObjectOfType<UIView>();
            if (uiView == null) return;

            foreach (var label in GameObject.FindObjectsOfType<UILabel>()) {
                if (label.textScale != ModInfo.Configuration.Scale) {
                    label.textScale = ModInfo.Configuration.Scale;
                    label.clipChildren = false;
                    label.Invalidate();
                }
            }
        }
    }
}
