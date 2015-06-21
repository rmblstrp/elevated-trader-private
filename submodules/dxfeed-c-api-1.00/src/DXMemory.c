/*
 * The contents of this file are subject to the Mozilla Public License Version
 * 1.1 (the "License"); you may not use this file except in compliance with
 * the License. You may obtain a copy of the License at
 * http://www.mozilla.org/MPL/
 *
 * Software distributed under the License is distributed on an "AS IS" basis,
 * WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License
 * for the specific language governing rights and limitations under the
 * License.
 *
 * The Initial Developer of the Original Code is Devexperts LLC.
 * Portions created by the Initial Developer are Copyright (C) 2010
 * the Initial Developer. All Rights Reserved.
 *
 * Contributor(s):
 *
 */

/*
 *	Implementation of the memory functions
 */

#include <Windows.h>

#include "DXMemory.h"
#include "DXErrorHandling.h"
#include "DXErrorCodes.h"
#include "Logger.h"

#include <string.h>
#include <stdlib.h>

/* -------------------------------------------------------------------------- */
/*
 *	Error related stuff
 */
/* -------------------------------------------------------------------------- */

void* dx_error_processor (void* src) {
    if (src == NULL) {
        dx_set_error_code(dx_mec_insufficient_memory);
    }

    return src;
}

/* -------------------------------------------------------------------------- */
/*
 *	Memory function wrappers implementation
 */
/* -------------------------------------------------------------------------- */

void* dx_malloc (int size) {
	void* r = malloc(size);
    return dx_error_processor(r);
}

/* -------------------------------------------------------------------------- */

void* dx_calloc (int num, int size) {
	void* r = calloc(num, size);
    return dx_error_processor(r);
}

/* -------------------------------------------------------------------------- */

void* dx_memcpy (void * destination, const void * source, int size) {
    
    return dx_error_processor(memcpy(destination, source, size));
}

/* -------------------------------------------------------------------------- */

void* dx_memmove (void * destination, const void * source, int size) {
    return dx_error_processor(memmove(destination, source, size));
}

/* -------------------------------------------------------------------------- */

void* dx_memset (void * destination, int c, int size) {
    return dx_error_processor(memset(destination, c, size));
}

/* -------------------------------------------------------------------------- */

void dx_free (void* buf) {
    free(buf);
}

/* -------------------------------------------------------------------------- */
/*
 *	Implementation of wrappers with no error handling mechanism enabled
 */
/* -------------------------------------------------------------------------- */

void* dx_calloc_no_ehm (int num, int size) {
	void *r = calloc(num, size);
    return r;
}

/* -------------------------------------------------------------------------- */

void dx_free_no_ehm (void* buf) {
    free(buf);
}