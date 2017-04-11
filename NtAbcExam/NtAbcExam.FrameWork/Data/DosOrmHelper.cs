using System.Collections.Generic;
using System.Data;
using System.Text;
using Dos.ORM;
using Newtonsoft.Json;

namespace NtAbcExam.FrameWork.Data
{
    public class DosOrmHelper
    {
        private static readonly Dictionary<string, string> OperTypes = new Dictionary<string, string>();

        static DosOrmHelper()
        {
            OperTypes.Add("Equals", "=");
            OperTypes.Add("UnEquals", "<>");
            OperTypes.Add("Greater", ">");
            OperTypes.Add("GreaterOrEquals", ">=");
            OperTypes.Add("Less", "<");
            OperTypes.Add("LessOrEquals", "<=");
            OperTypes.Add("Like", "like");
            OperTypes.Add("NotLike", "not like");
            OperTypes.Add("LeftLike", "like");
            OperTypes.Add("RightLike", "like");
            OperTypes.Add("In", "in");
            OperTypes.Add("NotIn", "not in");
            OperTypes.Add("IsNot", "is not");
            OperTypes.Add("Is", "is");
            OperTypes.Add("Between", "between {0} and {1}");
        }


        public static List<OrderByClip> GetOrderByClips(string order, string orderName)
        {
            List<OrderByClip> orderByClips = new List<OrderByClip>();
            if (!string.IsNullOrWhiteSpace(order) && !string.IsNullOrWhiteSpace(orderName))
            {
                OrderByOperater operater = OrderByOperater.ASC;

                if (order.ToLower() == "desc")
                {
                    operater = OrderByOperater.DESC;
                }
                orderByClips.Add(new OrderByClip(orderName, operater));
            }

            return orderByClips;
        }

        public static WhereClip GetWhereClips(ModelQuery query)
        {
            Dictionary<string, int> dictParams = new Dictionary<string, int>();
            List<ModelFilter> filters = JsonConvert.DeserializeObject<List<ModelFilter>>(query.filters);
            query.AllFilters.AddRange(filters);

            List<Parameter> paramList = new List<Parameter>();
            StringBuilder result = new StringBuilder();
            result.Append(" 1=1 ");
            if (query.AllFilters != null)
            {
                foreach (ModelFilter filter in query.AllFilters)
                {
                    if (filter.operType == "Between" && filter.value != null)
                    {
                        var values = filter.value.Split(',');
                        if (values.Length == 2)
                        {
                            string param1 = GetParamName(dictParams, filter.name);
                            string param2 = GetParamName(dictParams, filter.name);

                            result.AppendFormat(" AND ({0} BETWEEN @{1} AND @{2})", filter.name, param1, param2);

                            paramList.Add(new Parameter(param1, values[0]));
                            paramList.Add(new Parameter(param2, values[1]));

                        }
                    }
                    else if (filter.operType == "In" && filter.value != null)
                    {
                        result.AppendFormat(" AND ({0} IN ({1}))", filter.name, filter.value);
                    }
                    else if (filter.operType == "NotIn" && filter.value != null)
                    {
                        result.AppendFormat(" AND ({0} NOT IN ({1}))", filter.name, filter.value);
                    }
                    else if ((filter.operType == "Like" || filter.operType == "NotLike") && filter.value != null)
                    {
                        string param = GetParamName(dictParams, filter.name);
                        result.AppendFormat(" AND {0} {1} @{2}", filter.name, GetQueryOperator(filter.operType), param);
                        paramList.Add(new Parameter(param, string.Format("%{0}%", filter.value), DbType.String, null));
                    }
                    else if (filter.operType == "LeftLike" && filter.value != null)
                    {
                        string param = GetParamName(dictParams, filter.name);
                        result.AppendFormat(" AND {0} {1} @{2}", filter.name, GetQueryOperator(filter.operType), param);
                        paramList.Add(new Parameter(param, string.Format("%{0}", filter.value), DbType.String, null));
                    }
                    else if (filter.operType == "RightLike" && filter.value != null)
                    {
                        string param = GetParamName(dictParams, filter.name);
                        result.AppendFormat(" AND {0} {1} @{2}", filter.name, GetQueryOperator(filter.operType), param);
                        paramList.Add(new Parameter(param, string.Format("{0}%", filter.value), DbType.String, null));
                    }
                    else if (filter.operType == "IsNot")
                    {
                        result.AppendFormat(" AND {0} is not NULL", filter.name);
                    }
                    else if (filter.operType == "Is")
                    {
                        result.AppendFormat(" AND {0} is NULL", filter.name);
                    }
                    else
                    {
                        string param = GetParamName(dictParams, filter.name);
                        result.AppendFormat(" AND {0} {1} @{2}", filter.name, GetQueryOperator(filter.operType), param);
                        paramList.Add(new Parameter(param, GetValue(filter.value), null, null));
                    }
                }
            }

            return new WhereClip(result.ToString().TrimEnd(','), paramList.ToArray());
        }

        private static string GetValue(string value)
        {
            if (value.ToLower() == "#null#")
            {
                return "''";
            }
            else if (value.ToLower() == "#empty#")
            {
                return "''";
            }

            return value;
        }


        private static string GetParamName(Dictionary<string, int> dictParams, string paramName)
        {
            string result = paramName;
            if (dictParams.ContainsKey(paramName))
            {
                dictParams[paramName] += 1;
            }
            else
            {
                dictParams.Add(paramName, 1);
            }
            result = string.Format("{0}{1}", paramName, dictParams[paramName]);
            return result;
        }

        private static string GetQueryOperator(string operType)
        {
            if (OperTypes.ContainsKey(operType))
            {

                return OperTypes[operType];
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
