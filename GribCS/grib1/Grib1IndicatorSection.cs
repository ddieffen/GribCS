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
	
	/// <summary> A class that represents the IndicatorSection of a GRIB record.
	/// 
	/// </summary>
	/// <author>   Robb Kambic
	/// </author>
	/// <version>  1.0
	/// 
	/// </version>
    [GuidAttribute("F8681AB0-EA2B-4c20-AA23-A27C908117A8")]
    [ClassInterface(ClassInterfaceType.None)]
	public sealed class Grib1IndicatorSection : Seaware.GribCS.Grib1.IGrib1IndicatorSection
	{
		/// <summary> Get the byte length of this GRIB record.
		/// 
		/// </summary>
		/// <returns> length in bytes of GRIB record
		/// </returns>
		public long GribLength
		{
			get
			{
				return gribLength;
			}
			
		}
		/// <summary> Get the byte length of the IndicatorSection0 section.
		/// 
		/// </summary>
		/// <returns> length in bytes of IndicatorSection0 section
		/// </returns>
		public int Length
		{
			get
			{
				return length;
			}
			
		}
		/// <summary> Get the edition of the GRIB specification used.
		/// 
		/// </summary>
		/// <returns> edition number of GRIB specification
		/// </returns>
		public int GribEdition
		{
			get
			{
				return edition;
			}
			
		}
		/// <summary> Length in bytes of GRIB record.</summary>
		private long gribLength;
		
		/// <summary> Length in bytes of IndicatorSection.
		/// Section length differs between GRIB editions 1 and 2
		/// Currently only GRIB edition 1 supported - length is 16 octets/bytes.
		/// </summary>
		private int length;
		
		/// <summary> Discipline - GRIB Master Table Number.</summary>
		private int discipline;
		
		/// <summary> Edition of GRIB specification used.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'edition '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private int edition;
		
		// *** constructors *******************************************************
		
		/// <summary> Constructs a <tt>Grib1IndicatorSection</tt> object from a byteBuffer.
		/// 
		/// </summary>
		/// <param name="raf">RandomAccessFile with IndicatorSection content
		/// 
		/// </param>
		/// <throws>  NotSupportedException  if raf contains no valid GRIB file </throws>
		/// <throws>  IOException </throws>
		//UPGRADE_TODO: Class 'java.io.RandomAccessFile' was converted to 'System.IO.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioRandomAccessFile'"
		public Grib1IndicatorSection(System.IO.FileStream raf)
		{
            long mark = raf.Position;
			//if Grib edition 1, get bytes for the gribLength
			int[] data = new int[3];
			for (int i = 0; i < 3; i++)
			{
				data[i] = raf.ReadByte();
			}
			//System.out.println( "data[]=" + data[0] +", "+ data[1]+", "+data[2] ) ;
			
			// edition of GRIB specification
			edition = raf.ReadByte();
			//System.out.println( "edition=" + edition ) ;
			if (edition == 1)
			{
				// length of GRIB record
                // Reset to beginning, then read 3 bytes
                raf.Position = mark;
                gribLength = (long)GribNumbers.uint3(raf);
                // Skip next byte, edition already read
                raf.ReadByte();
				//System.out.println( "edition 1 gribLength=" + gribLength ) ;
				length = 8;
			}
			else if (edition == 2)
			{
				// length of GRIB record
				discipline = data[2];
				//System.out.println( "discipline=" + discipline) ;
                gribLength = GribNumbers.int8(raf);
				//System.out.println( "editon 2 gribLength=" + gribLength) ;
				length = 16;
			}
			else
			{
				throw new NotSupportedException("GRIB edition " + edition + " is not yet supported");
			}
		} // end Grib1IndicatorSection
		
		// --Commented out by Inspection START (12/5/05 3:52 PM):
		//   /**
		//    * Discipline - GRIB Master Table Number.
		//    * @return discipline as a number
		//    */
		//   public final int getDiscipline()
		//   {
		//      return discipline;
		//   }
		// --Commented out by Inspection STOP (12/5/05 3:52 PM)
		
		// --Commented out by Inspection START (12/5/05 3:52 PM):
		//   /**
		//    * Discipline - GRIB Master Table Name.
		//    * @return return Discipline Name as String
		//    */
		//   public final String getDisciplineName()
		//   {
		//      switch( discipline ) {
		//
		//         case 0: return "Meteorological products" ;
		//         case 1: return "Hydrological products";
		//         case 2: return "Land surface products";
		//         case 3: return "Space products";
		//         case 10: return "Oceanographic products";
		//         default: return "Unknown";
		//      }
		//
		//   }
		// --Commented out by Inspection STOP (12/5/05 3:52 PM)
	}
}