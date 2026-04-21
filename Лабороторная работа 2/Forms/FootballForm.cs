using Microsoft.EntityFrameworkCore;
using SportEventApp.Data;
using SportEventApp.Models;

namespace SportEventApp.Forms
{
    public partial class FootballForm : Form
{
    private readonly ApplicationDbContext _context;
    private readonly Football? _football;
    private TextBox txtName = null!;
    private TextBox txtPlayers = null!;
    private TextBox txtStadium = null!;
    private TextBox txtTeam1Score = null!;
    private TextBox txtTeam2Score = null!;
    private Button btnSave = null!;
    private Button btnCancel = null!;
    private Button? btnShootGoal;
    private Button? btnSubstitute;
    private Label? lblResult;
        
        public FootballForm(ApplicationDbContext context, Football? football = null)
        {
            _context = context;
            _football = football;
            InitializeComponent();
            
            if (football != null)
                LoadData();
        }
        
        // В методе InitializeComponent, настройте метки и текстбоксы
private void InitializeComponent()
{
    this.Text = _football == null ? "➕ Добавить матч" : "✏️ Редактировать матч";
    this.Size = new Size(550, 600);
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
        Text = "Название события:", 
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
        Text = "Кол-во игроков", 
        Location = new Point(20, y), 
        Size = new Size(labelWidth, 30),
        TextAlign = ContentAlignment.MiddleRight,
        Font = new Font("Segoe UI", 10, FontStyle.Bold)
    };
    txtPlayers = new TextBox { Location = new Point(180, y), Size = new Size(controlWidth, 30), Font = new Font("Segoe UI", 10) };
    y += spacing;
    
    // Стадион
    Label lblStadium = new Label 
    { 
        Text = "Стадион", 
        Location = new Point(20, y), 
        Size = new Size(labelWidth, 30),
        TextAlign = ContentAlignment.MiddleRight,
        Font = new Font("Segoe UI", 10, FontStyle.Bold)
    };
    txtStadium = new TextBox { Location = new Point(180, y), Size = new Size(controlWidth, 30), Font = new Font("Segoe UI", 10) };
    y += spacing;
    
    // Счет команды 1
    Label lblTeam1Score = new Label 
    { 
        Text = "Счет команды1", 
        Location = new Point(20, y), 
        Size = new Size(labelWidth, 30),
        TextAlign = ContentAlignment.MiddleRight,
        Font = new Font("Segoe UI", 10, FontStyle.Bold)
    };
    txtTeam1Score = new TextBox { Location = new Point(180, y), Size = new Size(100, 30), Font = new Font("Segoe UI", 10) };
    y += spacing;
    
    // Счет команды 2
    Label lblTeam2Score = new Label 
    { 
        Text = "Счет команды2", 
        Location = new Point(20, y), 
        Size = new Size(labelWidth, 30),
        TextAlign = ContentAlignment.MiddleRight,
        Font = new Font("Segoe UI", 10, FontStyle.Bold)
    };
    txtTeam2Score = new TextBox { Location = new Point(180, y), Size = new Size(100, 30), Font = new Font("Segoe UI", 10) };
    y += spacing;
    
    if (_football != null)
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
        
        btnShootGoal = new Button 
        { 
            Text = "⚽ Удар по воротам", 
            Location = new Point(20, y), 
            Size = new Size(150, 40),
            Font = new Font("Segoe UI", 10, FontStyle.Bold),
            BackColor = Color.LightGreen,
            FlatStyle = FlatStyle.Flat
        };
        btnSubstitute = new Button 
        { 
            Text = "🔄 Замена", 
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
        
        btnShootGoal.Click += (s, e) => PerformShootGoal();
        btnSubstitute.Click += (s, e) => PerformSubstitute();
        
        this.Controls.AddRange(new Control[] { lblSeparator, btnShootGoal, btnSubstitute, lblResult });
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
    
    this.Controls.AddRange(new Control[] { lblName, txtName, lblPlayers, txtPlayers, 
        lblStadium, txtStadium, lblTeam1Score, txtTeam1Score, lblTeam2Score, txtTeam2Score, 
        btnSave, btnCancel });
    
    btnSave.Click += (s, e) => Save();
}
        
