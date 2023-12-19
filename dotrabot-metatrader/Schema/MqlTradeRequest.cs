using System;

namespace Dotrabot.Metatrader.Schema
{
    public class TradeRequest
    {
        TradeRequestActions action;           // Trade operation type
        ulong magic;            // Expert Advisor ID (magic number)
        ulong order;            // Order ticket
        String symbol;           // Trade symbol
        double volume;           // Requested volume for a deal in lots
        double price;            // Price
        double stoplimit;        // StopLimit level of the order
        double sl;               // Stop Loss level of the order
        double tp;               // Take Profit level of the order
        ulong deviation;        // Maximal possible deviation from the requested price
        ENUM_ORDER_TYPE type;             // Order type
        ENUM_ORDER_TYPE_FILLING type_filling;     // Order execution type
        ENUM_ORDER_TYPE_TIME type_time;        // Order expiration type
        datetime expiration;       // Order expiration time (for the orders of ORDER_TIME_SPECIFIED type)
        String comment;          // Order comment
        ulong position;         // Position ticket
        ulong
    }
}