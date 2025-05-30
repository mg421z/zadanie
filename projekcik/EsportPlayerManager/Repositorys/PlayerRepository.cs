using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using EsportPlayerManager.Models;
using Npgsql;

public class PlayerRepository
{
    private readonly string _connectionString;
    private readonly List<Player> _inMemoryPlayers = new();

    public PlayerRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task InitDb()
    {
        try
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            await connection.ExecuteAsync("""
                CREATE TABLE IF NOT EXISTS players (
                    id SERIAL PRIMARY KEY,
                    Nickname TEXT NOT NULL,
                    Game TEXT NOT NULL,
                    SkillLevel TEXT NOT NULL,
                    StressLevel TEXT NOT NULL,
                    FatigueLevel TEXT NOT NULL
                )
            """);
            Console.WriteLine("Players DB init");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"DB initialization failed: {ex.Message}. Using in-memory storage.");
            
        }
    }

    public async Task<List<Player>> GetAllPlayers()
    {
        try
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            var players = await connection.QueryAsync<Player>("SELECT * FROM players");
            return players.ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"DB read failed: {ex.Message}. Returning in-memory players.");
            return _inMemoryPlayers.ToList();
        }
    }

    public async Task<int> AddPlayer(Player player)
    {
        try
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            return await connection.ExecuteAsync("""
                INSERT INTO players(Nickname, Game, SkillLevel, StressLevel, FatigueLevel)
                VALUES (@Nickname, @Game, @SkillLevel, @StressLevel, @FatigueLevel)
                """, player);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"DB insert failed: {ex.Message}. Adding player to in-memory list.");
            _inMemoryPlayers.Add(player);
            return 1;
        }
    }
}
