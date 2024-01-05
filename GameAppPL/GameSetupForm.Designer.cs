namespace BlackJackGame
{
    partial class GameSetupForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            panel1 = new Panel();
            txtNumDecks = new TextBox();
            btnStartGame = new Button();
            label3 = new Label();
            groupBox1 = new GroupBox();
            lstPlayerNames = new ListBox();
            txtName = new TextBox();
            btnDeletePlayer = new Button();
            btnEditPlayer = new Button();
            btnAddPlayer = new Button();
            label2 = new Label();
            panel1.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Broadway", 18F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.FromArgb(0, 64, 0);
            label1.Location = new Point(133, 26);
            label1.Name = "label1";
            label1.Size = new Size(204, 34);
            label1.TabIndex = 0;
            label1.Text = "Black Jack";
            // 
            // panel1
            // 
            panel1.BackColor = Color.DarkGreen;
            panel1.Controls.Add(txtNumDecks);
            panel1.Controls.Add(btnStartGame);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(groupBox1);
            panel1.Location = new Point(12, 81);
            panel1.Name = "panel1";
            panel1.Size = new Size(458, 472);
            panel1.TabIndex = 1;
            // 
            // txtNumDecks
            // 
            txtNumDecks.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            txtNumDecks.Location = new Point(221, 320);
            txtNumDecks.Name = "txtNumDecks";
            txtNumDecks.RightToLeft = RightToLeft.Yes;
            txtNumDecks.Size = new Size(195, 27);
            txtNumDecks.TabIndex = 7;
            txtNumDecks.Text = "txtNumDecks";
            // 
            // btnStartGame
            // 
            btnStartGame.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnStartGame.ForeColor = Color.FromArgb(0, 64, 0);
            btnStartGame.Location = new Point(118, 382);
            btnStartGame.Name = "btnStartGame";
            btnStartGame.Size = new Size(207, 57);
            btnStartGame.TabIndex = 2;
            btnStartGame.Text = "Start Game";
            btnStartGame.UseVisualStyleBackColor = true;
            btnStartGame.Click += btnStartGame_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = Color.White;
            label3.Location = new Point(36, 322);
            label3.Name = "label3";
            label3.Size = new Size(155, 23);
            label3.TabIndex = 1;
            label3.Text = "Number of Decks:";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(lstPlayerNames);
            groupBox1.Controls.Add(txtName);
            groupBox1.Controls.Add(btnDeletePlayer);
            groupBox1.Controls.Add(btnEditPlayer);
            groupBox1.Controls.Add(btnAddPlayer);
            groupBox1.Controls.Add(label2);
            groupBox1.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox1.ForeColor = Color.White;
            groupBox1.Location = new Point(18, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(419, 284);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Players";
            // 
            // lstPlayerNames
            // 
            lstPlayerNames.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lstPlayerNames.FormattingEnabled = true;
            lstPlayerNames.ItemHeight = 20;
            lstPlayerNames.Location = new Point(203, 91);
            lstPlayerNames.Name = "lstPlayerNames";
            lstPlayerNames.RightToLeft = RightToLeft.Yes;
            lstPlayerNames.Size = new Size(195, 164);
            lstPlayerNames.TabIndex = 6;
            // 
            // txtName
            // 
            txtName.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            txtName.Location = new Point(203, 40);
            txtName.Name = "txtName";
            txtName.RightToLeft = RightToLeft.Yes;
            txtName.Size = new Size(195, 27);
            txtName.TabIndex = 4;
            txtName.Text = "txtName";
            // 
            // btnDeletePlayer
            // 
            btnDeletePlayer.ForeColor = Color.Black;
            btnDeletePlayer.Location = new Point(19, 205);
            btnDeletePlayer.Name = "btnDeletePlayer";
            btnDeletePlayer.Size = new Size(148, 34);
            btnDeletePlayer.TabIndex = 3;
            btnDeletePlayer.Text = "Delete Player";
            btnDeletePlayer.UseVisualStyleBackColor = true;
            btnDeletePlayer.Click += btnDeletePlayer_Click;
            // 
            // btnEditPlayer
            // 
            btnEditPlayer.ForeColor = Color.Black;
            btnEditPlayer.Location = new Point(19, 152);
            btnEditPlayer.Name = "btnEditPlayer";
            btnEditPlayer.Size = new Size(148, 34);
            btnEditPlayer.TabIndex = 2;
            btnEditPlayer.Text = "Edit Player";
            btnEditPlayer.UseVisualStyleBackColor = true;
            btnEditPlayer.Click += btnEditPlayer_Click;
            // 
            // btnAddPlayer
            // 
            btnAddPlayer.ForeColor = Color.Black;
            btnAddPlayer.Location = new Point(19, 99);
            btnAddPlayer.Name = "btnAddPlayer";
            btnAddPlayer.Size = new Size(148, 34);
            btnAddPlayer.TabIndex = 1;
            btnAddPlayer.Text = "Add Player";
            btnAddPlayer.UseVisualStyleBackColor = true;
            btnAddPlayer.Click += btnAddPlayer_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(18, 41);
            label2.Name = "label2";
            label2.Size = new Size(62, 23);
            label2.TabIndex = 0;
            label2.Text = "Name:";
            // 
            // GameSetupForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Green;
            ClientSize = new Size(484, 565);
            Controls.Add(panel1);
            Controls.Add(label1);
            Name = "GameSetupForm";
            Text = "GameSetupForm";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Panel panel1;
        private GroupBox groupBox1;
        private Label label3;
        private Label label2;
        private Button btnAddPlayer;
        private Button btnStartGame;
        private Button btnDeletePlayer;
        private Button btnEditPlayer;
        private TextBox txtName;
        private ListBox lstPlayerNames;
        private TextBox txtNumDecks;
    }
}