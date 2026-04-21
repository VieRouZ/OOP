using Microsoft.EntityFrameworkCore;
using SportEventApp.Data;
using SportEventApp.Models;

namespace SportEventApp.Forms
{
    public partial class TennisForm : Form
    {
        private readonly ApplicationDbContext _context;
        private readonly Tennis? _tennis;
        private TextBox txtName = null!;
        private TextBox txtPlayers = null!;
        private TextBox txtSurface = null!;
        private TextBox txtSets = null!;
        private TextBox txtPlayer1Score = null!;
        private TextBox txtPlayer2Score = null!;
        private TextBox txtCurrentGame = null!;
        private Button btnSave = null!;
        private Button btnCancel = null!;
        private Button? btnServe;
        private Button? btnChangeSides;
        private Label? lblResult;
        
        public TennisForm(ApplicationDbContext context, Tennis? tennis = null)
        {
            _context = context;
            _tennis = tennis;
            InitializeComponent();
            
            if (tennis != null)
                LoadData();
        }
        
        private void InitializeComponent()
        {
            this.Text = _tennis == null ? "➕ Добавить матч" : "✏️ Редактировать матч";
            this.Size = new Size(550, 700);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Font = new Font("Segoe UI", 10);
            this.BackColor = Color.White;
            
            int y = 20;
            int spacing = 45;
            int labelWidth = 150;
            int controlWidth = 350;
            
            // Название
            Label lblName = new Label 
            { 
                Text = "Название события", 
                Location = new Point(20, y), 
                Size = new Size(labelWidth, 30),
                TextAlign = ContentAlignment.MiddleRight,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            txtName = new TextBox { Location = new Point(180, y), Size = new Size(controlWidth, 30), Font = new Font("Segoe UI", 10) };
            y += spacing;
            
            // Количество игроков
            Label lblPlayers = new Label 
            { 
                Text = "Кол-во игроков:", 
                Location = new Point(20, y), 
                Size = new Size(labelWidth, 30),
                TextAlign = ContentAlignment.MiddleRight,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            txtPlayers = new TextBox { Location = new Point(180, y), Size = new Size(controlWidth, 30), Font = new Font("Segoe UI", 10) };
            y += spacing;
            
            // Покрытие корта
            Label lblSurface = new Label 
            { 
                Text = "Покрытие корта", 
                Location = new Point(20, y), 
                Size = new Size(labelWidth, 30),
                TextAlign = ContentAlignment.MiddleRight,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            txtSurface = new TextBox { Location = new Point(180, y), Size = new Size(controlWidth, 30), Font = new Font("Segoe UI", 10) };
            y += spacing;
            
            // Количество сетов
            Label lblSets = new Label 
            { 
                Text = "Количество сетов", 
                Location = new Point(20, y), 
                Size = new Size(labelWidth, 30),
                TextAlign = ContentAlignment.MiddleRight,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            txtSets = new TextBox { Location = new Point(180, y), Size = new Size(100, 30), Font = new Font("Segoe UI", 10) };
            y += spacing;
            
            // Счет игрока 1
            Label lblPlayer1Score = new Label 
            { 
                Text = "Счет игрока1", 
                Location = new Point(20, y), 
                Size = new Size(labelWidth, 30),
                TextAlign = ContentAlignment.MiddleRight,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            txtPlayer1Score = new TextBox { Location = new Point(180, y), Size = new Size(100, 30), Font = new Font("Segoe UI", 10) };
            y += spacing;
            
            // Счет игрока 2
            Label lblPlayer2Score = new Label 
            { 
                Text = "Счет игрока2", 
                Location = new Point(20, y), 
                Size = new Size(labelWidth, 30),
                TextAlign = ContentAlignment.MiddleRight,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            txtPlayer2Score = new TextBox { Location = new Point(180, y), Size = new Size(100, 30), Font = new Font("Segoe UI", 10) };
            y += spacing;
            
            // Текущий гейм
            Label lblCurrentGame = new Label 
            { 
                Text = "Текущий гейм", 
                Location = new Point(20, y), 
                Size = new Size(labelWidth, 30),
                TextAlign = ContentAlignment.MiddleRight,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            txtCurrentGame = new TextBox { Location = new Point(180, y), Size = new Size(100, 30), Font = new Font("Segoe UI", 10) };
            y += spacing;
            
            if (_tennis != null)
            {
                // Разделитель
                Label lblSeparator = new Label
                {
                    Text = "══════════════════════════════════════════════════",
                    Location = new Point(20, y),
                    Size = new Size(500, 30),
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Color.Gray
                };
                y += 35;
                
                btnServe = new Button 
                { 
                    Text = "🎾 Выполнить подачу", 
                    Location = new Point(20, y), 
                    Size = new Size(150, 40),
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    BackColor = Color.LightGreen,
                    FlatStyle = FlatStyle.Flat
                };
                btnChangeSides = new Button 
                { 
                    Text = "🔄 Смена сторон", 
                    Location = new Point(180, y), 
                    Size = new Size(150, 40),
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    BackColor = Color.LightYellow,
                    FlatStyle = FlatStyle.Flat
                };
                y += 50;
                
                lblResult = new Label 
                { 
                    Text = "", 
                    Location = new Point(20, y), 
                    Size = new Size(500, 70), 
                    BorderStyle = BorderStyle.FixedSingle,
                    Font = new Font("Segoe UI", 10),
                    BackColor = Color.LightYellow,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                y += 85;
                
                btnServe.Click += (s, e) => PerformServe();
                btnChangeSides.Click += (s, e) => PerformChangeSides();
                
                this.Controls.AddRange(new Control[] { lblSeparator, btnServe, btnChangeSides, lblResult });
            }
            
            // Кнопки сохранения
            btnSave = new Button 
            { 
                Text = "💾 Сохранить", 
                Location = new Point(140, y), 
                Size = new Size(120, 40),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.LightBlue,
                FlatStyle = FlatStyle.Flat,
                DialogResult = DialogResult.OK
            };
            btnCancel = new Button 
            { 
                Text = "❌ Отмена", 
                Location = new Point(270, y), 
                Size = new Size(120, 40),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.LightGray,
                FlatStyle = FlatStyle.Flat,
                DialogResult = DialogResult.Cancel
            };
            
            this.Controls.AddRange(new Control[] { lblName, txtName, lblPlayers, txtPlayers, lblSurface, txtSurface,
                lblSets, txtSets, lblPlayer1Score, txtPlayer1Score, lblPlayer2Score, txtPlayer2Score, 
                lblCurrentGame, txtCurrentGame, btnSave, btnCancel });
            
            btnSave.Click += (s, e) => Save();
        }
        
        private void LoadData()
        {
            if (_tennis == null) return;
            
            txtName.Text = _tennis.EventName;
            txtPlayers.Text = _tennis.PlayersCount.ToString();
            txtSurface.Text = _tennis.CourtSurface;
            txtSets.Text = _tennis.SetsCount.ToString();
            txtPlayer1Score.Text = _tennis.Player1Score.ToString();
            txtPlayer2Score.Text = _tennis.Player2Score.ToString();
            txtCurrentGame.Text = _tennis.CurrentGame.ToString();
        }
        
        private void Save()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Введите название события", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            if (!int.TryParse(txtPlayers.Text, out int players) || players <= 0)
            {
                MessageBox.Show("Введите корректное количество игроков", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            if (_tennis == null)
            {
                var tennis = new Tennis
                {
                    EventName = txtName.Text,
                    PlayersCount = players,
                    CourtSurface = txtSurface.Text,
                    SetsCount = int.TryParse(txtSets.Text, out int sets) ? sets : 3,
                    Player1Score = int.TryParse(txtPlayer1Score.Text, out int s1) ? s1 : 0,
                    Player2Score = int.TryParse(txtPlayer2Score.Text, out int s2) ? s2 : 0,
                    CurrentGame = int.TryParse(txtCurrentGame.Text, out int game) ? game : 1,
                    IsActive = false,
                    EventType = "Tennis"
                };
                _context.SportEvents.Add(tennis);
            }
            else
            {
                _tennis.EventName = txtName.Text;
                _tennis.PlayersCount = players;
                _tennis.CourtSurface = txtSurface.Text;
                if (int.TryParse(txtSets.Text, out int sets))
                    _tennis.SetsCount = sets;
                if (int.TryParse(txtPlayer1Score.Text, out int s1))
                    _tennis.Player1Score = s1;
                if (int.TryParse(txtPlayer2Score.Text, out int s2))
                    _tennis.Player2Score = s2;
                if (int.TryParse(txtCurrentGame.Text, out int game))
                    _tennis.CurrentGame = game;
            }
            
            _context.SaveChanges();
            DialogResult = DialogResult.OK;
            Close();
        }
        
        private void PerformServe()
        {
            if (_tennis == null) return;
            if (btnServe == null || lblResult == null) return;
            
            var dialog = new Form
            {
                Text = "Выбор игрока",
                Size = new Size(300, 150),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog
            };
            
            Button btnPlayer1 = new Button { Text = "Игрок 1", Location = new Point(30, 30), Size = new Size(100, 40) };
            Button btnPlayer2 = new Button { Text = "Игрок 2", Location = new Point(150, 30), Size = new Size(100, 40) };
            
            btnPlayer1.Click += (s, e) =>
            {
                string result = _tennis.Serve("Игрок 1");
                _context.SaveChanges();
                if (lblResult != null) lblResult.Text = result;
                LoadData();
                dialog.Close();
            };
            
            btnPlayer2.Click += (s, e) =>
            {
                string result = _tennis.Serve("Игрок 2");
                _context.SaveChanges();
                if (lblResult != null) lblResult.Text = result;
                LoadData();
                dialog.Close();
            };
            
            dialog.Controls.AddRange(new Control[] { btnPlayer1, btnPlayer2 });
            dialog.ShowDialog();
        }
        
        private void PerformChangeSides()
        {
            if (_tennis == null) return;
            if (lblResult == null) return;
            
            string result = _tennis.ChangeSides();
            _context.SaveChanges();
            lblResult.Text = result;
            LoadData();
        }
    }
}