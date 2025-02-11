using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Reflection;

/// <summary>
/// Summary description for BusinessObjectHelper
/// </summary>
/// 
namespace BusinessObjectHelper
{
    public static class CBO
    {
        // Methods
        private static T CreateObject<T>(IDataReader dr, List<PropertyMappingInfo> propInfoList, int[] ordinals) where T : class, new()
        {
            T local = Activator.CreateInstance<T>();
            for (int i = 0; i <= (propInfoList.Count - 1); i++)
            {
                if (propInfoList[i].PropertyInfo.CanWrite)
                {
                    Type propertyType = propInfoList[i].PropertyInfo.PropertyType;
                    object defaultValue = propInfoList[i].DefaultValue;
                    if (!((ordinals[i] == -1) || dr.IsDBNull(ordinals[i])))
                    {
                        defaultValue = dr.GetValue(ordinals[i]);
                    }
                    try
                    {
                        propInfoList[i].PropertyInfo.SetValue(local, defaultValue, null);
                    }
                    catch
                    {
                        try
                        {
                            if (propertyType.BaseType.Equals(typeof(Enum)))
                            {
                                propInfoList[i].PropertyInfo.SetValue(local, Enum.ToObject(propertyType, defaultValue), null);
                            }
                            else
                            {
                                propInfoList[i].PropertyInfo.SetValue(local, Convert.ChangeType(defaultValue, propertyType), null);
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
            return local;
        }

        public static List<T> FillCollection<T>(Type objType, IDataReader dr) where T : class, new()
        {
            List<T> list = new List<T>();
            try
            {
                List<PropertyMappingInfo> properties = GetProperties(objType);
                int[] ordinals = GetOrdinals(properties, dr);
                while (dr.Read())
                {
                    T item = CreateObject<T>(dr, properties, ordinals);
                    list.Add(item);
                }
            }
            finally
            {
                if (!dr.IsClosed)
                {
                    dr.Close();
                }
            }
            return list;
        }

        public static C FillCollection<T, C>(Type objType, IDataReader dr)
            where T : class, new()
            where C : ICollection<T>, new()
        {
            C local = new C();
            try
            {
                List<PropertyMappingInfo> properties = GetProperties(objType);
                int[] ordinals = GetOrdinals(properties, dr);
                while (dr.Read())
                {
                    T item = CreateObject<T>(dr, properties, ordinals);
                    local.Add(item);
                }
            }
            finally
            {
                if (!dr.IsClosed)
                {
                    dr.Close();
                }
            }
            return local;
        }

        public static T FillObject<T>(Type objType, IDataReader dr) where T : class, new()
        {
            T local = default(T);
            try
            {
                List<PropertyMappingInfo> properties = GetProperties(objType);
                int[] ordinals = GetOrdinals(properties, dr);
                if (dr.Read())
                {
                    local = CreateObject<T>(dr, properties, ordinals);
                }
            }
            finally
            {
                if (!dr.IsClosed)
                {
                    dr.Close();
                }
            }
            return local;
        }

        private static int[] GetOrdinals(List<PropertyMappingInfo> propMapList, IDataReader dr)
        {
            int[] numArray = new int[propMapList.Count];
            if (dr != null)
            {
                for (int i = 0; i <= (propMapList.Count - 1); i++)
                {
                    numArray[i] = -1;
                    try
                    {
                        numArray[i] = dr.GetOrdinal(propMapList[i].DataFieldName);
                    }
                    catch (IndexOutOfRangeException)
                    {
                    }
                }
            }
            return numArray;
        }

        private static List<PropertyMappingInfo> GetProperties(Type objType)
        {
            List<PropertyMappingInfo> cache = MappingInfoCache.GetCache(objType.Name);
            if (cache == null)
            {
                cache = LoadPropertyMappingInfo(objType);
                MappingInfoCache.SetCache(objType.Name, cache);
            }
            return cache;
        }

        private static List<PropertyMappingInfo> LoadPropertyMappingInfo(Type objType)
        {
            List<PropertyMappingInfo> list = new List<PropertyMappingInfo>();
            foreach (PropertyInfo info in objType.GetProperties())
            {
                DataMappingAttribute customAttribute = (DataMappingAttribute)Attribute.GetCustomAttribute(info, typeof(DataMappingAttribute));
                if (customAttribute != null)
                {
                    PropertyMappingInfo item = new PropertyMappingInfo(customAttribute.DataFieldName, customAttribute.NullValue, info);
                    list.Add(item);
                }
            }
            return list;
        }
    }

    public sealed class DataMappingAttribute : Attribute
    {
        // Fields
        private string _dataFieldName;
        private object _nullValue;

        // Methods
        public DataMappingAttribute(object nullValue)
            : this(string.Empty, nullValue)
        {
        }

        public DataMappingAttribute(string dataFieldName, object nullValue)
        {
            this._dataFieldName = dataFieldName;
            this._nullValue = nullValue;
        }

        // Properties
        public string DataFieldName
        {
            get
            {
                return this._dataFieldName;
            }
        }

        public object NullValue
        {
            get
            {
                return this._nullValue;
            }
        }
    }

    internal static class MappingInfoCache
    {
        // Fields
        private static Dictionary<string, List<PropertyMappingInfo>> cache = new Dictionary<string, List<PropertyMappingInfo>>();

        // Methods
        public static void ClearCache()
        {
            cache.Clear();
        }

        internal static List<PropertyMappingInfo> GetCache(string typeName)
        {
            List<PropertyMappingInfo> list = null;
            try
            {
                list = cache[typeName];
            }
            catch (KeyNotFoundException)
            {
            }
            return list;
        }

        internal static void SetCache(string typeName, List<PropertyMappingInfo> mappingInfoList)
        {
            cache[typeName] = mappingInfoList;
        }
    }

    internal sealed class PropertyMappingInfo
    {
        // Fields
        private string _dataFieldName;
        private object _defaultValue;
        private PropertyInfo _propInfo;

        // Methods
        internal PropertyMappingInfo()
            : this(string.Empty, null, null)
        {
        }

        internal PropertyMappingInfo(string dataFieldName, object defaultValue, PropertyInfo info)
        {
            this._dataFieldName = dataFieldName;
            this._defaultValue = defaultValue;
            this._propInfo = info;
        }

        // Properties
        public string DataFieldName
        {
            get
            {
                if (string.IsNullOrEmpty(this._dataFieldName))
                {
                    this._dataFieldName = this._propInfo.Name;
                }
                return this._dataFieldName;
            }
        }

        public object DefaultValue
        {
            get
            {
                return this._defaultValue;
            }
        }

        public PropertyInfo PropertyInfo
        {
            get
            {
                return this._propInfo;
            }
        }
    }
}


