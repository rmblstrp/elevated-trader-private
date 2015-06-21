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

#ifndef CLIENT_MESSAGE_SENDER_H_INCLUDED
#define CLIENT_MESSAGE_SENDER_H_INCLUDED

#include "PrimitiveTypes.h"
#include "DXPMessageData.h"

bool dx_subscribe_symbols_to_events (dxf_connection_t connection,
                                     dxf_const_string_t* symbols, int symbol_count, int event_types, bool unsubscribe,
                                     bool task_mode);

bool dx_send_record_description (dxf_connection_t connection, bool task_mode);
bool dx_send_protocol_description (dxf_connection_t connection, bool task_mode);
bool dx_send_heartbeat (dxf_connection_t connection, bool task_mode);

#endif /* CLIENT_MESSAGE_SENDER_H_INCLUDED */