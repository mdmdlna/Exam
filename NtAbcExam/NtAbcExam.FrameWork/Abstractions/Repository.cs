using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Dos.ORM;
using NtAbcExam.FrameWork.Data;

namespace NtAbcExam.FrameWork.Abstractions
{
    public class Repository<T> where T : Entity
    {
        #region 查询
        /// <summary>
        /// 获取整表数据
        /// </summary>
        /// <returns></returns>
        public virtual List<T> GetAll()
        {
            return Db.Context.From<T>().ToList();
        }

        /// <summary>
        /// 根据条件判断是否存在数据
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual bool Any(Expression<Func<T, bool>> where)
        {
            return Db.Context.Exists<T>(where);
        }

        /// <summary>
        /// 取总数
        /// </summary>
        public virtual int Count(Expression<Func<T, bool>> where)
        {
            return Db.Context.From<T>().Where(where).Count();
        }

        /// <summary>
        /// 取总数
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual int Count(Where<T> where)
        {
            return Db.Context.From<T>().Where(where).Count();
        }

        public virtual List<T> Query(ModelQuery query, out int totalRow)
        {
            if (query == null)
            {
                throw new Exception(nameof(query));
            }

            var fs = Db.Context.From<T>()
                       .Where(DosOrmHelper.GetWhereClips(query))
                       .OrderBy(DosOrmHelper.GetOrderByClips(query.order, query.ordername).ToArray());

            totalRow = fs.Count();

            return fs.Page(query.limit, CalcPageIndex.GetPageIndex(query.offset, query.limit)).ToList<T>();
        }
        #endregion

        #region 插入
        /// <summary>
        /// 插入单个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual int Add(T entity)
        {
            return Db.Context.Insert<T>(entity);
        }

        /// <summary>
        /// 插入单个实体
        /// </summary>
        /// <param name="context"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual void Add(DbTrans context, T entity)
        {
            Db.Context.Insert<T>(context, entity);
            //context.Set<T>().Add(entity);
        }

        /// <summary>
        /// 插入多个实体
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual int Add(IEnumerable<T> entities)
        {
            return Db.Context.Insert<T>(entities);
        }

        public virtual void Add(DbTrans context, IEnumerable<T> entities)
        {
            Db.Context.Insert<T>(context, entities.ToArray());
        }
        #endregion

        #region 更新
        /// <summary>
        /// 更新单个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual int Update(T entity)
        {
            return Db.Context.Update(entity);
        }

        /// <summary>
        /// 更新单个实体
        /// </summary>
        public virtual int Update(T entity, Where where)
        {
            return Db.Context.Update(entity, where);
        }
        /// <summary>
        /// 更新单个实体
        /// </summary>
        public virtual int Update(T entity, Expression<Func<T, bool>> lambdaWhere)
        {
            return Db.Context.Update(entity, lambdaWhere);
        }

        public virtual void Update(DbTrans context, T entity)
        {
            Db.Context.Update(context, entity);
        }

        /// <summary>
        /// 更新多个实体
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual int Update(IEnumerable<T> entities)
        {
            var enumerable = entities as T[] ?? entities.ToArray();
            Db.Context.Update(enumerable.ToArray());
            return 1;
        }

        public virtual void Update(DbTrans context, IEnumerable<T> entities)
        {
            Db.Context.Update(context, entities.ToArray());
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除单个实体
        /// </summary>
        public virtual int Remove(T entitie)
        {
            return Db.Context.Delete<T>(entitie);
        }

        /// <summary>
        /// 删除多个实体
        /// </summary>
        public virtual int Remove(IEnumerable<T> entities)
        {
            return Db.Context.Delete<T>(entities);
        }

        /// <summary>
        /// 删除单个实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual int Remove(Guid? id)
        {
            if (id == null)
            {
                return 0;
            }
            return Db.Context.Delete<T>(id.Value);
        }

        /// <summary>
        /// 删除单个实体
        /// </summary>
        public virtual int Remove(Expression<Func<T, bool>> where)
        {
            return Db.Context.Delete<T>(where);
        }

        /// <summary>
        /// 删除单个实体
        /// </summary>
        public virtual int Remove(Where<T> where)
        {
            return Db.Context.Delete<T>(where.ToWhereClip());
        }
        #endregion
    }
}
