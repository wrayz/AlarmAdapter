using ModelLibrary.Generic;
using System.Collections.Generic;

namespace DataAccess
{
    /// <summary>
    /// 資料存取介面
    /// </summary>
    /// <typeparam name="T">存取實體類型</typeparam>
    public interface IDataAccess<T>
        where T : class
    {
        /// <summary>
        /// 實體資料取得
        /// </summary>
        /// <param name="option">資料查詢參數</param>
        /// <param name="condition">查詢條件</param>
        /// <param name="parms">其他傳入參數</param>
        /// <returns></returns>
        T Get(QueryOption option, T condition = null, params string[] parms);

        /// <summary>
        /// 實體集合取得
        /// </summary>
        /// <param name="option">資料查詢參數</param>
        /// <param name="condition">查詢條件</param>
        /// <param name="parms">其他傳入參數</param>
        /// <returns></returns>
        IEnumerable<T> GetList(QueryOption option, T condition = null, params string[] parms);

        /// <summary>
        /// 資料個數取得
        /// </summary>
        /// <param name="option">資料查詢參數</param>
        /// <param name="condition">查詢條件</param>
        /// <param name="parms">其他傳入參數</param>
        /// <returns></returns>
        int GetCount(QueryOption option, T condition = null, params string[] parms);

        /// <summary>
        /// 物件資料處理
        /// </summary>
        /// <param name="type">處理類型</param>
        /// <param name="data">實體資料</param>
        /// <param name="parameters">非實體屬性之參數資料</param>
        /// <param name="extand">擴展資訊</param>
        T Modify(string type, T data, object parameters = null, GenericExtand extand = null);

        /// <summary>
        /// 物件資料處理 (By SQL)
        /// </summary>
        /// <param name="type">處理類型</param>
        /// <param name="data">實體資料</param>
        /// <param name="extand">擴展資訊</param>
        T ModifyBySql(string type, T data, GenericExtand extand = null);

        /// <summary>
        /// 物件集合處理
        /// </summary>
        /// <param name="type">處理類型</param>
        /// <param name="list">實體集合</param>
        /// <param name="data">實體資料</param>
        /// <param name="parameters">非實體屬性之參數資料</param>
        /// <param name="extand">擴展資訊</param>
        IEnumerable<T> ModifyList(string type, IEnumerable<T> list, T data = null, object parameters = null, GenericExtand extand = null);

        /// <summary>
        /// 物件集合處理 (By SQL)
        /// </summary>
        /// <param name="type">處理類型</param>
        /// <param name="list">實體集合</param>
        /// <param name="extand">擴展資訊</param>
        IEnumerable<T> ModifyListBySql(string type, IEnumerable<T> list, GenericExtand extand = null);
    }
}