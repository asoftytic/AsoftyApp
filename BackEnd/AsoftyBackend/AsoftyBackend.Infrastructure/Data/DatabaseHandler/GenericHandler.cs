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

    public virtual GenericHandler<T> Where(Expression<Func<T, bool>> expression)
    {
        string sqlWhere = "";
        ReflectionUtils.ExpressionToString<T>(expression, ref sqlWhere);

        _where += sqlWhere;

        return this;
    }

    public virtual GenericHandler<T> ExplicitWhere(string explicitWhere)
    {
        string sqlWhere = $"(({explicitWhere}));";

        return this;
    }

}