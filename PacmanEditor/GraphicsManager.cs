using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanEditor
{
    public static class GraphicsManager
    {

        public static byte[] NesTilesToPc8bppTiles(byte[] nesTiles, int nbrOfTiles, byte bpp = 2)
        {
            // [r0, bp1][r1, bp1][r2, bp1][r3, bp1][r4, bp1][r5, bp1][r6, bp1][r7, bp1]
            // [r0, bp2][r1, bp2][r2, bp2][r3, bp2][r4, bp2][r5, bp2][r6, bp2][r7, bp2]
            byte[] tiles8x8 = new byte[0x40 * nbrOfTiles];
            switch (bpp)
            {
                case 2:
                    for (int i = 0; i < nbrOfTiles; i++)
                    {
                        for (int line = 0; line < 8; line++)
                        {
                            byte[] pixelvalues = new byte[2]
                            {
                                nesTiles[(line) + (i * 0x10)],
                                nesTiles[(line) + 8 + (i * 0x10)]
                            };
                            for (int pixel = 7; pixel >= 0; pixel--)
                            {

                                tiles8x8[pixel + (line * 8) + (i * 0x40)] |= (byte)(pixelvalues[0] & 0x01);
                                tiles8x8[pixel + (line * 8) + (i * 0x40)] |= (byte)((pixelvalues[1] & 0x01) << 1);
                                pixelvalues[0] >>= 1;
                                pixelvalues[1] >>= 1;
                            }
                        }
                    }
                    break;

                case 1:
                    for (int i = 0; i < nbrOfTiles; i++)
                    {
                        for (int line = 0; line < 8; line++)
                        {
                            byte[] pixelvalues = new byte[1]
                            {
                                nesTiles[(line) + (i * 0x08)],
                            };
                            for (int pixel = 7; pixel >= 0; pixel--)
                            {
                                tiles8x8[pixel + (line * 8) + (i * 0x40)] |= (byte)(pixelvalues[0] & 0x01);
                                pixelvalues[0] >>= 1;
                            }
                        }
                    }
                    break;
            }
            return tiles8x8;
        }


    }
}
