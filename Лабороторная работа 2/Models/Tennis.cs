using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportEventApp.Models
{
    [Table("TennisEvents")]
    public class Tennis : SportEvent
    {
        [MaxLength(50)]
        public string CourtSurface { get; set; } = string.Empty;
        
        public int SetsCount { get; set; }
        public int Player1Score { get; set; }
        public int Player2Score { get; set; }
        public int CurrentGame { get; set; }
        
        private static Random _random = new Random();
        
        public Tennis() : base() { }
        
        public Tennis(string name, int players, string surface, int sets, int score1, int score2, int game)
            : base(name, players)
        {
            CourtSurface = surface;
            SetsCount = sets;
            Player1Score = score1;
            Player2Score = score2;
            CurrentGame = game;
            EventType = "Tennis";
        }
        
        public Tennis(string name, int players, string surface, int sets)
            : this(name, players, surface, sets, 0, 0, 1) { }
        
        public Tennis(string name, int players)
            : this(name, players, "Грунт", 3, 0, 0, 1) { }
        
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
            return $"Событие: {EventName} | Вид: Теннис | Статус: {(IsActive ? "ИДЕТ МАТЧ" : "НЕ АКТИВЕН")} | Покрытие: {CourtSurface} | Сетов: {SetsCount} | Счет: {Player1Score}-{Player2Score} (гейм {CurrentGame})";
        }
        
        public string Serve(string player)
        {
            if (!IsActive)
                return $"{EventName}: Сначала начните матч!";
            
            bool success = _random.Next(100) < 70;
            
            if (success)
            {
                bool winPoint = _random.Next(100) < 40;
                if (winPoint)
                {
                    if (player == "Игрок 1")
                    {
                        Player1Score++;
                        return $"{EventName}: {player} выполняет подачу. Подача успешна! {player} выигрывает очко! Счет: {Player1Score}-{Player2Score}";
                    }
                    else if (player == "Игрок 2")
                    {
                        Player2Score++;
                        return $"{EventName}: {player} выполняет подачу. Подача успешна! {player} выигрывает очко! Счет: {Player1Score}-{Player2Score}";
                    }
                }
                return $"{EventName}: {player} выполняет подачу. Подача успешна!";
            }
            return $"{EventName}: {player} выполняет подачу. Ошибка подачи!";
        }
        
        public string ChangeSides()
        {
            CurrentGame++;
            return $"{EventName}: Смена сторон. Игроки меняются сторонами корта. Текущий гейм: {CurrentGame}";
        }
        
        public string GetCourtSurface() => CourtSurface;
        public void SetCourtSurface(string surface) => CourtSurface = surface;
        
        public int GetSetsCount() => SetsCount;
        public void SetSetsCount(int sets) => SetsCount = sets;
        
        public int GetPlayer1Score() => Player1Score;
        public void SetPlayer1Score(int score) => Player1Score = score;
        
        public int GetPlayer2Score() => Player2Score;
        public void SetPlayer2Score(int score) => Player2Score = score;
        
        public int GetCurrentGame() => CurrentGame;
        public void SetCurrentGame(int game) => CurrentGame = game;
    }
}