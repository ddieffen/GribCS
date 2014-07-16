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
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using Seaware.GribCS;

namespace Seaware.GribCS.Grib2
{
    [GuidAttribute("99241B81-4D14-4a71-B35F-2A3AAE62876D")]
    [ClassInterface(ClassInterfaceType.None)]
    public class Grib2LocalUseAdapter : IGrib2LocalUseAdapter
    {
        private MemoryStream _memStream;


        #region IGrib2LocalUseAdapter Members

        public void Connect(IGrib2LocalUseSection source)
        {
            if (source == null)
            {
                throw new ArgumentNullException();
            }
            _memStream = new MemoryStream(source.getBytes());
        }

        public string ReadString(int startByte, int byteCount, Encoding encoding)
        {
            byte[] bytes = ReadBytes(startByte, byteCount);
            string str = encoding.GetString(bytes);
            return str;
        }

        public int ReadInt32(int startByte)
        {
            // Use GribNumbers to handle endian byte swapping
            return GribNumbers.int4(_memStream);
        }

        public byte[] ReadBytes(int startByte, int byteCount)
        {
            byte[] bytes = new byte[byteCount];
            int n = _memStream.Read(bytes, startByte, byteCount);
            if (n != byteCount)
            {
                throw new ApplicationException("Could not read all bytes");
            }
            return bytes;
        }

        #endregion
    }
}
