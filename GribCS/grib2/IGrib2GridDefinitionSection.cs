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
    [GuidAttribute("36CB9B2D-2D87-4e06-9D1F-3486B5469884")]
    public interface IGrib2GridDefinitionSection
    {
        float Altitude { get; }
        int Angle { get; }
        string CheckSum { get; }
        float Dstart { get; }
        float Dx { get; }
        float Dy { get; }
        float EarthRadius { get; }
        float Factor { get; }
        int Gdtn { get; }
        string getShapeName();
        int Iolon { get; }
        float J { get; }
        float K { get; }
        float La1 { get; }
        float La2 { get; }
        float Lad { get; }
        float Lap { get; }
        float Latin1 { get; }
        float Latin2 { get; }
        float Lo1 { get; }
        float Lo2 { get; }
        float Lop { get; }
        float Lov { get; }
        float M { get; }
        float MajorAxis { get; }
        int Method { get; }
        float MinorAxis { get; }
        int Mode { get; }
        int N { get; }
        int N2 { get; }
        int N3 { get; }
        string Name { get; }
        float Nb { get; }
        int Nd { get; }
        int Ni { get; }
        float Nr { get; }
        int NumberPoints { get; }
        int Nx { get; }
        int Ny { get; }
        int Olon { get; }
        int Order { get; }
        float PoleLat { get; }
        float PoleLon { get; }
        int Position { get; }
        int ProjectionCenter { get; }
        int Resolution { get; }
        float Rotationangle { get; }
        int ScanMode { get; }
        int Shape { get; }
        int Source { get; }
        float SpLat { get; }
        float SpLon { get; }
        int Subdivisionsangle { get; }
        float Xo { get; }
        float Xp { get; }
        float Yo { get; }
        float Yp { get; }
    }
}
