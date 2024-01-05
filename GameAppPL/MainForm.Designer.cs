namespace BlackJackGame
{
    partial class MainForm
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
            lblPlayerWinsLabel = new Label();
            lblPlayerWins = new Label();
            label3 = new Label();
            lblDealerWinsLabel = new Label();
            lblDealerWins = new Label();
            label6 = new Label();
            pnlDealerHand = new Panel();
            pnlPlayerHand = new Panel();
            lstPlayers = new ListBox();
            btnShuffle = new Button();
            btnExit = new Button();
            btnStand = new Button();
            btnStartRound = new Button();
            btnHit = new Button();
            label1 = new Label();
            lblDeckCount = new Label();
            SuspendLayout();
            // 
            // lblPlayerWinsLabel
            // 
            lblPlayerWinsLabel.AutoSize = true;
            lblPlayerWinsLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblPlayerWinsLabel.ForeColor = Color.White;
            lblPlayerWinsLabel.Location = new Point(23, 20);
            lblPlayerWinsLabel.Name = "lblPlayerWinsLabel";
            lblPlayerWinsLabel.Size = new Size(138, 28);
            lblPlayerWinsLabel.TabIndex = 0;
            lblPlayerWinsLabel.Text = "Player's Wins";
            // 
            // lblPlayerWins
            // 
            lblPlayerWins.BackColor = Color.White;
            lblPlayerWins.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lblPlayerWins.Location = new Point(26, 60);
            lblPlayerWins.Name = "lblPlayerWins";
            lblPlayerWins.Size = new Size(144, 30);
            lblPlayerWins.TabIndex = 1;
            lblPlayerWins.Text = "lblPlayerWins";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Broadway", 22.2F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = Color.FromArgb(0, 64, 0);
            label3.Location = new Point(275, 19);
            label3.Name = "label3";
            label3.Size = new Size(249, 42);
            label3.TabIndex = 2;
            label3.Text = "Black Jack";
            // 
            // lblDealerWinsLabel
            // 
            lblDealerWinsLabel.AutoSize = true;
            lblDealerWinsLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblDealerWinsLabel.ForeColor = Color.White;
            lblDealerWinsLabel.Location = new Point(638, 21);
            lblDealerWinsLabel.Name = "lblDealerWinsLabel";
            lblDealerWinsLabel.Size = new Size(141, 28);
            lblDealerWinsLabel.TabIndex = 3;
            lblDealerWinsLabel.Text = "Dealer's Wins";
            lblDealerWinsLabel.TextAlign = ContentAlignment.TopRight;
            // 
            // lblDealerWins
            // 
            lblDealerWins.BackColor = Color.White;
            lblDealerWins.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lblDealerWins.Location = new Point(631, 60);
            lblDealerWins.Name = "lblDealerWins";
            lblDealerWins.Size = new Size(144, 30);
            lblDealerWins.TabIndex = 4;
            lblDealerWins.Text = "lblDealerWins";
            lblDealerWins.TextAlign = ContentAlignment.TopRight;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label6.ForeColor = Color.White;
            label6.Location = new Point(949, 77);
            label6.Name = "label6";
            label6.Size = new Size(93, 23);
            label6.TabIndex = 5;
            label6.Text = "All Players";
            // 
            // pnlDealerHand
            // 
            pnlDealerHand.BackColor = Color.DarkGreen;
            pnlDealerHand.Location = new Point(26, 107);
            pnlDealerHand.Name = "pnlDealerHand";
            pnlDealerHand.Size = new Size(751, 125);
            pnlDealerHand.TabIndex = 6;
            // 
            // pnlPlayerHand
            // 
            pnlPlayerHand.BackColor = Color.DarkGreen;
            pnlPlayerHand.Location = new Point(26, 260);
            pnlPlayerHand.Name = "pnlPlayerHand";
            pnlPlayerHand.Size = new Size(751, 125);
            pnlPlayerHand.TabIndex = 7;
            // 
            // lstPlayers
            // 
            lstPlayers.FormattingEnabled = true;
            lstPlayers.ItemHeight = 20;
            lstPlayers.Location = new Point(826, 107);
            lstPlayers.Name = "lstPlayers";
            lstPlayers.RightToLeft = RightToLeft.Yes;
            lstPlayers.SelectionMode = SelectionMode.None;
            lstPlayers.Size = new Size(216, 184);
            lstPlayers.TabIndex = 8;
            // 
            // btnShuffle
            // 
            btnShuffle.Enabled = false;
            btnShuffle.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnShuffle.Location = new Point(826, 307);
            btnShuffle.Name = "btnShuffle";
            btnShuffle.Size = new Size(216, 29);
            btnShuffle.TabIndex = 9;
            btnShuffle.Text = "Shuffle Deck";
            btnShuffle.UseVisualStyleBackColor = true;
            btnShuffle.Click += btnShuffle_Click;
            // 
            // btnExit
            // 
            btnExit.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnExit.Location = new Point(826, 345);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(216, 29);
            btnExit.TabIndex = 10;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // btnStand
            // 
            btnStand.BackColor = Color.Crimson;
            btnStand.Enabled = false;
            btnStand.FlatStyle = FlatStyle.Popup;
            btnStand.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnStand.Location = new Point(501, 432);
            btnStand.Name = "btnStand";
            btnStand.Size = new Size(106, 39);
            btnStand.TabIndex = 11;
            btnStand.Text = "Stand";
            btnStand.UseVisualStyleBackColor = false;
            btnStand.Click += btnStand_Click;
            // 
            // btnStartRound
            // 
            btnStartRound.BackColor = Color.White;
            btnStartRound.FlatStyle = FlatStyle.Popup;
            btnStartRound.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            btnStartRound.Location = new Point(319, 428);
            btnStartRound.Name = "btnStartRound";
            btnStartRound.Size = new Size(161, 45);
            btnStartRound.TabIndex = 12;
            btnStartRound.Text = "Start Round";
            btnStartRound.UseVisualStyleBackColor = false;
            btnStartRound.Click += btnStartRound_Click;
            // 
            // btnHit
            // 
            btnHit.BackColor = Color.Goldenrod;
            btnHit.Enabled = false;
            btnHit.FlatStyle = FlatStyle.Popup;
            btnHit.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnHit.ForeColor = Color.Black;
            btnHit.Location = new Point(191, 432);
            btnHit.Name = "btnHit";
            btnHit.Size = new Size(106, 39);
            btnHit.TabIndex = 13;
            btnHit.Text = "Hit";
            btnHit.UseVisualStyleBackColor = false;
            btnHit.Click += btnHit_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.White;
            label1.Location = new Point(319, 65);
            label1.Name = "label1";
            label1.Size = new Size(108, 23);
            label1.TabIndex = 14;
            label1.Text = "Deck Count:";
            // 
            // lblDeckCount
            // 
            lblDeckCount.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point);
            lblDeckCount.ForeColor = Color.White;
            lblDeckCount.Location = new Point(433, 65);
            lblDeckCount.Name = "lblDeckCount";
            lblDeckCount.Size = new Size(45, 23);
            lblDeckCount.TabIndex = 15;
            lblDeckCount.Text = "0";
            lblDeckCount.TextAlign = ContentAlignment.TopRight;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Green;
            ClientSize = new Size(1076, 502);
            Controls.Add(lblDeckCount);
            Controls.Add(label1);
            Controls.Add(btnHit);
            Controls.Add(btnStartRound);
            Controls.Add(btnStand);
            Controls.Add(btnExit);
            Controls.Add(btnShuffle);
            Controls.Add(lstPlayers);
            Controls.Add(pnlPlayerHand);
            Controls.Add(pnlDealerHand);
            Controls.Add(label6);
            Controls.Add(lblDealerWins);
            Controls.Add(lblDealerWinsLabel);
            Controls.Add(label3);
            Controls.Add(lblPlayerWins);
            Controls.Add(lblPlayerWinsLabel);
            Name = "MainForm";
            Text = "MainForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblPlayerWinsLabel;
        private Label lblPlayerWins;
        private Label label3;
        private Label lblDealerWinsLabel;
        private Label lblDealerWins;
        private Label label6;
        private Panel pnlDealerHand;
        private Panel pnlPlayerHand;
        private ListBox lstPlayers;
        private Button btnShuffle;
        private Button btnExit;
        private Button btnStand;
        private Button btnStartRound;
        private Button btnHit;
        private Label label1;
        private Label lblDeckCount;
    }
}