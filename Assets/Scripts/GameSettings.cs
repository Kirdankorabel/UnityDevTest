public static class GameSettings
{
    public const string BestResultNamePrefName = "BestResult";
    public const string IsNewPlayerPrefName = "IsNewPlayer";

    public static IPositionGetter PositionGetter { get; set; }
    public static float Sensivity { get; set; }
    public static float MaxTime { get; set; } = 30f;
    public static bool IsPaused { get; set; }
    public static int BestResult { get; set; }
    public static float ScreenWidth { get; set; }
}
