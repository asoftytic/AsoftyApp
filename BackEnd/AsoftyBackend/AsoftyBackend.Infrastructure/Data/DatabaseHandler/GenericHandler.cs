using AsoftyBackend.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AsoftyBackend.Infrastructure.Data.DatabaseHandler;

public abstract class GenericHandler<T> where T : class, new()
{
#pragma warning disable CS8618 
    protected string _tableName;
    protected string _fields;

    protected Type _type;
    protected string _where;
#pragma warning restore CS8618 


    protected void Initialize(T model)
    {
        if (model == null)
            throw new ArgumentNullException("'model' is null in AsoftyBackend.Infrastructure.Data.DatabaseHandler.GenericHandler<T>.Initialize().");

        _type = model.GetType();
        _tableName = _type.Name;
        _where = "";


    }

    //public virtual GenericHandler<T> Where(Expression<Func<T, bool>> expression)
    //{
    //    string sqlWhere = "";
    //    ReflectionUtils.ExpressionToString<T>(expression, ref sqlWhere);

    //    _where += $"{(_where.NullOrEmpty() ? "" : " AND ")}({sqlWhere})";

    //    return this;
    //}

    protected virtual GenericHandler<T> Where(object entity, Operator op = Operator.Equals, LogicalNode AndNodeInternal = LogicalNode.True, LogicalNode AndNodeExternal = LogicalNode.True)
    {

        string sqlWhere = "(";
        int i = 0;  //  Iterator

        var properties = Utils.Utils.ObjectToDictionary(entity);

        foreach(var property in properties)
        {
            if (property.Value == null) continue;

            if (i > 0) sqlWhere += $" {(AndNodeInternal == LogicalNode.True ? "AND" : "OR")} ";

            var propType = property.Value.GetType();
            var val = property.Value.AntiInjectionFormat();

            sqlWhere += $"{property.Key}{Utils.Utils.OperatorToSqlString(op)}{(propType == typeof(string) ? $"'{val}'" : $"{val}")}";

            i++;
        }

        sqlWhere += ")";

        _where += _where.NullOrEmpty() ? sqlWhere : $"{(AndNodeExternal == LogicalNode.True ? " AND " : " OR ")} {sqlWhere}";
        return this;

    }

    public virtual GenericHandler<T> WhereNot(object entity, LogicalNode AndNodeInternal = LogicalNode.True, LogicalNode AndNodeExternal = LogicalNode.True)
    {
        return Where(entity, Operator.NotEquals, AndNodeInternal, AndNodeExternal);
    }

    public virtual GenericHandler<T> Where(object entity, LogicalNode AndNodeInternal = LogicalNode.True, LogicalNode AndNodeExternal = LogicalNode.True)
    {
        return Where(entity, Operator.Equals, AndNodeInternal, AndNodeExternal);
    }

    public virtual GenericHandler<T> ExplicitWhere(string explicitWhere)
    {
        string sqlWhere = $"(({explicitWhere}));";

        return this;
    }

}