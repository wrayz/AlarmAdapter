using System;
using System.Reflection;

namespace BusinessLogic
{
    /// <summary>
    /// 基礎工廠
    /// </summary>
    public static class GenericBusinessFactory
    {
        /// <summary>
        /// Business Logic層實體取得
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IBusinessLogic<T> CreateInstance<T>()
            where T : class
        {
            IBusinessLogic<T> bll;

            Assembly assembly = Assembly.Load("BusinessLogic");
            Type type = assembly.GetType("BusinessLogic." + typeof(T).Name + "_BLL");

            if (type == null)
                bll = new GenericBusinessLogic<T>();
            else
                bll = Activator.CreateInstance(type) as IBusinessLogic<T>;

            return bll;
        }

        ///// <summary>
        ///// Business Logic層實體取得
        ///// </summary>
        ///// <typeparam name="T">存取實體類型</typeparam>
        ///// <typeparam name="U">輸出實體類型</typeparam>
        ///// <returns></returns>
        //public static IBusinessLogic<T, U> CreateInstance<T, U>()
        //    where T : class
        //    where U : class
        //{
        //    return new GenericBusinessLogic<T, U>();
        //}
    }
}