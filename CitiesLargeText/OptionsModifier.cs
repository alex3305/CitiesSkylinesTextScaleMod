namespace CitiesLargeText {

    using ColossalFramework.UI;
    using ICities;
    using System;
    using UnityEngine;

    class OptionsModifier {

        private static readonly string LabelName = "TextScaleLabel";

        private static readonly string LabelStatusName = "TextScaleStatusLabel";

        private static readonly string PlusButtonName = "TextScalePlusButton";

        private static readonly string MinusButtonName = "TextScaleMinusButton";

        private UIButton plusButton;

        private UIButton minusButton;

        private UILabel statusLabel;

        public OptionsModifier() {
            GetGraphicsPanel();

            if (this.GraphicsPanel == null) {
                DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Warning, "Could not add the setting for font scaling. Exiting.");
            }
            
            AddScalingOption();
        }

        UIPanel GraphicsPanel { get; set; }

        void AddScalingOption() {
            AddLabel();
            this.statusLabel = AddStatus();
            this.plusButton = AddButton("+", PlusButtonName, new Vector3(600, 195));
            this.minusButton = AddButton("-", MinusButtonName, new Vector3(500, 195));

            this.plusButton.eventClick += plusEvent;
            this.minusButton.eventClick += minusEvent;
        }

        UIButton AddButton(string label, string buttonName, Vector3 location) {
            var btnObject = new GameObject(buttonName, typeof(UIButton));
            btnObject.transform.parent = this.GraphicsPanel.transform;
            var button = btnObject.GetComponent<UIButton>();

            button.absolutePosition = location;
            button.width = 30;
            button.height = 30;
            button.text = label;
            button.textScale = 1.2f;
            button.textColor = new Color32(255, 255, 255, 255);

            button.normalBgSprite = "ButtonMenu";
            button.disabledBgSprite = "ButtonMenuDisabled";
            button.hoveredBgSprite = "ButtonMenuHovered";
            button.focusedBgSprite = "ButtonMenuFocused";
            button.pressedBgSprite = "ButtonMenuPressed";

            button.textColor = new Color32(255, 255, 255, 255);
            button.disabledTextColor = new Color32(7, 7, 7, 255);
            button.hoveredTextColor = new Color32(7, 132, 255, 255);
            button.focusedTextColor = new Color32(255, 255, 255, 255);
            button.pressedTextColor = new Color32(30, 30, 44, 255);

            button.playAudioEvents = true;

            return button;
        }

        void AddLabel() {
            var labelObject = new GameObject(LabelName, typeof(UILabel));
            labelObject.transform.parent = this.GraphicsPanel.transform;
            var label = labelObject.GetComponent<UILabel>();

            label.absolutePosition = new Vector3(500, 168); // 1088, 534
            label.width = 100;
            label.height = 30;
            label.text = "Text scaling";
            label.textScale = 1.1f;
            label.textColor = new Color32(255, 255, 255, 255);
        }

        UILabel AddStatus() {
            var labelObject = new GameObject(LabelStatusName, typeof(UILabel));
            labelObject.transform.parent = this.GraphicsPanel.transform;
            var label = labelObject.GetComponent<UILabel>();

            label.absolutePosition = new Vector3(550, 198);
            label.textAlignment = UIHorizontalAlignment.Center;
            label.width = 60;
            label.height = 30;
            label.text = ModInfo.Configuration.Scale.ToString("N1");
            label.textScale = 1.3f;
            label.textColor = new Color32(255, 255, 255, 255);

            return label;
        }

        void GetGraphicsPanel() {
            var uiView = GameObject.FindObjectOfType<UIView>();
            if (uiView == null) {
                return;
            }

            var panels = GameObject.FindObjectsOfType<UIPanel>();
            if (panels == null) {
                return;
            }

            foreach (var panel in panels) {
                if (panel.name.Equals("Graphics")) {
                    this.GraphicsPanel = panel;
                }
            }
        }

        void minusEvent(UIComponent component, UIMouseEventParameter eventParam) {
            ModInfo.Configuration.DecreaseScale();
            statusLabel.text = ModInfo.Configuration.Scale.ToString("N1");
            statusLabel.Invalidate();
        }

        void plusEvent(UIComponent component, UIMouseEventParameter eventParam) {
            ModInfo.Configuration.IncreaseScale();
            statusLabel.text = ModInfo.Configuration.Scale.ToString("N1");
            statusLabel.Invalidate();
        }

    }
}
