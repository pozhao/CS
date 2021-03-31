using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CSCommon.library
{
    /// <summary>
    /// 複製單一 or 多筆(List) Object 之 屬性
    /// </summary>
    public partial class CopyClass
    {

        /// <summary>
        /// 以來源或目的 Class 的屬性為主
        /// </summary>
        /// <remarks></remarks>
        public enum Scope
        {
            /// <summary>以來源有的屬性為主</summary>
            SourceAsMain,

            /// <summary>以目的有的屬性為主</summary>
            DestinationAsMain,

            /// <summary>以雙方都有的屬性為主</summary>
            Intersection
        }

        private PropertyInfo[] m_arySourceItem;
        private PropertyInfo[] m_aryDestItem;
        private bool[] m_aryDestIsList;
        private Scope m_objScope;

        /// <summary>
        /// New
        /// </summary>
        /// <param name="objSourceType">來源 Class</param>
        /// <param name="objDestType">目的 Class</param>
        /// <param name="intScope">以來源或目的 Class 的屬性為主</param>
        /// <param name="aryException">例外，不複製之屬性</param>
        public CopyClass(Type objSourceType, Type objDestType, Scope intScope, params string[] aryException)
        {
            var lstSourceItem = new List<PropertyInfo>();
            var lstDestItem = new List<PropertyInfo>();
            var lstDestIsList = new List<bool>();
            PropertyInfo objDestItem;
            m_objScope = intScope;
            if (intScope == Scope.SourceAsMain)
            {
                // 以來源有的屬性為主
                foreach (var objSourceItem in objSourceType.GetProperties())
                {
                    if (aryException.Contains(objSourceItem.Name))
                        continue;
                    lstSourceItem.Add(objSourceItem);
                    objDestItem = objDestType.GetProperty(objSourceItem.Name);
                    lstDestItem.Add(objDestItem);
                    lstDestIsList.Add(typeof(IList).IsAssignableFrom(objDestItem.PropertyType) & objDestItem.PropertyType.IsGenericType);
                }
            }
            else // If intScope = CopyClassScope.DestinationAsMain OrElse intScope = CopyClassScope.Intersection Then
            {
                // 以目的有的 or 雙方都有的屬性為主
                foreach (var currentObjDestItem in objDestType.GetProperties())
                {
                    objDestItem = currentObjDestItem;
                    if (aryException.Contains(objDestItem.Name))
                        continue;
                    if (intScope == Scope.Intersection)
                    {
                        // 以雙方都有的屬性為主
                        if (!(from p in objSourceType.GetProperties()
                              where (p.Name ?? "") == (objDestItem.Name ?? "")
                              select p).Any())
                        {
                            continue;
                        }
                    }

                    lstSourceItem.Add(objSourceType.GetProperty(objDestItem.Name));
                    lstDestItem.Add(objDestItem);
                    lstDestIsList.Add(typeof(IList).IsAssignableFrom(objDestItem.PropertyType) & objDestItem.PropertyType.IsGenericType);
                }
            }

            m_arySourceItem = lstSourceItem.ToArray();
            m_aryDestItem = lstDestItem.ToArray();
            m_aryDestIsList = lstDestIsList.ToArray();
        }

        /// <summary>
        /// 複製單一 Object 屬性
        /// </summary>
        /// <param name="objSource">來源 Object</param>
        /// <param name="objDest">目的 Object</param>
        public void CopyPropertyValue<T1, T2>(T1 objSource, T2 objDest)
        {
            object objDestList;
            for (int i = 0, loopTo = m_arySourceItem.Count() - 1; i <= loopTo; i++)
            {
                if (m_aryDestIsList[i])
                {
                    objDestList = (List<T2>) Activator.CreateInstance(m_aryDestItem[i].PropertyType);
                    CopyClassToClass(m_arySourceItem[i].GetValue((object)objSource), objDestList, m_objScope);
                    m_aryDestItem[i].SetValue(objDest, objDestList);
                }
                else
                {
                    m_aryDestItem[i].SetValue(objDest, m_arySourceItem[i].GetValue(objSource));
                }
            }
        }

        /// <summary>
        /// 複製單一 Object 屬性
        /// </summary>
        /// <param name="objSource">來源 Object</param>
        /// <param name="objDest">目的 Object</param>
        /// <param name="intScope">以來源或目的 Class 的屬性為主</param>
        /// <param name="aryException">例外，不複製之屬性</param>
        public static void CopyClassToClass<T1, T2>(T1 objSource, T2 objDest, Scope intScope, params string[] aryException)
        {
            // 
            var objClass = new CopyClass(typeof(T1), typeof(T2), intScope, aryException);
            objClass.CopyPropertyValue(objSource, objDest);
        }

        /// <summary>
        /// 複製多筆(List) Object 屬性
        /// </summary>
        /// <param name="lstSource">來源 Object list (IQueryable)</param>
        /// <param name="lstDest">目的 Object list</param>
        /// <param name="intScope">以來源或目的 Class 的屬性為主</param>
        /// <param name="aryException">例外，不複製之屬性</param>
        public static void CopyListToList<T1, T2>(IQueryable<T1> lstSource, ref List<T2> lstDest, Scope intScope, params string[] aryException) where T2 : class, new()
        {
            // 
            CopyListToList(lstSource.AsEnumerable(), ref lstDest, intScope, aryException);
        }

        /// <summary>
        /// 複製多筆(List) Object 屬性
        /// </summary>
        /// <param name="lstSource">來源 Object list (IEnumerable)</param>
        /// <param name="lstDest">目的 Object list</param>
        /// <param name="intScope">以來源或目的 Class 的屬性為主</param>
        /// <param name="aryException">例外，不複製之屬性</param>
        public static void CopyListToList<T1, T2>(IEnumerable<T1> lstSource, ref List<T2> lstDest, Scope intScope, params string[] aryException) where T2 : class, new()
        {
            // 
            T2 objDest;
            var objClass = new CopyClass(typeof(T1), typeof(T2), intScope, aryException);
            lstDest = new List<T2>();
            foreach (var objSource in lstSource)
            {
                objDest = new T2();
                objClass.CopyPropertyValue(objSource, objDest);
                lstDest.Add(objDest);
            }
        }
    }
}