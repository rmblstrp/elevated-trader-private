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
 *	Here we have what's related to the records we receive from the server.
 */
 
#ifndef RECORD_DATA_H_INCLUDED
#define RECORD_DATA_H_INCLUDED

#include "DXTypes.h"

/* -------------------------------------------------------------------------- */
/*
 *	Record type constants
 */
/* -------------------------------------------------------------------------- */

typedef enum {
    dx_rid_begin,
    dx_rid_trade = dx_rid_begin,
    dx_rid_quote,
    dx_rid_fundamental,
    dx_rid_profile,
    dx_rid_market_maker,
    dx_rid_time_and_sale,

    /* add new values above this line */

    dx_rid_count,
    dx_rid_invalid
} dx_record_id_t;

/* -------------------------------------------------------------------------- */
/*
 *	Record structures
 */
/* -------------------------------------------------------------------------- */

typedef struct {
    dxf_long_t time;
    dxf_char_t exchange_code;
    dxf_double_t price;
    dxf_long_t size;
    dxf_double_t day_volume;	
} dx_trade_t;

typedef struct {
    dxf_long_t bid_time;
    dxf_char_t bid_exchange_code;
    dxf_double_t bid_price;
    dxf_long_t bid_size;
    dxf_long_t ask_time;
    dxf_char_t ask_exchange_code;
    dxf_double_t ask_price;
    dxf_long_t ask_size;
} dx_quote_t;

typedef struct {
    dxf_double_t day_high_price;
    dxf_double_t day_low_price;
    dxf_double_t day_open_price;
    dxf_double_t prev_day_close_price;
    dxf_long_t open_interest;
} dx_fundamental_t;

typedef struct {
    dxf_const_string_t description;
} dx_profile_t;

typedef struct {
    dxf_char_t mm_exchange;
    dxf_int_t mm_id;
    dxf_double_t mmbid_price;
    dxf_int_t mmbid_size;
    dxf_double_t mmask_price;
    dxf_int_t mmask_size;
} dx_market_maker_t;

typedef struct {
    dxf_long_t event_id;
    dxf_long_t time;
    dxf_char_t exchange_code;
    dxf_double_t price;
    dxf_long_t size;
    dxf_double_t bid_price;
    dxf_double_t ask_price;
    dxf_const_string_t exchange_sale_conditions;
    dxf_bool_t is_trade;
    dxf_int_t type;    
} dx_time_and_sale_t;

#endif /* RECORD_DATA_H_INCLUDED */