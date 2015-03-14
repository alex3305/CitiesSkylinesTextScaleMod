namespace CitiesLargeText {

    using ICities;

    public class ModInfo : IUserMod {

        public static readonly string configPath = "TextScale.xml";

        public ModInfo() {
            Configuration = ScalingConfiguration.Deserialize(configPath);
            if (Configuration == null) {
                Configuration = new ScalingConfiguration();
            }

            new OptionsModifier();
        }

        public string Name {
            get { return "Cities Skylines Dynamic Text Mod"; }
        }

        public string Description {
            get { return "Enables text scaling ingame."; }
        }

        public static ScalingConfiguration Configuration { get; private set; }

    }
}
