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

namespace Seaware.GribCS.Grib1
{
	
	/// <summary> Grib1Record contains all the sections of a Grib record.</summary>
	/// <author>  Robb Kambic  11/13/03
	/// </author>
    [GuidAttribute("3A5F2356-D32E-4d5c-90B8-1646EA405079")]
    [ClassInterface(ClassInterfaceType.None)]
	public sealed class Grib1Record : Seaware.GribCS.Grib1.IGrib1Record
	{
		/// <summary>  Get header.</summary>
		/// <returns> header
		/// </returns>
		public System.String Header
		{
			get
			{
				return header;
			}
			
		}
		/// <summary>  Get Information record.</summary>
		/// <returns> an IS record
		/// </returns>
		public IGrib1IndicatorSection Is
		{
			get
			{
				return is_Renamed;
			}
			
		}
		/// <summary> Get Product Definition record.</summary>
		/// <returns> a PDS record
		/// </returns>
		public IGrib1ProductDefinitionSection PDS
		{
			get
			{
				return pds;
			}
			
		}
		/// <summary> Get Grid Definition record.</summary>
		/// <returns> a
		/// </returns>
		public IGrib1GridDefinitionSection GDS
		{
			get
			{
				return gds;
			}
			
		}
		/// <summary> Get offset to bms.</summary>
		/// <returns> long
		/// </returns>
		public long DataOffset
		{
			get
			{
				return dataOffset;
			}
			
			// --Commented out by Inspection START (11/9/05 10:25 AM):
			//   /**
			//    * Size to the end of the file
			//    * @return long
			//    */
			//   public long getEndRecordOffset()
			//   {
			//      return endRecordOffset;
			//   }
			// --Commented out by Inspection STOP (11/9/05 10:25 AM)
			
		}

        public long RecordOffset
        {
            get
            {
                return recordOffset;
            }
        }

		//UPGRADE_NOTE: Final was removed from the declaration of 'header '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.String header;
		
		/// <summary> The indicator section.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'is '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Grib1IndicatorSection is_Renamed;
		
		/// <summary> The product definition section.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'pds '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Grib1ProductDefinitionSection pds;
		
		/// <summary> The grid definition section.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'gds '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Grib1GridDefinitionSection gds;
		
		// --Commented out by Inspection START (11/9/05 10:25 AM):
		//   /**
		//    * Array with bytes
		//    */
		//   protected byte[] buf;
		// --Commented out by Inspection STOP (11/9/05 10:25 AM)

        private long recordOffset = -1; // The byte offset of this grib record (message) in the file from which it was read.
		
		private long dataOffset = - 1;
		private long endRecordOffset = - 1;
		
		/// <summary> Constructor.</summary>
		/// <param name="hdr">record header 
		/// </param>
		/// <param name="aIs">IS section
		/// </param>
		/// <param name="aPds">PDS section
		/// </param>
		/// <param name="aGds">GDS section
		/// </param>
		/// <param name="offset">to the BMS/BDS section of file
		/// </param>
		/// <param name="recOffset">to the EndOfRecord
		/// </param>
		public Grib1Record(System.String hdr, Grib1IndicatorSection aIs, Grib1ProductDefinitionSection aPds, Grib1GridDefinitionSection aGds, long offset, long recOffset)
		{
			header = hdr;
			is_Renamed = aIs;
			pds = aPds;
			gds = aGds;
			dataOffset = offset;
			endRecordOffset = recOffset;
		}

        public Grib1Record(System.String hdr, Grib1IndicatorSection aIs, Grib1ProductDefinitionSection aPds, Grib1GridDefinitionSection aGds, long offset, long recOffset, long startOfRecordOffset)
        {
            header = hdr;
            is_Renamed = aIs;
            pds = aPds;
            gds = aGds;
            dataOffset = offset;
            endRecordOffset = recOffset;
            recordOffset = startOfRecordOffset;
        }
		
		// --Commented out by Inspection START (11/9/05 10:25 AM):
		//   /**
		//    * Get buffer with bds and bms
		//    * @return buf
		//    */
		//   public byte[] getBuf()
		//   {
		//      return buf;
		//   }
		// --Commented out by Inspection STOP (11/9/05 10:25 AM)
	} // end Grib1Record
}