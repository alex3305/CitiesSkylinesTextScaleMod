namespace CitiesLargeText {

    using ColossalFramework.UI;
    using ICities;
    using UnityEngine;

    public class Scaling {

        private static Scaling instance;

        public static Scaling Instance {
            get {
                return (instance = instance ?? new Scaling());
            }
        }

        public void UpdateUI(UILabel[] labels) {
            DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message,
                    "Updating UI.");

            foreach (var label in labels) {
                if (label.textScale != ModInfo.Configuration.Scale) {
                    label.textScale = ModInfo.Configuration.Scale;
                    label.clipChildren = false;
                    label.size = new Vector2(label.size.x, label.size.y * ModInfo.Configuration.Scale);
                    label.Invalidate();
                }
            }
        }

    }

    public class InitialScaling : LoadingExtensionBase {

        public override void OnLevelLoaded(LoadMode mode) {
            base.OnLevelLoaded(mode);

            DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message,
                "Loading text scale mod...");

            var uiView = GameObject.FindObjectOfType<UIView>();
            if (uiView == null) {
                DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Error, "Cannot load UIView. Scaling will not work!");
                return;
            }

            DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message,
                "Starting UI scale.");

            Scaling.Instance.UpdateUI(GameObject.FindObjectsOfType<UILabel>());
        }
    }

    public class ContinuesScaling : ThreadingExtensionBase {

        private float oldTimeDelta;

        public override void OnUpdate(float realTimeDelta, float simulationTimeDelta) {
            if (oldTimeDelta > 5) {
                oldTimeDelta = 0;
                Scaling.Instance.UpdateUI(GameObject.FindObjectsOfType<UILabel>());
            } else {
                oldTimeDelta += realTimeDelta;
            }

            base.OnUpdate(realTimeDelta, simulationTimeDelta);
        }

    }
}
