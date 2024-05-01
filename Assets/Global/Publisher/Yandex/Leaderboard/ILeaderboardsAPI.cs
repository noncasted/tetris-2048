namespace Global.Publisher.Yandex.Leaderboard
{
    public interface ILeaderboardsAPI
    {
        void SetLeaderboard_Internal(string target, int score);
        void GetLeaderboard_Internal(string target, int quantityTop, int quantityAround);
    }
}