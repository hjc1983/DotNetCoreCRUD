using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using TestDotNetCore.Models;

namespace TestDotNetCore.Models
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> Fetch();
        Task<Movie> GetById(int id);
        void Delete(int id);

        Task<Movie> CreateAsync(Movie data);
        Task<Movie> UpdateAsync(Movie data);

        //Task<IEnumerable<Movie>> GetByReleaseDate(DateTime releaseDate);
    }
}


public class MovieRepository : IMovieRepository
{
    private readonly IConfiguration _config;

    public MovieRepository(IConfiguration config)
    {
        _config = config;
    }

    public IDbConnection Connection
    {
        get { return new SqlConnection(_config.GetConnectionString("MyConnectionString")); }
    }

    //public async Task<IEnumerable<Movie>> GetByReleaseDate(DateTime releaseDate)
    //{
    //    using (IDbConnection conn = Connection)
    //    {
    //        string squery = "SELECT * FROM project.Movie WHERE ReleaseDate = @date";
    //        conn.Open();
    //        var result = await conn.QueryAsync<Movie>(squery, new { date = releaseDate });
    //        return result.ToList();
    //    }
    //}

    public async Task<IEnumerable<Movie>> Fetch()
    {
        using (IDbConnection conn = Connection)
        {
            conn.Open();
            var result = await conn.QueryAsync<Movie>("cusp_MovieSelect",commandType: CommandType.StoredProcedure);
            return result.ToList();
        }
    }

    public async Task<Movie> GetById(int id)
    {
        using (IDbConnection conn = Connection)
        {
            conn.Open();
            var result = await conn.QueryAsync<Movie>("cusp_MovieSelectId", new {id = id},
                commandType: CommandType.StoredProcedure);
            return result.FirstOrDefault();
        }
    }

    public async void Delete(int id)
    {
        using (IDbConnection conn = Connection)
        {
            conn.Open();
            var result = await conn.QueryAsync<Movie>("cusp_MovieDelete", new { id = id },
                commandType: CommandType.StoredProcedure);
        }
    }

    public async Task<Movie> CreateAsync(Movie data)
    {
        using (IDbConnection conn = Connection)
        {
            conn.Open();
            var result = await conn.QueryAsync<Movie>("cusp_MovieInsert",
                new {Title = data.Title, Description = "TBA", Price = data.Price, ReleaseDate = data.ReleaseDate},
                commandType: CommandType.StoredProcedure);

            return result.FirstOrDefault();
        }
    }

    public async Task<Movie> UpdateAsync(Movie data)
    {
        using (IDbConnection conn = Connection)
        {
            conn.Open();
            var result = await conn.QueryAsync<Movie>("cusp_MovieUpdate",
                new
                {
                    Id = data.Id, Title = data.Title, Description = "Updated", Price = data.Price,
                    ReleaseDate = data.ReleaseDate
                },
                commandType: CommandType.StoredProcedure);

            return result.FirstOrDefault();
        }
    }
}
