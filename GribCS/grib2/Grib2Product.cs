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

/// <summary> Grib2Product.java</summary>
/// <author>  Robb Kambic
/// </author>
using System;
using System.Runtime.InteropServices;

namespace Seaware.GribCS.Grib2
{
	
	/// <summary> Class which has all the necessary information about
	/// a record in a Grib2 File to extract the data.
	/// </summary>
    [GuidAttribute("50EE0598-9C02-4aa0-B622-C1A8B2FD37C7")]
    [ClassInterface(ClassInterfaceType.None)]
	public sealed class Grib2Product : Seaware.GribCS.Grib2.IGrib2Product
	{
		/// <summary> Discipline number for this record.</summary>
		/// <returns> discipline
		/// </returns>
		public int Discipline
		{
			get
			{
				return discipline;
			}
			
		}
		/// <summary> Reference time for this product.</summary>
		/// <returns> referenceTime
		/// </returns>
		public System.String ReferenceTime
		{
			get
			{
				return referenceTime;
			}
			
		}
		/// <summary> GDSkey is a MD5 checksum of the GDS for this record.</summary>
		/// <returns> gdsKey
		/// </returns>
		public System.String GDSkey
		{
			get
			{
				return gdsKey;
			}
			
		}
		/// <summary> Actual PDS of this record.</summary>
		/// <returns> pds
		/// </returns>
		public IGrib2ProductDefinitionSection PDS
		{
			get
			{
				return pds;
			}
			
		}
		/// <summary> ID of this record.</summary>
		/// <returns> id
		/// </returns>
		public IGrib2IdentificationSection ID
		{
			get
			{
				return id;
			}
			
		}
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'header '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.String header;
		//UPGRADE_NOTE: Final was removed from the declaration of 'discipline '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private int discipline;
		//UPGRADE_NOTE: Final was removed from the declaration of 'referenceTime '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.String referenceTime;
		private Grib2IdentificationSection id = null;
		//UPGRADE_NOTE: Final was removed from the declaration of 'gdsKey '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.String gdsKey;
		private Grib2ProductDefinitionSection pds = null;
		private long GdsOffset = - 1;
		private long PdsOffset = - 1;
		
		// --Commented out by Inspection START (11/16/05 2:14 PM):
		//   public Grib2Product(){
		//   }
		// --Commented out by Inspection STOP (11/16/05 2:14 PM)
		
		/// <summary> Constructor.</summary>
		/// <param name="header">
		/// </param>
		/// <param name="is">
		/// </param>
		/// <param name="id">
		/// </param>
		/// <param name="gdsKey">
		/// </param>
		/// <param name="pds">
		/// </param>
		/// <param name="GdsOffset">
		/// </param>
		/// <param name="PdsOffset">PDS offset in Grib file
		/// </param>
		public Grib2Product(System.String header, Grib2IndicatorSection is_Renamed, Grib2IdentificationSection id, System.String gdsKey, Grib2ProductDefinitionSection pds, long GdsOffset, long PdsOffset)
		{
			
			this.header = header;
			this.discipline = is_Renamed.Discipline;
			this.id = id;
			this.referenceTime = id.ReferenceTime;
			this.gdsKey = gdsKey;
			this.pds = pds;
			this.GdsOffset = GdsOffset;
			this.PdsOffset = PdsOffset;
		}
		
		// --Commented out by Inspection START (11/16/05 2:14 PM):
		//   public String getHeader(){
		//      return header;
		//   }
		// --Commented out by Inspection STOP (11/16/05 2:14 PM)
		
		/// <summary> Actual GDS offset in the Grib2 file.</summary>
		/// <returns> GdsOffset
		/// </returns>
		public long getGdsOffset()
		{
			return GdsOffset;
		}
		
		/// <summary> PDS offset in the file.</summary>
		/// <returns> PdsOffset
		/// </returns>
		public long getPdsOffset()
		{
			return PdsOffset;
		}
	}
}