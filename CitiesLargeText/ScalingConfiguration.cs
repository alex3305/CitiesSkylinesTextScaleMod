namespace CitiesLargeText {

    using System.IO;
    using System.Xml.Serialization;

    public class ScalingConfiguration {

        private static float initialScale = 0.75f;

        public ScalingConfiguration() {
            Scale = initialScale;
        }

        public float Scale { get; private set; }

        public void IncreaseScale() {
            Scale += .01f;
            Serialize(ModInfo.configPath, this);
        }

        public void DecreaseScale() {
            Scale -= .01f;
            Serialize(ModInfo.configPath, this);
        }

        public static void Serialize(string filename, ScalingConfiguration config) {
            var serializer = new XmlSerializer(typeof(ScalingConfiguration));

            using (var writer = new StreamWriter(filename)) {
                config.OnPreSerialize();
                serializer.Serialize(writer, config);
            }
        }

        public static ScalingConfiguration Deserialize(string filename) {
            var serializer = new XmlSerializer(typeof(ScalingConfiguration));

            try {
                using (var reader = new StreamReader(filename)) {
                    var config = (ScalingConfiguration)serializer.Deserialize(reader);
                    config.OnPostDeserialize();
                    return config;
                }
            } catch { }

            return null;
        }

        public void OnPreSerialize() {
            // Stub method.
        }

        public void OnPostDeserialize() {
            // Stub method.
        }

    }

}