        private void LoadData()
        {
        if (_football == null) return;
        txtName.Text = _football.EventName;
        txtPlayers.Text = _football.PlayersCount.ToString();
        txtStadium.Text = _football.StadiumName;
        txtTeam1Score.Text = _football.Team1Score.ToString();
        txtTeam2Score.Text = _football.Team2Score.ToString();
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
            
            if (_football == null)
            {
                var football = new Football
                {
                    EventName = txtName.Text,
                    PlayersCount = players,
                    StadiumName = txtStadium.Text,
                    Team1Score = int.TryParse(txtTeam1Score.Text, out int s1) ? s1 : 0,
                    Team2Score = int.TryParse(txtTeam2Score.Text, out int s2) ? s2 : 0,
                    IsActive = false,
                    EventType = "Football"
                };
                _context.SportEvents.Add(football);
            }
            else
            {
                _football.EventName = txtName.Text;
                _football.PlayersCount = players;
                _football.StadiumName = txtStadium.Text;
                if (int.TryParse(txtTeam1Score.Text, out int s1))
                    _football.Team1Score = s1;
                if (int.TryParse(txtTeam2Score.Text, out int s2))
                    _football.Team2Score = s2;
            }
            
            _context.SaveChanges();
            DialogResult = DialogResult.OK;
            Close();
        }
        
        private void PerformShootGoal()
{
    if (_football == null) return;
    if (btnShootGoal == null || lblResult == null) return;
    
    var dialog = new Form
    {
        Text = "Выбор команды",
        Size = new Size(300, 150),
        StartPosition = FormStartPosition.CenterParent,
        FormBorderStyle = FormBorderStyle.FixedDialog
    };
    
    Button btnTeam1 = new Button { Text = "Команда 1", Location = new Point(30, 30), Size = new Size(100, 40) };
    Button btnTeam2 = new Button { Text = "Команда 2", Location = new Point(150, 30), Size = new Size(100, 40) };
    
    btnTeam1.Click += (s, e) =>
    {
        string result = _football.ShootOnGoal("Команда 1");
        _context.SaveChanges();
        if (lblResult != null) lblResult.Text = result;
        LoadData();
        dialog.Close();
    };
    
    btnTeam2.Click += (s, e) =>
    {
        string result = _football.ShootOnGoal("Команда 2");
        _context.SaveChanges();
        if (lblResult != null) lblResult.Text = result;
        LoadData();
        dialog.Close();
    };
    
    dialog.Controls.AddRange(new Control[] { btnTeam1, btnTeam2 });
    dialog.ShowDialog();
}

private void PerformSubstitute()
{
    if (_football == null) return;
    if (btnSubstitute == null || lblResult == null) return;
    
    var dialog = new Form
    {
        Text = "Замена игрока",
        Size = new Size(400, 180),
        StartPosition = FormStartPosition.CenterParent,
        FormBorderStyle = FormBorderStyle.FixedDialog
    };
    
    Label lblOut = new Label { Text = "Уходящий игрок:", Location = new Point(20, 20), AutoSize = true };
    TextBox txtOut = new TextBox { Location = new Point(140, 17), Size = new Size(220, 25) };
    
    Label lblIn = new Label { Text = "Входящий игрок:", Location = new Point(20, 60), AutoSize = true };
    TextBox txtIn = new TextBox { Location = new Point(140, 57), Size = new Size(220, 25) };
    
    Button btnOk = new Button { Text = "Выполнить замену", Location = new Point(130, 100), Size = new Size(130, 35) };
    
    btnOk.Click += (s, e) =>
    {
        string result = _football.SubstitutePlayer(txtOut.Text, txtIn.Text);
        _context.SaveChanges();
        if (lblResult != null) lblResult.Text = result;
        dialog.Close();
    };
    
    dialog.Controls.AddRange(new Control[] { lblOut, txtOut, lblIn, txtIn, btnOk });
    dialog.ShowDialog();
}
    }
}