using ModelLibrary.Generic;
using System.Collections.Generic;

namespace BusinessLogic
{
    /// <summary>
    /// 商業邏輯介面
    /// </summary>
    /// <typeparam name="T">存取實體類型</typeparam>
    public interface IBusinessLogic<T>
        where T : class
    {
        /// <summary>
        /// 實體資料取得
        /// </summary>
        /// <param name="option">資料查詢參數</param>
        /// <param name="user">使用者資料</param>
        /// <param name="condition">查詢條件</param>
        /// <param name="parms">其他傳入參數</param>
        /// <returns></returns>
        T Get(QueryOption option, UserLogin user, T condition = null, params string[] parms);

        /// <summary>
        /// 實體集合取得
        /// </summary>
        /// <param name="option">資料查詢參數</param>
        /// <param name="user">使用者資料</param>
        /// <param name="condition">查詢條件</param>
        /// <param name="parms">其他傳入參數</param>
        /// <returns></returns>
        IEnumerable<T> GetList(QueryOption option, UserLogin user, T condition = null, params string[] parms);

        /// <summary>
        /// 資料個數取得
        /// </summary>
        /// <param name="option">資料查詢參數</param>
        /// <param name="user">使用者資料</param>
        /// <param name="condition">查詢條件</param>
        /// <param name="parms">其他傳入參數</param>
        /// <returns></returns>
        int GetCount(QueryOption option, UserLogin user, T condition = null, params string[] parms);

        /// <summary>
        /// 資料是否存在
        /// </summary>
        /// <param name="option">資料查詢參數</param>
        /// <param name="user">使用者資料</param>
        /// <param name="condition">查詢條件</param>
        /// <param name="parms">其他傳入參數</param>
        /// <returns></returns>
        bool IsExists(QueryOption option, UserLogin user, T condition = null, params string[] parms);

        /// <summary>
        /// 物件資料處理
        /// </summary>
        /// <param name="type">處理類型</param>
        /// <param name="user">使用者資料</param>
        /// <param name="data">實體資料</param>
        /// <param name="parameters">非實體屬性之參數資料</param>
        /// <param name="file">是否搬移檔案</param>
        /// <param name="isUser">是否需設定登入使用者</param>
        /// <param name="extand">擴展資訊</param>
        T Modify(string type, UserLogin user, T data, object parameters = null, bool file = false,
                 bool isUser = false, GenericExtand extand = null);

        /// <summary>
        /// 物件資料處理 (By SQL)
        /// </summary>
        /// <param name="type">處理類型</param>
        /// <param name="user">使用者資料</param>
        /// <param name="data">實體資料</param>
        /// <param name="file">是否搬移檔案</param>
        /// <param name="isUser">是否需設定登入使用者</param>
        /// <param name="extand">擴展資訊</param>
        T ModifyBySql(string type, UserLogin user, T data, bool file = false,
                      bool isUser = false, GenericExtand extand = null);

        /// <summary>
        /// 物件集合處理
        /// </summary>
        /// <param name="type">處理類型</param>
        /// <param name="user">使用者資料</param>
        /// <param name="list">實體集合</param>
        /// <param name="data">實體資料</param>
        /// <param name="parameters">非實體屬性之參數資料</param>
        /// <param name="file">是否搬移檔案</param>
        /// <param name="isUser">是否需設定登入使用者</param>
        /// <param name="extand">擴展資訊</param>
        IEnumerable<T> ModifyList(string type, UserLogin user, IEnumerable<T> list, T data = null, object parameters = null,
                                  bool file = false, bool isUser = false, GenericExtand extand = null);

        /// <summary>
        /// 物件集合處理 (By SQL)
        /// </summary>
        /// <param name="type">處理類型</param>
        /// <param name="user">使用者資料</param>
        /// <param name="list">實體集合</param>
        /// <param name="file">是否搬移檔案</param>
        /// <param name="isUser">是否需設定登入使用者</param>
        /// <param name="extand">擴展資訊</param>
        IEnumerable<T> ModifyListBySql(string type, UserLogin user, IEnumerable<T> list, bool file = false,
                                       bool isUser = false, GenericExtand extand = null);

        /// <summary>
        /// 使用狀態更新
        /// </summary>
        /// <param name="entity">實體資料</param>
        /// <param name="user">使用者資料</param>
        void StatusChange(T entity, UserLogin user);
    }
}