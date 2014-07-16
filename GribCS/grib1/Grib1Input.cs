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
	
	/// <summary> A class that scans a GRIB file to extract product information. </summary>
    [GuidAttribute("0FB284A1-77DB-45f6-8F36-2E0300D7E3CF")]
    [ClassInterface(ClassInterfaceType.None)]
	public sealed class Grib1Input : Seaware.GribCS.Grib1.IGrib1Input
	{
		/// <summary> Grib edition number 1, 2 or 0 not a Grib file.</summary>
		/// <throws>  NotSupportedException </throws>
		/// <returns> int 0 not a Grib file, 1 Grib1, 2 Grib2
		/// </returns>
		public int Edition
		{
			get
			{
				long length = (raf.Length < 4000L)?raf.Length:4000L;
				if (!seekHeader(raf, length))
				{
					return 0; // not valid Grib file
				}
				//  Read Section 0 Indicator Section to get Edition number
				Grib1IndicatorSection is_Renamed = new Grib1IndicatorSection(raf); // section 0
				return is_Renamed.GribEdition;
			}
			// end getEdition
			
		}
		/// <summary> Get products of the GRIB file.
		/// 
		/// </summary>
		/// <returns> products 
		/// </returns>
		public System.Collections.ArrayList Products
		{
			get
			{
				return products;
			}
			
		}
		/// <summary> Get records of the GRIB file.
		/// 
		/// </summary>
		/// <returns> records 
		/// </returns>
		public System.Collections.ArrayList Records
		{
			get
			{
				return records;
			}
			
		}
		/// <summary> Get GDS's of the GRIB file.
		/// 
		/// </summary>
		/// <returns> gdsHM 
		/// </returns>
		//UPGRADE_TODO: Class 'java.util.HashMap' was converted to 'System.Collections.Hashtable' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilHashMap'"
		public System.Collections.Hashtable GDSs
		{
			get
			{
				return gdsHM;
			}
			
		}

        public int ProductsCount
        {
            get
            {
                return products.Count;
            }
        }
        public int RecordsCount
        {
            get
            {
                return records.Count;
            }
        }

		/*
		* raf for grib file
		*/
		private System.IO.FileStream raf = null;
		
		/*
		* the header of Grib record
		*/
		private System.String header = "GRIB";
		
		/*
		* stores records of Grib file, records consist of objects for each section.
		* there are 5 sections to a Grib1 record.
		*/
		//UPGRADE_NOTE: Final was removed from the declaration of 'records '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.Collections.ArrayList records = new System.Collections.ArrayList();
		
		/*
		* stores products of Grib file, products have enough info to get the
		* metadata about a parameter and the data. products are lightweight
		* records. 
		*/
		//UPGRADE_NOTE: Final was removed from the declaration of 'products '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.Collections.ArrayList products = new System.Collections.ArrayList();
		
		/*
		* stores GDS of Grib1 file, there is possibility of more than 1
		*/
		//UPGRADE_NOTE: Final was removed from the declaration of 'gdsHM '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_TODO: Class 'java.util.HashMap' was converted to 'System.Collections.Hashtable' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilHashMap'"
		private System.Collections.Hashtable gdsHM = new System.Collections.Hashtable();
		
		// *** constructors *******************************************************
		
		/// <summary> Constructs a <tt>Grib1Input</tt> object from a raf.
		/// 
		/// </summary>
		/// <param name="raf">with GRIB content
		/// 
		/// </param>
		//UPGRADE_TODO: Class 'java.io.RandomAccessFile' was converted to 'System.IO.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioRandomAccessFile'"
		public Grib1Input(System.IO.FileStream raf)
		{
			this.raf = raf;
		}

        public Grib1Input()
        {
        }
		
		/// <summary> scans a Grib file to gather information that could be used to
		/// create an index or dump the metadata contents.
		/// 
		/// </summary>
		/// <param name="getProducts">products have enough information for data extractions
		/// </param>
		/// <param name="oneRecord">returns after processing one record in the Grib file
		/// </param>
		/// <throws>  NotSupportedException </throws>
		public void  scan(bool getProducts, bool oneRecord)
		{
			long start = (System.DateTime.Now.Ticks - 621355968000000000) / 10000;
			// stores the number of times a particular GDS is used
			//UPGRADE_TODO: Class 'java.util.HashMap' was converted to 'System.Collections.Hashtable' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilHashMap'"
			System.Collections.Hashtable gdsCounter = new System.Collections.Hashtable();
			Grib1ProductDefinitionSection pds = null;
			Grib1GridDefinitionSection gds = null;
            long startOffset = -1;
			
			//System.out.println("file position =" + raf.Position); 
			while (raf.Position < raf.Length)
			{
                if (seekHeader(raf, raf.Length, out startOffset))
				{
					// Read Section 0 Indicator Section
					Grib1IndicatorSection is_Renamed = new Grib1IndicatorSection(raf);
					//System.out.println( "Grib record length=" + is.getGribLength());
					// EOR (EndOfRecord) calculated so skipping data sections is faster
					long EOR = raf.Position + is_Renamed.GribLength - is_Renamed.Length;
					
					// Read Section 1 Product Definition Section PDS
					pds = new Grib1ProductDefinitionSection(raf);
					if (pds.LengthErr)
						continue;
					
					if (pds.gdsExists())
					{
						// Read Section 2 Grid Definition Section GDS
						gds = new Grib1GridDefinitionSection(raf);
					}
					else
					{
						// GDS doesn't exist so make one
						//System.out.println("GribRecord: No GDS included.");
						//System.out.println("Process ID:" + pds.getProcess_Id() );
						//System.out.println("Grid ID:" + pds.getGrid_Id() );
						gds = (Grib1GridDefinitionSection) new Grib1Grid(pds);
					}
					// obtain BMS or BDS offset in the file for this product
					long dataOffset = 0;
					if (pds.Center == 98)
					{
						// check for ecmwf offset by 1 bug
						int length = (int)GribNumbers.uint3(raf); // should be length of BMS
						if ((length + raf.Position) < EOR)
						{
							dataOffset = raf.Position - 3; // ok
						}
						else
						{
							dataOffset = raf.Position - 2;
						}
					}
					else
					{
						dataOffset = raf.Position;
					}
					// position filePointer to EndOfRecord
					raf.Seek(EOR, System.IO.SeekOrigin.Begin);
					//System.out.println("file offset = " + raf.Position);
					
					// assume scan ok
					if (getProducts)
					{
						Grib1Product gp = new Grib1Product(header, pds, getGDSkey(gds, gdsCounter), dataOffset, raf.Position);
						products.Add(gp);
					}
					else
					{
                        Grib1Record gr = new Grib1Record(header, is_Renamed, pds, gds, dataOffset, raf.Position, startOffset);
						records.Add(gr);
					}
					if (oneRecord)
						return ;
					
					// early return because ending "7777" missing
					if (raf.Position > raf.Length)
					{
						raf.Seek(0, System.IO.SeekOrigin.Begin);
						System.Console.Error.WriteLine("Grib1Input: possible file corruption");
						checkGDSkeys(gds, gdsCounter);
						return ;
					}
				} // end if seekHeader
				//System.out.println( "raf.Position=" + raf.Position);
				//System.out.println( "raf.Length=" + raf.Length );
			} // end while raf.Position < raf.Length
			//System.out.println("GribInput: processed in " +
			//   (System.currentTimeMillis()- start) + " milliseconds");
			checkGDSkeys(gds, gdsCounter);
			return ;
		} // end scan
		
		//UPGRADE_TODO: Class 'java.io.RandomAccessFile' was converted to 'System.IO.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioRandomAccessFile'"
		private bool seekHeader(System.IO.FileStream raf, long stop, out long startOffset)
		{
			// seek header
			System.Text.StringBuilder hdr = new System.Text.StringBuilder();
			int match = 0;
            startOffset = -1;
			
			while (raf.Position < stop)
			{
				// code must be "G" "R" "I" "B"
				char c = (char) raf.ReadByte();
				
				hdr.Append((char) c);
				if (c == 'G')
				{
					match = 1;
                    startOffset = raf.Position - 1;
				}
				else if ((c == 'R') && (match == 1))
				{
					match = 2;
				}
				else if ((c == 'I') && (match == 2))
				{
					match = 3;
				}
				else if ((c == 'B') && (match == 3))
				{
					return true;
				}
				else
				{
					match = 0; /* Needed to protect against "GaRaIaB" case. */
				}
			}
			return false;
		} // end seekHeader

        private bool seekHeader(System.IO.FileStream raf, long stop)
        {
            long startOffsetDummy = -1;
            return seekHeader(raf, stop, out startOffsetDummy);
        }
		
		//UPGRADE_TODO: Class 'java.util.HashMap' was converted to 'System.Collections.Hashtable' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilHashMap'"
		private System.String getGDSkey(Grib1GridDefinitionSection gds, System.Collections.Hashtable gdsCounter)
		{
			
			System.String key = gds.CheckSum;
			// only Lat/Lon grids can have > 1 GDSs
			if (gds.GridType == 0 || gds.GridType == 4)
			{
				if (!gdsHM.ContainsKey(key))
				{
					// check if gds is already saved
					gdsHM[key] = gds;
				}
			}
			else if (!gdsHM.ContainsKey(key))
			{
				// check if gds is already saved
				gdsHM[key] = gds;
				gdsCounter[key] = "1";
			}
			else
			{
				// increment the counter for this GDS
				//UPGRADE_TODO: Method 'java.util.HashMap.get' was converted to 'System.Collections.Hashtable.Item' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilHashMapget_javalangObject'"
				int count = System.Int32.Parse((System.String) gdsCounter[key]);
				gdsCounter[key] = System.Convert.ToString(++count);
			}
			return key;
		} // end getGDSkey
		
		//UPGRADE_TODO: Class 'java.util.HashMap' was converted to 'System.Collections.Hashtable' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilHashMap'"
		private void  checkGDSkeys(Grib1GridDefinitionSection gds, System.Collections.Hashtable gdsCounter)
		{
			
			// lat/lon grids can have > 1 GDSs
			if (gds.GridType == 0 || gds.GridType == 4)
			{
				return ;
			}
			System.String bestKey = "";
			int count = 0;
			// find bestKey with the most counts
			//UPGRADE_TODO: Method 'java.util.HashMap.keySet' was converted to 'SupportClass.HashSetSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilHashMapkeySet'"
			//UPGRADE_TODO: Method 'java.util.Iterator.hasNext' was converted to 'System.Collections.IEnumerator.MoveNext' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilIteratorhasNext'"
			for (System.Collections.IEnumerator it = new SupportClass.HashSetSupport(gdsCounter.Keys).GetEnumerator(); it.MoveNext(); )
			{
				//UPGRADE_TODO: Method 'java.util.Iterator.next' was converted to 'System.Collections.IEnumerator.Current' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilIteratornext'"
				System.String key = (System.String) it.Current;
				//UPGRADE_TODO: Method 'java.util.HashMap.get' was converted to 'System.Collections.Hashtable.Item' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilHashMapget_javalangObject'"
				int gdsCount = System.Int32.Parse((System.String) gdsCounter[key]);
				if (gdsCount > count)
				{
					count = gdsCount;
					bestKey = key;
				}
			}
			// remove best key from gdsCounter, others will be removed from gdsHM
			gdsCounter.Remove(bestKey);
			// remove all GDSs using the gdsCounter   
			//UPGRADE_TODO: Method 'java.util.HashMap.keySet' was converted to 'SupportClass.HashSetSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilHashMapkeySet'"
			//UPGRADE_TODO: Method 'java.util.Iterator.hasNext' was converted to 'System.Collections.IEnumerator.MoveNext' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilIteratorhasNext'"
			for (System.Collections.IEnumerator it = new SupportClass.HashSetSupport(gdsCounter.Keys).GetEnumerator(); it.MoveNext(); )
			{
				//UPGRADE_TODO: Method 'java.util.Iterator.next' was converted to 'System.Collections.IEnumerator.Current' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilIteratornext'"
				System.String key = (System.String) it.Current;
				gdsHM.Remove(key);
			}
			// reset GDS keys in products too
			for (int i = 0; i < products.Count; i++)
			{
				Grib1Product g1p = (Grib1Product) products[i];
				g1p.GDSkey = bestKey;
			}
			return ;
		} // end checkGDSkeys

        public IGrib1Record GetRecord(int idx)
        {
            if (idx < 0 || idx >= records.Count)
            {
                throw new IndexOutOfRangeException();
            }
            return records[idx] as Grib1Record;
        }

        public IGrib1Product GetProduct(int idx)
        {
            if (idx < 0 || idx >= products.Count)
            {
                throw new IndexOutOfRangeException();
            }
            return products[idx] as Grib1Product;
        }

        public void setFilename(string filename)
        {
            this.raf = new System.IO.FileStream(filename, System.IO.FileMode.Open, System.IO.FileAccess.Read);
        }

        public void closeFile()
        {
            this.raf.Close();
        }

        public float MissingValue 
        {
            get
            {
                return Grib1BinaryDataSection.MissingValue;
            }
        }

	} // end Grib1Input
}