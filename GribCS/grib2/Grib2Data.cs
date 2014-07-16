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
using System.Runtime.InteropServices;

namespace Seaware.GribCS.Grib2
{
	
	/// <summary> A class used to extract data from a Grib2 file.
	/// see <a href="../../../IndexFormat.txt"> IndexFormat.txt</a>
	/// </summary>
    [GuidAttribute("862A3829-24B9-4aab-83F0-D08D061A9B80")]
    [ClassInterface(ClassInterfaceType.None)]
	public sealed class Grib2Data : Seaware.GribCS.Grib2.IGrib2Data
	{
		/* 
		*  used to hold open file descriptor
		*/
		//UPGRADE_TODO: Class 'java.io.RandomAccessFile' was converted to 'System.IO.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioRandomAccessFile'"
		private System.IO.FileStream raf = null;
		
		// *** constructors *******************************************************
		/// <summary> Constructs a  Grib2Data object for a RandomAccessFile.
		/// 
		/// </summary>
		/// <param name="raf">ucar.unidata.io.RandomAccessFile with GRIB content
		/// </param>
		/// <throws>  NoValidGribException </throws>
		//UPGRADE_TODO: Class 'java.io.RandomAccessFile' was converted to 'System.IO.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioRandomAccessFile'"
		public Grib2Data(System.IO.FileStream raf)
		{
			this.raf = raf;
		}

        public Grib2Data()
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
		
		/// <summary> Reads the Grib data with a certain offsets in the file.
		/// 
		/// </summary>
		/// <param name="GdsOffset">
		/// </param>
		/// <param name="PdsOffset">
		/// </param>
		/// <throws>  IOException  if raf does not contain a valid GRIB record. </throws>
		/// <returns> float[]
		/// </returns>
		public float[] getData(long GdsOffset, long PdsOffset)
		{
            if (raf == null)
            {
                throw new ApplicationException("Grib2Input.scan called without file");
            }

			long start = (System.DateTime.Now.Ticks - 621355968000000000) / 10000;
			
			raf.Seek(GdsOffset, System.IO.SeekOrigin.Begin);
			
			// Need section 3, 4, 5, 6, and 7 to read/interpet the data
			Grib2GridDefinitionSection gds = new Grib2GridDefinitionSection(raf, false); // Section 3 no checksum
			
			raf.Seek(PdsOffset, System.IO.SeekOrigin.Begin); // could have more than one pds for a gds
			Grib2ProductDefinitionSection pds = new Grib2ProductDefinitionSection(raf); // Section 4
			
			Grib2DataRepresentationSection drs = new Grib2DataRepresentationSection(raf); // Section 5
			
			Grib2BitMapSection bms = new Grib2BitMapSection(raf, gds); // Section 6
			
			Grib2DataSection ds = new Grib2DataSection(true, raf, gds, drs, bms); // Section 7
			//System.out.println("DS offset=" + ds.getOffset() );
			
			return ds.Data;
		} // end getData
	} // end Grib2Data
}