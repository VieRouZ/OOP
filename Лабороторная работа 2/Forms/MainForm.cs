using Microsoft.EntityFrameworkCore;
using SportEventApp.Data;
using SportEventApp.Models;

namespace SportEventApp.Forms
{
    public partial class MainForm : Form
    {
        private readonly ApplicationDbContext _context;
        private DataGridView dataGridView = null!;
        private ComboBox cmbEventType = null!;
        private Button btnAdd = null!;
        private Button btnEdit = null!;
        private Button btnDelete = null!;
        private Button btnStartStop = null!;
        private Button btnShowInfo = null!;
        private Button btnRefresh = null!;
        
        // Уникальные кнопки для футбола
        private Button btnShootGoal = null!;
        private Button btnSubstitute = null!;
        
        // Уникальные кнопки для тенниса
        private Button btnServe = null!;
        private Button btnChangeSides = null!;
        
        // Панель для уникальных методов
        private Panel panelUniqueMethods = null!;
        private Label lblResult = null!;
        
        private SportEvent? _selectedEvent;
        private int _selectedEventId = -1;
        
        public MainForm(ApplicationDbContext context)
        {
            _context = context;
            InitializeComponent();
            
            // Подписываемся на событие загрузки формы
            this.Load += (s, e) => LoadData();
            
            // Подписываемся на событие выбора строки в DataGridView
            dataGridView.SelectionChanged += DataGridView_SelectionChanged;
        }
        
