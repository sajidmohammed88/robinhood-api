using RobinhoodLibrary.Data.News;
using RobinhoodLibrary.Data.Quote;
using RobinhoodLibrary.Data.User;
using RobinhoodLibrary.Enum;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RobinhoodLibrary.Abstractions
{
    /// <summary>
    /// Quote interface that responsible on quote data endpoints.
    /// </summary>
    public interface IQuoteDataService
    {
        /// <summary>
        /// Get the quote data.
        /// </summary>
        /// <param name="stock">The stock instrument or symbol.</param>
        /// <returns>The quote data</returns>
        Task<QuoteData> GetQuoteData(string stock);

        /// <summary>
        /// Get the quote with a specified keys.
        /// </summary>
        /// <param name="stock">The stock.</param>
        /// <param name="keys">The attributes to fetch.</param>
        /// <returns>The value of specified keys.</returns>
        Task<IList<string>> GetQuoteWithSpecifiedKeys(string stock, string keys);

        /// <summary>
        /// Get the quotes data.
        /// </summary>
        /// <param name="stocks">The stock list.</param>
        /// <returns>The quotes data</returns>
        Task<IList<QuoteData>> GetQuotesData(IList<string> stocks);

        /// <summary>
        /// Get the quotes with specified keys.
        /// </summary>
        /// <param name="stocks">The stocks.</param>
        /// <param name="keys">The attributes to fetch.</param>
        /// <returns>he value of specified keys by stock.</returns>
        Task<IDictionary<string, IList<string>>> GetQuotesWithSpecifiedKeys(IList<string> stocks, string keys);

        /// <summary>
        /// Get the historical data of stock.
        /// </summary>
        /// <param name="stocks">The stock tickers.</param>
        /// <param name="interval">The resolution of data.</param>
        /// <param name="span">The span.</param>
        /// <param name="bounds">The bounds.</param>
        /// <returns>Historical data of stock.</returns>
        Task<IList<HistoricalsData>> GetHistoricalQuotes(IList<string> stocks, string interval, Span span, Bounds bounds = Bounds.Regular);

        /// <summary>
        /// Gets the tickers by tag.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <returns>The tickers.</returns>
        Task<IList<string>> GetTickersByTag(string tag);

        /// <summary>
        /// Get the instrument.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>The instrument.</returns>
        Task<Instrument> GetInstrument(string url);

        /// <summary>
        /// Get the news.
        /// </summary>
        /// <param name="stock">The stock.</param>
        /// <returns>The news data list.</returns>
        Task<IList<NewsData>> GetNews(string stock);

        /// <summary>
        /// Get the account.
        /// </summary>
        /// <returns>The account information</returns>
        Task<Account> GetAccount();

        /// <summary>
        /// Gets the instrument watch lists.
        /// </summary>
        /// <returns>The instrument list.</returns>
        Task<IList<Instrument>> GetWatchLists();

        /// <summary>
        /// Get the stock market data.
        /// </summary>
        /// <param name="instruments">The instruments.</param>
        /// <returns>Stock market data.</returns>
        Task<dynamic> GetStockMarketData(IList<string> instruments);

        /// <summary>
        /// Get the number of robinhood users who own the given stock.
        /// </summary>
        /// <param name="stock">The stock.</param>
        /// <returns>The number of robinhood users who own the given stock</returns>
        Task<dynamic> GetPopularity(string stock);
    }
}
