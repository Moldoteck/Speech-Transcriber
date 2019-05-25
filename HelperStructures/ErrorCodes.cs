/**************************************************************************
 *                                                                        *
 *  File:        ErrorCodes.cs                                            *
 *  Copyright:   (c) 2019, Cristian Pădureac                              *
 *  Description: ErrorCodes object is responsible with providing          *
 *               easy managing of possible error types in                 *
 *               implemented solution                                     *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/

namespace Speech_Transcriber
{
    public enum ErrorCode { SUCCESS, NULL_ARGUMENT, ACCESS_DENIED, ALREADY_CONNECTED, ALREADY_STARTED, CONNECTION_ABORTED, CONNECTION_REFUSED, EXTERNAL_COMPONENT_ERROR, INVALID_ARGUMENT, NETWORK_UNREACHABLE, NOT_CONNECTED, OPERATION_ABORTED, OPERATION_NOT_SUPPORTED, STATE_INVALID, TIMED_OUT };
}