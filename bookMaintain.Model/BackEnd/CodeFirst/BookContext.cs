using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace bookMaintain.Model.BackEnd.CodeFirst
{
    /// <summary>
    /// 以Book為主軸連結其他事務
    /// BookContext.Categories為一個DbSet，可以用函式調用貌似list的概念
    /// </summary>
    public class BookContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public BookContext(DbContextOptions<BookContext> options) : base(options)
        {
        }
        public Microsoft.EntityFrameworkCore.DbSet<BookData> BookData { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<BookClass> BookClass { get; set; }

       
        /*public int SaveChangesCount { get; private set; }
        public override int SaveChanges()
        {
            this.SaveChangesCount++;
            return 1;
        }*/
      

        /// <summary>
        /// 可能版本太高目前有find
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        /*
        public BookDataEntity Find(params object[] keyValues)
        {
            //返回List集合序列中唯一记录，如果没有或返回多条记录，则引发异常。
            var id = (int)keyValues.Single();
            return keyValues.SingleOrDefault(b => b.BOOK_ID == id);
        }
        */
    }

    
    /*public static class BookContextObjectMother
    {
        /// <summary>
        /// 可以強制轉型
        /// BOOK_ID = 102,
        /// BOOK_NAME = "王心凌1",
        /// BOOK_CLASS_ID = "應用系統整合1",
        /// BOOK_AUTHOR = "書本作者1",
        /// BOOK_BOUGHT_DATE = "2022-07-29 00:00:00.000",
        /// BOOK_PUBLISHER = "學貫行銷股份有限公司1",
        /// BOOK_NOTE = "光碟共4片",
        /// BOOK_STATUS = "A",
        /// BOOK_KEEPER = "書本作者",
        /// BOOK_AMOUNT = 0,
        /// CREATE_DATE = "2022-07-29 00:00:00.000",
        /// MODIFY_DATE = "2022-07-29 00:00:00.000",
        /// CREATE_USER = "書本作者",
        /// MODIFY_USER = "書本作者"
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static IEnumerable<BookData> CreateBookData(BookData arg)
        {
            yield return new BookData()
            {
                BOOK_ID          = arg.BOOK_ID,
                BOOK_NAME        = arg.BOOK_NAME,
                BOOK_CLASS_ID    = arg.BOOK_CLASS_ID,
                BOOK_AUTHOR      = arg.BOOK_AUTHOR,
                BOOK_BOUGHT_DATE = arg.BOOK_BOUGHT_DATE,
                BOOK_PUBLISHER   = arg.BOOK_PUBLISHER,
                BOOK_NOTE        = arg.BOOK_NOTE,
                BOOK_STATUS      = arg.BOOK_STATUS,
                BOOK_KEEPER      = arg.BOOK_KEEPER,
                BOOK_AMOUNT      = arg.BOOK_AMOUNT,
                CREATE_DATE      = arg.CREATE_DATE,
                MODIFY_DATE      = arg.MODIFY_DATE,
                CREATE_USER      = arg.CREATE_USER,
                MODIFY_USER      = arg.MODIFY_USER
            };
        }
    }

    /// <summary>
    /// IDbAsyncEnumerable非同步可列舉
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class TestDbSet<TEntity> : DbSet<TEntity>, IQueryable, IEnumerable<TEntity>, IDbAsyncEnumerable<TEntity>
        where TEntity : class
    {
        ObservableCollection<TEntity> _data;
        IQueryable _query;

        public TestDbSet()
        {
            _data = new ObservableCollection<TEntity>();
            _query = _data.AsQueryable();
        }

        public override TEntity Add(TEntity item)
        {
            _data.Add(item);
            return item;
        }

        public override TEntity Remove(TEntity item)
        {
            _data.Remove(item);
            return item;
        }

        public override TEntity Attach(TEntity item)
        {
            _data.Add(item);
            return item;
        }

        public override TEntity Create()
        {
            return Activator.CreateInstance<TEntity>();
        }

        public override TDerivedEntity Create<TDerivedEntity>()
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }

        public override ObservableCollection<TEntity> Local
        {
            get { return _data; }
        }

        Type IQueryable.ElementType
        {
            get { return _query.ElementType; }
        }

        Expression IQueryable.Expression
        {
            get { return _query.Expression; }
        }

        IQueryProvider IQueryable.Provider
        {
            get { return new TestDbAsyncQueryProvider<TEntity>(_query.Provider); }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IDbAsyncEnumerator<TEntity> IDbAsyncEnumerable<TEntity>.GetAsyncEnumerator()
        {
            return new TestDbAsyncEnumerator<TEntity>(_data.GetEnumerator());
        }

        readonly HashSet<TEntity> _set;
        readonly IQueryable<TEntity> _queryableSet;
    }

    internal class TestDbAsyncQueryProvider<TEntity> : IQueryProvider
    {
        private readonly IQueryProvider _inner;

        internal TestDbAsyncQueryProvider(IQueryProvider inner)
        {
            _inner = inner;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            return new TestDbAsyncEnumerable<TEntity>(expression);
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new TestDbAsyncEnumerable<TElement>(expression);
        }

        public object Execute(Expression expression)
        {
            return _inner.Execute(expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return _inner.Execute<TResult>(expression);
        }

        public Task<object> ExecuteAsync(Expression expression, CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute(expression));
        }

        public Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute<TResult>(expression));
        }
    }

    internal class TestDbAsyncEnumerable<T> : EnumerableQuery<T>, IDbAsyncEnumerable<T>, IQueryable<T>
    {
        public TestDbAsyncEnumerable(IEnumerable<T> enumerable)
            : base(enumerable)
        { }

        public TestDbAsyncEnumerable(Expression expression)
            : base(expression)
        { }

        public IDbAsyncEnumerator<T> GetAsyncEnumerator()
        {
            return new TestDbAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
        }

        IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator()
        {
            return GetAsyncEnumerator();
        }

        IQueryProvider IQueryable.Provider
        {
            get { return new TestDbAsyncQueryProvider<T>(this); }
        }
    }

    internal class TestDbAsyncEnumerator<T> : IDbAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _inner;

        public TestDbAsyncEnumerator(IEnumerator<T> inner)
        {
            _inner = inner;
        }

        public void Dispose()
        {
            _inner.Dispose();
        }

        public Task<bool> MoveNextAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(_inner.MoveNext());
        }

        public T Current
        {
            get { return _inner.Current; }
        }

        object IDbAsyncEnumerator.Current
        {
            get { return Current; }
        }
    }*/
}
