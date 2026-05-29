using System;
using System.Drawing;
using System.Windows.Forms;

namespace BeneathTheSurface
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Text = "Beneath The Surface";

            // Свързваме събитието за Full Screen оразмеряване
            this.Resize += MainMenu_Resize;
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            CenterMenuElements();
        }

        private void MainMenu_Resize(object sender, EventArgs e)
        {
            this.SuspendLayout();
            CenterMenuElements();
            this.ResumeLayout(true);
        }

        private void CenterMenuElements()
        {
            int screenWidth = this.ClientSize.Width;
            int screenHeight = this.ClientSize.Height;

            // КОДЪТ МЕСТИ САМО ЦЕНТЪРА И ЗАПАЗВА РАЗМЕРИТЕ ТИ ДО ПИКPriority!

            // 1. Центриране на Title
            this.Title.Left = (screenWidth - this.Title.Width) / 2;
            this.Title.Top = (int)(screenHeight * 0.18);

            // 2. Центриране на PLAY
            this.play.Left = (screenWidth - this.play.Width) / 2;
            this.play.Top = this.Title.Bottom + 35;

            // 3. Центриране на CREDITS
            this.credits.Left = (screenWidth - this.credits.Width) / 2;
            this.credits.Top = this.play.Bottom + 15;

            // 4. Центриране на EXIT
            this.exit.Left = (screenWidth - this.exit.Width) / 2;
            this.exit.Top = this.credits.Bottom + 15;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            FallenRuins newForm = new FallenRuins();
            newForm.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Game created by: Ademm93", "Credits");
        }

        private void label1_Click(object sender, EventArgs e) { }
    }
}
