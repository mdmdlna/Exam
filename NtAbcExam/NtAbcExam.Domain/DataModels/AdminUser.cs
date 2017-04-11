using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dos.ORM;

namespace NtAbcExam.Domain.DataModels
{
    /// <summary>
    /// 实体类AdminUser。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Table("AdminUser")]
    [Serializable]
    public partial class AdminUser : Entity
    {
        #region Model
        private int _Id;
        private string _UserName;
        private string _UserPwd;
        private int _DeptId;
        private bool _IsSa;

        /// <summary>
        /// 
        /// </summary>
        [Field("Id")]
        public int Id
        {
            get { return _Id; }
            set
            {
                this.OnPropertyValueChange("Id");
                this._Id = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Field("UserName")]
        public string UserName
        {
            get { return _UserName; }
            set
            {
                this.OnPropertyValueChange("UserName");
                this._UserName = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Field("UserPwd")]
        public string UserPwd
        {
            get { return _UserPwd; }
            set
            {
                this.OnPropertyValueChange("UserPwd");
                this._UserPwd = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Field("DeptId")]
        public int DeptId
        {
            get { return _DeptId; }
            set
            {
                this.OnPropertyValueChange("DeptId");
                this._DeptId = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Field("IsSa")]
        public bool IsSa
        {
            get { return _IsSa; }
            set
            {
                this.OnPropertyValueChange("IsSa");
                this._IsSa = value;
            }
        }
        #endregion

        #region Method
        /// <summary>
        /// 获取实体中的主键列
        /// </summary>
        public override Field[] GetPrimaryKeyFields()
        {
            return new Field[] {
                _.Id,
            };
        }
        /// <summary>
        /// 获取实体中的标识列
        /// </summary>
        public override Field GetIdentityField()
        {
            return _.Id;
        }
        /// <summary>
        /// 获取列信息
        /// </summary>
        public override Field[] GetFields()
        {
            return new Field[] {
                _.Id,
                _.UserName,
                _.UserPwd,
                _.DeptId,
                _.IsSa,
            };
        }
        /// <summary>
        /// 获取值信息
        /// </summary>
        public override object[] GetValues()
        {
            return new object[] {
                this._Id,
                this._UserName,
                this._UserPwd,
                this._DeptId,
                this._IsSa,
            };
        }
        /// <summary>
        /// 是否是v1.10.5.6及以上版本实体。
        /// </summary>
        /// <returns></returns>
        public override bool V1_10_5_6_Plus()
        {
            return true;
        }
        #endregion

        #region _Field
        /// <summary>
        /// 字段信息
        /// </summary>
        public class _
        {
            /// <summary>
            /// * 
            /// </summary>
            public static readonly Field All = new Field("*", "AdminUser");
            /// <summary>
			/// 
			/// </summary>
			public static readonly Field Id = new Field("Id", "AdminUser", "");
            /// <summary>
			/// 
			/// </summary>
			public static readonly Field UserName = new Field("UserName", "AdminUser", "");
            /// <summary>
			/// 
			/// </summary>
			public static readonly Field UserPwd = new Field("UserPwd", "AdminUser", "");
            /// <summary>
			/// 
			/// </summary>
			public static readonly Field DeptId = new Field("DeptId", "AdminUser", "");
            /// <summary>
			/// 
			/// </summary>
			public static readonly Field IsSa = new Field("IsSa", "AdminUser", "");
        }
        #endregion
    }

}
