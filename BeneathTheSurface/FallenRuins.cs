using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace BeneathTheSurface
{
    public partial class FallenRuins : Form
    {
        // --- Позиция, размер и скорост на героя ---
        private Point playerPosition;

        // СТАБИЛНИ ОГРОМНИ РАЗМЕРИ НА ЕКРАНА:
        private Size normalSize = new Size(220, 280);
        private Size crouchSize = new Size(220, 190);
        private Size playerSize;
        private int playerSpeed = 8;

        // --- Физика за Скачане и Гравитация ---
        private bool isJumping = false;
        private double jumpSpeed = 0;
        private double gravity = 1.2;
        private int groundLevel = 0;

        private Timer gameTimer;
        private bool moveLeft, moveRight, isCrouching;

        // --- Четирите чисти изрязани кадъра ---
        private Image frameIdle;
        private Image frameWalk;
        private Image frameCrouch;
        private Image frameJump;

        private bool useFirstFrame = true;
        private int animationCounter = 0;
        private bool facingRight = true;

        public FallenRuins()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            this.BackColor = Color.Black;
            this.Text = "Fallen Ruins";
            this.WindowState = FormWindowState.Maximized;

            playerSize = normalSize;

            // 1. ИЗРЯЗВАНЕ СЪС ЗАЩИТА ОТ DPI РАЗТЯГАНЕ
            CropLinearSpriteSheetFromFile();

            this.Load += FallenRuins_Load;
            this.KeyDown += FallenRuins_KeyDown;
            this.KeyUp += FallenRuins_KeyUp;
            this.Paint += FallenRuins_Paint;

            // Игрален таймер
            gameTimer = new Timer();
            gameTimer.Interval = 16;
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();
        }

        private void FallenRuins_Load(object sender, EventArgs e)
        {
            int centerX = (this.ClientSize.Width - playerSize.Width) / 2;

            // Заковаваме краката на героя перфектно върху каменните плочи (долу на екрана)
            groundLevel = this.ClientSize.Height - 110;

            playerPosition = new Point(centerX, groundLevel - playerSize.Height);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED (Пълна хардуерна мазнота и 0% лаг)
                return cp;
            }
        }

        private void CropLinearSpriteSheetFromFile()
        {
            try
            {
                string projectPath = AppDomain.CurrentDomain.BaseDirectory;
                string filePath = Path.Combine(projectPath, "..", "..", "player_sheet.png");

                if (!File.Exists(filePath))
                {
                    filePath = Path.Combine(projectPath, "player_sheet.png");
                }

                if (!File.Exists(filePath))
                {
                    MessageBox.Show("Грешка: Файлът player_sheet.png не беше намерен!");
                    return;
                }

                // ВАЖНО: Зареждаме картинката първо през суров поток от байтове.
                // Това пречи на Windows изкуствено да разтяга или свива пикселите спрямо DPI на монитора ти!
                byte[] bytes = File.ReadAllBytes(filePath);
                using (MemoryStream ms = new MemoryStream(bytes))
                using (Bitmap sourceSheet = new Bitmap(ms))
                {
                    // Взимаме чистите физически пиксели
                    int totalWidth = sourceSheet.Width;
                    int totalHeight = sourceSheet.Height;
                    int frameWidth = totalWidth / 4;

                    // Добавяме защитен бордюр от 6 пиксела, за да отрежем абсолютно всякакви остатъци от съседни пози
                    int padding = 6;
                    int safeWidth = frameWidth - (padding * 2);

                    // Изрязване по хоризонталната линия с нулиране на DPI резолюцията на под-кадрите
                    frameIdle = CutFrame(sourceSheet, padding, 0, safeWidth, totalHeight);
                    frameWalk = CutFrame(sourceSheet, frameWidth + padding, 0, safeWidth, totalHeight);
                    frameCrouch = CutFrame(sourceSheet, (frameWidth * 2) + padding, 0, safeWidth, totalHeight);
                    frameJump = CutFrame(sourceSheet, (frameWidth * 3) + padding, 0, safeWidth, totalHeight);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка при прецизно изрязване: " + ex.Message);
            }
        }

        private Bitmap CutFrame(Bitmap src, int x, int y, int w, int h)
        {
            Bitmap bmp = new Bitmap(w, h);

            // Казваме на новия кадър да забрави мащабирането на Windows и да бъде с нормална резолюция
            bmp.SetResolution(96, 96);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                // Настройки за кристално чист и остър ретро Pixel Art
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;

                g.DrawImage(src, new Rectangle(0, 0, w, h), new Rectangle(x, y, w, h), GraphicsUnit.Pixel);
            }
            return bmp;
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            groundLevel = this.ClientSize.Height - 110;

            int currentFeetY = playerPosition.Y + playerSize.Height;

            if (isCrouching && !isJumping)
            {
                playerSize = crouchSize;
                playerSpeed = 3;
            }
            else
            {
                playerSize = normalSize;
                playerSpeed = 8;
            }

            // Напасване на Y позицията при клякане
            playerPosition.Y = currentFeetY - playerSize.Height;

            if (moveLeft && playerPosition.X > 0) playerPosition.X -= playerSpeed;
            if (moveRight && playerPosition.X + playerSize.Width < this.ClientSize.Width) playerPosition.X += playerSpeed;

            if (isJumping)
            {
                playerPosition.Y += (int)jumpSpeed;
                jumpSpeed += gravity;

                if (playerPosition.Y + playerSize.Height >= groundLevel)
                {
                    playerPosition.Y = groundLevel - playerSize.Height;
                    isJumping = false;
                }
            }
            else
            {
                if (playerPosition.Y + playerSize.Height != groundLevel)
                {
                    playerPosition.Y = groundLevel - playerSize.Height;
                }
            }

            this.Invalidate();
        }

        private void FallenRuins_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Настройка, която спира размазването на пикселите при рисуване на екрана
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

            if (moveLeft) facingRight = false;
            if (moveRight) facingRight = true;

            Image currentFrame = frameIdle;

            if (isJumping) currentFrame = frameJump;
            else if (isCrouching) currentFrame = frameCrouch;
            else if (moveLeft || moveRight)
            {
                animationCounter++;
                if (animationCounter >= 8)
                {
                    useFirstFrame = !useFirstFrame;
                    animationCounter = 0;
                }
                currentFrame = useFirstFrame ? frameIdle : frameWalk;
            }

            if (currentFrame != null)
            {
                Image frameToDraw = (Image)currentFrame.Clone();

                if (!facingRight)
                {
                    frameToDraw.RotateFlip(RotateFlipType.RotateNoneFlipX);
                }

                // РИСУВАНЕ С ТОЧНИЯ ОГРОМЕН РАЗМЕР
                g.DrawImage(frameToDraw, new Rectangle(playerPosition, playerSize));
                frameToDraw.Dispose();
            }
        }

        private void FallenRuins_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A) moveLeft = true;
            if (e.KeyCode == Keys.D) moveRight = true;

            if (e.KeyCode == Keys.W && !isJumping && !isCrouching)
            {
                isJumping = true;
                jumpSpeed = -22;
            }

            if (e.KeyCode == Keys.S) isCrouching = true;
        }

        private void FallenRuins_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A) moveLeft = false;
            if (e.KeyCode == Keys.D) moveRight = false;
            if (e.KeyCode == Keys.S) isCrouching = false;
        }
    }
}
