namespace PacmanEditor
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            mapPicturebox = new PictureBox();
            tilesPicturebox = new PictureBox();
            selectedTileLabel = new Label();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openROMToolStripMenuItem = new ToolStripMenuItem();
            saveROMToolStripMenuItem = new ToolStripMenuItem();
            tileinfosCheckbox = new CheckBox();
            label1 = new Label();
            dotseatCheckbox = new CheckBox();
            dotseatTextbox = new TextBox();
            tileradioBox = new RadioButton();
            spriteradioBox = new RadioButton();
            lockCheckbox = new CheckBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            ((System.ComponentModel.ISupportInitialize)mapPicturebox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tilesPicturebox).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // mapPicturebox
            // 
            mapPicturebox.BackColor = Color.Black;
            mapPicturebox.Location = new Point(12, 27);
            mapPicturebox.Name = "mapPicturebox";
            mapPicturebox.Size = new Size(360, 448);
            mapPicturebox.TabIndex = 0;
            mapPicturebox.TabStop = false;
            mapPicturebox.Paint += mapPicturebox_Paint;
            mapPicturebox.MouseDown += mapPicturebox_MouseDown;
            mapPicturebox.MouseMove += mapPicturebox_MouseMove;
            mapPicturebox.MouseUp += mapPicturebox_MouseUp;
            mapPicturebox.Move += mapPicturebox_Move;
            // 
            // tilesPicturebox
            // 
            tilesPicturebox.BackColor = Color.Black;
            tilesPicturebox.Location = new Point(530, 27);
            tilesPicturebox.Name = "tilesPicturebox";
            tilesPicturebox.Size = new Size(256, 368);
            tilesPicturebox.TabIndex = 1;
            tilesPicturebox.TabStop = false;
            tilesPicturebox.Paint += tilesPicturebox_Paint;
            tilesPicturebox.MouseDown += tilesPicturebox_MouseDown;
            // 
            // selectedTileLabel
            // 
            selectedTileLabel.AutoSize = true;
            selectedTileLabel.Location = new Point(380, 159);
            selectedTileLabel.Name = "selectedTileLabel";
            selectedTileLabel.Size = new Size(81, 15);
            selectedTileLabel.TabIndex = 2;
            selectedTileLabel.Text = "Selected Tile : ";
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(799, 24);
            menuStrip1.TabIndex = 3;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openROMToolStripMenuItem, saveROMToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // openROMToolStripMenuItem
            // 
            openROMToolStripMenuItem.Name = "openROMToolStripMenuItem";
            openROMToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.O;
            openROMToolStripMenuItem.Size = new Size(176, 22);
            openROMToolStripMenuItem.Text = "Open ROM";
            openROMToolStripMenuItem.Click += openROMToolStripMenuItem_Click;
            // 
            // saveROMToolStripMenuItem
            // 
            saveROMToolStripMenuItem.Name = "saveROMToolStripMenuItem";
            saveROMToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            saveROMToolStripMenuItem.Size = new Size(176, 22);
            saveROMToolStripMenuItem.Text = "Save ROM";
            saveROMToolStripMenuItem.Click += saveROMToolStripMenuItem_Click;
            // 
            // tileinfosCheckbox
            // 
            tileinfosCheckbox.AutoSize = true;
            tileinfosCheckbox.Checked = true;
            tileinfosCheckbox.CheckState = CheckState.Checked;
            tileinfosCheckbox.Location = new Point(380, 137);
            tileinfosCheckbox.Name = "tileinfosCheckbox";
            tileinfosCheckbox.Size = new Size(73, 19);
            tileinfosCheckbox.TabIndex = 4;
            tileinfosCheckbox.Text = "Tile Infos";
            tileinfosCheckbox.UseVisualStyleBackColor = true;
            tileinfosCheckbox.CheckedChanged += tileinfosCheckbox_CheckedChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(378, 195);
            label1.Name = "label1";
            label1.Size = new Size(119, 15);
            label1.TabIndex = 5;
            label1.Text = "Number dots to eat : ";
            // 
            // dotseatCheckbox
            // 
            dotseatCheckbox.AutoSize = true;
            dotseatCheckbox.Checked = true;
            dotseatCheckbox.CheckState = CheckState.Checked;
            dotseatCheckbox.Location = new Point(378, 242);
            dotseatCheckbox.Name = "dotseatCheckbox";
            dotseatCheckbox.Size = new Size(92, 19);
            dotseatCheckbox.TabIndex = 6;
            dotseatCheckbox.Text = "same as tiles";
            dotseatCheckbox.UseVisualStyleBackColor = true;
            dotseatCheckbox.CheckedChanged += dotseatCheckbox_CheckedChanged;
            // 
            // dotseatTextbox
            // 
            dotseatTextbox.Location = new Point(378, 213);
            dotseatTextbox.Name = "dotseatTextbox";
            dotseatTextbox.ReadOnly = true;
            dotseatTextbox.Size = new Size(100, 23);
            dotseatTextbox.TabIndex = 7;
            dotseatTextbox.TextChanged += dotseatTextbox_TextChanged;
            // 
            // tileradioBox
            // 
            tileradioBox.AutoSize = true;
            tileradioBox.Checked = true;
            tileradioBox.Location = new Point(378, 27);
            tileradioBox.Name = "tileradioBox";
            tileradioBox.Size = new Size(71, 19);
            tileradioBox.TabIndex = 8;
            tileradioBox.TabStop = true;
            tileradioBox.Text = "Edit Tiles";
            tileradioBox.UseVisualStyleBackColor = true;
            // 
            // spriteradioBox
            // 
            spriteradioBox.AutoSize = true;
            spriteradioBox.Location = new Point(378, 52);
            spriteradioBox.Name = "spriteradioBox";
            spriteradioBox.Size = new Size(83, 19);
            spriteradioBox.TabIndex = 9;
            spriteradioBox.TabStop = true;
            spriteradioBox.Text = "Edit Sprites";
            spriteradioBox.UseVisualStyleBackColor = true;
            // 
            // lockCheckbox
            // 
            lockCheckbox.AutoSize = true;
            lockCheckbox.Checked = true;
            lockCheckbox.CheckState = CheckState.Checked;
            lockCheckbox.Location = new Point(378, 77);
            lockCheckbox.Name = "lockCheckbox";
            lockCheckbox.Size = new Size(107, 19);
            lockCheckbox.TabIndex = 12;
            lockCheckbox.Text = "lock to 8x8 grid";
            lockCheckbox.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.Goldenrod;
            label2.Location = new Point(530, 445);
            label2.Name = "label2";
            label2.Size = new Size(228, 30);
            label2.TabIndex = 13;
            label2.Text = "Warnng you can only have 4 \"big dots\"\r\nhaving more can cause them to disappear";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(530, 398);
            label3.Name = "label3";
            label3.Size = new Size(242, 45);
            label3.TabIndex = 14;
            label3.Text = "Big dots are weird you want to keep them in \r\ncorners and aligned to each other otherwise\r\nthey will not work properly";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 478);
            label4.Name = "label4";
            label4.Size = new Size(360, 15);
            label4.TabIndex = 15;
            label4.Text = "The warp position < > is hard coded and cannot be moved for now";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(799, 499);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(lockCheckbox);
            Controls.Add(spriteradioBox);
            Controls.Add(tileradioBox);
            Controls.Add(dotseatTextbox);
            Controls.Add(dotseatCheckbox);
            Controls.Add(label1);
            Controls.Add(tileinfosCheckbox);
            Controls.Add(selectedTileLabel);
            Controls.Add(tilesPicturebox);
            Controls.Add(mapPicturebox);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "NES Pacman Editor";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)mapPicturebox).EndInit();
            ((System.ComponentModel.ISupportInitialize)tilesPicturebox).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox mapPicturebox;
        private PictureBox tilesPicturebox;
        private Label selectedTileLabel;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem openROMToolStripMenuItem;
        private ToolStripMenuItem saveROMToolStripMenuItem;
        private CheckBox tileinfosCheckbox;
        private Label label1;
        private CheckBox dotseatCheckbox;
        private TextBox dotseatTextbox;
        private RadioButton tileradioBox;
        private RadioButton spriteradioBox;
        private CheckBox lockCheckbox;
        private Label label2;
        private Label label3;
        private Label label4;
    }
}