        private void InitializeComponent()
        {
            this.Text = "Спортивные события";
            this.Size = new Size(1300, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Segoe UI", 10);
            this.MinimumSize = new Size(1200, 700);
            
            // DataGridView
            dataGridView = new DataGridView
            {
                Location = new Point(12, 60),
                Size = new Size(1260, 450),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                BackgroundColor = Color.White,
                GridColor = Color.LightGray,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            
            // Фильтр
            Label lblFilter = new Label
            {
                Text = "Фильтр по виду спорта:",
                Location = new Point(12, 25),
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            
            cmbEventType = new ComboBox
            {
                Location = new Point(170, 22),
                Size = new Size(150, 28),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10)
            };
            cmbEventType.Items.AddRange(new object[] { "Все", "Футбол", "Теннис" });
            cmbEventType.SelectedIndex = 0;
            cmbEventType.SelectedIndexChanged += (s, e) => LoadData();
            
            // Основные кнопки
            btnAdd = new Button 
            { 
                Text = "➕ Добавить", 
                Location = new Point(12, 520), 
                Size = new Size(110, 40),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.LightGreen,
                FlatStyle = FlatStyle.Flat
            };
            
            btnEdit = new Button 
            { 
                Text = "✏️ Редактировать", 
                Location = new Point(132, 520), 
                Size = new Size(110, 40),
                Font = new Font("Segoe UI", 10),
                BackColor = Color.LightYellow,
                FlatStyle = FlatStyle.Flat
            };
            
            btnDelete = new Button 
            { 
                Text = "🗑️ Удалить", 
                Location = new Point(252, 520), 
                Size = new Size(110, 40),
                Font = new Font("Segoe UI", 10),
                BackColor = Color.LightCoral,
                FlatStyle = FlatStyle.Flat
            };
            
            btnStartStop = new Button 
            { 
                Text = "▶️ Начать матч", 
                Location = new Point(372, 520), 
                Size = new Size(110, 40),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.LightBlue,
                FlatStyle = FlatStyle.Flat
            };
            
            btnShowInfo = new Button 
            { 
                Text = "ℹ️ Информация", 
                Location = new Point(492, 520), 
                Size = new Size(110, 40),
                Font = new Font("Segoe UI", 10),
                BackColor = Color.LightGray,
                FlatStyle = FlatStyle.Flat
            };
            
            btnRefresh = new Button 
            { 
                Text = "🔄 Обновить", 
                Location = new Point(612, 520), 
                Size = new Size(110, 40),
                Font = new Font("Segoe UI", 10),
                BackColor = Color.LightSteelBlue,
                FlatStyle = FlatStyle.Flat
            };
            
            // Панель для уникальных методов
            panelUniqueMethods = new Panel
            {
                Location = new Point(12, 570),
                Size = new Size(1260, 180),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.AliceBlue
            };
            
            // Заголовок панели
            Label lblUniqueTitle = new Label
            {
                Text = "🎯 УНИКАЛЬНЫЕ МЕТОДЫ",
                Location = new Point(10, 5),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.DarkBlue
            };
            panelUniqueMethods.Controls.Add(lblUniqueTitle);
            
            // Кнопки для футбола
            Label lblFootballTitle = new Label
            {
                Text = "⚽ ФУТБОЛ",
                Location = new Point(20, 35),
                Size = new Size(100, 25),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.DarkGreen
            };
            
            btnShootGoal = new Button 
            { 
                Text = "⚽ Удар по воротам", 
                Location = new Point(20, 65), 
                Size = new Size(140, 35),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                BackColor = Color.LightGreen,
                FlatStyle = FlatStyle.Flat,
                Enabled = false
            };
            
            btnSubstitute = new Button 
            { 
                Text = "🔄 Замена игрока", 
                Location = new Point(170, 65), 
                Size = new Size(140, 35),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                BackColor = Color.LightYellow,
                FlatStyle = FlatStyle.Flat,
                Enabled = false
            };
            
            // Кнопки для тенниса
            Label lblTennisTitle = new Label
            {
                Text = "🎾 ТЕННИС",
                Location = new Point(350, 35),
                Size = new Size(100, 25),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.DarkRed
            };
            
            btnServe = new Button 
            { 
                Text = "🎾 Выполнить подачу", 
                Location = new Point(350, 65), 
                Size = new Size(140, 35),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                BackColor = Color.LightBlue,
                FlatStyle = FlatStyle.Flat,
                Enabled = false
            };
            
            btnChangeSides = new Button 
            { 
                Text = "🔄 Смена сторон", 
                Location = new Point(500, 65), 
                Size = new Size(140, 35),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                BackColor = Color.LightCyan,
                FlatStyle = FlatStyle.Flat,
                Enabled = false
            };
            
            // Результат операции
            Label lblResultTitle = new Label
            {
                Text = "Результат:",
                Location = new Point(20, 110),
                Size = new Size(80, 25),
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            
            lblResult = new Label 
            { 
                Text = "Выберите событие для выполнения уникальных методов", 
                Location = new Point(100, 110), 
                Size = new Size(1140, 55), 
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 9),
                BackColor = Color.White,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(5)
            };
            
            panelUniqueMethods.Controls.AddRange(new Control[] { 
                lblFootballTitle, btnShootGoal, btnSubstitute,
                lblTennisTitle, btnServe, btnChangeSides,
                lblResultTitle, lblResult
            });
            
            // Добавляем все контролы
            this.Controls.AddRange(new Control[] { dataGridView, lblFilter, cmbEventType, 
                btnAdd, btnEdit, btnDelete, btnStartStop, btnShowInfo, btnRefresh, panelUniqueMethods });
            
            // Подписки на события
            btnAdd.Click += (s, e) => ShowAddDialog();
            btnEdit.Click += (s, e) => EditSelected();
            btnDelete.Click += (s, e) => DeleteSelected();
            btnStartStop.Click += (s, e) => StartStopSelected();
            btnShowInfo.Click += (s, e) => ShowInfoSelected();
            btnRefresh.Click += BtnRefresh_Click;
            
            // Подписки на уникальные методы
            btnShootGoal.Click += (s, e) => PerformShootGoal();
            btnSubstitute.Click += (s, e) => PerformSubstitute();
            btnServe.Click += (s, e) => PerformServe();
            btnChangeSides.Click += (s, e) => PerformChangeSides();
        }
        
        private void DataGridView_SelectionChanged(object? sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                var idCell = dataGridView.SelectedRows[0].Cells["Id"];
                if (idCell != null && idCell.Value != null)
                {
                    _selectedEventId = (int)idCell.Value;
                    _selectedEvent = _context.SportEvents.Find(_selectedEventId);
                    UpdateUniqueButtonsState();
                    UpdateStartStopButtonText();
                }
            }
            else
            {
                _selectedEvent = null;
                _selectedEventId = -1;
                UpdateUniqueButtonsState();
                UpdateStartStopButtonText();
            }
        }
        
        private void UpdateUniqueButtonsState()
        {
            // Блокируем все кнопки по умолчанию
            btnShootGoal.Enabled = false;
            btnSubstitute.Enabled = false;
            btnServe.Enabled = false;
            btnChangeSides.Enabled = false;
            
            if (_selectedEvent != null && _selectedEvent.IsActive)
            {
                if (_selectedEvent is Football)
                {
                    btnShootGoal.Enabled = true;
                    btnSubstitute.Enabled = true;
                    lblResult.Text = "⚽ Выбран футбольный матч. Доступны: удар по воротам, замена игрока.";
                }
                else if (_selectedEvent is Tennis)
                {
                    btnServe.Enabled = true;
                    btnChangeSides.Enabled = true;
                    lblResult.Text = "🎾 Выбран теннисный матч. Доступны: подача, смена сторон.";
                }
            }
            else if (_selectedEvent != null && !_selectedEvent.IsActive)
            {
                lblResult.Text = "⏸️ Матч не активен. Сначала начните матч!";
            }
            else
            {
                lblResult.Text = "Выберите событие для выполнения уникальных методов";
            }
        }
        
        private void UpdateStartStopButtonText()
        {
            if (_selectedEvent != null && _selectedEvent.IsActive)
            {
                btnStartStop.Text = "⏹️ Завершить матч";
                btnStartStop.BackColor = Color.LightSalmon;
            }
            else
            {
                btnStartStop.Text = "▶️ Начать матч";
                btnStartStop.BackColor = Color.LightBlue;
            }
        }
        
        private void StartStopSelected()
        {
            if (_selectedEvent == null)
            {
                MessageBox.Show("Выберите событие", "Внимание", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            if (!_selectedEvent.IsActive)
            {
                // Начинаем матч
                _selectedEvent.StartGame();
                _context.SaveChanges();
                LoadDataAndRestoreSelection();
                UpdateUniqueButtonsState();
                UpdateStartStopButtonText();
                MessageBox.Show(_selectedEvent.ShowInfo(), "Матч начат", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblResult.Text = "✅ Матч начат! Теперь доступны уникальные методы.";
            }
            else
            {
                // Завершаем матч
                _selectedEvent.StopGame();
                _context.SaveChanges();
                LoadDataAndRestoreSelection();
                UpdateUniqueButtonsState();
                UpdateStartStopButtonText();
                MessageBox.Show(_selectedEvent.ShowInfo(), "Матч завершен", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblResult.Text = "⏹️ Матч завершен. Чтобы выполнять уникальные методы, начните матч заново.";
            }
        }
        
        private void LoadDataAndRestoreSelection()
        {
            // Сохраняем ID выбранного события
            int savedId = _selectedEventId;
            
            // Перезагружаем данные
            LoadData();
            
            // Восстанавливаем выделение
            if (savedId != -1)
            {
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    var idCell = row.Cells["Id"];
                    if (idCell != null && idCell.Value != null && (int)idCell.Value == savedId)
                    {
                        row.Selected = true;
                        dataGridView.FirstDisplayedScrollingRowIndex = row.Index;
                        break;
                    }
                }
            }
            
            // Обновляем выбранное событие
            if (savedId != -1)
            {
                _selectedEvent = _context.SportEvents.Find(savedId);
                _selectedEventId = savedId;
            }
        }
        
        private void PerformShootGoal()
        {
            if (_selectedEvent is Football football)
            {
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
                    string result = football.ShootOnGoal("Команда 1");
                    _context.SaveChanges();
                    lblResult.Text = result;
                    MessageBox.Show(result, "Удар по воротам", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataAndRestoreSelection();
                    dialog.Close();
                };
                
                btnTeam2.Click += (s, e) =>
                {
                    string result = football.ShootOnGoal("Команда 2");
                    _context.SaveChanges();
                    lblResult.Text = result;
                    MessageBox.Show(result, "Удар по воротам", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataAndRestoreSelection();
                    dialog.Close();
                };
                
                dialog.Controls.AddRange(new Control[] { btnTeam1, btnTeam2 });
                dialog.ShowDialog();
            }
        }
        
        private void PerformSubstitute()
        {
            if (_selectedEvent is Football football)
            {
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
                    if (string.IsNullOrWhiteSpace(txtOut.Text) || string.IsNullOrWhiteSpace(txtIn.Text))
                    {
                        MessageBox.Show("Введите имена игроков", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    
                    string result = football.SubstitutePlayer(txtOut.Text, txtIn.Text);
                    _context.SaveChanges();
                    lblResult.Text = result;
                    MessageBox.Show(result, "Замена игрока", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataAndRestoreSelection();
                    dialog.Close();
                };
                
                dialog.Controls.AddRange(new Control[] { lblOut, txtOut, lblIn, txtIn, btnOk });
                dialog.ShowDialog();
            }
        }
        
        private void PerformServe()
        {
            if (_selectedEvent is Tennis tennis)
            {
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
                    string result = tennis.Serve("Игрок 1");
                    _context.SaveChanges();
                    lblResult.Text = result;
                    MessageBox.Show(result, "Подача", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataAndRestoreSelection();
                    dialog.Close();
                };
                
                btnPlayer2.Click += (s, e) =>
                {
                    string result = tennis.Serve("Игрок 2");
                    _context.SaveChanges();
                    lblResult.Text = result;
                    MessageBox.Show(result, "Подача", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataAndRestoreSelection();
                    dialog.Close();
                };
                
                dialog.Controls.AddRange(new Control[] { btnPlayer1, btnPlayer2 });
                dialog.ShowDialog();
            }
        }
        
        private void PerformChangeSides()
        {
            if (_selectedEvent is Tennis tennis)
            {
                string result = tennis.ChangeSides();
                _context.SaveChanges();
                lblResult.Text = result;
                MessageBox.Show(result, "Смена сторон", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataAndRestoreSelection();
            }
        }
        
        private void BtnRefresh_Click(object? sender, EventArgs e)
        {
            try
            {
                _context.ChangeTracker.Clear();
                LoadDataAndRestoreSelection();
                lblResult.Text = "Данные обновлены. Выберите событие.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении: {ex.Message}", "Ошибка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void ShowAddDialog()
        {
            var dialog = new Form
            {
                Text = "Выбор типа события",
                Size = new Size(350, 180),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = Color.White
            };
            
            Label lblQuestion = new Label
            {
                Text = "Что вы хотите добавить?",
                Location = new Point(20, 25),
                Size = new Size(300, 30),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };
            
            Button btnFootball = new Button
            {
                Text = "⚽ Футбольный матч",
                Location = new Point(30, 70),
                Size = new Size(130, 45),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                BackColor = Color.LightGreen,
                FlatStyle = FlatStyle.Flat
            };
            
            Button btnTennis = new Button
            {
                Text = "🎾 Теннисный матч",
                Location = new Point(180, 70),
                Size = new Size(130, 45),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                BackColor = Color.LightBlue,
                FlatStyle = FlatStyle.Flat
            };
            
            btnFootball.Click += (s, e) =>
            {
                dialog.Close();
                OpenFootballForm(null);
            };
            
            btnTennis.Click += (s, e) =>
            {
                dialog.Close();
                OpenTennisForm(null);
            };
            
            dialog.Controls.AddRange(new Control[] { lblQuestion, btnFootball, btnTennis });
            dialog.ShowDialog();
        }
        
        private void LoadData()
        {
            try
            {
                dataGridView.DataSource = null;
                
                var query = _context.SportEvents.AsQueryable();
                
                if (cmbEventType.SelectedItem?.ToString() == "Футбол")
                    query = query.OfType<Football>();
                else if (cmbEventType.SelectedItem?.ToString() == "Теннис")
                    query = query.OfType<Tennis>();
                
                var events = query.ToList();
                
                var dataSource = events.Select(e => new
                {
                    Id = e.Id,
                    ВидСпорта = e.EventType == "Football" ? "⚽ Футбол" : "🎾 Теннис",
                    Название = e.EventName,
                    Статус = e.IsActive ? "▶️ ИДЕТ МАТЧ" : "⏹️ Не активен",
                    Игроков = e.PlayersCount,
                    Детали = GetDetailsString(e)
                }).ToList();
                
                dataGridView.DataSource = dataSource;
                ConfigureDataGridViewColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void ConfigureDataGridViewColumns()
        {
            if (dataGridView.Columns.Count == 0) return;
            
            try
            {
                if (dataGridView.Columns["Id"] != null)
                    dataGridView.Columns["Id"].Visible = false;
                
                if (dataGridView.Columns["ВидСпорта"] != null)
                    dataGridView.Columns["ВидСпорта"].Width = 100;
                
                if (dataGridView.Columns["Название"] != null)
                    dataGridView.Columns["Название"].Width = 200;
                
                if (dataGridView.Columns["Статус"] != null)
                    dataGridView.Columns["Статус"].Width = 120;
                
                if (dataGridView.Columns["Игроков"] != null)
                    dataGridView.Columns["Игроков"].Width = 80;
                
                if (dataGridView.Columns["Детали"] != null)
                    dataGridView.Columns["Детали"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    var statusCell = row.Cells["Статус"];
                    if (statusCell != null && statusCell.Value != null && 
                        statusCell.Value.ToString()?.Contains("ИДЕТ") == true)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.White;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка настройки колонок: {ex.Message}");
            }
        }
        
        private string GetDetailsString(SportEvent e)
        {
            if (e is Football f)
            {
                return $"🏟️ Стадион: {f.StadiumName} | ⚽ Счет: {f.Team1Score}:{f.Team2Score}";
            }
            else if (e is Tennis t)
            {
                return $"🎾 Покрытие: {t.CourtSurface} | 📊 Счет: {t.Player1Score}-{t.Player2Score} | 🎮 Гейм: {t.CurrentGame}";
            }
            return "";
        }
        
        private void OpenFootballForm(Football? football)
        {
            using (var form = new FootballForm(_context, football))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadDataAndRestoreSelection();
                }
            }
        }
        
        private void OpenTennisForm(Tennis? tennis)
        {
            using (var form = new TennisForm(_context, tennis))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadDataAndRestoreSelection();
                }
            }
        }
        
        private void EditSelected()
        {
            if (_selectedEvent == null)
            {
                MessageBox.Show("Выберите событие для редактирования", "Внимание", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            if (_selectedEvent is Football football)
                OpenFootballForm(football);
            else if (_selectedEvent is Tennis tennis)
                OpenTennisForm(tennis);
        }
        
        private void DeleteSelected()
        {
            if (_selectedEvent == null)
            {
                MessageBox.Show("Выберите событие для удаления", "Внимание", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            if (MessageBox.Show("Вы уверены, что хотите удалить выбранное событие?", "Подтверждение", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _context.SportEvents.Remove(_selectedEvent);
                _context.SaveChanges();
                _selectedEvent = null;
                _selectedEventId = -1;
                LoadData();
                UpdateUniqueButtonsState();
                UpdateStartStopButtonText();
                MessageBox.Show("Событие удалено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        private void ShowInfoSelected()
        {
            if (_selectedEvent != null)
            {
                MessageBox.Show(_selectedEvent.ShowInfo(), "Информация о событии", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Выберите событие", "Внимание", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}