using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace PacmanEditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        byte[] ROMData = new byte[0x6010];
        byte[] GfxData = new byte[0x2000];
        byte[] PCGfxData = new byte[0x2000];
        PointeredImage tilesBitmap = new PointeredImage(128, 184);
        PointeredImage screenBitmap = new PointeredImage(176, 256);
        byte[] tilemap = new byte[594];
        string openedROMPath = "";
        byte nbrDots = 0;
        private unsafe void Form1_Load(object sender, EventArgs e)
        {



        }

        // tile 04 = <- wrap
        // tile 05 = -> wrap
        // tile 06 = on top

        private void tilesPicturebox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            //e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;
            e.Graphics.DrawImage(tilesBitmap.bitmap, new Rectangle(1, 1, 256, 368), new Rectangle(0, 0, 128, 184), GraphicsUnit.Pixel);


            if (tileinfosCheckbox.Checked)
            {
                e.Graphics.DrawString("<", this.Font, Brushes.White, 4 * 16, 0);
                e.Graphics.DrawString(">", this.Font, Brushes.White, 5 * 16, 0);
                e.Graphics.DrawString("O", this.Font, Brushes.White, 6 * 16, 0);
            }

            int rx = (selectedTile % 16);
            int ry = (selectedTile / 16);
            e.Graphics.DrawRectangle(Pens.Lime, new Rectangle(rx * 16, ry * 16, 16, 16));
        }
        ushort[] dotsAddress = new ushort[4];
        private void mapPicturebox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            //e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
            e.Graphics.DrawImage(screenBitmap.bitmap, new Rectangle(1, 1, 352, 512), new Rectangle(0, 0, 176, 256), GraphicsUnit.Pixel);
            int bigdotcount = 0;
            label2.Visible = false;
            if (dotseatCheckbox.Checked)
            {
                nbrDots = 0;
            }
            for (int i = 0; i < 594; i++)
            {
                int rx = (i % 22);
                int ry = (i / 22);
                if (dotseatCheckbox.Checked)
                {
                    
                    if (tilemap[i] == 0x03 || tilemap[i] == 0x09 || tilemap[i] == 0x01)
                    {
                        nbrDots += 1;
                    }
                    
                }
                if (tilemap[i] == 0x01)
                {
                    int startingPPUAddr = 0x2040;
                    if (bigdotcount <= 3)
                    {
                        dotsAddress[bigdotcount] = (ushort)((startingPPUAddr + rx) + (ry * 0x20));
                    }
                    else
                    {
                        label2.Visible = true;
                    }
                    bigdotcount++;
                    //dotsAddress = 
                }

                if (tileinfosCheckbox.Checked)
                {
                    if (tilemap[i] == 4)
                    {

                        e.Graphics.DrawString("<", this.Font, Brushes.White, rx * 16, ry * 16);
                    }

                    if (tilemap[i] == 5)
                    {
                        e.Graphics.DrawString(">", this.Font, Brushes.White, rx * 16, ry * 16);
                    }
                    else if (tilemap[i] == 6)
                    {
                        e.Graphics.DrawString("O", this.Font, Brushes.White, rx * 16, ry * 16);
                    }
                    //e.Graphics.DrawRectangle(Pens.Lime, new Rectangle(rx * 16, ry * 16, 16, 16));
                }
            }
            if (dotseatCheckbox.Checked)
            {
                dotseatTextbox.Text = nbrDots.ToString();
            }
        }
        int mtx = 0;
        int mty = 0;
        int lastmtx = 0;
        int lastmty = 0;
        bool mdown = false;
        byte selectedTile = 0;
        Sprite selectedSprite = null;
        private void mapPicturebox_MouseDown(object sender, MouseEventArgs e)
        {
            mdown = true;
            if (tileradioBox.Checked)
            {
                mtx = Math.Clamp((e.X / 16), 0, 21);
                mty = Math.Clamp((e.Y / 16), 0, 26);
                int tid = (mtx + (mty * 22));
                if (e.Button == MouseButtons.Left)
                {

                    tilemap[tid] = selectedTile;
                    screenBitmap.DrawBitmapTile(mtx * 8, mty * 8, tilesBitmap, tilemap[tid], 1);
                    
                    mapPicturebox.Refresh();
                }
                else if (e.Button == MouseButtons.Right)
                {
                    selectedTile = tilemap[tid];
                    selectedTileLabel.Text = "Selected Tile : " + selectedTile.ToString("X2");
                }
            }
            else
            {
                if (spriteradioBox.Checked)
                {
                    int rex = (e.X / 2);
                    int rey = (e.Y / 2);

                    foreach (Sprite spr in sprites)
                    {
                        if (rex > spr.x - 12 && rex < spr.x + 4 && rey > spr.y - 12 && rey < spr.y + 4)
                        {
                            selectedSprite = spr;
                            break;
                        }
                    }
                }


                DrawTilemap(); // do not update whole tilemap if in tilemap that's useless
            }

            DrawSprites();
            mapPicturebox.Refresh();

        }

        private void mapPicturebox_Move(object sender, EventArgs e)
        {

        }

        private void mapPicturebox_MouseMove(object sender, MouseEventArgs e)
        {
            if (mdown)
            {
                if (tileradioBox.Checked)
                {
                    mtx = Math.Clamp((e.X / 16), 0, 21);
                    mty = Math.Clamp((e.Y / 16), 0, 26);


                    if (mtx != lastmtx || mty != lastmty)
                    {

                        int tid = (mtx + (mty * 22));
                        tilemap[tid] = selectedTile;
                        screenBitmap.DrawBitmapTile(mtx * 8, mty * 8, tilesBitmap, tilemap[tid], 1);
                        
                        lastmty = mty;
                        lastmtx = mtx;
                    }
                }
                else
                {
                    if (spriteradioBox.Checked)
                    {
                        if (selectedSprite != null)
                        {
                            selectedSprite.x = (byte)((e.X / 2)+6);
                            selectedSprite.y = (byte)((e.Y / 2)+6);
                            if (lockCheckbox.Checked)
                            {
                                selectedSprite.x = (byte)(((selectedSprite.x+6) / 8) * 8);
                                selectedSprite.y = (byte)(((selectedSprite.y+6) / 8) * 8);
                            }
                        }
                    }

                    DrawTilemap(); // do not update whole tilemap if in tilemap that's useless
                }
                DrawSprites();
                mapPicturebox.Refresh();
            }
        }

        private void mapPicturebox_MouseUp(object sender, MouseEventArgs e)
        {
            selectedSprite = null;
            mdown = false;
        }

        private void tilesPicturebox_MouseDown(object sender, MouseEventArgs e)
        {
            int mx = Math.Clamp((e.X / 16), 0, 16);
            int my = Math.Clamp((e.Y / 16), 0, 8);
            int tid = (mx + (my * 16));

            if (tid > 0x3F)
            {
                tid = 0x3F;
            }
            else if (tid < 0)
            {
                tid = 0;
            }
            selectedTile = (byte)tid;
            selectedTileLabel.Text = "Selected Tile : " + selectedTile.ToString("X2");
            tilesPicturebox.Refresh();

        }
        Sprite[] sprites = new Sprite[6];
        private void openROMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Nes ROM|*.nes";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                openedROMPath = openFileDialog.FileName;
                this.Text = "NES Pacman Editor - " + openFileDialog.FileName;
                FileStream fs = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read);
                fs.Read(ROMData, 0, ROMData.Length);
                fs.Close();
                Array.Copy(ROMData, 0x4010, GfxData, 0, 0x2000);

                ColorPalette cp = screenBitmap.bitmap.Palette;
                cp.Entries[0] = Color.Black;
                cp.Entries[1] = Color.White;
                cp.Entries[2] = Color.Black;
                cp.Entries[3] = Color.FromArgb(166, 0, 0);

                cp.Entries[(1 * 16) + 0] = Color.Black;
                cp.Entries[(1 * 16) + 1] = Color.FromArgb(0, 113, 239);
                cp.Entries[(1 * 16) + 2] = Color.Black;
                cp.Entries[(1 * 16) + 3] = Color.FromArgb(255, 154, 56);

                cp.Entries[(2 * 16) + 0] = Color.Black;
                cp.Entries[(2 * 16) + 1] = Color.FromArgb(219, 40, 0);
                cp.Entries[(2 * 16) + 2] = Color.FromArgb(255, 117, 97);
                cp.Entries[(2 * 16) + 3] = Color.FromArgb(166, 0, 0);

                cp.Entries[(3 * 16) + 0] = Color.Black;
                cp.Entries[(3 * 16) + 1] = Color.FromArgb(0, 150, 0);
                cp.Entries[(3 * 16) + 2] = Color.FromArgb(203, 77, 12);
                cp.Entries[(3 * 16) + 3] = Color.FromArgb(32, 56, 239);

                // SPRs
                cp.Entries[(4 * 16) + 0] = Color.Black;
                cp.Entries[(4 * 16) + 1] = Color.FromArgb(255, 154, 56);
                cp.Entries[(4 * 16) + 2] = Color.White;
                cp.Entries[(4 * 16) + 3] = Color.FromArgb(166, 0, 0);

                cp.Entries[(5 * 16) + 0] = Color.Black;
                cp.Entries[(5 * 16) + 1] = Color.FromArgb(0, 113, 239);
                cp.Entries[(5 * 16) + 2] = Color.White;
                cp.Entries[(5 * 16) + 3] = Color.FromArgb(215, 203, 255);

                cp.Entries[(6 * 16) + 0] = Color.Black;
                cp.Entries[(6 * 16) + 1] = Color.FromArgb(60, 190, 255);
                cp.Entries[(6 * 16) + 2] = Color.White;
                cp.Entries[(6 * 16) + 3] = Color.FromArgb(60, 190, 255);

                cp.Entries[(7 * 16) + 0] = Color.Black;
                cp.Entries[(7 * 16) + 1] = Color.FromArgb(219, 40, 0);
                cp.Entries[(7 * 16) + 2] = Color.White;
                cp.Entries[(7 * 16) + 3] = Color.FromArgb(203, 77, 12);




                screenBitmap.bitmap.Palette = cp;
                tilesBitmap.bitmap.Palette = cp;

                PCGfxData = GraphicsManager.NesTilesToPc8bppTiles(GfxData, 336, 2);

                tilesBitmap.Draw8bppTiles(0, 0, PCGfxData, 16);

                tilesPicturebox.Refresh();
                int tilemapPos = 0;
                int pos = 0x2CFC;
                while (tilemapPos < 594)
                {

                    if ((ROMData[pos] & 0xC0) != 0x00) // do 4 tiles copy from 10
                    {
                        byte t = (byte)(ROMData[pos] & 0x3F);
                        byte c = (byte)((ROMData[pos] & 0xC0) >> 6);
                        for (int i = 0; i < c + 1; i++)
                        {
                            tilemap[tilemapPos] = (byte)(t);
                            tilemapPos++;
                        }
                        pos++;
                        continue;

                    }
                    else
                    {
                        tilemap[tilemapPos] = ROMData[pos];
                        tilemapPos++;
                        pos++;
                        continue;
                    }
                }

                if (ROMData[0x3510] != 0xFF)
                {
                    // sprites are moved
                    sprites[0] = new Sprite(ROMData[0x3511], ROMData[0x3515], 0x3C, 1); // pac-man
                    sprites[2] = new Sprite(ROMData[0x3519], ROMData[0x351D], 0x11B, 4); // red ghost
                    sprites[3] = new Sprite(ROMData[0x3521], ROMData[0x3525], 0x11B, 5); // purple ghost
                    sprites[4] = new Sprite(ROMData[0x3529], ROMData[0x352D], 0x11B, 6); // blue ghost
                    sprites[5] = new Sprite(ROMData[0x3531], ROMData[0x3535], 0x11B, 7); // orange ghost

                }
                else
                {
                    sprites[0] = new Sprite(ROMData[0xC0D], ROMData[0xC09], 0x3C, 1); // pac-man
                    sprites[2] = new Sprite(ROMData[0xC0D], ROMData[0xC15], 0x11B, 4); // red ghost
                    sprites[3] = new Sprite(ROMData[0xC0D], ROMData[0xC1F], 0x11B, 5); // purple ghost
                    sprites[4] = new Sprite(ROMData[0xC15], ROMData[0xC1F], 0x11B, 6); // blue ghost
                    sprites[5] = new Sprite(ROMData[0xC1B], ROMData[0xC1F], 0x11B, 7); // orange ghost
                }


                sprites[1] = new Sprite(ROMData[0x203F], ROMData[0x2043], 0x60, 2); // fruit

                //sprites[0] = new Sprite(ROMData[0xC0D], ROMData[0xC09], 0x3C, 1); // pac-man
                //sprites[1] = new Sprite(ROMData[0x203F], ROMData[0x2043], 0x60, 2); // fruit
                DrawTilemap();
                DrawSprites();

                nbrDots = ROMData[0x0F49];
                dotseatTextbox.Text = nbrDots.ToString();
                mapPicturebox.Refresh();

            }
        }

        private void DrawTilemap()
        {
            int tile = 0;
            for (int y = 0; y < 27; y++)
            {
                for (int x = 0; x < 22; x++)
                {

                    screenBitmap.DrawBitmapTile(x * 8, y * 8, tilesBitmap, tilemap[tile], 1, false);
                    tile++;
                }
            }
        }
        private void DrawSprites()
        {
            foreach(Sprite spr in sprites)
            {
                if (spr != null)
                {
                    screenBitmap.DrawBitmapTile(spr.x - 12, spr.y - 12, tilesBitmap, (ushort)spr.chr, spr.pal, true);
                    screenBitmap.DrawBitmapTile(spr.x - 12 + 8, spr.y - 12, tilesBitmap, (ushort)(spr.chr + 1), spr.pal, true);
                    
                    if (spr.chr == 0x11B)
                    {
                        screenBitmap.DrawBitmapTile(spr.x - 12, spr.y - 12 + 8, tilesBitmap, (ushort)(spr.chr + 3), spr.pal, true);
                        screenBitmap.DrawBitmapTile(spr.x - 12 + 8, spr.y - 12 + 8, tilesBitmap, (ushort)(spr.chr + 4), spr.pal, true);
                    }
                    else
                    {
                        screenBitmap.DrawBitmapTile(spr.x - 12, spr.y - 12 + 8, tilesBitmap, (ushort)(spr.chr + 2), spr.pal, true);
                        screenBitmap.DrawBitmapTile(spr.x - 12 + 8, spr.y - 12 + 8, tilesBitmap, (ushort)(spr.chr + 3), spr.pal, true);
                    }

                }
            }
        }
        //0C08

        private void saveROMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openedROMPath != "")
            {
                byte[] saveData = AttemptSave();
                if (saveData.Length < 0x1A0)
                {
                    for (int i = 0; i < saveData.Length; i++)
                    {
                        ROMData[0x2CFC + i] = saveData[i];
                    }
                    

                    //0C08

                    byte[] newJmpData = new byte[6]
                    {
                        0x20, 0x00, 0xF5, //JSR
                        0x4C, 0x16, 0xCC //JMP
                    };
                    for (int i = 0; i < newJmpData.Length; i++)
                    {
                        ROMData[0x0C08+i] = newJmpData[i];
                    }

                    byte[] newPosData = new byte[41]
                    {
                    0xA9, 0x60, 0x85, 0x1A, // Pac X
                    0xA9, 0xA8, 0x85, 0x1C, // Pac Y

                    0xA9, 0x60, 0x85, 0x1E, // red X
                    0xA9, 0x58, 0x85, 0x20, // red Y

                    0xA9, 0x60, 0x85, 0x22, // purpleX
                    0xA9, 0x70, 0x85, 0x24, // purpleY

                    0xA9, 0x58, 0x85, 0x26, // blueX
                    0xA9, 0x70, 0x85, 0x28, // blueY

                    0xA9, 0x68, 0x85, 0x2A, // orangeX
                    0xA9, 0x70, 0x85, 0x2C, // orangeY
                    0x60 //RTS
                    };


                    newPosData[1] = sprites[0].x;
                    newPosData[5] = sprites[0].y;

                    newPosData[9] = sprites[2].x;
                    newPosData[13] = sprites[2].y;

                    newPosData[17] = sprites[3].x;
                    newPosData[21] = sprites[3].y;

                    newPosData[25] = sprites[4].x;
                    newPosData[29] = sprites[4].y;

                    newPosData[33] = sprites[5].x;
                    newPosData[37] = sprites[5].y;

                    for (int i = 0; i < newPosData.Length; i++)
                    {
                        ROMData[0x3510+i] = newPosData[i];
                    }
                    ROMData[0x203F] = sprites[1].x;
                    ROMData[0x2043] = sprites[1].y;

                    ROMData[0x0F49] = nbrDots;

                    ROMData[0x1F7C] = (byte)((dotsAddress[1] & 0xFF00) >> 8);
                    ROMData[0x1F7C + 1] = (byte)((dotsAddress[1] & 0x00FF));

                    ROMData[0x1F7C + (1 * 2)] = (byte)((dotsAddress[0] & 0xFF00) >> 8);
                    ROMData[0x1F7C + 1 + (1 * 2)] = (byte)((dotsAddress[0] & 0x00FF));

                    ROMData[0x1F7C + (2 * 2)] = (byte)((dotsAddress[3] & 0xFF00) >> 8);
                    ROMData[0x1F7C + 1 + (2 * 2)] = (byte)((dotsAddress[3] & 0x00FF));

                    ROMData[0x1F7C + (3 * 2)] = (byte)((dotsAddress[2] & 0xFF00) >> 8);
                    ROMData[0x1F7C + 1 + (3 * 2)] = (byte)((dotsAddress[2] & 0x00FF));

                    FileStream fs = new FileStream(openedROMPath, FileMode.Open, FileAccess.Write);
                    fs.Write(ROMData, 0, ROMData.Length);
                    fs.Close();


                    //00000F49 number of dots

                }
                else
                {
                    MessageBox.Show("Too many tile data, to save space try repeating same time 4 times in horizontal");

                }

            }


        }

        private byte[] AttemptSave()
        {
            List<byte> data = new List<byte>();
            int tpos = 0;

            while (tpos < 594)
            {
                // check up to 4 bytes if 4 are the same add them, otherwhise check if 3 are same
                byte[] b = new byte[4] { 0xF4, 0xF4, 0xF4, 0xF4 }; // random values
                for (int i = 0; i < 4; i++)
                {
                    if (tpos + i >= 594)
                    {
                        break;
                    }
                    b[i] = tilemap[tpos + i];
                }


                if (b[0] == b[1] && b[0] == b[2] && b[0] == b[3]) // 4 bytes matching
                {
                    data.Add((byte)((3 << 6) | b[0]));
                    tpos += 4;
                }
                else if (b[0] == b[1] && b[0] == b[2]) // 3 bytes matching
                {
                    data.Add((byte)((2 << 6) | b[0]));
                    tpos += 3;
                }
                else if (b[0] == b[1]) // 2 bytes matching
                {
                    data.Add((byte)((1 << 6) | b[0]));
                    tpos += 2;
                }
                else
                {
                    data.Add(b[0]);
                    tpos += 1;
                }
            }
            return data.ToArray();


        }

        private void tileinfosCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            mapPicturebox.Refresh();
            tilesPicturebox.Refresh();
        }

        private void dotseatCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            dotseatTextbox.ReadOnly = dotseatCheckbox.Checked;
        }

        private void dotseatTextbox_TextChanged(object sender, EventArgs e)
        {
            if (!dotseatTextbox.ReadOnly) 
            {
                nbrDots = byte.Parse(dotseatTextbox.Text);
            }
        }
    }
}
