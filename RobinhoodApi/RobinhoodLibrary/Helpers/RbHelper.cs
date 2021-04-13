﻿using RobinhoodLibrary.Data.Orders.Request;
using RobinhoodLibrary.Data.Quote;
using RobinhoodLibrary.Enum;
using RobinhoodLibrary.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;

namespace RobinhoodLibrary.Helpers
{
    /// <summary>
    /// Class used to help build static functions.
    /// </summary>
    internal static class RbHelper
    {
        private static string GetValue(object obj, string key) =>
            obj.GetType().GetProperties()
                .FirstOrDefault(_ => _.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name == key)
                ?.GetValue(obj)
                ?.ToString();

        internal static IList<string> GetValueByKeys(string keys, QuoteData quoteData)
        {
            return keys.Split(',')
                .Select(key => GetValue(quoteData, key))
                .ToList();
        }

        internal static string BuildUrlMarketData(string optionId = null) => !string.IsNullOrEmpty(optionId)
            ? $"{Constants.Routes.MarketDataBase}{optionId}/"
            : Constants.Routes.MarketDataBase;

        internal static IDictionary<string, string> BuildOrderContent(OrderRequest orderRequest, string accountUrl) =>
            new Dictionary<string, string>
            {
                {"account", accountUrl},
                {"instrument", orderRequest.InstrumentUrl},
                {"symbol", orderRequest.Symbol},
                {"type", orderRequest.OrderType.ToString().ToLower()},
                {"time_in_force", orderRequest.TimeInForce.ToString().ToLower()},
                {"trigger", orderRequest.Trigger.ToString().ToLower()},
                {"price", orderRequest.Price},
                {"stop_price", orderRequest.StopPrice},
                {"quantity", orderRequest.Quantity.ToString()},
                {"side", orderRequest.Side.ToString().ToLower()}
            };

        internal static OrderRequest BuildOrderRequestForMarket(string instrumentUrl, string symbol,
            TimeInForce timeInForce, int quantity, string price = null, string stopPrice = null) =>
            new OrderRequest
            {
                InstrumentUrl = instrumentUrl,
                Symbol = symbol,
                TimeInForce = timeInForce,
                Quantity = quantity,
                Price = price,
                StopPrice = stopPrice
            };

        internal static void CheckOrderRequest(OrderRequest orderRequest, string price, string lastTradeParse)
        {
            if (orderRequest == null)
            {
                throw new RequestCheckException("The order request is null");
            }

            if (string.IsNullOrEmpty(orderRequest.InstrumentUrl) || string.IsNullOrEmpty(orderRequest.Symbol))
            {
                throw new RequestCheckException("Neither InstrumentUrl nor symbol were passed to SubmitBuyOrder");
            }

            if (orderRequest.OrderType == OrderType.Limit)
            {
                if (orderRequest.Price == null)
                {
                    throw new RequestCheckException("Limit order has no price in call to SubmitBuyOrder");
                }
            }

            if (orderRequest.Trigger == Trigger.Stop)
            {
                if (orderRequest.StopPrice == null)
                {
                    throw new RequestCheckException("Stop order has no stop_price in call to SubmitBuyOrder");
                }
            }

            if (orderRequest.StopPrice != null && orderRequest.Trigger != Trigger.Stop)
            {
                throw new RequestCheckException("Stop price set for non-stop order in call to SubmitBuyOrder");
            }

            if (orderRequest.Price != null)
            {
                if (orderRequest.OrderType == OrderType.Market)
                {
                    throw new RequestCheckException("Market order has price limit in call to SubmitBuyOrder");
                }
            }
            else
            {
                orderRequest.Price = price ?? lastTradeParse;
            }

            if (orderRequest.Quantity <= 0)
            {
                throw new RequestCheckException("Quantity must be positive number in call to SubmitBuyOrder");
            }
        }
    }
}