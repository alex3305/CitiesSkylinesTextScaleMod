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

        public UILabel[] Labels { get; set; }

        public void UpdateUI(UILabel[] labels) {
            Labels = labels;

            foreach (var label in Labels) {
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

            var uiView = GameObject.FindObjectOfType<UIView>();
            if (uiView == null) return;

            Scaling.Instance.UpdateUI(GameObject.FindObjectsOfType<UILabel>());
        }
    }

    public class ContinuesScaling : ThreadingExtensionBase {

        public override void OnUpdate(float realTimeDelta, float simulationTimeDelta) {
            var labels = GameObject.FindObjectsOfType<UILabel>();
            if (Scaling.Instance.Labels != labels) {
                Scaling.Instance.UpdateUI(labels);
            }

            base.OnUpdate(realTimeDelta, simulationTimeDelta);
        }

    }
}
