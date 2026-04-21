using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportEventApp.Models
{
    [Table("FootballEvents")]
    public class Football : SportEvent
    {
        [MaxLength(100)]
        public string StadiumName { get; set; } = string.Empty;
        
        public int Team1Score { get; set; }
        public int Team2Score { get; set; }
        
        private static Random _random = new Random();
        
        public Football() : base() { }
        
        public Football(string name, int players, string stadium, int score1, int score2)
            : base(name, players)
        {
            StadiumName = stadium;
            Team1Score = score1;
            Team2Score = score2;
            EventType = "Football";
        }
        
        public Football(string name, int players, string stadium)
            : this(name, players, stadium, 0, 0) { }
        
        public Football(string name, int players)
            : this(name, players, "Стандартный стадион", 0, 0) { }
        
        public override void StartGame()
        {
            IsActive = true;
        }
        
        public override void StopGame()
        {
            IsActive = false;
        }
        
        public override string ShowInfo()
        {
            return $"Событие: {EventName} | Вид: Футбол | Статус: {(IsActive ? "ИДЕТ МАТЧ" : "НЕ АКТИВЕН")} | Игроков: {PlayersCount} | Стадион: {StadiumName} | Счет: {Team1Score}:{Team2Score}";
        }
        
        public string ShootOnGoal(string team)
        {
            if (!IsActive)
                return $"{EventName}: Сначала начните матч!";
            
            bool isGoal = _random.Next(10) < 9;
            
            if (isGoal)
            {
                if (team == "Команда 1" || team == "хозяева")
                {
                    Team1Score++;
                    return $"{EventName}: ГООООЛ! Счет: {Team1Score}:{Team2Score}";
                }
                else if (team == "Команда 2" || team == "гости")
                {
                    Team2Score++;
                    return $"{EventName}: ГООООЛ! Счет: {Team1Score}:{Team2Score}";
                }
            }
            return $"{EventName}: Мимо ворот!";
        }
        
        public string SubstitutePlayer(string playerOut, string playerIn)
        {
            if (!IsActive)
                return $"{EventName}: Замена возможна только во время матча!";
            
            return $"{EventName}: Замена - {playerOut} уходит, {playerIn} выходит на поле";
        }
        
        public string GetStadiumName() => StadiumName;
        public void SetStadiumName(string name) => StadiumName = name;
        
        public int GetTeam1Score() => Team1Score;
        public void SetTeam1Score(int score) => Team1Score = score;
        
        public int GetTeam2Score() => Team2Score;
        public void SetTeam2Score(int score) => Team2Score = score;
    }
}