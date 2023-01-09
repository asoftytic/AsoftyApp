using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AsoftyBackend.Infrastructure.Data.Attributes;
using AsoftyBackend.Infrastructure.Data.Model;
using AsoftyBackend.Infrastructure.Utils;
using Dapper;
using MySql.Data.MySqlClient;

namespace AsoftyBackend.Infrastructure.Data.DatabaseHandler;

public class QueryGenericHandler<T> : GenericHandler<T> where T : DbEntity, new()
{
    MySqlConnection _connection;

    public QueryGenericHandler()
    {
        base.Initialize(new T());
        _connection = new MySqlConnection();    //  Suprise warning
        OpenConection();
    }

    public async Task<IEnumerable<T>> QueryAsync()
    {

        var query =
            $"SELECT * \n" +
            $"FROM {_type.Name}\n" +
            $"{(_where.NullOrEmpty() ? "" : $"WHERE ({_where}) ")};";

        _where = "";
        OpenConection();

        return await _connection.QueryAsync<T>(query);

    }

    /// <summary>
    /// DELETE FROM [model] specified above. Use Where() method before or this execute DELETE clause without where.
    /// </summary>
    /// <returns></returns>
    public async Task<int> Delete()
    {
        return await Delete(_where);
    }

    public async Task<int> DeleteWhereAsync(Expression<Func<T, bool>> whereExpression)
    {
        string whereClause = "";
        ReflectionUtils.ExpressionToString(whereExpression, ref whereClause);

        return await Delete(whereClause);
    }

    private async Task<int> Delete(string whereClauseStr)
    {
        string sqlQuery = $"DELETE FROM {_type.Name} {(whereClauseStr.NullOrEmpty() ? "" : $"WHERE ({whereClauseStr});")};";

        OpenConection();

        return await _connection.ExecuteAsync(sqlQuery);
    }



    public async Task<int> InsertAsync(object entity, params string[] IgnoreFields)
    {
        //  Select the null values to ignore
        var nullValues = Utils.Utils.ObjectToDictionary(entity).Where(e => e.Value == null).Select(e => e.Key).ToArray();

        IEnumerable<string> fields = entity.GetType()
            .GetProperties()
            .Where(f => !f.CustomAttributes.Where(a =>  a.AttributeType.Name == typeof(SqlIgnoreAttribute).Name).Any() || !f.CustomAttributes.Where(a => a.AttributeType.Name == typeof(PrimaryKeyAttribute).Name).Any())
            .Where(f => Array.IndexOf(IgnoreFields, f.Name) == -1)
            .Where(f => Array.IndexOf(nullValues, f.Name) == -1)
            .Select(f => f.Name);

        string sqlQuery = $"INSERT INTO {typeof(T).Name} ({String.Join(", ", fields)}) VALUES({String.Join(", ", fields.Select(f => ("@" + f)))});";

        _where = "";
        OpenConection();

        return  await _connection.ExecuteAsync(sqlQuery, entity);
    }

    public async Task<int> UpdateAsync(object entity, params string[] IgnoreFields)
    {
        return await UpdateAsync(entity, _where, IgnoreFields);
    }

    public async Task<int> UpdateWhereAsync(object entity, Expression<Func<T, bool>> whereExpression, params string[] IgnoreFields)
    {
        string whereClause = "";
        ReflectionUtils.ExpressionToString(whereExpression, ref whereClause);
        return await UpdateAsync(entity, whereClause, IgnoreFields);
    }

    private async Task<int> UpdateAsync(object entity,string whereClauseStr, params string[] IgnoreFields)
    {
        string whereClause = whereClauseStr;

        //  Select the null values to ignore
        var nullValues = Utils.Utils.ObjectToDictionary(entity).Where(e => e.Value == null).Select(e => e.Key).ToArray();

        IEnumerable<string> fields = entity.GetType()
            .GetProperties()
            .Where(f => !f.CustomAttributes.Where(a => a.AttributeType.Name == typeof(SqlIgnoreAttribute).Name).Any() || !f.CustomAttributes.Where(a => a.AttributeType.Name == typeof(PrimaryKeyAttribute).Name).Any())
            .Where(f => Array.IndexOf(IgnoreFields, f.Name) == -1)
            .Where(f => Array.IndexOf(nullValues, f.Name) == -1)
            .Select(f => f.Name);

        string sqlQuery = $"UPDATE {typeof(T).Name} SET {string.Join("", fields.Select((f, i) => (i == 0 ? "" : ", ") + $"{f} = @{f}"))} {(whereClause.NullOrEmpty() ? "" : "WHERE " + whereClause)};";

        _where = "";    //  Clear where
        OpenConection();

        return await _connection.ExecuteAsync(sqlQuery, entity);
    }

    /*
     public async Task<int> InsertSqlAsync(string query)
    {
        OpenConection();

        return await _connection.ExecuteAsync(query);
    }
     */


    //  Multiples Insert
    /*
    public async Task<int> InsertAsync(IEnumerable<T> entity)
    {
        IEnumerable<string> fields = typeof(T)
            .GetProperties()
            .Where(f => !f.CustomAttributes.Where(a => a.AttributeType.Name == typeof(SqlIgnoreAttribute).Name).Any() || !f.CustomAttributes.Where(a => a.AttributeType.Name == typeof(PrimaryKeyAttribute).Name).Any())
            .Select(f => f.Name);

        string sqlQuery = $"INSERT INTO {typeof(T).Name} ({String.Join(", ", fields)}) VALUES({String.Join(", ", fields.Select(f => ("@" + f)))});";

        OpenConection();

        //return await _connection.ExecuteAsync(sqlQuery, entity);
        throw new NotImplementedException("ToDo: refactorize first. this has bugs");
    }
    */


    public override QueryGenericHandler<T> Where(Expression<Func<T, bool>> expression)
    {
        return (QueryGenericHandler<T>)base.Where(expression);
    }

    public override QueryGenericHandler<T> ExplicitWhere(string expression)
    {
        return (QueryGenericHandler<T>)base.ExplicitWhere(expression);
    }

    public async Task OpenConectionAsync()
    {
        if(_connection == null || _connection.State != System.Data.ConnectionState.Open)
        {
            _connection = new MySqlConnection("Server=localhost;User=root;Password=12345;Database=AsoftyDb;");  //  Todo: Resolve this shit
            await _connection.OpenAsync();
        }
    }

    public void OpenConection()
    {
        if (_connection == null || _connection.State != System.Data.ConnectionState.Open)
        {
            _connection = new MySqlConnection("Server=localhost;User=root;Password=12345;Database=AsoftyDb;");  //  Todo: Resolve this shit
            _connection.Open();
        }
    }

    

}