using System;


namespace Assets.Scripts.Globals
{
    
    public static class EnumHandler
    {
        public static string ConvertToString(this Enum eff)
        {
            return Enum.GetName(eff.GetType(), eff);
        }

        public static EnumType ConverToEnum<EnumType>(this string enumValue)
        {
            return (EnumType)Enum.Parse(typeof(EnumType), enumValue);
        }
    }
    
    public static class Constants
    {
        public const string friendly = "Friendly";
        public const string player = "Player";
        public const string evilBlock = "EvilBlock";
        public const string landingPad = "LandingPad";
    }

    public enum Scenes
    {
        Scene1,
        Scene2,
        Scene3
    }

    public enum State
    {
        Alive,
        Dying,
        Transcending
    }

    
}
