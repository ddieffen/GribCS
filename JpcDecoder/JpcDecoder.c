/*
 * This file is part of JpcDecoder.
 * 
 * Copyright 2006-2010 Seaware AB, PO Box 1244, SE-131 28 Nacka Strand,
 * Sweden, info@seaware.se.
 *
*/


#include "stdafx.h"
#include "jasper/jasper.h"

#ifdef _MANAGED
#pragma managed(push, off)
#endif

/*BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
					 )
{
    return TRUE;
}*/
#define _SO_CALL __stdcall

int __declspec(dllexport) __stdcall dec_jpeg2000(char *injpc, int bufsize, int **outfld)
{
	int ier;
    int i,j,k;
    jas_image_t *image = 0;
    jas_stream_t *jpcstream;
    jas_image_cmpt_t *pcmpt;
    char *opts = 0;
    jas_matrix_t *data;

	jas_init();

    ier = 0;
//   
//     Create jas_stream_t containing input JPEG200 codestream in memory.
//       

    jpcstream = jas_stream_memopen(injpc, bufsize);

//   
//     Decode JPEG200 codestream into jas_image_t structure.
//       
    image = jpc_decode(jpcstream, opts);
    if ( image == 0 ) 
	{
       return -3;
    }
    
    pcmpt=image->cmpts_[0];

//   Expecting jpeg2000 image to be grayscale only.
//   No color components.
//
    if (image->numcmpts_ != 1 ) 
	{
       return (-5);
    }

// 
//    Create a data matrix of grayscale image values decoded from
//    the jpeg2000 codestream.
//
    data = jas_matrix_create(jas_image_height(image), jas_image_width(image));
    jas_image_readcmpt(image,0,0,0,jas_image_width(image),
                       jas_image_height(image),data);
//
//    Copy data matrix to output integer array.
//
    k = 0;
    for (i=0;i<pcmpt->height_;i++) 
	{
      for (j=0;j<pcmpt->width_;j++) 
	  {
        (*outfld)[k++]=data->rows_[i][j];
	  }
	}
//
//     Clean up JasPer work structures.
//
    jas_matrix_destroy(data);
    ier = jas_stream_close(jpcstream);
    jas_image_destroy(image);

    return 0;	
}


#ifdef _MANAGED
#pragma managed(pop)
#endif

