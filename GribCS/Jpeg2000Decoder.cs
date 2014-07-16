/*
 * This file is part of GribCS.
 * This code is based on an automatic conversion of JGRIB Beta 7 
 * (http://jgrib.sourceforge.net/) from Java to C#.
 * 
 * C# code: Copyright 2006-2010 Seaware AB, PO Box 1244, SE-131 28 
 * Nacka Strand, Sweden, info@seaware.se.
 * 
 * Java-code: Copyright 1997-2006 Unidata Program Center/University 
 * Corporation for Atmospheric Research, P.O. Box 3000, Boulder, CO 80307,
 * support@unidata.ucar.edu.
 * 
 * GribCS is free software: you can redistribute it and/or modify it under 
 * the terms of the GNU Lesser General Public License as published by the 
 * Free Software Foundation, either version 3 of the License, or (at your 
 * option) any later version.
 * 
 * GribCS is distributed in the hope that it will be useful, but 
 * WITHOUT ANY WARRANTY; without even the implied warranty of 
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU Lesser
 * General Public License for more details.
 * 
 * You should have received a copy of the GNU Lesser General Public License 
 * along with GribCS.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;


namespace Seaware.GribCS
{
    [ComVisible(false)]
    class Jpeg2000Decoder
    {
        public class JpcDecoder
        {
            [DllImport("jpcdecoder.dll")]
            public static extern int dec_jpeg2000(byte[] injpc, int bufsize, ref IntPtr outfld);
        }

        private int[] _decodedData;

        public int[] Decode(byte[] streamBuf, int dataCount)
        {
            _decodedData = new int[dataCount];
            IntPtr dataBuf = Marshal.AllocCoTaskMem(Marshal.SizeOf(dataCount) * dataCount);
            int res = JpcDecoder.dec_jpeg2000(streamBuf, streamBuf.Length, ref dataBuf);
            Marshal.Copy(dataBuf, _decodedData, 0, dataCount);
            Marshal.FreeCoTaskMem(dataBuf);

            if( res != 0 )
            {
                _decodedData = null;
                // TODO Error handling
            }
            return _decodedData;
        }

    }
}
