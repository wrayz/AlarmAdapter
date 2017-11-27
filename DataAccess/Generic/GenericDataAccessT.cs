using ModelLibrary.Generic;
using Newtonsoft.Json;
using SourceHelper.Core;
using SourceHelper.Enumerate;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DataAccess
{
    /// <summary>
    /// 資料存取物件
    /// </summary>
    /// <typeparam name="T">存取實體類型</typeparam>
    public class GenericDataAccess<T> : IDataAccess<T>
        where T : class
    {
        /// <summary>
        /// 實體資料取得
        /// </summary>
        /// <param name="option">資料查詢參數</param>
        /// <param name="condition">查詢條件</param>
        /// <param name="parms">其他傳入參數</param>
        /// <returns></returns>
        public T Get(QueryOption option, T condition = null, params string[] parms)
        {
            var context = QueryContextFactory.CreateInstance<T>(option);
            DataQuery(context.Main, option, condition, parms);
            return context.GetEntity();
        }

        /// <summary>
        /// 實體集合取得
        /// </summary>
        /// <param name="option">資料查詢參數</param>
        /// <param name="condition">查詢條件</param>
        /// <param name="parms">其他傳入參數</param>
        /// <returns></returns>
        public IEnumerable<T> GetList(QueryOption option, T condition = null, params string[] parms)
        {
            var context = QueryContextFactory.CreateInstance<T>(option);
            DataQuery(context.Main, option, condition, parms);
            DataPagerSort(context.Main, option);
            return context.GetEntities();
        }

        /// <summary>
        /// 資料個數取得
        /// </summary>
        /// <param name="option">資料查詢參數</param>
        /// <param name="condition">查詢條件</param>
        /// <param name="parms">其他傳入參數</param>
        /// <returns></returns>
        public int GetCount(QueryOption option, T condition = null, params string[] parms)
        {
            var context = QueryContextFactory.CreateInstance<T>(option);
            DataQuery(context.Main, option, condition, parms);
            return context.GetCount();
        }

        /// <summary>
        /// 物件資料處理
        /// </summary>
        /// <param name="type">處理類型</param>
        /// <param name="data">實體資料</param>
        /// <param name="parameters">非實體屬性之參數資料</param>
        /// <param name="extand">擴展資訊</param>
        public T Modify(string type, T data, object parameters = null, GenericExtand extand = null)
        {
            var context = ModifyContextFactory.CreateInstance<T>(extand);
            var output = context.Modify(type, data, parameters).Item1;
            ModifyAfter(extand, output);
            return output;
        }

        /// <summary>
        /// 物件資料處理 (By SQL)
        /// </summary>
        /// <param name="type">處理類型</param>
        /// <param name="data">實體資料</param>
        /// <param name="extand">擴展資訊</param>
        public T ModifyBySql(string type, T data, GenericExtand extand = null)
        {
            var context = ModifyContextFactory.CreateInstance<T>(extand);
            var output = context.ModifyBySql(type, data).Item1;
            ModifyAfter(extand, output);
            return output;
        }

        /// <summary>
        /// 物件集合處理
        /// </summary>
        /// <param name="type">處理類型</param>
        /// <param name="data">實體資料</param>
        /// <param name="list">實體集合</param>
        /// <param name="parameters">非實體屬性之參數資料</param>
        /// <param name="extand">擴展資訊</param>
        public IEnumerable<T> ModifyList(string type, IEnumerable<T> list, T data = null, object parameters = null, GenericExtand extand = null)
        {
            var context = ModifyContextFactory.CreateInstance<T>(extand);
            var output = context.ModifyList(type, list, data, parameters).Item1;
            ModifyListAfter(extand, output);
            return output;
        }

        /// <summary>
        /// 物件集合處理 (By SQL)
        /// </summary>
        /// <param name="type">處理類型</param>
        /// <param name="list">實體集合</param>
        /// <param name="extand">擴展資訊</param>
        public IEnumerable<T> ModifyListBySql(string type, IEnumerable<T> list, GenericExtand extand = null)
        {
            var context = ModifyContextFactory.CreateInstance<T>(extand);
            var output = context.ModifyListBySql(type, list).Item1;
            ModifyListAfter(extand, output);
            return output;
        }

        /// <summary>
        /// 實體資料查詢
        /// </summary>
        /// <param name="query">查詢計劃</param>
        /// <param name="option">資料查詢參數</param>
        /// <param name="condition">查詢條件</param>
        /// <param name="parms">其他傳入參數</param>
        protected void DataQuery(QueryPlan<T> query, QueryOption option, T condition = null, params string[] parms)
        {
            if (option.Relation)
                query.JoinObject();

            if (string.IsNullOrEmpty(option.Source))
                query.Source(SourceType.None);
            else
                query.Source(option.Source);

            if (!string.IsNullOrEmpty(option.VirtualName))
                query.Virtual(option.VirtualName);

            if (!string.IsNullOrEmpty(option.Condition))
            {
                List<QueryCondition> conditions = JsonConvert.DeserializeObject<List<QueryCondition>>(option.Condition);
                query.Query(conditions);
            }

            if (condition != null)
                query.Query(condition);

            if (option.Plan != null)
            {
                query.SelectPlan(option.Plan.Select)
                     .JoinPlan(option.Plan.Join);
            }

            if (option.Custom != null)
            {
                List<QueryCondition> userConditions = JsonConvert.DeserializeObject<List<QueryCondition>>(option.Custom.Condition);
                query.UserQuery(userConditions);
            }

            if (!string.IsNullOrEmpty(option.Extand.Method))
                ExtensionQuery(query, option.Extand.Method, parms);
        }

        /// <summary>
        /// 實體資料分頁與排序
        /// </summary>
        /// <param name="query">查詢計劃</param>
        /// <param name="option">資料查詢參數</param>
        protected void DataPagerSort(QueryPlan<T> query, QueryOption option)
        {
            if (option.Page != null)
                query.Pager(option.Page.Size, option.Page.Index);

            if (option.Plan != null)
                query.OrderBy(option.Plan.Order);

            if (option.Custom != null)
            {
                List<UserOrder> orders = JsonConvert.DeserializeObject<List<UserOrder>>(option.Custom.Order);
                query.OrderBy(orders);
            }
        }

        /// <summary>
        /// 查詢條件擴展呼叫
        /// </summary>
        /// <param name="query">查詢計劃</param>
        /// <param name="name">查詢條件擴展方法名</param>
        /// <param name="parms">其他傳入參數</param>
        private void ExtensionQuery(QueryPlan<T> query, string name, params string[] parms)
        {
            MethodInfo method = GetType().GetMethod(name, BindingFlags.Instance | BindingFlags.Public);

            if (method == null)
                throw new Exception(string.Format("在DataAccess找不到方法{0}", name));

            method.Invoke(this, new object[] { query, parms });
        }

        /// <summary>
        /// 物件資料處理之後動作
        /// </summary>
        /// <param name="extand">擴展資訊</param>
        /// <param name="data">物料資料</param>
        private void ModifyAfter(GenericExtand extand, T data)
        {
            if (extand == null || string.IsNullOrEmpty(extand.Method))
                return;

            MethodInfo method = GetType().GetMethod(extand.Method, BindingFlags.Instance | BindingFlags.Public);
            if (method != null)
                method.Invoke(this, new object[] { data });
        }

        /// <summary>
        /// 物件集合處理之後動作
        /// </summary>
        /// <param name="extand">擴展資訊</param>
        /// <param name="data">物料資料</param>
        private void ModifyListAfter(GenericExtand extand, IEnumerable<T> data)
        {
            if (extand == null || string.IsNullOrEmpty(extand.Method))
                return;

            MethodInfo method = GetType().GetMethod(extand.Method, BindingFlags.Instance | BindingFlags.Public);
            if (method != null)
                method.Invoke(this, new object[] { data });
        }
    }
}