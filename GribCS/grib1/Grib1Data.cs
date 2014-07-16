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
using Seaware.GribCS;
using System.Runtime.InteropServices;

namespace Seaware.GribCS.Grib1
{
	
	/// <summary> A class used to extract data from a GRIB1 file. 
	/// see <a href="../../../IndexFormat.txt"> IndexFormat.txt</a>
	/// </summary>
    [GuidAttribute("127A0B38-C065-4fa7-AD64-E88233E292AF")]
    [ClassInterface(ClassInterfaceType.None)]
	public sealed class Grib1Data : Seaware.GribCS.Grib1.IGrib1Data
	{
		/* 
		*  used to hold open file descriptor
		*/
		//UPGRADE_TODO: Class 'java.io.RandomAccessFile' was converted to 'System.IO.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioRandomAccessFile'"
		private System.IO.FileStream raf = null;
		
		// *** constructors *******************************************************
		
		/// <summary> Constructs a Grib2Data object from a stream.
		/// 
		/// </summary>
		/// <param name="raf">ucar.unidata.io.RandomAccessFile with GRIB content.
		/// 
		/// </param>
		//UPGRADE_TODO: Class 'java.io.RandomAccessFile' was converted to 'System.IO.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioRandomAccessFile'"
		public Grib1Data(System.IO.FileStream raf)
		{
			this.raf = raf;
		}

        public Grib1Data()
        {
        }

        public void setFilename(string filename)
        {
            this.raf = new System.IO.FileStream(filename, System.IO.FileMode.Open, System.IO.FileAccess.Read);
        }

        public void closeFile()
        {
            this.raf.Close();
        }
		
		/// <summary> Reads the Grib data 
		/// 
		/// </summary>
		/// <param name="offset"> offset into file.
		/// </param>
		/// <param name="DecimalScale">
		/// </param>
		/// <param name="bmsExists">
		/// </param>
		/// <throws>  NotSupportedException </throws>
		/// <returns> float[]
		/// </returns>
		public float[] getData(long offset, int DecimalScale, bool bmsExists)
		{
			long start = (System.DateTime.Now.Ticks - 621355968000000000) / 10000;
			
			raf.Seek(offset, System.IO.SeekOrigin.Begin);
			//System.out.println( "raf.Position=" + raf.Position );
			
			// Need section 3 and 4 to read/interpet the data, section 5
			// as a check that all data read and sections are correct
			
			Grib1BitMapSection bms = null;
			if (bmsExists)
			// read Bit Mapped Section 3
				bms = new Grib1BitMapSection(raf);
			
			// read Binary Data Section 4
			Grib1BinaryDataSection bds = new Grib1BinaryDataSection(raf, DecimalScale, bms);
			
			return bds.Values;
		} // end getData
	} // end Grib1Data
}