using DataAccess;
using ModelLibrary.Generic;
using System.Collections.Generic;
using System;
using System.Reflection;
using DataExpansion;
using System.Collections;
using FileHelper;

namespace BusinessLogic
{
    /// <summary>
    /// 商業邏輯物件
    /// </summary>
    /// <typeparam name="T">存取實體類型</typeparam>
    public class GenericBusinessLogic<T> : IBusinessLogic<T>
        where T : class
    {
        /// <summary>
        /// 資料存取物件
        /// </summary>
        protected IDataAccess<T> _dao;

        /// <summary>
        /// 建構式
        /// </summary>
        public GenericBusinessLogic()
        {
            _dao = GenericDataAccessFactory.CreateInstance<T>();
        }

        /// <summary>
        /// 實體資料取得
        /// </summary>
        /// <param name="option">資料查詢參數</param>
        /// <param name="user">使用者資料</param>
        /// <param name="condition">查詢條件</param>
        /// <param name="parms">其他傳入參數</param>
        /// <returns></returns>
        public T Get(QueryOption option, UserLogin user, T condition = null, params string[] parms)
        {
            GetUserLimit(option, user, ref condition);
            return _dao.Get(option, condition, parms);
        }

        /// <summary>
        /// 資料集合取得
        /// </summary>
        /// <param name="option">資料查詢參數</param>
        /// <param name="user">使用者資料</param>
        /// <param name="condition">查詢條件</param>
        /// <param name="parms">其他傳入參數</param>
        /// <returns></returns>
        public IEnumerable<T> GetList(QueryOption option, UserLogin user, T condition = null, params string[] parms)
        {
            GetUserLimit(option, user, ref condition);
            return _dao.GetList(option, condition, parms);
        }

        /// <summary>
        /// 資料個數取得
        /// </summary>
        /// <param name="option">資料查詢參數</param>
        /// <param name="user">使用者資料</param>
        /// <param name="condition">查詢條件</param>
        /// <param name="parms">其他傳入參數</param>
        /// <returns></returns>
        public int GetCount(QueryOption option, UserLogin user, T condition = null, params string[] parms)
        {
            GetUserLimit(option, user, ref condition);
            return _dao.GetCount(option, condition, parms);
        }

        /// <summary>
        /// 資料是否存在
        /// </summary>
        /// <param name="option">資料查詢參數</param>
        /// <param name="user">使用者資料</param>
        /// <param name="condition">查詢條件</param>
        /// <param name="parms">其他傳入參數</param>
        /// <returns></returns>
        public bool IsExists(QueryOption option, UserLogin user, T condition = null, params string[] parms)
        {
            return GetCount(option, user, condition, parms) > 0;
        }

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
        public T Modify(string type, UserLogin user, T data, object parameters = null, bool file = false,
                        bool isUser = false, GenericExtand extand = null)
        {
            //使用者資料
            if (isUser)
                SetLoginUser(data, user.USERID, user.ORG_SN);
            //物件資料處理
            ModifyBefore(extand.Before, data, user);
            var output = _dao.Modify(type, data, parameters, extand);
            //檔案搬移
            if (file)
                FileMove(data);
            ModifyAfter(extand.Method, output, user);
            return output;
        }

        /// <summary>
        /// 物件資料處理 (By SQL)
        /// </summary>
        /// <param name="type">處理類型</param>
        /// <param name="user">使用者資料</param>
        /// <param name="data">實體資料</param>
        /// <param name="file">是否搬移檔案</param>
        /// <param name="isUser">是否需設定登入使用者</param>
        /// <param name="extand">擴展資訊</param>
        public T ModifyBySql(string type, UserLogin user, T data, bool file = false,
                                bool isUser = false, GenericExtand extand = null)
        {
            //使用者資料
            if (isUser)
                SetLoginUser(data, user.USERID, user.ORG_SN);
            //物件資料處理
            ModifyBefore(extand.Before, data, user);
            var output = _dao.ModifyBySql(type, data, extand);
            //檔案搬移
            if (file)
                FileMove(data);
            ModifyAfter(extand.Method, output, user);
            return output;
        }

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
        public IEnumerable<T> ModifyList(string type, UserLogin user, IEnumerable<T> list, T data = null, object parameters = null,
                                         bool file = false, bool isUser = false, GenericExtand extand = null)
        {
            //使用者資料
            if (isUser)
            {
                foreach (T item in list)
                    SetLoginUser(item, user.USERID, user.ORG_SN);
            }
            //資料修改
            ModifyListBefore(extand.Before, list, user);
            var output = _dao.ModifyList(type, list, data, parameters, extand);
            //檔案搬移
            if (file)
                FileMove(list);
            ModifyListAfter(extand.Method, output, user);
            return output;
        }

        /// <summary>
        /// 物件集合處理 (By SQL)
        /// </summary>
        /// <param name="type">處理類型</param>
        /// <param name="user">使用者資料</param>
        /// <param name="list">實體集合</param>
        /// <param name="file">是否搬移檔案</param>
        /// <param name="isUser">是否需設定登入使用者</param>
        /// <param name="extand">擴展資訊</param>
        public IEnumerable<T> ModifyListBySql(string type, UserLogin user, IEnumerable<T> list, bool file = false,
                                              bool isUser = false, GenericExtand extand = null)
        {
            //使用者資料
            if (isUser)
            {
                foreach (T data in list)
                    SetLoginUser(data, user.USERID, user.ORG_SN);
            }
            //資料修改
            ModifyListBefore(extand.Before, list, user);
            var output = _dao.ModifyListBySql(type, list, extand);
            //檔案搬移
            if (file)
                FileMove(list);
            ModifyListAfter(extand.Method, output, user);
            return output;
        }

        /// <summary>
        /// 使用狀態更新
        /// </summary>
        /// <param name="entity">實體資料</param>
        /// <param name="user">使用者資料</param>
        public void StatusChange(T entity, UserLogin user)
        {
            Type type = typeof(T);
            T data = Activator.CreateInstance(type) as T;

            foreach (PropertyInfo property in type.GetProperties())
            {
                //主鍵特性
                var attrKey = property.GetCustomAttribute(typeof(KeyAttribute));
                //使用狀態特性
                var attrStatus = property.GetCustomAttribute(typeof(StatusAttribute));

                //主鍵
                object value;
                if (attrKey != null)
                {
                    value = property.GetValue(entity, null);
                    property.SetValue(data, value);
                }
                //狀態
                else if (attrStatus != null)
                {
                    value = property.GetValue(entity, null);
                    property.SetValue(data, value);
                }
                else
                {
                    continue;
                }
            }

            //使用狀態更新
            ModifyBySql("Update", user, data, extand: new GenericExtand());
        }

        /// <summary>
        /// 使用者權限條件設定
        /// </summary>
        /// <param name="option">資料查詢參數</param>
        /// <param name="user">使用者資料</param>
        /// <param name="condition">查詢條件</param>
        /// <returns></returns>
        protected void GetUserLimit(QueryOption option, UserLogin user, ref T condition)
        {
            //登入人員
            if (option.User)
                condition = SetLoginUser(condition, user.USERID);
            //角色權限
            if (!string.IsNullOrEmpty(option.LimitType))
                condition = SetRoleLimit(option.LimitType, condition, user);
        }

        /// <summary>
        /// 登入使用者設定
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="condition">實體資料</param>
        /// <param name="userId">使用者帳號</param>
        /// <param name="userOrg">使用者組織</param>
        /// <returns></returns>
        private T SetLoginUser(T condition, string userId, string userOrg = "")
        {
            Type type = typeof(T);

            if (condition == null)
                condition = Activator.CreateInstance(type) as T;

            foreach (PropertyInfo property in type.GetProperties())
            {
                //使用者帳號
                Attribute attrUser = property.GetCustomAttribute(typeof(UserAttribute));
                //使用者組織
                Attribute attrOrg = property.GetCustomAttribute(typeof(OrgAttribute));

                //帳號設定
                if (attrUser != null)
                    property.SetValue(condition, userId);

                //組織設定
                if (attrOrg != null && !string.IsNullOrEmpty(userOrg))
                    property.SetValue(condition, userOrg);
            }

            return condition;
        }

        /// <summary>
        /// 權限條件設定
        /// </summary>
        /// <typeparam name="T">實體類型</typeparam>
        /// <param name="limit">權限類型</param>
        /// <param name="condition">查詢條件</param>
        /// <param name="user">使用者資料</param>
        /// <returns></returns>
        private T SetRoleLimit(string limit, T condition, UserLogin user)
        {
            Type type = GetType().Assembly.GetType("BusinessLogic.Limit." + typeof(T).Name);

            if (condition == null)
                condition = Activator.CreateInstance(type) as T;

            if (type == null)
                throw new Exception(string.Format("找不到實體{0}的權限條件設定(BusinessLogic.Limit.{0})", typeof(T).Name));

            MethodInfo method = type.GetMethod(limit, BindingFlags.Static | BindingFlags.Public);

            if (method == null)
                throw new Exception("找不到" + limit + "的權限條件設定方法");

            return method.Invoke(null, new object[] { condition, user }) as T;
        }

        /// <summary>
        /// 物件資料處理之前動作
        /// </summary>
        /// <param name="name">方法名稱</param>
        /// <param name="data">物料資料</param>
        /// <param name="user">使用者資料</param>
        private void ModifyBefore(string name, T data, UserLogin user)
        {
            if (string.IsNullOrEmpty(name))
                return;

            MethodInfo method = GetType().GetMethod(name, BindingFlags.Instance | BindingFlags.Public);
            if (method != null)
                method.Invoke(this, new object[] { data, user });
        }

        /// <summary>
        /// 物件資料處理之後動作
        /// </summary>
        /// <param name="name">方法名稱</param>
        /// <param name="data">物料資料</param>
        /// <param name="user">使用者資料</param>
        private void ModifyAfter(string name, T data, UserLogin user)
        {
            if (string.IsNullOrEmpty(name))
                return;

            MethodInfo method = GetType().GetMethod(name, BindingFlags.Instance | BindingFlags.Public);
            if (method != null)
                method.Invoke(this, new object[] { data, user });
        }

        /// <summary>
        /// 物件集合處理之前動作
        /// </summary>
        /// <param name="name">方法名稱</param>
        /// <param name="data">物料資料</param>
        /// <param name="user">使用者資料</param>
        private void ModifyListBefore(string name, IEnumerable<T> data, UserLogin user)
        {
            if (string.IsNullOrEmpty(name))
                return;

            MethodInfo method = GetType().GetMethod(name, BindingFlags.Instance | BindingFlags.Public);
            if (method != null)
                method.Invoke(this, new object[] { data, user });
        }

        /// <summary>
        /// 物件集合處理之後動作
        /// </summary>
        /// <param name="name">方法名稱</param>
        /// <param name="data">物料資料</param>
        /// <param name="user">使用者資料</param>
        private void ModifyListAfter(string name, IEnumerable<T> data, UserLogin user)
        {
            if (string.IsNullOrEmpty(name))
                return;

            MethodInfo method = GetType().GetMethod(name, BindingFlags.Instance | BindingFlags.Public);
            if (method != null)
                method.Invoke(this, new object[] { data, user });
        }

        /// <summary>
        /// 檔案搬移
        /// </summary>
        /// <param name="list">實料集合</param>
        private void FileMove(IEnumerable<T> list)
        {
            foreach (T data in list)
                FileMove(data);
        }

        /// <summary>
        /// 檔案搬移
        /// </summary>
        /// <param name="data">實體資料</param>
        private void FileMove(T data)
        {
            Type type = typeof(T);
            foreach (PropertyInfo property in type.GetProperties())
            {
                //檔案搬移特性
                Attribute attribute = property.GetCustomAttribute(typeof(FileMoveAttribute));

                //判斷是否存在
                if (attribute == null)
                    continue;

                //正式資料夾名稱
                string name = (attribute as FileMoveAttribute).Name;

                if (property.PropertyType.IsGenericType)
                {
                    var list = property.GetValue(data) as IList;
                    if (list != null)
                    {
                        foreach (object obj in list)
                        {
                            PropertyInfo propFile = obj.GetType().GetProperty((attribute as FileMoveAttribute).PropertyName);
                            object objName = propFile.GetValue(obj, null);
                            string str = FileManager.TempPath;
                            if (objName != null)
                                FileManager.MoveToFormal(name, objName.ToString());
                        }
                    }
                }
                else
                {
                    object objName = property.GetValue(data, null);
                    if (objName != null)
                        FileManager.MoveToFormal(name, objName.ToString());
                }
            }
        }
    }
}