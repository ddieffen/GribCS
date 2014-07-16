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

/// <summary> Grib2Record.java.</summary>
/// <author>  Robb Kambic
/// </author>
using System;
using System.Runtime.InteropServices;

namespace Seaware.GribCS.Grib2
{
	
	/// <summary> Class which represents a record in a Grib2File.</summary>
    [GuidAttribute("DE926262-13F0-47ca-90D0-6B175CA6F98C")]
    [ClassInterface(ClassInterfaceType.None)]
	public sealed class Grib2Record : Seaware.GribCS.Grib2.IGrib2Record
	{
		/// <summary> returns Header of Grib record.</summary>
		/// <returns> header
		/// </returns>
		public System.String Header
		{
			get
			{
				return header;
			}
			
		}
		/// <summary> returns Inofrmation Section of record.</summary>
		/// <returns> is
		/// </returns>
		public IGrib2IndicatorSection Is
		{
			get
			{
				return is_Renamed;
			}
			
		}
		/// <summary> returns IdentificationSection.</summary>
		/// <returns> IdentificationSection
		/// </returns>
		public IGrib2IdentificationSection ID
		{
			get
			{
				return id;
			}
			
		}
		/// <summary> returns GDS of record.</summary>
		/// <returns> gds
		/// </returns>
		public IGrib2GridDefinitionSection GDS
		{
			get
			{
				return gds;
			}
			
		}
		/// <summary> returns PDS.</summary>
		/// <returns> pds
		/// </returns>
		public IGrib2ProductDefinitionSection PDS
		{
			get
			{
				return pds;
			}
			
		}
		/// <summary> returns Data Representation Section.</summary>
		/// <returns> DataRepresentationSection
		/// </returns>
		public IGrib2DataRepresentationSection DRS
		{
			get
			{
				return drs;
			}
			
		}
        /// <summary> returns Local Use Section.</summary>
        /// <returns> DataRepresentationSection
        /// </returns>
        public IGrib2LocalUseSection LUS
        {
            get
            {
                return lus;
            }

        }
		
		/// <summary> Grib record header.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'header '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.String header;
		/// <summary> Grib2IndicatorSection object.</summary>
		private Grib2IndicatorSection is_Renamed = null;
		/// <summary> Grib2IdentificationSection object.</summary>
		private Grib2IdentificationSection id = null;
		/// <summary> Grib2GridDefinitionSection object.</summary>
		private Grib2GridDefinitionSection gds = null;
		/// <summary> Grib2ProductDefinitionSection object.</summary>
		private Grib2ProductDefinitionSection pds = null;
		/// <summary> Grib2DataRepresentationSection object.</summary>
		private Grib2DataRepresentationSection drs = null;
        /// <summary> Grib2LocalUseSection object.</summary>
        private Grib2LocalUseSection lus = null;
		// --Commented out by Inspection START (12/8/05 1:27 PM):
		//   /**
		//    * Grib2BitMapSection object.
		//    */
		//   private Grib2BitMapSection bms = null;
		// --Commented out by Inspection STOP (12/8/05 1:27 PM)
		// --Commented out by Inspection START (12/8/05 1:26 PM):
		//   /**
		//    * Grib2DataSection object.
		//    */
		//   private Grib2DataSection ds = null;
		// --Commented out by Inspection STOP (12/8/05 1:26 PM)
		/// <summary> GdsOffset in file.</summary>
		private long GdsOffset = - 1;
		/// <summary> PdsOffset in file.</summary>
		private long PdsOffset = - 1;
		
		/// <summary> Construction for Grib2Record.</summary>
		/// <param name="header">
		/// </param>
		/// <param name="is">
		/// </param>
		/// <param name="id">
		/// </param>
		/// <param name="gds">
		/// </param>
		/// <param name="pds">
		/// </param>
		/// <param name="drs">
		/// </param>
		/// <param name="bms">
		/// </param>
		/// <param name="GdsOffset">
		/// </param>
		/// <param name="PdsOffset">PDS offset in Grib file
		/// </param>
		public Grib2Record(System.String header, Grib2IndicatorSection is_Renamed, 
               Grib2IdentificationSection id, Grib2GridDefinitionSection gds, 
               Grib2ProductDefinitionSection pds, Grib2DataRepresentationSection drs, 
               Grib2BitMapSection bms, long GdsOffset, long PdsOffset,
               Grib2LocalUseSection lus)
		{
			
			this.header = header;
			this.is_Renamed = is_Renamed;
			this.id = id;
			this.gds = gds;
			this.pds = pds;
			this.drs = drs;
			//this.bms = bms;
			this.GdsOffset = GdsOffset;
			this.PdsOffset = PdsOffset;
            this.lus = lus;
		}
		
		/// <summary> returns GDS offset in file.</summary>
		/// <returns> GdsOffset
		/// </returns>
		public long getGdsOffset()
		{
			return GdsOffset;
		}
		
		/// <summary> returns Pds Offset.</summary>
		/// <returns> PdsOffset
		/// </returns>
		public long getPdsOffset()
		{
			return PdsOffset;
		}
		
		// --Commented out by Inspection START (12/8/05 1:26 PM):
		//    /**
		//     * returns BitMapSection.
		//     * @return BitMapSection
		//     */
		//   public final Grib2BitMapSection getBMS(){
		//      return bms;
		//   }
		// --Commented out by Inspection STOP (12/8/05 1:26 PM)
		
		// --Commented out by Inspection START (12/8/05 1:26 PM):
		//    /**
		//     * returns DataSection.
		//     * @return DataSection
		//     */
		//   public final Grib2DataSection getDS(){
		//      return ds;
		//   }
		// --Commented out by Inspection STOP (12/8/05 1:26 PM)
	}
}