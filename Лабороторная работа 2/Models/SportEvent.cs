using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportEventApp.Models
{
    [Table("SportEvents")]
    public abstract class SportEvent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string EventName { get; set; } = string.Empty;
        
        public bool IsActive { get; set; }
        
        [Required]
        public int PlayersCount { get; set; }
        
        public string EventType { get; set; } = string.Empty;
        
        protected SportEvent() { }
        
        protected SportEvent(string name, int players)
        {
            EventName = name;
            PlayersCount = players;
            IsActive = false;
        }
        
        public abstract void StartGame();
        public abstract void StopGame();
        public abstract string ShowInfo();
        
        public string GetName() => EventName;
        public void SetName(string name) => EventName = name;
        
        public int GetPlayersCount() => PlayersCount;
        public void SetPlayersCount(int count) => PlayersCount = count;
        
        public bool GetStatus() => IsActive;
        public void SetStatus(bool status) => IsActive = status;
    }
}