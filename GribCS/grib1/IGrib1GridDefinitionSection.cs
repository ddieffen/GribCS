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
    [GuidAttribute("59E31DA1-833F-4f35-880F-FEE99A5EEFC2")]
    public interface IGrib1GridDefinitionSection
    {
        string CheckSum { get; }
        double Dx { get; }
        double Dy { get; }
        int Gdtn { get; }
        string getName();
        string getShapeName();
        int GridType { get; }
        bool IsThin { get; }
        double La1 { get; }
        double La2 { get; }
        double Lad { get; }
        double Latin { get; }
        double Latin1 { get; }
        double Latin2 { get; }
        double Lo1 { get; }
        double Lo2 { get; }
        double Lov { get; }
        double Np { get; }
        int Nx { get; }
        int Ny { get; }
        int ProjectionCenter { get; }
        int Resolution { get; }
        int ScanMode { get; }
        int Shape { get; }
        double SpLat { get; }
        double SpLon { get; }
    }
}
