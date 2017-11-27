using ModelLibrary.Generic;
using SourceHelper.Core;
using SourceHelper.Database;
using System;
using System.Reflection;

namespace DataAccess
{
    /// <summary>
    /// 資料存取物件工廠
    /// </summary>
    public static class GenericDataAccessFactory
    {
        /// <summary>
        /// Data Access層實體取得
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IDataAccess<T> CreateInstance<T>()
            where T : class
        {
            IDataAccess<T> dao;

            Assembly assembly = Assembly.Load("DataAccess");
            Type type = assembly.GetType("DataAccess." + typeof(T).Name + "_DAO");

            if (type == null)
                dao = new GenericDataAccess<T>();
            else
                dao = Activator.CreateInstance(type) as IDataAccess<T>;

            return dao;
        }
    }

    /// <summary>
    /// 實體查詢物件工廠
    /// </summary>
    internal static class QueryContextFactory
    {
        /// <summary>
        /// 實體查詢物件取得
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option">資料查詢參數</param>
        public static QueryContext<T> CreateInstance<T>(QueryOption option)
            where T : class
        {
            if (option == null || option.Extand == null || string.IsNullOrEmpty(option.Extand.Model))
                return CreateInstance<T>(ConnectionType.Default);
            else
                return CreateInstance<T>((ConnectionType)Enum.Parse(typeof(ConnectionType), option.Extand.Model));
        }

        /// <summary>
        /// 實體查詢物件取得
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type">預設連線類型</param>
        /// <returns></returns>
        public static QueryContext<T> CreateInstance<T>(ConnectionType type = ConnectionType.Default)
            where T : class
        {
            var name = ConnectionSetting.GetConnection(type);
            DataProvider provider = ProviderFactory.CreateInstance(name);
            return new QueryContext<T>(provider);
        }
    }

    /// <summary>
    /// 實體處理物件工廠
    /// </summary>
    internal static class ModifyContextFactory
    {
        /// <summary>
        /// 實體查詢物件取得
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="extand">擴展資訊</param>
        public static ModifyContext<T> CreateInstance<T>(GenericExtand extand)
            where T : class
        {
            if (extand == null || string.IsNullOrEmpty(extand.Model))
                return CreateInstance<T>(ConnectionType.Default);
            else
                return CreateInstance<T>((ConnectionType)Enum.Parse(typeof(ConnectionType), extand.Model));
        }

        /// <summary>
        /// 實體處理物件取得
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type">預設連線類型</param>
        /// <returns></returns>
        public static ModifyContext<T> CreateInstance<T>(ConnectionType type = ConnectionType.Default)
            where T : class
        {
            var name = ConnectionSetting.GetConnection(type);
            DataProvider provider = ProviderFactory.CreateInstance(name);
            return new ModifyContext<T>(provider);
        }
    }
}